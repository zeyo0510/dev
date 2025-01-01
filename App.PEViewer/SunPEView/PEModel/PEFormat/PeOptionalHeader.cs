using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat.Enums;

namespace SunPEView.PEModel.PEFormat
{
    class PeOptionalHeader : AbstractPeDescriptor
    {

        /// <summary>
        /// Magic number identifying image file.
        /// </summary>
        public PeElem<ushort> Magic { get; private set; }

        /// <summary>
        /// Linker major version number.
        /// </summary>
        public PeElem<byte> MajorLinkerVersion { get; private set; }

        /// <summary>
        ///  Linker minor version number.
        /// </summary>
        public PeElem<byte> MinorLinkerVersion { get; private set; }

        /// <summary>
        /// Size of the/all code section(s).
        /// </summary>
        public PeElem<uint> SizeOfCode { get; private set; }

        /// <summary>
        /// Size of the/all initialized data section(s)
        /// </summary>
        public PeElem<uint> SizeOfInitializedData { get; private set; }

        /// <summary>
        /// Size of the/all uninitialized data section(s)
        /// </summary>
        public PeElem<uint> SizeOfUninitializedData { get; private set; }

        /// <summary>
        /// RVA of the entry point function
        /// </summary>
        public PeElem<uint> AddressOfEntryPoint { get; private set; }

        /// <summary>
        /// RVA of the beginning of the file’s code sections(s).
        /// </summary>
        public PeElem<uint> BaseOfCode { get; private set; }

        /// <summary>
        /// RVA of the beginning of the file’s data sections(s).
        /// Does NOT EXIST in 64bit headers!
        /// </summary>
        public PeElem<uint> BaseOfData { get; private set; }

        /// <summary>
        /// Image’s preferred starting virtual address.
        /// Is 4 Bytes in 32bit headers.
        /// This is only valid in 32bit files.
        /// </summary>
        public PeElem<uint> ImageBase32 { get; private set; }

        /// <summary>
        /// Image’s preferred starting virtual address.
        /// Is 8 Bytes in 64bit headers.
        /// This is only valid in 64bit files.
        /// </summary>
        public PeElem<ulong> ImageBase64 { get; private set; }

        /// <summary>
        /// Alignment of sections when loaded in memory.
        /// </summary>
        public PeElem<uint> SectionAlignment { get; private set; }

        /// <summary>
        /// Alignment of sections in the disk image file.
        /// </summary>
        public PeElem<uint> FileAlignment { get; private set; }

        /// <summary>
        /// Major version number of OS.
        /// </summary>
        public PeElem<ushort> MajorOperatingSystemVersion { get; private set; }

        /// <summary>
        /// Minor version number of OS.
        /// </summary>
        public PeElem<ushort> MinorOperatingSystemVersion { get; private set; }

        /// <summary>
        /// Major version number of application.
        /// </summary>
        public PeElem<ushort> MajorImageVersion { get; private set; }

        /// <summary>
        /// Minor version number of application.
        /// </summary>
        public PeElem<ushort> MinorImageVersion { get; private set; }

        /// <summary>
        /// Major version number of subsystem.
        /// </summary>
        public PeElem<ushort> MajorSubsystemVersion { get; private set; }

        /// <summary>
        /// Minor version number of subsystem.
        /// </summary>
        public PeElem<ushort> MinorSubsystemVersion { get; private set; }

        /// <summary>
        /// Reserved.
        /// </summary>
        public PeElem<uint> Win32VersionValue { get; private set; }

        /// <summary>
        /// Size of the image file (in bytes) including all headers.
        /// </summary>
        public PeElem<uint> SizeOfImage { get; private set; }

        /// <summary>
        /// Sum of DOS header + stub, PE header, PE Fileheader, section headers.
        /// </summary>
        public PeElem<uint> SizeOfHeaders { get; private set; }

        /// <summary>
        /// Checksum of the disk image file
        /// </summary>
        public PeElem<uint> CheckSum { get; private set; }

        /// <summary>
        /// User interface subsystem 
        /// </summary>
        public PeElem<ushort> Subsystem { get; private set; }

        /// <summary>
        /// Dll Characteristics
        /// </summary>
        public PeElem<ushort> DllCharacteristics { get; private set; }

        /// <summary>
        /// Size of virtual memory to reserve for the initial thread’s stack
        /// Is 4 Bytes in 32bit headers.
        /// This is only valid in 32bit files.       
        /// </summary>
        public PeElem<uint> SizeOfStackReserve32 { get; private set; }

        /// <summary>
        /// Size of virtual memory to reserve for the initial thread’s stack
        /// Is 8 Bytes in 64bit headers.     
        /// This is only valid in 64bit files.
        /// </summary>
        public PeElem<ulong> SizeOfStackReserve64 { get; private set; }

