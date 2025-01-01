using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.Enums;
using SunPEView.PEModel.PEFormat.ExportDirectoryTable;
using SunPEView.PEModel.PEFormat.Resources;
using SunPEView.PEModel.PEFormat.DebugDirectory;
using SunPEView.PEModel.PEFormat.DataDirectories;
using SunPEView.PEModel.PEFormat.DotNet;

namespace SunPEView.PEModel
{
    /// <summary>
    /// Main class for the application to access a PE file.
    /// </summary>
    class PEFile : BinaryFile, PEFileTreeView.IPEFile
    {
        /************************************************************
         * Fields and constants
         ************************************************************/

        /************************************************************
         * Variables
         ************************************************************/
        ushort magicValueOptionalHeader = 0;

        /************************************************************
         * Properties
         ************************************************************/
        public FileLocationCalculator FileLocationCalculator
        {
            get;
            private set;
        }

        /************************************************************
         * PE Properties
         ************************************************************/
        public DosHeader DosHeader
        {
            get;
            private set;
        }

        public PeHeader PeHeader
        {
            get;
            private set;
        }

        public PeFileHeader PeFileHeader
        {
            get;
            private set;
        }

        public PeOptionalHeader PeOptionalHeader
        {
            get;
            private set;
        }

        public PeDataDirectoryTable PeDataDirectoryTable
        {
            get;
            private set;
        }

        public PeSectionHeaderTable PeSectionHeaderTable
        {
            get;
            private set;
        }

        public PeImportDirectoryTable PeImportDirectoryTable
        {
            get;
            private set;
        }

        public PeExportDirectoryTable PeExportDirectoryTable
        {
            get;
            private set;
        }

        public PeResourceDirectoryTable PeResourceDirectoryTable
        {
            get;
            private set;
        }

        public PeDebugDirectoryTable PeDebugDirectoryTable
        {
            get;
            private set;
        }

        public PeRelocationTable PeRelocationTable
        {
            get;
            private set;
        }

        public PeExceptionTable PeExceptionTable
        {
            get;
            private set;
        }

        public PeTLSDirectoryTable PeTLSDirectoryTable
        {
            get;
            private set;
        }

        public PeDotNetDirectory PeDotNetDirectory
        {
            get;
            private set;
        }

        /************************************************************
         * Methods
         ************************************************************/

        public PEFile(string filename) : base(filename) { }

