using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;

namespace SunPEView.ContentVisualizer
{
    class RelocationTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeRelocationTable relocTable;

        public RelocationTableFiller(ContentDataGridView dataGridView, PeRelocationTable relocTable) : 
            base(dataGridView) 
        {
            this.relocTable = relocTable;
        }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 5;
            dataGridView.Columns[0].Name = "Name";
            dataGridView.Columns[1].Name = "File Offset";
            dataGridView.Columns[2].Name = "Virtual Address";
            dataGridView.Columns[3].Name = "SizeOfBlock";
            dataGridView.Columns[4].Name = "#Fixups";

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 80;
            dataGridView.Columns[3].Width = 70;
            dataGridView.Columns[4].Width = 70;

            foreach (PeRelocationEntry relocEntry in relocTable)
            {
                int rowIndex = dataGridView.Rows.Add(relocEntry.ToString(),
                    relocEntry.VirtualAddress.OffsetString,
                    StringUtil.GetFormattedHexString(relocEntry.VirtualAddress.Value),
                    StringUtil.GetFormattedHexString(relocEntry.SizeOfBlock.Value),
                    relocEntry.FixupsItems.Count);
                dataGridView.Rows[rowIndex].Tag = relocEntry.VirtualAddress;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = relocEntry.ToString();
            }

        }
    }
}
