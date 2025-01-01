using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    /// <summary>
    /// This class the whole import directory.
    /// </summary>
    class PeImportDirectoryTable
    {
        /// <summary>
        /// list of all import descriptors
        /// </summary>
        private List<PeImportDescriptor> importDescriptors;

        /// <summary>
        /// reference to pe file to get access to file location calculator
        /// </summary>
        private PEFile peFile;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="peFile"></param>
        public PeImportDirectoryTable(PEFile peFile)
        {
            this.peFile = peFile;
        }

        public void Read(IFileAccess fDataReader)
        {
            importDescriptors = new List<PeImportDescriptor>();

            // read first import descriptor, should always exist
            PeImportDescriptor nextImportDescriptor = new PeImportDescriptor(peFile);
            nextImportDescriptor.Read(fDataReader);

            while (!isTerminatingImportDescriptor(nextImportDescriptor) && 
                   fDataReader.Position < fDataReader.Length - 20)
            {
                // add it to our datamodel list
                importDescriptors.Add(nextImportDescriptor);

                // at this point, the standard data was already read for this import, so parse the details.
                nextImportDescriptor.ReadImportThunkData();

                /* important to create a new import descriptor object otherwise the previous one will be overwritten */
                nextImportDescriptor = new PeImportDescriptor(peFile);
                nextImportDescriptor.Read(fDataReader);
            }
        }

        /// <summary>
        /// Check whether the given import descriptor is the terminating one, so all entrys are zero.
        /// </summary>
        /// <param name="impDesc"></param>
        /// <returns></returns>
        private bool isTerminatingImportDescriptor(PeImportDescriptor impDesc)
        {
            /* default case: all values are zero = terminating descriptor */
            if (impDesc.OriginalFirstThunk.Value == 0 &&
                    impDesc.TimeDateStamp.Value == 0 &&
                    impDesc.ForwarderChain.Value == 0 &&
                    impDesc.NameRva.Value == 0 &&
                    impDesc.FirstThunk.Value == 0)
            {
                return true;
            }

            /* to handle defect PE files, some special handling */
            if (importDescriptors.Count > 0)
            {
                PeImportDescriptor pei = importDescriptors[importDescriptors.Count-1];
                if (pei.OriginalFirstThunk.Value == impDesc.OriginalFirstThunk.Value &&
                    pei.FirstThunk.Value == impDesc.FirstThunk.Value)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The number of import directories / DLLs in this file.
        /// </summary>
        public int Count
        {
            get { return importDescriptors.Count; }
        }

        public IEnumerator<PeImportDescriptor> GetEnumerator()
        {
            foreach (PeImportDescriptor importDesc in importDescriptors)
            {
                yield return importDesc;
            }
        }
    }
}
