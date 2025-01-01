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
    class PeOptionalHeaderFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public PeOptionalHeaderFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

        public void FillContent(PEFile peFile)
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
            dataGridView.Columns[3].Width = 130;
            dataGridView.Columns[4].Width = 180;

            foreach (IPeElem elem in peFile.PeOptionalHeader)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                     elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeOptionalHeader.GetDescription(elem.Name);
            }

            // add special meanings
            AddSpecialMeanings(peFile);
        }

        private void AddSpecialMeanings(PEFile peFile)
        {
            // add clickable area for magic values
            EOptionalHeaderMagic emachine = (EOptionalHeaderMagic)peFile.PeOptionalHeader.Magic.Value;
            dataGridView.Rows[0].Cells[4].Value = emachine.ToString();
            dataGridView.Rows[0].Cells[4].Style.BackColor = Color.Yellow;

            // add clickable area for subsystem
            ESubsystem esubsystem = (ESubsystem)peFile.PeOptionalHeader.Subsystem.Value;
            dataGridView.Rows[21].Cells[4].Value = esubsystem.ToString();
            dataGridView.Rows[21].Cells[4].Style.BackColor = Color.Yellow;

            // add clickable area for dll characteristics
            dataGridView.Rows[22].Cells[4].Value = "<click for details>";
            dataGridView.Rows[22].Cells[4].Style.BackColor = Color.Yellow;

            // add click handler
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
                GenericEnumDisplayWindow gedw = new GenericEnumDisplayWindow((EOptionalHeaderMagic)peFile.PeOptionalHeader.Magic.Value, "Optional Header Magic Values");
                gedw.Height = 170;
                gedw.ShowDialog();
            }
            else if (e.RowIndex == 21 && e.ColumnIndex == 4)
            {
                GenericEnumDisplayWindow gedw = new GenericEnumDisplayWindow((ESubsystem)peFile.PeOptionalHeader.Subsystem.Value, "Subsystem");
                gedw.Height = 330;
                gedw.ShowDialog();
            }
            else if (e.RowIndex == 22 && e.ColumnIndex == 4)
            {
                GenericEnumFlagsDisplayWindow gedw = new GenericEnumFlagsDisplayWindow
                    ("DLL Characteristics", EDllCharacteristics.DLLCHARACTERISTICS_DYNAMIC_BASE,
                    peFile.PeOptionalHeader.DllCharacteristics.Value);

                gedw.Width = 450;
                gedw.Height = 200;
                gedw.ShowDialog();
            }
        }
    }
}