        public override void LoadFile()
        {
            base.LoadFile();

            DosHeader = new DosHeader();
            DosHeader.Read(FileData);

            if (!isDosHeaderValid())
            {
                PELogger.Instance.Log("Invalid PE Signature.", PELogger.LoggingLevel.ERROR);
                return;
            }

            PeHeader = new PeHeader();
            FileData.Position = DosHeader.E_lfanew.Value;
            PeHeader.Read(FileData);

            if (!isPeHeaderValid())
            {
                PELogger.Instance.Log("Invalid PE Header.", PELogger.LoggingLevel.ERROR);
                return;
            }

            PeFileHeader = new PeFileHeader();
            FileData.Position = (uint)PeHeader.FileStartOffset + 4;
            PeFileHeader.Read(FileData);

            // read optional header, either 32 or 64 bit depending on the magic value
            uint optionalHeaderStartOffset = FileData.Position;
            magicValueOptionalHeader = FileData.ReadUInt16();
            FileData.Position = optionalHeaderStartOffset;
            if (magicValueOptionalHeader == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC ||
                magicValueOptionalHeader == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
            {
                PeOptionalHeader = new PeOptionalHeader();
                PeOptionalHeader.Read(FileData);
            }
            else if (magicValueOptionalHeader == (ushort)EOptionalHeaderMagic.IMAGE_ROM_OPTIONAL_HDR_MAGIC)
            {
                PELogger.Instance.Log("ROM images not supported.", PELogger.LoggingLevel.ERROR);
                return;
            }
            else
            {
                PELogger.Instance.Log("Invalid Magic value in Optional Header.", PELogger.LoggingLevel.ERROR);
                return;
            }

            // read data directories
            PeDataDirectoryTable = new PeDataDirectoryTable(this, PeOptionalHeader.NumberOfRvaAndSizes.Value);
            PeDataDirectoryTable.Read(FileData);

            // read sections. The Section Header starts directly after the optional header, whose size is given by SizeOfOptionalHeader in FileHeader.
            PELogger.Instance.Log("Reading PE sections...", PELogger.LoggingLevel.INFO);
            FileData.Position = optionalHeaderStartOffset + PeFileHeader.SizeOfOptionalHeader.Value;
            if (FileData.Position <= FileSize)
            {
                PeSectionHeaderTable = new PeSectionHeaderTable();
                PeSectionHeaderTable.Read(FileData, PeFileHeader.NumberOfSections.Value);
                // init file location calculator
                ulong imageBase = (isPe64() ? PeOptionalHeader.ImageBase64.Value : PeOptionalHeader.ImageBase32.Value);
                if (PeFileHeader.NumberOfSections.Value != 0)
                {
                    FileLocationCalculator = new FileLocationCalculator(PeSectionHeaderTable, FileSize, imageBase);
                }
                else
                {
                    PELogger.Instance.Log("No PE sections found.", PELogger.LoggingLevel.WARNING);
                }
            }


            // read import directory
            PELogger.Instance.Log("Reading Import Directory...", PELogger.LoggingLevel.INFO);
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_IMPORT].Exists() && FileLocationCalculator != null)
            {
                // get file offset of import directory
                long importDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_IMPORT].VirtualAddress.Value);
                PeImportDirectoryTable = new PeImportDirectoryTable(this);
                // backup file pointer and set current to import table
                FileData.PushAddress();
                FileData.Position = (uint)importDirOffset;
                PeImportDirectoryTable.Read(FileData);
                FileData.PopAddress();
            }
            
            // read export directory
            PELogger.Instance.Log("Reading Export Directory...", PELogger.LoggingLevel.INFO);
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_EXPORT].Exists() && FileLocationCalculator != null)
            {
                // get file offset of export directory
                long exportDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_EXPORT].VirtualAddress.Value);
                PeExportDirectoryTable = new PeExportDirectoryTable(this);
                // backup file pointer and set current to import table
                FileData.PushAddress();
                FileData.Position = (uint)exportDirOffset;
                PeExportDirectoryTable.Read(FileData);
                FileData.PopAddress();
            }

            // read resource directoy
            PELogger.Instance.Log("Reading Resource Directory...", PELogger.LoggingLevel.INFO);
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].Exists() && FileLocationCalculator != null)
            {
                // get file offset of export directory
                long resourceDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].VirtualAddress.Value);
                PeResourceDirectoryTable = new PeResourceDirectoryTable(this, resourceDirOffset);
                PeResourceDirectoryTable.Read(FileData);
            }

            // read debug directory
            PELogger.Instance.Log("Reading Debug Directory...", PELogger.LoggingLevel.INFO);
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_DEBUG].Exists() && FileLocationCalculator != null)
            {
                long debugDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_DEBUG].VirtualAddress.Value);
                PeDebugDirectoryTable = new PeDebugDirectoryTable(this, debugDirOffset);
                FileData.PushAddress();
                PeDebugDirectoryTable.Read(FileData);
                FileData.PopAddress();
            }

            // read relocation directory
            PELogger.Instance.Log("Reading Relocation Directory...", PELogger.LoggingLevel.INFO);
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_BASERELOC].Exists() && FileLocationCalculator != null)
            {
                long relocDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_BASERELOC].VirtualAddress.Value);
                PeRelocationTable = new PeRelocationTable(this, relocDirOffset);
                FileData.PushAddress();
                PeRelocationTable.Read(FileData);
                FileData.PopAddress();
            }

            // setup exception directory
            PELogger.Instance.Log("Reading Exception Directory...", PELogger.LoggingLevel.INFO);
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_EXCEPTION].Exists() && FileLocationCalculator != null)
            {
                long excepDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_EXCEPTION].VirtualAddress.Value);
                PeExceptionTable = new PeExceptionTable(this, excepDirOffset);
                // lazy reading implemented 
            }

            // TLS directory
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_TLS].Exists() && FileLocationCalculator != null)
            {
                long tlsDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_TLS].VirtualAddress.Value);
                PeTLSDirectoryTable = new PeTLSDirectoryTable(this, tlsDirOffset);
                // lazy reading implemented
            }

            // .NET / CLI directory
            if (PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR].Exists() && FileLocationCalculator != null)
            {
                PELogger.Instance.Log("Reading .NET Directory...", PELogger.LoggingLevel.INFO);
                long cliDirOffset = FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR].VirtualAddress.Value);
                PeDotNetDirectory = new PeDotNetDirectory(this, cliDirOffset);
                // lazy reading implemented
            }

            PELogger.Instance.Log("Reading PE done.", PELogger.LoggingLevel.INFO);
        }

        /// <summary>
        /// Returns true if file contains a valid DOS header.
        /// </summary>
        /// <returns></returns>
        public bool isDosHeaderValid()
        {
            return (DosHeader != null &&
                    DosHeader.E_magic.Value == DosHeader.DOS_SIGNATURE &&
                    DosHeader.E_lfanew.Value != 0);
        }

        /// <summary>
        /// Returns true if file is a valid PE file.
        /// </summary>
        /// <returns></returns>
        public bool isPeHeaderValid()
        {
            return(PeHeader != null &&
                   PeHeader.Signature.Value == PeHeader.PE_SIGNATURE);
        }

        /// <summary>
        /// Returns true if file is a 32bit PE file.
        /// </summary>
        /// <returns></returns>
        public bool isPe32()
        {
            return (magicValueOptionalHeader == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR32_MAGIC);
        }

        /// <summary>
        /// Returns true if file is a 64bit PE file.
        /// </summary>
        /// <returns></returns>
        public bool isPe64()
        {
            return (magicValueOptionalHeader == (ushort)EOptionalHeaderMagic.IMAGE_NT_OPTIONAL_HDR64_MAGIC);
        }

        /// <summary>
        /// Returns true if file contains an Import Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsImportDirectory()
        {
            return (PeDataDirectoryTable != null && 
                    PeImportDirectoryTable != null && 
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_IMPORT].Exists());
        }

        /// <summary>
        /// Returns true if file contains a Export Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsExportDirectory()
        {
            return (PeDataDirectoryTable != null && 
                    PeExportDirectoryTable != null && 
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_EXPORT].Exists());
        }

        /// <summary>
        /// Returns true if file contains a Resource Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsResourceDirectory()
        {
            return (PeDataDirectoryTable != null && 
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].Exists());
        }

        /// <summary>
        /// Returns true if file contains a Debug Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsDebugDirectory()
        {
            return (PeDataDirectoryTable != null &&
                    PeDebugDirectoryTable != null &&
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_DEBUG].Exists());
        }

        /// <summary>
        /// Returns true if file contains a Relocation Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsRelocationDirectory()
        {
            return (PeDataDirectoryTable != null &&
                    PeRelocationTable != null && 
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_BASERELOC].Exists());
        }

        /// <summary>
        /// Returns true if file contains a Exception Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsExceptionDirectory()
        {
            return (PeDataDirectoryTable != null &&
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_EXCEPTION].Exists());
        }

        /// <summary>
        /// Returns true if file contains a TLS Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsTLSDirectory()
        {
            return (PeDataDirectoryTable != null &&
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_TLS].Exists());
        }

        /// <summary>
        /// Returns true if file contains a .NET Directory entry.
        /// </summary>
        /// <returns></returns>
        public bool ExistsDotNetDirectory()
        {
            return (PeDataDirectoryTable != null &&
                    PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR].Exists());
        }

        /// <summary>
        /// Returns the file offset of the beginning of resource directory.
        /// </summary>
        /// <returns></returns>
        public long GetResourceDirStartFileOffset()
        {
            return FileLocationCalculator.GetFileOffsetFromRVA(PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].VirtualAddress.Value);
        }

        /// <summary>
        /// Returns the file offset into the resource directory.
        /// </summary>
        /// <param name="deltaRva">RVA which must be a delta value to the beginning of the resource directory.</param>
        /// <returns></returns>
        public long GetResourceFileOffset(long deltaRva)
        {
            long rva = PeDataDirectoryTable[EDataDirectory.IMAGE_DIRECTORY_ENTRY_RESOURCE].VirtualAddress.Value + deltaRva;
            return FileLocationCalculator.GetFileOffsetFromRVA(rva);
        }


    }
}
