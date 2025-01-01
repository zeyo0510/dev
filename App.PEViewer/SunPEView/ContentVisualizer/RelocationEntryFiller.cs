using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;

namespace SunPEView.ContentVisualizer
{
    /// <summary>
    /// Fills all fixups of one relocation entry into gridview.
    /// </summary>
    class RelocationEntryFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeRelocationEntry relocEntryDir;

        public RelocationEntryFiller(ContentDataGridView dataGridView, PeRelocationEntry relocEntryDir) : 
            base(dataGridView) 
        {
            this.relocEntryDir = relocEntryDir;
        }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 6;
            dataGridView.Columns[0].Name = "Item";
            dataGridView.Columns[1].Name = "File Offset";
            dataGridView.Columns[2].Name = "Value";
            dataGridView.Columns[3].Name = "Value (RVA)";
            dataGridView.Columns[4].Name = "Offset";
            dataGridView.Columns[5].Name = "Type";
            
            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 70;
            dataGridView.Columns[4].Width = 70;
            dataGridView.Columns[5].Width = 180;

            foreach (PeRelocationFixup fixupElem in relocEntryDir)
            {
                int rowIndex = dataGridView.Rows.Add(fixupElem.FixupValue.Name, fixupElem.FixupValue.OffsetString,
                        fixupElem.FixupValue.ValueString());
                dataGridView.Rows[rowIndex].Tag = fixupElem.FixupValue;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = relocEntryDir.ToString();

                dataGridView.Rows[rowIndex].Cells[3].Value = StringUtil.GetFormattedHexString(fixupElem.GetRva());
                dataGridView.Rows[rowIndex].Cells[4].Value = StringUtil.GetFormattedHexString(fixupElem.GetOffset());
                dataGridView.Rows[rowIndex].Cells[5].Value = fixupElem.GetRelocationTypeString();
            }
        }
    }
}
