using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using SunPEView.PEModel.PEFormat.ImportDirectoryTable;
using System.Collections.ObjectModel;

namespace SunPEView.PEModel.PEFormat
{
    /// <summary>
    /// This class represens the import data of one imported DLL.
    /// </summary>
    class PeImportDescriptor
    {
        /// <summary>
        /// RVA of the first thunk
        /// </summary>
        public PeElem<uint> OriginalFirstThunk { get; private set; }

        /// <summary>
        /// Time stamp that is set to zero until the image is bound
        /// </summary>
        public PeElem<uint> TimeDateStamp { get; private set; }

        /// <summary>
        /// Index of the first forwarder reference
        /// </summary>
        public PeElem<uint> ForwarderChain { get; private set; }

        /// <summary>
        /// The address of an ASCII string that contains the name of the DLL. This address is relative to the image base.
        /// </summary>
        public PeElem<uint> NameRva { get; private set; }

        /// <summary>
        /// The RVA of the import address table. 
        /// The contents of this table are identical to the contents of the import lookup table 
        /// until the image is bound.
        /// </summary>
        public PeElem<uint> FirstThunk { get; private set; }

        /// <summary>
        /// File offset of begin of this import table.
        /// </summary>
        public uint ImportStartFileOffset { get; private set; }

        /// <summary>
        /// List of all thunk data RVAs from the original thunk array.
        /// </summary>
        private List<PeImageOriginalThunkData> originalThunkDataList;

        /// <summary>
        /// List of all thunk data RVAs from the first thunk array.
        /// </summary>
        private List<PeImportFirstThunkData> firstThunkIATDataList;

        /// <summary>
        /// reference to pe file to get access to file location calculator
        /// </summary>
        private PEFile peFile;

        /// <summary>
        /// file access object
        /// </summary>
        private IFileAccess fDataReader;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeImportDescriptor(PEFile peFile)
        {
            this.peFile = peFile;
            this.originalThunkDataList = new List<PeImageOriginalThunkData>();
            this.firstThunkIATDataList = new List<PeImportFirstThunkData>();
        }
        
        public void Read(IFileAccess fDataReader)
        {
            this.fDataReader = fDataReader;
            ImportStartFileOffset = fDataReader.Position;

            OriginalFirstThunk = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "OriginalFirstThunk");
            TimeDateStamp = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "TimeDateStamp");
            ForwarderChain = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "ForwarderChain");
            NameRva = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Name");
            FirstThunk = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "FirstThunk");
        }

        /// <summary>
        /// Returns the name of the DLL.
        /// </summary>
        /// <returns></returns>
        public string getDllName()
        {
            uint fileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(NameRva.Value);
            return fDataReader.ReadAscii(fileOffset, (uint)fDataReader.Length);
        }

        public int Count
        {
            get
            {
                if (originalThunkDataList.Count != 0)
                {
                    return originalThunkDataList.Count;
                }
                else
                {   /* if original first thunk is not available, use first thunk */
                    return firstThunkIATDataList.Count;
                }
            }
        }

        /// <summary>
        /// Get the actual imported function information from the original first thunk for this dll.
        /// </summary>
        internal void ReadImportThunkData()
        {
            if (peFile.isPe64())
            {
                ReadImportOriginalThunkData64();
                ReadFirstThunkIATData64();
            }
            else
            {
                ReadImportOriginalThunkData32();
                ReadFirstThunkIATData32();
            }
        }

        internal void ReadImportOriginalThunkData32()
        {
            // file offset to first thunk entry to which OriginalFirstThunk points to
            uint firstThunkFileAddress = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(OriginalFirstThunk.Value);
            if (firstThunkFileAddress == 0)
                firstThunkFileAddress = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(FirstThunk.Value); ;

            // get the first RVA of the thunk array to the actual thunk data
            uint RvaToThunkStruct = fDataReader.ReadUInt32(firstThunkFileAddress);

            while (RvaToThunkStruct != 0)
            {
                // get fileoffset for the current thunk
                uint thunkDataFileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(RvaToThunkStruct);

                // create a thunk data object, get data and add it to list.
                PeImageOriginalThunkData thunkData = new PeImageOriginalThunkData(peFile, RvaToThunkStruct, firstThunkFileAddress);
                thunkData.Read(fDataReader);
                originalThunkDataList.Add(thunkData);

                // get next thunk RVA
                firstThunkFileAddress += 4;
                RvaToThunkStruct = fDataReader.ReadUInt32(firstThunkFileAddress);
            }
        }

        internal void ReadImportOriginalThunkData64()
        {
            // file offset to first thunk entry to which OriginalFirstThunk points to
            uint firstThunkFileAddress = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(OriginalFirstThunk.Value);
            if (firstThunkFileAddress == 0)
                firstThunkFileAddress = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(FirstThunk.Value);

            // get the first RVA of the thunk array to the actual thunk data
            ulong RvaToThunkStruct = fDataReader.ReadUInt64(firstThunkFileAddress);

            while (RvaToThunkStruct != 0)
            {
                // get fileoffset for the current thunk
                uint thunkDataFileOffset = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA((long)RvaToThunkStruct);

                // create a thunk data object, get data and add it to list.
                PeImageOriginalThunkData thunkData = new PeImageOriginalThunkData(peFile, RvaToThunkStruct, firstThunkFileAddress);
                thunkData.Read(fDataReader);
                originalThunkDataList.Add(thunkData);

                // get next thunk RVA
                firstThunkFileAddress += 8;
                RvaToThunkStruct = fDataReader.ReadUInt64(firstThunkFileAddress);
            }
        }

        internal void ReadFirstThunkIATData64()
        {
            // file offset to first thunk entry to which FirstThunk points to
            uint firstThunkFileAddress = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(FirstThunk.Value);

            // get the first RVA of the thunk array to the actual thunk data
            ulong RvaToThunkStruct = fDataReader.ReadUInt64(firstThunkFileAddress);

            while (RvaToThunkStruct != 0)
            {
                firstThunkIATDataList.Add(new PeImportFirstThunkData(RvaToThunkStruct, firstThunkFileAddress));

                // get next thunk RVA
                firstThunkFileAddress += 8;
                RvaToThunkStruct = fDataReader.ReadUInt64(firstThunkFileAddress);
            }
        }

        internal void ReadFirstThunkIATData32()
        {
            // file offset to first thunk entry to which FirstThunk points to
            uint firstThunkFileAddress = (uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(FirstThunk.Value);

            // get the first RVA of the thunk array to the actual thunk data
            ulong RvaToThunkStruct = fDataReader.ReadUInt32(firstThunkFileAddress);

            while (RvaToThunkStruct != 0)
            {
                firstThunkIATDataList.Add(new PeImportFirstThunkData(RvaToThunkStruct, firstThunkFileAddress));

                // get next thunk RVA
                firstThunkFileAddress += 4;
                RvaToThunkStruct = fDataReader.ReadUInt32(firstThunkFileAddress);
            }
        }

        /// <summary>
        /// Loop over all original first thunk elements.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PeImageOriginalThunkData> GetEnumerator()
        {
            foreach (PeImageOriginalThunkData thunk in originalThunkDataList)
            {
                yield return thunk;
            }
        }

        public ReadOnlyCollection<PeImageOriginalThunkData> GetOriginalFirstThunk()
        {
            return new ReadOnlyCollection<PeImageOriginalThunkData>(originalThunkDataList);
        }

        /// <summary>
        /// Get the first thunk value of given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PeImportFirstThunkData getFirstThunkValue(int index)
        {
            return firstThunkIATDataList.ElementAt(index);
        }
    }
}
