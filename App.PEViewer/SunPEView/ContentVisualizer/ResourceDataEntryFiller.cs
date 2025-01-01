using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat.Resources;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.Resources.StandardId;
using System.Drawing;
using System.Windows.Forms;
using SunPEView.ContentVisualizer.Windows;
using System.Globalization;


namespace SunPEView.ContentVisualizer
{
    /// <summary>
    /// Class to handle the visualization of resource data entries depending on the types.
    /// Currently supported:
    /// - Version Info 
    /// - Config Files
    /// - Icons & Icon groups
    /// - Cursors & cursor groups
    /// - String Tables
    /// - Dialogs
    /// - Menus
    /// - Bitmaps
    /// - RCData
    /// - Accelerators
    /// </summary>
    class ResourceDataEntryFiller : AbstractFiller, IContentFiller
    {
        private PEFile peFile;
        private PeResourceDataEntry resDataDirEntry;

        public ResourceDataEntryFiller(ContentDataGridView dataGridView, PeResourceDataEntry resDataDirEntry) : base(dataGridView)
        {
            this.resDataDirEntry = resDataDirEntry;
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
            dataGridView.Columns[4].Width = 250;

            foreach (IPeElem elem in resDataDirEntry)
            {
                int rowIndex = dataGridView.Rows.Add(elem.Name, elem.OffsetString,
                     elem.SizeInBytes(), elem.ValueString());
                dataGridView.Rows[rowIndex].Tag = elem;
                // add description as tooltip
                dataGridView.Rows[rowIndex].Cells[0].ToolTipText = resDataDirEntry.GetDescription(elem.Name);
            }
           // add special meanings
            AddSpecialMeanings(peFile);
        }

        private void AddSpecialMeanings(PEFile peFile)
        {
            // add file offset of data offset
            dataGridView.Rows[0].Cells[4].Value = "Pointer to Data @ File Offset " +
                StringUtil.GetFormattedStringUint((uint)peFile.FileLocationCalculator.GetFileOffsetFromRVA(resDataDirEntry.OffsetToData.Value));
            dataGridView.Rows[1].Cells[4].Value = "Size in bytes of resource data: " + resDataDirEntry.Size.Value;

            if (resDataDirEntry.IsTopLevelOfTypeID())
            {
                switch (resDataDirEntry.GetTopLevelEntryID())
                {
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_VERSIONINFO:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CONFIGFILES:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONS:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_STRINGTABLES:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_DIALOGS:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_MENUS:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_BITMAPS:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORS:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_HTMLPAGES:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_RCDATA:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ACCELERATORS:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_FONTDIRECORY:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_FONTS:
                    case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_MESSAGETABLES:
                    {
                        dataGridView.Rows.Add("[Placeholder]", "", "", "", "");
                        dataGridView.Rows[4].Cells[4].Value = "Click here to view data";
                        dataGridView.Rows[4].Cells[4].Style.BackColor = Color.Yellow;
                        AddCellClickHandler(dataGridView_CellClick);
                    }
                    break;

                    /* following standard resource do not have a special meaning directly */
                    //case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONGROUPS:
                    //case (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORGROUPS:

                    default: 
                        /* those standard IDs are not yet supported */
                        break;
                }
            }
            else
            {
                /* no standard resource, add handle to show hex bytes */
                dataGridView.Rows.Add("[Placeholder]", "", "", "", "");
                dataGridView.Rows[4].Cells[4].Value = "Click here to view data";
                dataGridView.Rows[4].Cells[4].Style.BackColor = Color.Yellow;
                AddCellClickHandler(dataGridView_CellClick);
            }
        }

