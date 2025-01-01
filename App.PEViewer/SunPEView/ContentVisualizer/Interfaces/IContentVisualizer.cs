using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using System.Windows.Forms;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.Resources;
using SunPEView.PEModel.PEFormat.DebugDirectory;
using PEFileTreeView;
using SunPEView.PEModel.PEFormat.Resources.StandardId;

namespace SunPEView.ContentVisualizer.Interfaces
{
    delegate void SelectedRowChangedDelegate(object sender, SectionRowChangeEvents e);

    /// <summary>
    /// EventArgs class for the selected row changed event.
    /// </summary>
    internal class SectionRowChangeEvents : DataGridViewCellEventArgs
    {
        private IPeElem peElem;

        public SectionRowChangeEvents(DataGridViewCellEventArgs e, IPeElem elem)
            : base(e.ColumnIndex, e.RowIndex)
        {
            peElem = elem;
        }

        public IPeElem PeElem
        {
            get { return peElem; }
        }
    }

    internal interface IContentVisualizer
    {
        void ShowDosHeader(PEFile peFile);
        void ShowPeHeader(PEFile peFile);
        void ShowPeFileHeader(PEFile peFile);
        void ShowPeOptionalHeader(PEFile peFile);
        void ShowDataDirectoryTable(PEFile peFile);
        void ShowOverallSections(PEFile peFile);
        void ShowSection(PEFile peFile, PeSectionHeader section);
        void ShowImportTable(PEFile peFile);
        void ShowExportTable(PEFile peFile);
        void ShowExportedFunctionsTable(PEFile peFile);

        void ShowDebugDirectoryTable(PEFile peFile, PeDebugDirectoryTable debugDirTable);
        void ShowDebugDirectory(PEFile peFile, PeDebugDirectory debugDataDir);
        void ShowRelocationTable(PEFile peFile, PeRelocationTable relocTable);
        void ShowRelocationEntry(PEFile pefile, PeRelocationEntry relocEntry);
        void ShowExceptionTable(PEFile peFile);
        void ShowTLSTable(PEFile peFile);
        void ShowDotNetTable(PEFile peFile);

        event SelectedRowChangedDelegate SelectedRowChanged;

        void ShowImport(PEFile peFile, PeImportDescriptor impDesc);

        /*
         * PE Resource Handling
         */
        void ShowResourceTable(PEFile peFile, PeResourceDirectoryTable resDir);
        void ShowResourceEntry(PEFile peFile, PeResourceDirectoryEntry resDirEntry);
        void ShowResourceDataEntry(PEFile peFile, PeResourceDataEntry resDataDirEntry);
        // extended standard resource handling
        void ShowStdResIconGroupDirEntry(PEFile peFile, PeIconGroupDirEntry resIconGroupDir);
        void ShowStdResGroupGroupDirEntry(PEFile peFile, PeCursorGroupDirEntry resCursorGroupDir);

        /// <summary>
        /// Called when a resource data entry of a icon group is selected.
        /// This function allows to dynamically add the icon directories to the GUI (lazy loading).
        /// </summary>
        /// <param name="peFileTreeView"></param>
        /// <param name="peFile"></param>
        /// <param name="pESectionNode"></param>
        /// <param name="peResourceDataEntry"></param>
        void AddStdResIconGroupElements(PEFileTreeViewControl peFileTreeView, PEFile peFile,
                                PEFileTreeView.PESectionNode pESectionNode, PeIconGroupResource resIconGroup);

        /// <summary>
        /// Called when a resource data entry of a cursor group is selected.
        /// This function allows to dynamically add the icon directories to the GUI (lazy loading).
        /// </summary>
        /// <param name="peFileTreeView"></param>
        /// <param name="peFile"></param>
        /// <param name="pESectionNode"></param>
        /// <param name="resCursorGroup"></param>
        void AddStdResCursorGroupElements(PEFileTreeViewControl peFileTreeView, PEFile peFile,
                                PEFileTreeView.PESectionNode pESectionNode, PeCursorGroupResource resCursorGroup);
        
    }
}
