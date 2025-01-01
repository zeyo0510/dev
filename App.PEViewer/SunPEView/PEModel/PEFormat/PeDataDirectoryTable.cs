using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat.Enums;


namespace SunPEView.PEModel.PEFormat
{
    class PeDataDirectoryTable : AbstractPeDescriptor
    {
        private uint NumOfDirectories = 16;

        private PeDataDirectory[] DataDirectories;

        private PEFile peFile;

        public PeDataDirectoryTable(PEFile peFile, uint numDataDirs)
        {
            this.peFile = peFile;
            this.NumOfDirectories = numDataDirs;
        }

        public void Read(IFileAccess fDataReader)
        {
            DataDirectories = new PeDataDirectory[NumOfDirectories];
            for (int i = 0; i < NumOfDirectories; i++)
            {
                DataDirectories[i] = new PeDataDirectory(peFile);
                DataDirectories[i].Read(fDataReader, ((EDataDirectory)i).GetDisplayName());
            }
        }

        public PeDataDirectory this[int index]
        {
            get { return DataDirectories[index]; }
        }

        public PeDataDirectory this[EDataDirectory index]
        {
            get { return DataDirectories[(int)index]; }
        }

        public IEnumerator<PeDataDirectory> GetEnumerator()
        {
            foreach (PeDataDirectory dataDir in DataDirectories)
            {
                yield return dataDir;
            }
        }
        

        protected override void AddDescriptions()
        {
            for (int i = 0; i < NumOfDirectories; i++)
            {
                AddDescriptionEntry(((EDataDirectory)i).GetDisplayName() + " RVA", ((EDataDirectory)i).GetDescription());
                AddDescriptionEntry(((EDataDirectory)i).GetDisplayName() + " Size", ((EDataDirectory)i).GetDescription());
            }
        }
    }
}