        void dataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ignore header click
                return;
            }
            else if (e.RowIndex == 4)
            {
                if (resDataDirEntry.IsTopLevelOfTypeID())
                {
                    /* CONFIG FILES */
                    if (resDataDirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CONFIGFILES)
                    {
                        PeConfigFileResource configFileRes = (PeConfigFileResource)resDataDirEntry.GetStandardResource();
                        if (configFileRes != null)
                        {
                            GenericStringWindow gsw = new GenericStringWindow(configFileRes.fileContentStr.Value);
                            gsw.ShowDialog();
                        }
                    }
                    /* ICON */
                    else if (resDataDirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONS)
                    {
                        PeIconResource iconRes = (PeIconResource)resDataDirEntry.GetStandardResource();
                        if (iconRes != null)
                        {
                            Icon icon = iconRes.getIcon();
                            if (icon != null)
                            {
                                IconForm bmf = new IconForm(iconRes);
                                bmf.Text = "Icon (" + icon.Width + "," + icon.Height + ")";
                                bmf.ShowDialog();
                            }
                            else
                            {
                                PELogger.Instance.Log("Icon not supported.", PELogger.LoggingLevel.ERROR);
                            }
                        }
                    }
                    /* ICON GROUPS -> no special meaning*/
                    /* VERSION INFO */
                    else if (resDataDirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_VERSIONINFO)
                    {
                        PeVersionInfoResource versionInfoRes = (PeVersionInfoResource)resDataDirEntry.GetStandardResource();
                        if (versionInfoRes != null)
                        {
                            GenericStringWindow gsw = new GenericStringWindow(versionInfoRes.GetVersionInfoResourceAsString());
                            gsw.Text = "Version Info";
                            gsw.Font = new Font(FontFamily.GenericMonospace, 8);
                            gsw.Show();
                        }
                    }
                    /* STRING TABLE */
                    else if (resDataDirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_STRINGTABLES)
                    {
                        PeStringTableResource strTableRes = (PeStringTableResource)resDataDirEntry.GetStandardResource();
                        if (strTableRes != null)
                        {
                            GenericStringWindow gsw = new GenericStringWindow(strTableRes.GetStringTable());
                            gsw.Text = "String Table";
                            gsw.Font = new Font(FontFamily.GenericMonospace, 8);
                            gsw.Show();
                        }
                    }
                    /* BITMAPS */
                    else if (resDataDirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_BITMAPS)
                    {
                        PeBitmapResource bmpRes = (PeBitmapResource)resDataDirEntry.GetStandardResource();
                        if (bmpRes != null)
                        {
                            Bitmap bmp = bmpRes.GetBitmap();
                            if (bmp != null)
                            {
                                BitmapForm bmf = new BitmapForm(bmpRes);
                                bmf.Text = "Bitmap (" + bmp.Width + "," + bmp.Height + ")";
                                bmf.ShowDialog();
                            }
                            else
                            {
                                PELogger.Instance.Log("Bitmap not supported.", PELogger.LoggingLevel.ERROR);
                            }
                        }
                    }
                    /* CURSORS */
                    else if (resDataDirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORS)
                    {
                        PeCursorResource cursorRes = (PeCursorResource)resDataDirEntry.GetStandardResource();
                        if (cursorRes != null)
                        {
                            Cursor cursor = cursorRes.getCursor();
                            if (cursor != null)
                            {
                                CursorForm bmf = new CursorForm(cursorRes);
                                bmf.Text = "Cursor (" + cursorRes.CursorWidth + "," + cursorRes.CursorHeight + ")";
                                bmf.ShowDialog();
                            }
                            else
                            {
                                PELogger.Instance.Log("Icon not supported.", PELogger.LoggingLevel.ERROR);
                            }
                        }
                    }
                    /* CURSOR GROUPS -> no special meaning */
                    /* HTML PAGES */
                    else if (resDataDirEntry.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_HTMLPAGES)
                    {
                        PeHtmlPageResource htmlPageRes = (PeHtmlPageResource)resDataDirEntry.GetStandardResource();
                        if (htmlPageRes != null && htmlPageRes.HtmlPageStr != null)
                        {
                            HtmlPageWindow hpw = new HtmlPageWindow(htmlPageRes);
                            hpw.ShowDialog();
                        }
                        else
                        {
                            PELogger.Instance.Log("No HTML page was loaded.", PELogger.LoggingLevel.ERROR);
                        }
                    }
                    else
                    {
                        if (peFile.FileData.IsAddressRangeValid(
                            (uint)resDataDirEntry.GetFileOffsetToRawData(), resDataDirEntry.Size.Value))
                        {
                            GenericByteArrayWindow gbaw = new GenericByteArrayWindow(peFile.FileData,
                            (uint)resDataDirEntry.GetFileOffsetToRawData(), resDataDirEntry.Size.Value);
                            gbaw.Text = "Resource Data Entry(Offset: " +
                                    StringUtil.GetFormattedHexString(resDataDirEntry.GetFileOffsetToRawData()) +
                                    " , Size: " + StringUtil.GetFormattedHexString(resDataDirEntry.Size.Value) + ")";
                            gbaw.ShowDialog();
                        }
                        else
                        {
                            PELogger.Instance.Log("Offset and size are invalid", PELogger.LoggingLevel.ERROR);
                        }
                    }
                }
                
            }
        }

        
    }
}
