using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.Resources;

namespace SunPEView.ContentVisualizer
{
    class ResourceTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeResourceDirectoryTable resDir;

        public ResourceTableFiller(ContentDataGridView dataGridView, PeResourceDirectoryTable resDir) : base(dataGridView) 
        {
            this.resDir = resDir;
        }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 5;
            dataGridView.Columns[0].Name = "PE Member";
            dataGridView.Columns[1].Name = "Offset";
            dataGridView.Columns[2].Name = "Size (Bytes)";
            dataGridView.Columns[3].Name = "Value";
            dataGridView.Columns[4].Name = "Meaning";

            dataGridView.Columns[0].Width = 140;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 100;
            dataGridView.Columns[4].Width = 180;

            foreach (IPeElem elem in resDir)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                     elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = resDir.GetDescription(elem.Name);
            }
        }
    }
}
