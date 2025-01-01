using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.DebugDirectory;
using SunPEView.PEModel.PEFormat.Enums;
using System.Drawing;
using SunPEView.ContentVisualizer.Windows;
using System.Windows.Forms;

namespace SunPEView.ContentVisualizer
{
    class DebugDirFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeDebugDirectory debugDataDir;

        public DebugDirFiller(ContentDataGridView dataGridView, PeDebugDirectory debugDataDir) : 
            base(dataGridView) 
        {
            this.debugDataDir = debugDataDir;
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

            dataGridView.Columns[0].Width = 160;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 100;
            dataGridView.Columns[4].Width = 220;

            foreach (IPeElem elem in debugDataDir)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                        elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = debugDataDir.GetDescription(elem.Name);
            }
            AddSpecialMeanings();
           
        }

        private void AddSpecialMeanings()
        {
            // add readable version of date time
            DateTime dt = new DateTime(1970, 1, 1);
            DateTime targetDt = dt.AddSeconds(debugDataDir.TimeDateStamp.Value);
            dataGridView.Rows[1].Cells[4].Value = targetDt.ToString();

            // add clickable area for debug type
            EDebugType type = (EDebugType)debugDataDir.Type.Value;
            dataGridView.Rows[4].Cells[4].Value = type.ToString();
            dataGridView.Rows[4].Cells[4].Style.BackColor = Color.Yellow;

            // add clickable area of data
            dataGridView.Rows[7].Cells[4].Value = "Click here to mark data";
            dataGridView.Rows[7].Cells[4].Style.BackColor = Color.Yellow;

            AddCellClickHandler(dataGridView_CellClick);
        }

        void dataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ignore header click
                return;
            }
            else if (e.RowIndex == 4 && e.ColumnIndex == 4)
            {
                /* show window with all possible machine values */
                GenericEnumDisplayWindow gedw = new GenericEnumDisplayWindow((EDebugType)debugDataDir.Type.Value, "Debug type values");
                gedw.Height = 300;
                gedw.ShowDialog();
            }
            else if (e.RowIndex == 7 && e.ColumnIndex == 4)
            {
                if (peFile.FileData.IsAddressRangeValid(
                        (uint)debugDataDir.PointerToRawData.Offset, debugDataDir.SizeOfData.Value))
                {
                    GenericByteArrayWindow gbaw = new GenericByteArrayWindow(peFile.FileData,
                        (uint)debugDataDir.PointerToRawData.Offset, debugDataDir.SizeOfData.Value);
                    gbaw.Text = "Data of Debug Directory (Offset: " +
                            StringUtil.GetFormattedHexString(debugDataDir.PointerToRawData.Offset) +
                            " , Size: " + StringUtil.GetFormattedHexString(debugDataDir.SizeOfData.Value) + ")";
                    gbaw.ShowDialog();
                }
                else
                {
                    PELogger.Instance.Log("Offset and size are invalid.", PELogger.LoggingLevel.ERROR);
                }
            }
        }
    }
}
