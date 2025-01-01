using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;

namespace SunPEView.ContentVisualizer
{
    class ExportTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public ExportTableFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

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

            foreach (IPeElem elem in (IEnumerable<IPeElem>)peFile.PeExportDirectoryTable)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                     elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeExportDirectoryTable.GetDescription(elem.Name);
            }

            // add special meanings
            AddSpecialMeanings(peFile);
        }

        private void AddSpecialMeanings(PEFile peFile)
        {
            // add readable version of date time
            DateTime dt = new DateTime(1970, 1, 1);
            DateTime targetDt = dt.AddSeconds(peFile.PeExportDirectoryTable.TimeDateStamp.Value);
            dataGridView.Rows[1].Cells[4].Value = targetDt.ToString();

            // add dll name
            dataGridView.Rows[4].Cells[4].Value = peFile.PeExportDirectoryTable.getDllName();
        }
    }
}
