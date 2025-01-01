using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat.Resources;
using SunPEView.PEModel.PEFormat;

namespace SunPEView.ContentVisualizer
{
    class ResourceEntryFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeResourceDirectoryEntry resDirEntry;

        public ResourceEntryFiller(ContentDataGridView dataGridView, PeResourceDirectoryEntry resDirEntry) : base(dataGridView)
        {
            this.resDirEntry = resDirEntry;
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

            dataGridView.Columns[0].Width = 140;
            dataGridView.Columns[1].Width = 80;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 100;
            dataGridView.Columns[4].Width = 280;

            foreach (IPeElem elem in resDirEntry)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                     elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = resDirEntry.GetDescription(elem.Name);
            }

            // add special meanings
            AddSpecialMeanings(peFile);
        }

        private void AddSpecialMeanings(PEFile peFile)
        {
            if (resDirEntry.EntryType == PeResourceDirectoryEntry.ResourceDirectoryEntryType.ID)
            {
                // NameRvaId meaning
                uint id = resDirEntry.NameRvaId.Value;

                if (resDirEntry.LevelOnBranch == 1)
                {
                    if (PeResourceStandardIdentifier.IsStandardID(id))
                    {
                        dataGridView.Rows[0].Cells[4].Value = "ID: " + StringUtil.GetFormattedStringUint(id) +
                            "  ( " + PeResourceStandardIdentifier.Identifier[id] + " )";
                    }
                    else
                    {
                        dataGridView.Rows[0].Cells[4].Value = "ID: " + StringUtil.GetFormattedStringUint(id);
                    }
                }
                else
                {
                    dataGridView.Rows[0].Cells[4].Value = "ID: " + StringUtil.GetFormattedStringUint(id);
                }
            }
            else
            {
                // NAME type
                dataGridView.Rows[0].Cells[4].Value = "Name: " + resDirEntry.getEntryName();
            }

            // DataEntrySubdirRva meaning
            if (resDirEntry.IsLeaf())
            {
                uint dataEntryOffset = (resDirEntry.DataEntrySubdirRva.Value & PeResourceDirectoryEntry.IMAGE_RESOURCE_NAME_OFFSET_MASK);
                dataGridView.Rows[1].Cells[4].Value = "Pointer to Data Entry @ File Offset " + 
                    StringUtil.GetFormattedStringUint((uint)peFile.GetResourceFileOffset(resDirEntry.DataEntrySubdirRva.Value));
            }
            else
            {
                uint dataEntryRvaOffset = (resDirEntry.DataEntrySubdirRva.Value & PeResourceDirectoryEntry.IMAGE_RESOURCE_NAME_OFFSET_MASK);
                uint dataEntryFileOffset = (uint)peFile.GetResourceFileOffset(dataEntryRvaOffset);
                dataGridView.Rows[1].Cells[4].Value = "Pointer to Directory table @ File Offset " + StringUtil.GetFormattedStringUint(dataEntryFileOffset);
            }
        }
    }
}
