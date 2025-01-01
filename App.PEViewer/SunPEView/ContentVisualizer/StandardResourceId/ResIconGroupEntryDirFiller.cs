using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat.Resources.StandardId;
using SunPEView.PEModel.PEFormat;
using System.Drawing;

namespace SunPEView.ContentVisualizer.StandardResourceId
{
    class ResIconGroupEntryDirFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeIconGroupDirEntry iconGroupDirEntry;

        public ResIconGroupEntryDirFiller(ContentDataGridView dataGridView, PeIconGroupDirEntry iconGroupDirEntry) : 
            base(dataGridView) 
        {
            this.iconGroupDirEntry = iconGroupDirEntry;
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

            foreach (IPeElem elem in iconGroupDirEntry)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                        elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = iconGroupDirEntry.GetDescription(elem.Name);
            }

            // add special meanings
            AddSpecialMeanings(peFile);
        }

        private void AddSpecialMeanings(PEFile peFile)
        {
            // width in decimal
            dataGridView.Rows[0].Cells[4].Value = "Width in pixels: " + iconGroupDirEntry.bWidth.Value;
            // height in decimal
            dataGridView.Rows[1].Cells[4].Value = "Height in pixels: " + iconGroupDirEntry.bHeight.Value;

            dataGridView.Rows.Add("[Placeholder]", "", "", "", "");
            dataGridView.Rows[8].Cells[4].Value = "Click here to go the icon resource";
            dataGridView.Rows[8].Cells[4].Style.BackColor = Color.Yellow;
            AddCellClickHandler(dataGridView_CellClick);
        }

        void dataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ignore header click
                return;
            }
            else if (e.RowIndex == 8)
            {
                PeIconResource iconRes = iconGroupDirEntry.GetResIcon();
                Form1 form1 = (Form1)dataGridView.Tag;
                form1.getPeTreeView().SelectNode(iconRes.getParentResourceDataEntry.ToString());
            }
        }
    }
}
