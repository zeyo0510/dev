using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Resources.StandardId
{
    /// <summary>
    /// Infos taken from http://code.google.com/p/pefile/wiki/ReadingResourceStrings.
    /// Structure: 
    /// - 2 byte length of string (Pascal-type string)
    ///   Can be zero, then there is no string and a 2-byte length entry directly follows
    /// - if length > 0, then UNICODE string of this length directly follows
    /// Each StringTable has 16 entries, if necessary filled up with empty strings.
    /// </summary>
    class PeStringTableResource : IPeStandardResource
    {
        private PEFile peFile;

        List<PeStringTableEntry> stringTableEntries;

        public PeStringTableResource(PEFile peFile)
        {
            this.peFile = peFile;
        }

        public void Read(IFileAccess fDataReader, long fileOffset, long length)
        {
            fDataReader.PushAddress();
            fDataReader.Position = (uint)fileOffset;

            stringTableEntries = new List<PeStringTableEntry>();

            uint curOffset = 0;
            while (curOffset < length)
            {
                uint offset = fDataReader.Position + curOffset;
                ushort strLength = fDataReader.ReadUInt16(offset);
                curOffset += 2;

                if (strLength > 0)
                {
                    string str = fDataReader.ReadUnicode(fDataReader.Position + curOffset, strLength);
                    stringTableEntries.Add(new PeStringTableEntry(offset, strLength, str));
                    curOffset += (uint)(strLength * 2);
                }
                else
                {
                    stringTableEntries.Add(new PeStringTableEntry(offset, strLength, String.Empty));
                }
            }
            
            fDataReader.PopAddress();
        }

        /// <summary>
        /// Get the complete string table as one formatted table-like string.
        /// </summary>
        /// <returns></returns>
        public string GetStringTable()
        {
            if (stringTableEntries == null)
                return "NOT YET LOADED";

            StringBuilder sb = new StringBuilder(4000);
            string formatStr = "{0,-20}{1,-15}{2,-10}{3,-10}";
            sb.AppendLine("Version Info Resource");

            sb.AppendLine(String.Format(formatStr, "ID", "Offset", "Len", "Value"));
            sb.AppendLine("---------------------------------------------------------");
            int index = 0;
            foreach (PeStringTableEntry entry in stringTableEntries)
            {
                sb.AppendLine(String.Format(formatStr,
                    StringUtil.GetFormattedHexString(index),
                    StringUtil.GetFormattedHexString(entry.FileOffset),
                    StringUtil.GetFormattedHexString(entry.StrLen), 
                    entry.StringValue));
                index++;
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// Class to hold the information of one string entry from a string table.
    /// </summary>
    class PeStringTableEntry
    {
        public uint FileOffset { get; private set; }
        public ushort StrLen { get; private set; }
        public string StringValue { get; private set; } 

        public PeStringTableEntry(uint offset, ushort strLength, string str)
        {
            this.FileOffset = offset;
            this.StrLen = strLength;
            this.StringValue = str;
        }

    }
}
