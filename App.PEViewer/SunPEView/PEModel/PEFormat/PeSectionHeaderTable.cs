using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    class PeSectionHeaderTable
    {
        private PeSectionHeader[] Sections;

        public void Read(IFileAccess fDataReader, ushort numOfSections)
        {
            Sections = new PeSectionHeader[numOfSections];
            for (int i = 0; i < numOfSections; i++)
            {
                Sections[i] = new PeSectionHeader(i);
                Sections[i].Read(fDataReader);
            }
            NumOfSections = numOfSections;
        }

        public ushort NumOfSections
        {
            get;
            private set;
        }

        public PeSectionHeader this[int index]
        {
            get { return Sections[index]; }
        }

        public IEnumerator<PeSectionHeader> GetEnumerator()
        {
            foreach (PeSectionHeader section in Sections)
            {
                yield return section;
            }
        }
    }
}
