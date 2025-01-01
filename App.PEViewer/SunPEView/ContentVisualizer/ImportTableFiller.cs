using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;

namespace SunPEView.ContentVisualizer
{
    class ImportTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public ImportTableFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 7;
            dataGridView.Columns[0].Name = "Name";
            dataGridView.Columns[1].Name = "#Imports";
            dataGridView.Columns[2].Name = "Original FT";
            dataGridView.Columns[3].Name = "TimeDateStamp";
            dataGridView.Columns[4].Name = "ForwarderChain";
            dataGridView.Columns[5].Name = "Name RVA";
            dataGridView.Columns[6].Name = "FirstThunk";

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 90;
            dataGridView.Columns[3].Width = 90;
            dataGridView.Columns[4].Width = 90;
            dataGridView.Columns[5].Width = 85;
            dataGridView.Columns[6].Width = 70;


            foreach (PeImportDescriptor importDesc in peFile.PeImportDirectoryTable)
            {
                int rowIndex = dataGridView.Rows.Add(
                    importDesc.getDllName(),
                    StringUtil.GetFormattedHexString(importDesc.Count) + " (" + importDesc.Count + " dez)",
                    importDesc.OriginalFirstThunk.ValueString(),
                    importDesc.TimeDateStamp.ValueString(),
                    importDesc.ForwarderChain.ValueString(),
                    importDesc.NameRva.ValueString(),
                    importDesc.FirstThunk.ValueString());

                IPeElem importRegion = new PeElem<PeRegion>(importDesc.ImportStartFileOffset, 
                    new PeRegion(20), "Import " + importDesc.getDllName());
                dataGridView.Rows[rowIndex].Tag = importRegion;
            }
        }
    }
}
