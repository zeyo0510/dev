using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.Enums;
using System.Drawing;
using SunPEView.ContentVisualizer.Windows;
using SunPEView.PEModel;

namespace SunPEView.ContentVisualizer
{
    class SectionTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public SectionTableFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 10;
            dataGridView.Columns[0].Name = "Name";
            dataGridView.Columns[1].Name = "Virtual Size";
            dataGridView.Columns[2].Name = "Virtual Address";
            dataGridView.Columns[3].Name = "Raw Size";
            dataGridView.Columns[4].Name = "Raw Address";
            dataGridView.Columns[5].Name = "Relocations Address";
            dataGridView.Columns[6].Name = "Line Numbers Address";
            dataGridView.Columns[7].Name = "#Relocations";
            dataGridView.Columns[8].Name = "#Line Numbers";
            dataGridView.Columns[9].Name = "Characteristics";

            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[1].Width = 70;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 70;
            dataGridView.Columns[4].Width = 70;
            dataGridView.Columns[5].Width = 70;
            dataGridView.Columns[6].Width = 70;
            dataGridView.Columns[7].Width = 80;
            dataGridView.Columns[8].Width = 70;
            dataGridView.Columns[9].Width = 90;

            foreach (PeSectionHeader section in peFile.PeSectionHeaderTable)
            {
                int rowIndex = dataGridView.Rows.Add(
                        section.Name.ValueString(),
                        section.VirtualSize.ValueString(),
                        section.VirtualAddress.ValueString(),
                        section.SizeOfRawData.ValueString(),
                        section.PointerToRawData.ValueString(),
                        section.PointerToRelocations.ValueString(),
                        section.PointerToLinenumbers.ValueString(),
                        section.NumberOfRelocations.ValueString(),
                        section.NumberOfLinenumbers.ValueString(),
                        section.Characteristics.ValueString());

                IPeElem sectionRegion = new PeElem<PeRegion>(section.PointerToRawData.Value,
                    new PeRegion(section.SizeOfRawData.Value), "Section " + section.Name);
                dataGridView.Rows[rowIndex].Tag = sectionRegion;
                //AddSpecialMeanings(section);

                // add clickable area for characteristics
                //ESectionCharacteristics characteristics = (ESectionCharacteristics)section.Characteristics.Value;
                dataGridView.Rows[rowIndex].Cells[9].Style.BackColor = Color.Yellow;
            }
            AddCellClickHandler(dataGridView_CellClick);
        }

        void dataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ignore header click
                return;
            }
            else if (e.ColumnIndex == 9)
            {
                int rowIndex = e.RowIndex;
                
                GenericEnumFlagsDisplayWindow g2 = new GenericEnumFlagsDisplayWindow
                    ("Caption", ESectionCharacteristics.IMAGE_SCN_ALIGN_1024BYTES, 
                    peFile.PeSectionHeaderTable[rowIndex].Characteristics.Value);
                g2.Width = 420;
                g2.Height = 750;
                g2.ShowDialog();
            }
        }


    }
}
