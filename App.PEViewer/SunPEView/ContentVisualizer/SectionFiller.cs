using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat.Enums;
using System.Drawing;
using SunPEView.ContentVisualizer.Windows;

namespace SunPEView.ContentVisualizer
{
    class SectionFiller : AbstractFiller, IContentFiller
    {
        private PeSectionHeader sectionHeader;

        public SectionFiller(ContentDataGridView dataGridView, PeSectionHeader section) : base(dataGridView) 
        {
            this.sectionHeader = section;
        }

        public void FillContent(PEModel.PEFile peFile)
        {
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

            foreach (IPeElem elem in sectionHeader)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                        elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = sectionHeader.GetDescription(elem.Name);
            }

            AddSpecialMeanings();

        }

        private void AddSpecialMeanings()
        {
            // add clickable area for characteristics
            //ESectionCharacteristics characteristics = (ESectionCharacteristics)sectionHeader.Characteristics.Value;
            dataGridView.Rows[9].Cells[4].Value = "<click for details>";
            dataGridView.Rows[9].Cells[4].Style.BackColor = Color.Yellow;

            AddCellClickHandler(dataGridView_CellClick);
        }

        void dataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ignore header click
                return;
            }
            else if (e.RowIndex == 9 && e.ColumnIndex == 4)
            {
                GenericEnumFlagsDisplayWindow gefdw = new GenericEnumFlagsDisplayWindow
                    ("Section characteristics", ESectionCharacteristics.IMAGE_SCN_ALIGN_1024BYTES,
                    sectionHeader.Characteristics.Value);

                gefdw.Width = 420;
                gefdw.Height = 780;
                gefdw.ShowDialog();
            }
        }
    }
}