        /// <summary>
        /// Size of virtual memory initially committed for the initial thread’s stack
        /// Is 4 Bytes in 32bit headers.
        /// This is only valid in 32bit files.      
        /// </summary>
        public PeElem<uint> SizeOfStackCommit32 { get; private set; }

        /// <summary>
        /// Size of virtual memory initially committed for the initial thread’s stack
        /// Is 8 Bytes in 64bit headers.
        /// This is only valid in 64bit files.      
        /// </summary>
        public PeElem<ulong> SizeOfStackCommit64 { get; private set; }

        /// <summary>
        /// Size of virtual memory to reserve for the initial process heap.
        /// Is 4 Bytes in 32bit headers. 
        /// This is only valid in 32bit files.
        /// </summary>
        public PeElem<uint> SizeOfHeapReserve32 { get; private set; }

        /// <summary>
        /// Size of virtual memory to reserve for the initial process heap.
        /// Is 8 Bytes in 64bit headers. 
        /// This is only valid in 64bit files.
        /// </summary>
        public PeElem<ulong> SizeOfHeapReserve64 { get; private set; }

        /// <summary>
        /// Size of virtual memory initially committed  for the initial process heap.
        /// Is 4 Bytes in 32bit headers.
        /// This is only valid in 32bit files.       
        /// </summary>
        public PeElem<uint> SizeOfHeapCommit32 { get; private set; }

        /// <summary>
        /// Size of virtual memory initially committed  for the initial process heap.
        /// Is 8 Bytes in 64bit headers.   
        /// This is only valid in 64bit files. 
        /// </summary>
        public PeElem<ulong> SizeOfHeapCommit64 { get; private set; }

        /// <summary>
        /// Loader flags
        /// </summary>
        public PeElem<uint> LoaderFlags { get; private set; }

        /// <summary>
        ///  Number of entries in the DataDirectory 
        /// </summary>
        public PeElem<uint> NumberOfRvaAndSizes { get; private set; }


        public void Read(IFileAccess fDataReader)
        {
            Magic = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Magic");
            MajorLinkerVersion = new PeElem<byte>(fDataReader.Position, fDataReader.ReadByte(), "MajorLinkerVersion");
            MinorLinkerVersion = new PeElem<byte>(fDataReader.Position, fDataReader.ReadByte(), "MinorLinkerVersion");
            SizeOfCode = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfCode");
            SizeOfInitializedData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfInitializedData");
            SizeOfUninitializedData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfUninitializedData");
            AddressOfEntryPoint = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "AddressOfEntryPoint");
            BaseOfCode = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "BaseOfCode");

