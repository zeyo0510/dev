using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat.Enums;
using System.Drawing;
using SunPEView.ContentVisualizer.Windows;
using System.Windows.Forms;

namespace SunPEView.ContentVisualizer
{
    class PeFileHeaderFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public PeFileHeaderFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

        public void FillContent(SunPEView.PEModel.PEFile peFile)
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

            foreach (IPeElem elem in peFile.PeFileHeader)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                     elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeFileHeader.GetDescription(elem.Name);
            }

            // add special meanings
            AddSpecialMeanings(peFile);
        }

        private void AddSpecialMeanings(PEFile peFile)
        {
            // avoid resorting when column header is clicked
           // dataGridView.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            // add clickable area for machine types
            EMachine emachine = (EMachine)peFile.PeFileHeader.Machine.Value;
            dataGridView.Rows[0].Cells[4].Value = emachine.ToString();
            dataGridView.Rows[0].Cells[4].Style.BackColor = Color.Yellow;
            
            // add readable version of date time
            DateTime dt = new DateTime(1970, 1, 1);
            DateTime targetDt = dt.AddSeconds(peFile.PeFileHeader.TimeDateStamp.Value);
            dataGridView.Rows[2].Cells[4].Value = targetDt.ToString();

            // add clickable area for characteristics
            dataGridView.Rows[6].Cells[4].Value = "<click for details>";
            dataGridView.Rows[6].Cells[4].Style.BackColor = Color.Yellow;

            AddCellClickHandler(dataGridView_CellClick);
        }

        void dataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ignore header click
                return;
            }
            else if (e.RowIndex == 0 && e.ColumnIndex == 4)
            {
                /* show window with all possible machine values */
                GenericEnumDisplayWindow gedw = new GenericEnumDisplayWindow((EMachine)peFile.PeFileHeader.Machine.Value, "Machine type values");
                gedw.Height = 600;
                gedw.StartPosition = FormStartPosition.CenterScreen;
                gedw.ShowDialog();
            }
            else if (e.RowIndex == 6 && e.ColumnIndex == 4)
            {
                GenericEnumFlagsDisplayWindow gefdw = new GenericEnumFlagsDisplayWindow
                    ("File characteristics", ECharacteristics.IMAGE_FILE_32BIT_MACHINE,
                    peFile.PeFileHeader.Characteristics.Value);
                
                gefdw.Width = 420;
                gefdw.Height = 350;
                gefdw.ShowDialog();
            }
        }
    }
}
