using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel;

namespace SunPEView.ContentVisualizer
{
    /// <summary>
    /// Fill information of all PeDataDirectores into GUI.
    /// </summary>
    class DataDirectoryTableFiller : AbstractFiller, IContentFiller
    {
        public DataDirectoryTableFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

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

            foreach (PeDataDirectory dataDir in peFile.PeDataDirectoryTable)
            {
                int rowIndex = -1;
                foreach (IPeElem elem in dataDir)
                {
                    rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                         elem.SizeInBytes(), elem.ValueString());
                    dataGridView.Rows[rowIndex].Tag = elem;
                    // add description as tooltip
                    dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeDataDirectoryTable.GetDescription(elem.Name);
                }

                // add note if data directory values are given, but are invalid
                if (!dataDir.Exists())
                {
                    //PELogger.Instance.Log(dataDir.ToString() + " is invalid.", PELogger.LoggingLevel.WARNING);
                    dataGridView.Rows[rowIndex - 1].Cells[4].Value = "Invalid";
                }
            }
        }
    }
}
