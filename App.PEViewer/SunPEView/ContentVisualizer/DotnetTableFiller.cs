using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using System.Drawing;
using SunPEView.ContentVisualizer.Windows;
using SunPEView.PEModel.PEFormat.Enums.Dotnet;

namespace SunPEView.ContentVisualizer
{
    class DotnetTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public DotnetTableFiller(ContentDataGridView dataGridView) : 
            base(dataGridView) 
        {
        }
        
        public void FillContent(PEFile peFile)
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

            foreach (IPeElem elem in peFile.PeDotNetDirectory)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                        elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeDotNetDirectory.GetDescription(elem.Name);
            }

            AddSpecialMeanings();
        }

        private void AddSpecialMeanings()
        {
            // add clickable area for flags
            dataGridView.Rows[5].Cells[4].Value = "Click here to mark data";
            dataGridView.Rows[5].Cells[4].Style.BackColor = Color.Yellow;

            AddCellClickHandler(dataGridView_CellClick);
        }

        void dataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ignore header click
                return;
            }
            else if (e.RowIndex == 5 && e.ColumnIndex == 4)
            {
                GenericEnumFlagsDisplayWindow gefdw = new GenericEnumFlagsDisplayWindow
                    ("COM Image Flags", EDotnetHeaderFlags.COMIMAGE_FLAGS_32BITREQUIRED,
                    peFile.PeDotNetDirectory.Flags.Value);
                gefdw.Width = 420;
                gefdw.Height = 200;
                gefdw.ShowDialog();
            }
        }
    }
}
