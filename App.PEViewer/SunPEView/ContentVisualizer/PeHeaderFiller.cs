using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel;

namespace SunPEView.ContentVisualizer
{
    class PeHeaderFiller : AbstractFiller, IContentFiller
    {
        public PeHeaderFiller(ContentDataGridView dataGridView) : base(dataGridView) {}

        public void FillContent(PEFile peFile)
        {
            dataGridView.ColumnCount = 5;
            dataGridView.Columns[0].Name = "PE Member";
            dataGridView.Columns[1].Name = "Offset";
            dataGridView.Columns[2].Name = "Size (Bytes)";
            dataGridView.Columns[3].Name = "Value";
            dataGridView.Columns[4].Name = "Meaning";

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 90;
            dataGridView.Columns[3].Width = 100;
            dataGridView.Columns[4].Width = 100;

            foreach (IPeElem elem in peFile.PeHeader)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                     elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeHeader.GetDescription(elem.Name);
            }

            // add special meanings
            AddSpecialMeanings(peFile);
        }

        private void AddSpecialMeanings(PEFile peFile)
        {
            // add signature as string
            dataGridView.Rows[0].Cells[4].Value = peFile.PeHeader.Signature.NumValAsAscii();
        }
    }
}
