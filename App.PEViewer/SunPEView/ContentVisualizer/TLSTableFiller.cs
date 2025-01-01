using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;

namespace SunPEView.ContentVisualizer
{
    class TLSTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public TLSTableFiller(ContentDataGridView dataGridView) : 
            base(dataGridView) 
        {
        }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 4;
            dataGridView.Columns[0].Name = "Name";
            dataGridView.Columns[1].Name = "File Offset";
            dataGridView.Columns[2].Name = "Size";
            dataGridView.Columns[3].Name = "Value";

            dataGridView.Columns[0].Width = 140;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 80;
            dataGridView.Columns[3].Width = 80;

            foreach (IPeElem elem in peFile.PeTLSDirectoryTable)
            {
                int rowIndex = dataGridView.Rows.Add(
                    elem.Name,
                    elem.OffsetString,
                    elem.SizeInBytes(),
                    elem.ValueString()
                    );
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeTLSDirectoryTable.GetDescription(elem.Name);
            }
        }
    }
}
