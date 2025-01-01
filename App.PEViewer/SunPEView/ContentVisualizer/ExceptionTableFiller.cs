using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.DataDirectories;

namespace SunPEView.ContentVisualizer
{
    /// <summary>
    /// Visualization class of exception table.
    /// </summary>
    class ExceptionTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public ExceptionTableFiller(ContentDataGridView dataGridView) : 
            base(dataGridView) 
        {
        }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 4;
            dataGridView.Columns[0].Name = "File Offset";
            dataGridView.Columns[1].Name = "BeginAddress";
            dataGridView.Columns[2].Name = "EndAddress";
            dataGridView.Columns[3].Name = "UnwindInfoAddress";

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 80;
            dataGridView.Columns[3].Width = 100;

            foreach (PeExcepFuncEntryIA3264 entry in peFile.PeExceptionTable.GetEntries())
            {
                int rowIndex = dataGridView.Rows.Add(
                    entry.BeginAddress.OffsetString,
                    StringUtil.GetFormattedHexString(entry.BeginAddress.Value),
                    StringUtil.GetFormattedHexString(entry.EndAddress.Value),
                    StringUtil.GetFormattedHexString(entry.UnwindInfoAddress.Value));
                dataGridView.Rows[rowIndex].Tag = entry.BeginAddress;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = entry.ToString();
            }
        }
    
    }
}
