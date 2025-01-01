using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.Exceptions;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /// <summary>
    /// This class is the representation for the VS_VERSIONINFO structure.
    /// http://msdn.microsoft.com/en-us/library/ms647001%28v=vs.85%29.aspx
    /// 
    /// VS_VERSIONINFO
    /// {
    ///  ...
    ///  VS_FIXEDFILEINFO
    ///  {
    ///    ...
    ///    StringFileInfo[]
    ///    {
    ///      ...
    ///      StringTable[]
    ///      {
    ///        ...
    ///        String[]
    ///      }
    ///    }
    ///    VarFileInfo[]
    ///    {
    ///      ...
    ///      VarFileInfo[]
    ///      {
    ///        ...
    ///        Var[]
    ///      }
    ///    }
    ///  }
    ///  ...
    /// }
    /// </summary>
    class PeVersionInfoResource : IPeStandardResource
    {
        private static readonly string TYPE_STRINGFILEINFO_KEY = "StringFileInfo";
        private static readonly string TYPE_VARFILEINFO_KEY = "VarFileInfo";

        /* VS_VERSIONINFO structure */
        /// <summary>
        /// The length, in bytes, of the VS_VERSIONINFO structure. 
        /// This length does not include any padding that aligns any subsequent version resource data on a 32-bit boundary. 
        /// </summary>
        public PeElem<ushort> wLength { get; private set; }

        /// <summary>
        /// The length, in bytes, of the Value member. 
        /// This value is zero if there is no Value member associated with the current version structure.  
        /// </summary>
        public PeElem<ushort> wValueLength { get; private set; }

        /// <summary>
        /// The type of data in the version resource. 
        /// This member is 1 if the version resource contains text data and 0 if the version resource contains binary data. 
        /// </summary>
        public PeElem<ushort> wType { get; private set; }

        /// <summary>
        /// The Unicode string L"VS_VERSION_INFO". 
        /// </summary>
        public PeElem<string> szKey { get; private set; }

        /// <summary>
        /// Contains as many zero words as necessary to align the Value member on a 32-bit boundary.  
        /// </summary>
        public PeElem<ushort> Padding1 { get; private set; }


        // HERE COMES THE FILEVERSION STRUCTURE
        public FixedFileInfo vsFixedFileInfo { get; private set; }

        /// <summary>
        /// As many zero words as necessary to align the Children member on a 32-bit boundary. 
        /// These bytes are not included in wValueLength. This member is optional. 
        /// </summary>
        public PeElem<ushort> Padding2 { get; private set; }

        /// <summary>
        /// An array of zero or one StringFileInfo structures, and zero or one VarFileInfo structures 
        /// that are children of the current VS_VERSIONINFO structure. 
        /// </summary>
        public PeElem<ushort> Children { get; private set; }

        /// <summary>
        /// StringFileInfo structure list
        /// </summary>
        public List<StringFileInfo> StringFileInfos { get; private set; }

        /// <summary>
        /// VarFileInfo structure list
        /// </summary>
        public List<VarFileInfo> VarFileInfos { get; private set; }

        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            fDataReader.PushAddress();

            fDataReader.Position = (uint)fileOffset;
            wLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wLength");
            wValueLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wValueLength");
            wType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wType");
            szKey = new PeElem<string>(fDataReader.Position, fDataReader.ReadUnicodeMaxLength(15), "szKey");
            szKey.SetStringSize((int)(fDataReader.Position - szKey.Offset));
            fDataReader.Position += 2;  // take account of zero-terminated szKey unicode string
            fDataReader.IncFilePosTo32BitBoundary();    // skip Padding 1
            // VS_FIXEDFILEINFO structure is 52 bytes long, however wValueLength specifies the site of Value member.
            // Not sure what to do if wValueLength is not 52
            if (wValueLength.Value == 52)
            {
                vsFixedFileInfo = new FixedFileInfo();
                vsFixedFileInfo.Read(fDataReader, 52);
            }
            else
            {
                // unknown VS_FIXEDFILEINFO structure size, skip it and proceed for now
                fDataReader.Position += wValueLength.Value;
            }
            fDataReader.IncFilePosTo32BitBoundary();    // skip Padding 2

            // here comes an array of zero or one StringFileInfo structures, and zero or one VarFileInfo structures
            // 1. Check the first for 4 bytes if zero - then we reached the end of the array
            // 2. Determine the type, we have to read wType - this is always at offset 6
            // 3a. If wType == L"VarFileInfo", it's an VarInfo structure
            // 3b. If wType == L"StringFileInfo", it's an StringFileInfo structure

            StringFileInfos = new List<StringFileInfo>();
            VarFileInfos = new List<VarFileInfo>();
            uint tmpUInt32EndMarker;
            do
            {
                uint startOfCurArrayElem = (uint)fDataReader.Position;
                tmpUInt32EndMarker = fDataReader.ReadUInt32(startOfCurArrayElem);
                if (tmpUInt32EndMarker != 0)
                {
                    string keyStr = fDataReader.ReadUnicodeMaxLength(startOfCurArrayElem + 6, 39);
                    if (keyStr.Equals(TYPE_STRINGFILEINFO_KEY))
                    {
                        StringFileInfo stringFileInfoStruct = new StringFileInfo();
                        stringFileInfoStruct.Read(fDataReader);
                        StringFileInfos.Add(stringFileInfoStruct);
                    }
                    else if (keyStr.Equals(TYPE_VARFILEINFO_KEY))
                    {
                        VarFileInfo varFileInfo = new VarFileInfo();
                        varFileInfo.Read(fDataReader);
                        VarFileInfos.Add(varFileInfo);
                    }
                    else
                    {
                        throw new PEModelException();
                    }
                }
            } while (fDataReader.Position < fileOffset + wLength.Value); //while (tmpUInt32EndMarker != 0);

            fDataReader.PopAddress();
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return wLength;
            yield return wValueLength;
            yield return wType;
            yield return szKey;
            if (vsFixedFileInfo != null)
            {
                foreach (IPeElem fileInfoElem in vsFixedFileInfo)
                {
                    yield return fileInfoElem;
                }
            }
        }

        /// <summary>
        /// Get a complete version info resource entry as one formatted string.
        /// </summary>
        /// <param name="versionInfoRes"></param>
        /// <returns></returns>
        public string GetVersionInfoResourceAsString()
        {
            if (StringFileInfos == null)
                return "NOT YET INITIALIZED";

            StringBuilder sb = new StringBuilder(4000);
            string formatStr = "{0,-20}{1,-15}{2,-10}{3,-10}";
            sb.AppendLine("Version Info Resource");

            sb.AppendLine(String.Format(formatStr, "Name", "Offset", "Size", "Value"));
            sb.AppendLine("---------------------------------------------------------");
            foreach (IPeElem elem in this)
            {
                sb.AppendLine(String.Format(formatStr,
                    elem.Name, elem.OffsetString, elem.SizeInBytes(), elem.ValueString()));
            }

            sb.AppendLine();
            foreach (StringFileInfo sfi in this.StringFileInfos)
            {
                sb.AppendLine();
                sb.AppendLine("StringFileInfo:");
                foreach (IPeElem sfiElem in sfi)
                {
                    sb.AppendLine(String.Format(formatStr,
                        sfiElem.Name, sfiElem.OffsetString, sfiElem.SizeInBytes(), sfiElem.ValueString()));
                }

                foreach (StringTable stab in sfi.Children)
                {
                    sb.AppendLine("StringTable:");
                    foreach (IPeElem stElem in stab)
                    {
                        sb.AppendLine(String.Format(formatStr,
                            stElem.Name, stElem.OffsetString, stElem.SizeInBytes(), stElem.ValueString()));
                    }
                    foreach (StringStruct sstr in stab.Children)
                    {
                        sb.AppendLine("String:");
                        foreach (IPeElem stStrucElem in sstr)
                        {
                            sb.AppendLine(String.Format(formatStr,
                                stStrucElem.Name, stStrucElem.OffsetString,
                                                stStrucElem.SizeInBytes(), stStrucElem.ValueString()));
                        }
                    }
                }
            }

            foreach (VarFileInfo vfi in this.VarFileInfos)
            {
                sb.AppendLine();
                sb.AppendLine("VarFileInfo:");
                foreach (IPeElem vfiElem in vfi)
                {
                    sb.AppendLine(String.Format(formatStr,
                        vfiElem.Name, vfiElem.OffsetString, vfiElem.SizeInBytes(), vfiElem.ValueString()));
                }
                foreach (VarStruct vs in vfi.Children)
                {
                    sb.AppendLine("Var:");
                    foreach (IPeElem vfiElem in vs)
                    {
                        sb.AppendLine(String.Format(formatStr,
                            vfiElem.Name, vfiElem.OffsetString, vfiElem.SizeInBytes(), vfiElem.ValueString()));
                    }
                    StringBuilder sb2 = new StringBuilder();
                    foreach (uint val in vs.Values)
                    {
                        sb2.Append(StringUtil.GetFormattedStringUint(val) + " ");
                    }
                    sb.AppendLine(String.Format(formatStr,
                        "Values", "", "", sb2.ToString()));
                }
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// VS_FIXEDFILEINFO structure
    /// http://msdn.microsoft.com/en-us/library/ms646997%28v=vs.85%29.aspx
    /// </summary>
    class FixedFileInfo
    {
        public PeElem<uint> dwSignature { get; private set; }
        public PeElem<uint> dwStrucVersion { get; private set; }
        public PeElem<uint> dwFileVersionMS { get; private set; }
        public PeElem<uint> dwFileVersionLS { get; private set; }
        public PeElem<uint> dwProductVersionMS { get; private set; }
        public PeElem<uint> dwProductVersionLS { get; private set; }
        public PeElem<uint> dwFileFlagsMask { get; private set; }
        public PeElem<uint> dwFileFlags { get; private set; }
        public PeElem<uint> dwFileOS { get; private set; }
        public PeElem<uint> dwFileType { get; private set; }
        public PeElem<uint> dwFileSubtype { get; private set; }
        public PeElem<uint> dwFileDateMS { get; private set; }
        public PeElem<uint> dwFileDateLS { get; private set; }
        public void Read(IFileAccess fDataReader, long expectedLength)
        {
            dwSignature = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwSignature");
            dwStrucVersion = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwStrucVersion");
            dwFileVersionMS = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileVersionMS");
            dwFileVersionLS = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileVersionLS");
            dwProductVersionMS = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwProductVersionMS");
            dwProductVersionLS = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwProductVersionLS");
            dwFileFlagsMask = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileFlagsMask");
            dwFileFlags = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileFlags");
            dwFileOS = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileOS");
            dwFileType = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileType");
            dwFileSubtype = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileSubtype");
            dwFileDateMS = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileDateMS");
            dwFileDateLS = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "dwFileDateLS");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return dwSignature;
            yield return dwStrucVersion;
            yield return dwFileVersionMS;
            yield return dwFileVersionLS;
            yield return dwProductVersionMS;
            yield return dwProductVersionLS;
            yield return dwFileFlagsMask;
            yield return dwFileFlags;
            yield return dwFileOS;
            yield return dwFileType;
            yield return dwFileSubtype;
            yield return dwFileDateMS;
            yield return dwFileDateLS;
        }
    }

    /// <summary>
    /// StringFileInfo structure
    /// http://msdn.microsoft.com/en-us/library/ms646989%28v=vs.85%29.aspx
    /// </summary>
    class StringFileInfo
    {
        public PeElem<ushort> wLength { get; private set; }
        public PeElem<ushort> wValueLength { get; private set; }
        public PeElem<ushort> wType { get; private set; }
        public PeElem<string> szKey { get; private set; }
        public List<StringTable> Children { get; private set; }

        public void Read(IFileAccess fDataReader)
        {
            uint startFileOffset = fDataReader.Position;
            wLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wLength");
            wValueLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wValueLength");
            wType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wType");
            szKey = new PeElem<string>(fDataReader.Position, fDataReader.ReadUnicodeMaxLength(30), "szKey");
            szKey.SetStringSize((int)(fDataReader.Position - szKey.Offset));
            fDataReader.IncFilePosTo32BitBoundary();    // skip padding

            Children = new List<StringTable>();
            do
            {
                StringTable childStringTable = new StringTable();
                childStringTable.Read(fDataReader);
                Children.Add(childStringTable);
            } 
            while ((fDataReader.Position - startFileOffset) < wLength.Value);
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return wLength;
            yield return wValueLength;
            yield return wType;
            yield return szKey;
        }
    }

    
    /// <summary>
    /// VarFileInfo structure
    /// http://msdn.microsoft.com/en-us/library/ms646995%28v=vs.85%29.aspx
    /// </summary>
    class VarFileInfo
    {
        public PeElem<ushort> wLength { get; private set; }
        public PeElem<ushort> wValueLength { get; private set; }
        public PeElem<ushort> wType { get; private set; }
        public PeElem<string> szKey { get; private set; }
        public List<VarStruct> Children { get; private set; }

        public void Read(IFileAccess fDataReader)
        {
            uint startFileOffset = fDataReader.Position;
            wLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wLength");
            wValueLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wValueLength");
            wType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wType");
            szKey = new PeElem<string>(fDataReader.Position, fDataReader.ReadUnicodeMaxLength(30), "szKey");
            szKey.SetStringSize((int)(fDataReader.Position - szKey.Offset));
            fDataReader.IncFilePosTo32BitBoundary();    // skip padding

            Children = new List<VarStruct>();
            do
            {
                VarStruct varStruct = new VarStruct();
                varStruct.Read(fDataReader);
                Children.Add(varStruct);
            } 
            while ((fDataReader.Position - startFileOffset) < wLength.Value);
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return wLength;
            yield return wValueLength;
            yield return wType;
            yield return szKey;
        }
    }

    /// <summary>
    /// StringTable structure
    /// http://msdn.microsoft.com/en-us/library/ms646992%28v=vs.85%29.aspx
    /// </summary>
    class StringTable
    {
        /// <summary>
        /// The length, in bytes, of this StringTable structure, including all structures indicated by the Children member. 
        /// </summary>
        public PeElem<ushort> wLength { get; private set; }
        /// <summary>
        /// This member is always equal to zero. 
        /// </summary>
        public PeElem<ushort> wValueLength { get; private set; }
        /// <summary>
        /// The type of data in the version resource. This member is 1 if the version resource contains text data and 0 if the version resource contains binary data. 
        /// </summary>
        public PeElem<ushort> wType { get; private set; }
        /// <summary>
        /// An 8-digit hexadecimal number stored as a Unicode string.
        /// </summary>
        public PeElem<string> szKey { get; private set; }
        public List<StringStruct> Children { get; private set; }

        public void Read(IFileAccess fDataReader)
        {
            uint startFileOffset = fDataReader.Position;
            wLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wLength");
            wValueLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wValueLength");
            wType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wType");
            szKey = new PeElem<string>(fDataReader.Position, fDataReader.ReadUnicodeMaxLength(30), "szKey");
            szKey.SetStringSize((int)(fDataReader.Position - szKey.Offset));
            fDataReader.IncFilePosTo32BitBoundary();    // skip padding

            // get Strings
            Children = new List<StringStruct>();
            do
            {
                StringStruct childString = new StringStruct();
                childString.Read(fDataReader);
                Children.Add(childString);
            }
            while ((fDataReader.Position - startFileOffset) < wLength.Value);
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return wLength;
            yield return wValueLength;
            yield return wType;
            yield return szKey;
        }
    }

    /// <summary>
    /// String Structure
    /// http://msdn.microsoft.com/en-us/library/ms646987%28v=vs.85%29.aspx
    /// </summary>
    class StringStruct
    {
        public PeElem<ushort> wLength { get; private set; }
        public PeElem<ushort> wValueLength { get; private set; }
        public PeElem<ushort> wType { get; private set; }
        public PeElem<string> szKey { get; private set; }
        public PeElem<string> Value { get; private set; }

        public void Read(IFileAccess fDataReader)
        {
            wLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wLength");
            wValueLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wValueLength");
            wType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wType");
            szKey = new PeElem<string>(fDataReader.Position, fDataReader.ReadUnicodeMaxLength(60), "szKey");
            szKey.SetStringSize((int)(fDataReader.Position - szKey.Offset));
            fDataReader.IncFilePosTo32BitBoundary();    // skip padding
            // get actual string value
            Value = new PeElem<string>(fDataReader.Position, fDataReader.ReadUnicodeMaxLength(200), "Value");
            Value.SetStringSize((int)(fDataReader.Position - Value.Offset));
            fDataReader.IncFilePosTo32BitBoundary();    
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return wLength;
            yield return wValueLength;
            yield return wType;
            yield return szKey;
            yield return Value;
        }
    }

    /// <summary>
    /// Var Structure
    /// http://msdn.microsoft.com/en-us/library/ms646994%28v=vs.85%29.aspx
    /// </summary>
    class VarStruct
    {
        public PeElem<ushort> wLength { get; private set; }
        public PeElem<ushort> wValueLength { get; private set; }
        public PeElem<ushort> wType { get; private set; }
        public PeElem<string> szKey { get; private set; }
        public List<uint> Values { get; private set; }

        public void Read(IFileAccess fDataReader)
        {
            uint startFileOffset = fDataReader.Position;
            wLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wLength");
            wValueLength = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wValueLength");
            wType = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "wType");
            szKey = new PeElem<string>(fDataReader.Position, fDataReader.ReadUnicodeMaxLength(30), "szKey");
            szKey.SetStringSize((int)(fDataReader.Position - szKey.Offset));
            fDataReader.IncFilePosTo32BitBoundary();    // skip padding

            Values = new List<uint>();
            do {
               Values.Add(fDataReader.ReadUInt32());
            }
            while ((fDataReader.Position - startFileOffset) < wLength.Value);
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return wLength;
            yield return wValueLength;
            yield return wType;
            yield return szKey;
        }
    }
}
