using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.ExportDirectoryTable;

namespace SunPEView.ContentVisualizer
{
    class ExportedFunctionsTableFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;

        public ExportedFunctionsTableFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

        public void FillContent(PEModel.PEFile peFile)
        {
            this.peFile = peFile;

            dataGridView.ColumnCount = 5;
            dataGridView.Columns[0].Name = "FuncOrdinal";
            dataGridView.Columns[1].Name = "FileOrdinal";
            dataGridView.Columns[2].Name = "Function RVA";
            dataGridView.Columns[3].Name = "Name RVA";
            dataGridView.Columns[4].Name = "Name";

            dataGridView.Columns[0].Width = 80;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 100;
            dataGridView.Columns[3].Width = 100;
            dataGridView.Columns[4].Width = 180;

            foreach (PeExportFunction elem in (IEnumerable<PeExportFunction>)peFile.PeExportDirectoryTable)
            {
                int rowIndex = dataGridView.Rows.Add(
                    StringUtil.GetFormattedHexString((int)elem.FunctionOrdinal),
                    StringUtil.GetFormattedHexString((int)elem.FileOrdinal),
                    StringUtil.GetFormattedStringUint(elem.AddressValue),
                    StringUtil.GetFormattedStringUint(elem.NameRva), 
                    elem.Name);
                //dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = peFile.PeFileHeader.GetDescription(elem.Name);
            }
        }
    }
}