            if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
            {
                BaseOfData = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "BaseOfData");
                ImageBase32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "ImageBase");
            }
            else if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
            {
                ImageBase64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "ImageBase");
            }

            SectionAlignment = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SectionAlignment");
            FileAlignment = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "FileAlignment");
            MajorOperatingSystemVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MajorOperatingSystemVersion");
            MinorOperatingSystemVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MinorOperatingSystemVersion");
            MajorImageVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MajorImageVersion");
            MinorImageVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MinorImageVersion");
            MajorSubsystemVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MajorSubsystemVersion");
            MinorSubsystemVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MinorSubsystemVersion");
            Win32VersionValue = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "MinorSubsystemVersion");
            SizeOfImage = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfImage");
            SizeOfHeaders = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfHeaders");
            CheckSum = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "CheckSum");
            Subsystem = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "Subsystem");
            DllCharacteristics = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "DllCharacteristics");

            if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
            {
                SizeOfStackReserve32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfStackReserve");
                SizeOfStackCommit32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfStackCommit");
                SizeOfHeapReserve32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfHeapReserve");
                SizeOfHeapCommit32 = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfHeapCommit");
            }
            else if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
            {
                SizeOfStackReserve64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "SizeOfStackReserve");
                SizeOfStackCommit64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "SizeOfStackCommit");
                SizeOfHeapReserve64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "SizeOfHeapReserve");
                SizeOfHeapCommit64 = new PeElem<ulong>(fDataReader.Position, fDataReader.ReadUInt64(), "SizeOfHeapCommit");
            }

            LoaderFlags = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "LoaderFlags");
            NumberOfRvaAndSizes = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "NumberOfRvaAndSizes");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return Magic;
            yield return MajorLinkerVersion;
            yield return MinorLinkerVersion;
            yield return SizeOfCode;
            yield return SizeOfInitializedData;
            yield return SizeOfUninitializedData;
            yield return AddressOfEntryPoint;
            yield return BaseOfCode;

            if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
            {
                yield return BaseOfData;
                yield return ImageBase32;
            }
            else
            {
                yield return ImageBase64;
            }

            yield return SectionAlignment;
            yield return FileAlignment;
            yield return MajorOperatingSystemVersion;
            yield return MinorOperatingSystemVersion;
            yield return MajorImageVersion;
            yield return MinorImageVersion;
            yield return MajorSubsystemVersion;
            yield return MinorSubsystemVersion;
            yield return Win32VersionValue;
            yield return SizeOfImage;
            yield return SizeOfHeaders;
            yield return CheckSum;
            yield return Subsystem;
            yield return DllCharacteristics;

            if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
            {
                yield return SizeOfStackReserve32;
                yield return SizeOfStackCommit32;
                yield return SizeOfHeapReserve32;
                yield return SizeOfHeapCommit32;
            }
            else
            {
                yield return SizeOfStackReserve64;
                yield return SizeOfStackCommit64;
                yield return SizeOfHeapReserve64;
                yield return SizeOfHeapCommit64;
            }

            yield return LoaderFlags;
            yield return NumberOfRvaAndSizes;
        }


        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Magic.Name, "Magic number identifying the image");
            AddDescriptionEntry(MajorLinkerVersion.Name, "Linker major version number");
            AddDescriptionEntry(MinorLinkerVersion.Name, "Linker minor version number");
            AddDescriptionEntry(SizeOfCode.Name, "Size of the/all code section(s)");
            AddDescriptionEntry(SizeOfInitializedData.Name, "Size of the/all initialized data section(s)");
            AddDescriptionEntry(SizeOfUninitializedData.Name, "Size of the/all uninitialized data section(s)");
            AddDescriptionEntry(AddressOfEntryPoint.Name, "RVA of the entry point function");
            AddDescriptionEntry(BaseOfCode.Name, "RVA of the beginning of the file’s code sections(s)");
            
            if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
            {
                AddDescriptionEntry(BaseOfData.Name, "RVA of the beginning of the file’s data sections(s)");
                AddDescriptionEntry(ImageBase32.Name, "Image’s preferred starting virtual address");
            }
            else if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
            {
                AddDescriptionEntry(ImageBase64.Name, "Image’s preferred starting virtual address");
            }

            AddDescriptionEntry(SectionAlignment.Name, "Alignment of sections when loaded in memory");
            AddDescriptionEntry(FileAlignment.Name, "Alignment of sections in disk image");

            AddDescriptionEntry(MajorOperatingSystemVersion.Name, " Major version number of the required OS");
            AddDescriptionEntry(MinorOperatingSystemVersion.Name, " Minor version number of the required OS");
            AddDescriptionEntry(MajorImageVersion.Name, " Major version number of the application");
            AddDescriptionEntry(MinorImageVersion.Name, " Minor version number of the application");
            AddDescriptionEntry(MajorSubsystemVersion.Name, "Major version number of the subsystem");
            AddDescriptionEntry(MinorSubsystemVersion.Name, "Minor version number of the subsystem");

            AddDescriptionEntry(Win32VersionValue.Name, "Reserved");
            AddDescriptionEntry(SizeOfImage.Name, "Size of the image file in bytes including headers");
            AddDescriptionEntry(SizeOfHeaders.Name, "Sum of the sizes of DOS header, PE- & PEFile header and section headers");
            AddDescriptionEntry(CheckSum.Name, "Checksum of the disk image file");
            AddDescriptionEntry(Subsystem.Name, "User interface subsystem required to run the image file");
            AddDescriptionEntry(DllCharacteristics.Name, "Dll Characteristics");
            if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
            {
                AddDescriptionEntry(SizeOfStackReserve32.Name, "Size of virtual memory to reserve for the initial thread’s stack");
                AddDescriptionEntry(SizeOfStackCommit32.Name, "Size of virtual memory initially committed for the initial thread’s stack ");
                AddDescriptionEntry(SizeOfHeapReserve32.Name, "Size of virtual memory to reserve for the initial process heap");
                AddDescriptionEntry(SizeOfHeapCommit32.Name, "Size of virtual memory initially committed for the process heap");
            }
            else if (Magic.Value == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
            {
                AddDescriptionEntry(SizeOfStackReserve64.Name, "Size of virtual memory to reserve for the initial thread’s stack");
                AddDescriptionEntry(SizeOfStackCommit64.Name, "Size of virtual memory initially committed for the initial thread’s stack ");
                AddDescriptionEntry(SizeOfHeapReserve64.Name, "Size of virtual memory to reserve for the initial process heap");
                AddDescriptionEntry(SizeOfHeapCommit64.Name, "Size of virtual memory initially committed for the process heap");
            }
            
            AddDescriptionEntry(LoaderFlags.Name, "Loader Flags");
            AddDescriptionEntry(NumberOfRvaAndSizes.Name, "Number of entries in the DataDirectory (min. 16)");
        }
    }
}
