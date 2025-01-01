using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat.DebugDirectory;

namespace SunPEView.ContentVisualizer
{
    class DebugTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeDebugDirectoryTable debugDataTable;

        public DebugTableFiller(ContentDataGridView dataGridView, PeDebugDirectoryTable debugDataTable) : 
            base(dataGridView) 
        {
            this.debugDataTable = debugDataTable;
        }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 2;
            dataGridView.Columns[0].Name = "Attribute";
            dataGridView.Columns[1].Name = "Value";

            dataGridView.Rows.Add("#DebugDirectories", debugDataTable.DebugDirectories.Length);
        }
    }
}
