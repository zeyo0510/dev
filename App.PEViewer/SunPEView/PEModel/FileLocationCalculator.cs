using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.Exceptions;


namespace SunPEView.PEModel
{
    /*
     * Class to convert virtual addresses, relative virtual addresses and raw addresses based
     * on the section table information of the PE file.
     */
    class FileLocationCalculator
    {
        private static readonly string OFFSET_BEYOND_FILESIZE = "Out of file range";
        private static readonly string OFFSET_BEFORE_FIRST_FILESEC = "Before first section";
        private static readonly string OFFSET_BEYOND_LAST_FILESEC = "Beyond last section";

        private PeSectionHeaderTable    sectionTable;
        private long fileSize;
        private long imageBase;

        public FileLocationCalculator(PeSectionHeaderTable sectionTable, long fileSize, ulong imageBase)
        {
            this.sectionTable = sectionTable;
            this.fileSize = fileSize;
            this.imageBase = (long)imageBase;
        }

        public long GetFileOffsetFromRVA(long rva)
        {
            for (int sectionIndex = 0; sectionIndex < sectionTable.NumOfSections - 1; sectionIndex++)
            {
                if (rva >= sectionTable[sectionIndex].VirtualAddress.Value && 
                    rva < sectionTable[sectionIndex+1].VirtualAddress.Value)
                {
                    return (rva - sectionTable[sectionIndex].VirtualAddress.Value + sectionTable[sectionIndex].PointerToRawData.Value);
                }
            }

            if (rva < sectionTable[0].VirtualAddress.Value)
            {
                // RVA is before the first section
                return rva;
            }
            else
            {
                // RVA rva lays beyond the last section
                PeSectionHeader lastSection = sectionTable[sectionTable.NumOfSections - 1];
                return (rva - lastSection.VirtualAddress.Value + lastSection.PointerToRawData.Value);
            }
        }

        public long GetVAFromRVA(long rva)
        {
            return (rva + (long)imageBase);
        }

        public void GetAddressesFromRVA(long rva, out long fileOffset, out long va)
        {
            fileOffset = GetFileOffsetFromRVA(rva);
            va = GetVAFromRVA(rva);
        }

        public long GetFileOffsetFromVA(long va)
        {
            long rva = GetRVAFromVA(va);
            return GetFileOffsetFromRVA(rva);
        }

        public long GetRVAFromVA(long va)
        {
            if (va < imageBase)
                throw new AddressOutOfRange("VA is too small.");

            return (va - imageBase);
        }

        public void GetAddressesFromVA(long va, out long fileOffset, out long rva)
        {
            fileOffset = GetFileOffsetFromVA(va);
            rva = GetRVAFromVA(va);
        }

        public long GetRVAFromFileOffset(long fileOffset)
        {
            if (fileOffset >= fileSize)
                throw new AddressOutOfRange("File Offset is greater than size of file.");

            for (int sectionIndex = 0; sectionIndex < sectionTable.NumOfSections - 1; sectionIndex++)
            {
                if (fileOffset >= sectionTable[sectionIndex].PointerToRawData.Value &&
                    fileOffset < sectionTable[sectionIndex + 1].PointerToRawData.Value)
                {
                    return (fileOffset - sectionTable[sectionIndex].PointerToRawData.Value + sectionTable[sectionIndex].VirtualAddress.Value);
                }
            }

            if (fileOffset < sectionTable[0].PointerToRawData.Value)
            {
                // file offset is before the first section
                return fileOffset;
            }
            else
            {
                // file offset rva lays beyond the last section
                PeSectionHeader lastSection = sectionTable[sectionTable.NumOfSections - 1];
                return (fileOffset - lastSection.PointerToRawData.Value + lastSection.VirtualAddress.Value);
            }
        }

        public long GetVAFromFileOffset(long fileOffset)
        {
            long rva = GetRVAFromFileOffset(fileOffset);
            return GetVAFromRVA(rva);
        }

        public void GetAddressesFromFileOffset(long fileOffset, out long rva, out long va)
        {
            rva = GetRVAFromFileOffset(fileOffset);
            va = GetVAFromRVA(rva);
        }

        public string GetSectionName(long fileOffset)
        {
            if (fileOffset >= fileSize)
            {
                return OFFSET_BEYOND_FILESIZE;
            }
            for (int sectionIndex = 0; sectionIndex < sectionTable.NumOfSections - 1; sectionIndex++)
            {
                if (fileOffset >= sectionTable[sectionIndex].PointerToRawData.Value &&
                    fileOffset < sectionTable[sectionIndex + 1].PointerToRawData.Value)
                {
                    return sectionTable[sectionIndex].Name.Value;
                }
            }

            if (fileOffset < sectionTable[0].PointerToRawData.Value)
            {
                // file offset is before the first section
                return OFFSET_BEFORE_FIRST_FILESEC;
            }
            else
            {
                if (fileOffset <= sectionTable[sectionTable.NumOfSections - 1].PointerToRawData.Value +
                                  sectionTable[sectionTable.NumOfSections - 1].SizeOfRawData.Value)
                {
                    // file offset is inside last section
                    return sectionTable[sectionTable.NumOfSections - 1].Name.Value;
                }
                else
                {
                    // file offset rva lays beyond the last section
                    return OFFSET_BEYOND_LAST_FILESEC;
                }
            }
        }

        
    }
}
