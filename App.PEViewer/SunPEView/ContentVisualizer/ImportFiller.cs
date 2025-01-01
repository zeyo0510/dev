using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.ImportDirectoryTable;

namespace SunPEView.ContentVisualizer
{
    class ImportFiller : AbstractFiller, IContentFiller
    {
        private PeImportDescriptor importDesc;

        public ImportFiller(ContentDataGridView dataGridView, PeImportDescriptor importDesc)
            : base(dataGridView) 
        {
            this.importDesc = importDesc;
        }

        public void FillContent(PEModel.PEFile peFile)
        {
            dataGridView.ColumnCount = 6;
            dataGridView.Columns[0].Name = "Thunk RVA";
            dataGridView.Columns[1].Name = "Thunk File Offset";
            dataGridView.Columns[2].Name = "First Thunk RVA";
            dataGridView.Columns[3].Name = "Hint-Name File Offset";
            dataGridView.Columns[4].Name = "Hint";
            dataGridView.Columns[5].Name = "Name";

            dataGridView.Columns[0].Width = 110;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 110;
            dataGridView.Columns[3].Width = 80;
            dataGridView.Columns[4].Width = 50;
            dataGridView.Columns[5].Width = 220;

            int index = 0;
            int rowIndex;
            foreach (PeImageOriginalThunkData elem in importDesc.GetOriginalFirstThunk())
            {
                string thunkFileOffset =
                    peFile.isPe64() ?
                    StringUtil.GetFormattedStringUlong(elem.ThunkValue) :
                    StringUtil.GetFormattedStringUint((uint)elem.ThunkValue);


                if (elem.isImportByOrdinal())
                {
                    /* import by ordinal */
                    rowIndex = dataGridView.Rows.Add(
                        thunkFileOffset,
                        StringUtil.GetFormattedStringUint(elem.FileOffsetThunkData),
                        importDesc.getFirstThunkValue(index).FirstThunkRva.ValueString(),
                        "-",
                        elem.Hint.ValueString(),
                        "Ordinal: " + elem.Name.ValueString());

                    IPeElem importRegion = new PeElem<PeRegion>(elem.Hint.Offset,
                            new PeRegion(8), "Ordinal: " + elem.Name.Value);
                    dataGridView.Rows[rowIndex].Tag = importRegion;
                }
                else
                {
                    /* import by name */
                    rowIndex = dataGridView.Rows.Add(
                        thunkFileOffset,
                        StringUtil.GetFormattedStringUint(elem.FileOffsetThunkData),
                        importDesc.getFirstThunkValue(index).FirstThunkRva.ValueString(),
                        elem.Hint.OffsetString,
                        elem.Hint.ValueString(),
                        elem.Name.ValueString());

                    IPeElem importRegion = new PeElem<PeRegion>(elem.Hint.Offset,
                        new PeRegion(elem.ThunkLengthInBytes), "Function: " + elem.Name.Value);
                    dataGridView.Rows[rowIndex].Tag = importRegion;
                }

                index++;
                // add description as tooltip
                // dataGridView.Rows[rowIndex].Cells[0].ToolTipText = elem.GetDescription(elem.Name);
            }

            //AddSpecialMeanings();

        }
    }
}
