using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SunPEView.ContentVisualizer.Interfaces;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.ExportDirectoryTable;
using SunPEView.PEModel.PEFormat.Resources;
using SunPEView.PEModel.PEFormat.DebugDirectory;
using SunPEView.PEModel.PEFormat.Resources.StandardId;
using SunPEView.ContentVisualizer.StandardResourceId;

namespace SunPEView.ContentVisualizer
{
    class ContentDataGridView : DataGridView, IContentVisualizer
    {
        private MenuItem flcAsVaMenuItem;
        private MenuItem flcAsRVaMenuItem;
        private MenuItem flcAsFileOffMenuItem;

        private MenuItem hexBoxAsVaMenuItem;
        private MenuItem hexBoxAsRVaMenuItem;
        private MenuItem hexBoxAsFileOffMenuItem;

        public ContentDataGridView()
            : base()
        {           
            ReadOnly = true;
            MultiSelect = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            SelectionMode = DataGridViewSelectionMode.CellSelect;
            AllowUserToResizeRows = false;
            AllowUserToAddRows = false;

            /* build context menu */
            gridContextMenu = new ContextMenu();
            MenuItem copyMenuItem = new MenuItem("Copy");
            copyMenuItem.Click += new EventHandler(copyMenuItem_Click);
            gridContextMenu.MenuItems.Add(copyMenuItem);

            flcAsVaMenuItem = new MenuItem("Send to FLC as VA");
            flcAsVaMenuItem.Click += new EventHandler(sendToFlcMenuItem_Click);
            gridContextMenu.MenuItems.Add(flcAsVaMenuItem);
            flcAsRVaMenuItem = new MenuItem("Send to FLC as RVA");
            flcAsRVaMenuItem.Click += new EventHandler(sendToFlcMenuItem_Click);
            gridContextMenu.MenuItems.Add(flcAsRVaMenuItem);
            flcAsFileOffMenuItem = new MenuItem("Send to FLC as File Offset");
            flcAsFileOffMenuItem.Click += new EventHandler(sendToFlcMenuItem_Click);
            gridContextMenu.MenuItems.Add(flcAsFileOffMenuItem);

            hexBoxAsVaMenuItem = new MenuItem("Go in HexView as VA");
            hexBoxAsVaMenuItem.Click += new EventHandler(goToHexBoxMenuItem_Click);
            gridContextMenu.MenuItems.Add(hexBoxAsVaMenuItem);

            hexBoxAsRVaMenuItem = new MenuItem("Go in HexView as RVA");
            hexBoxAsRVaMenuItem.Click += new EventHandler(goToHexBoxMenuItem_Click);
            gridContextMenu.MenuItems.Add(hexBoxAsRVaMenuItem);

            hexBoxAsFileOffMenuItem = new MenuItem("Go in HexView as FileOffset");
            hexBoxAsFileOffMenuItem.Click += new EventHandler(goToHexBoxMenuItem_Click);
            gridContextMenu.MenuItems.Add(hexBoxAsFileOffMenuItem);
            

            // add handler to avoid selection of first column
            CellStateChanged += new DataGridViewCellStateChangedEventHandler(ContentDataGridView_CellStateChanged);

            CellContentClick += new DataGridViewCellEventHandler(ContentDataGridView_CellContentClick);

            MouseClick += new MouseEventHandler(ContentDataGridView_MouseClick);
        }


        void ContentDataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell.ColumnIndex == 0)
            {
                e.Cell.Selected = false;
            }

            //Console.Out.WriteLine(e.Cell.RowIndex + "  " + e.Cell.ColumnIndex);
        }

        private void InitDefaultContentLayout()
        {
            // init standard layout
            ColumnCount = 1;
            Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
        }

        void IContentVisualizer.ShowDosHeader(PEFile peFile)
        {
            InitDefaultContentLayout();
            DosHeaderFiller dhf = new DosHeaderFiller(this);
            dhf.FillContent(peFile);
        }

        void IContentVisualizer.ShowPeHeader(PEFile peFile)
        {
            InitDefaultContentLayout();
            PeHeaderFiller phf = new PeHeaderFiller(this);
            phf.FillContent(peFile);
        }

        void IContentVisualizer.ShowPeFileHeader(PEFile peFile)
        {
            InitDefaultContentLayout();
            PeFileHeaderFiller pfhf = new PeFileHeaderFiller(this);
            pfhf.FillContent(peFile);
        }

        void IContentVisualizer.ShowPeOptionalHeader(PEFile peFile)
        {
            InitDefaultContentLayout();
            PeOptionalHeaderFiller pfhf = new PeOptionalHeaderFiller(this);
            pfhf.FillContent(peFile);
        }

        void IContentVisualizer.ShowDataDirectoryTable(PEFile peFile)
        {
            InitDefaultContentLayout();
            DataDirectoryTableFiller ddtf = new DataDirectoryTableFiller(this);
            ddtf.FillContent(peFile);
        }

        void IContentVisualizer.ShowOverallSections(PEFile peFile)
        {
            InitDefaultContentLayout();
            SectionTableFiller stf = new SectionTableFiller(this);
            stf.FillContent(peFile);
        }

        void IContentVisualizer.ShowSection(PEFile peFile, PeSectionHeader section)
        {
            InitDefaultContentLayout();
            SectionFiller sf = new SectionFiller(this, section);
            sf.FillContent(peFile);
        }

        void IContentVisualizer.ShowImportTable(PEFile peFile)
        {
            InitDefaultContentLayout();
            ImportTableFiller itf = new ImportTableFiller(this);
            itf.FillContent(peFile);
        }

        void IContentVisualizer.ShowImport(PEFile peFile, PeImportDescriptor impDesc)
        {
            InitDefaultContentLayout();
            ImportFiller impFiller = new ImportFiller(this, impDesc);
            impFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowExportTable(PEFile peFile)
        {
            InitDefaultContentLayout();
            ExportTableFiller exDirFiller = new ExportTableFiller(this);
            exDirFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowExportedFunctionsTable(PEFile peFile)
        {
            InitDefaultContentLayout();
            ExportedFunctionsTableFiller exDirFiller = new ExportedFunctionsTableFiller(this);
            exDirFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowResourceTable(PEFile peFile, PeResourceDirectoryTable resDir)
        {
            InitDefaultContentLayout();
            ResourceTableFiller resDirFiller = new ResourceTableFiller(this, resDir);
            resDirFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowResourceEntry(PEFile peFile, PeResourceDirectoryEntry resDirEntry)
        {
            InitDefaultContentLayout();
            ResourceEntryFiller resDirEntryFiller = new ResourceEntryFiller(this, resDirEntry);
            resDirEntryFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowResourceDataEntry(PEFile peFile, PeResourceDataEntry resDataDirEntry)
        {
            InitDefaultContentLayout();
            ResourceDataEntryFiller resDataDirEntryFiller = new ResourceDataEntryFiller(this, resDataDirEntry);
            resDataDirEntryFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowDebugDirectoryTable(PEFile peFile, PeDebugDirectoryTable debugDirTable)
        {
            InitDefaultContentLayout();
            DebugTableFiller debugDataDirFiller = new DebugTableFiller(this, debugDirTable);
            debugDataDirFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowDebugDirectory(PEFile peFile, PeDebugDirectory debugDataDir)
        {
            InitDefaultContentLayout();
            DebugDirFiller debugDataDirFiller = new DebugDirFiller(this, debugDataDir);
            debugDataDirFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowRelocationEntry(PEFile pefile, PeRelocationEntry relocEntry)
        {
            InitDefaultContentLayout();
            RelocationEntryFiller relocEntryFiller = new RelocationEntryFiller(this, relocEntry);
            relocEntryFiller.FillContent(pefile);
        }

        void IContentVisualizer.ShowRelocationTable(PEFile peFile, PeRelocationTable relocTable)
        {
            InitDefaultContentLayout();
            RelocationTableFiller relocTableFiller = new RelocationTableFiller(this, relocTable);
            relocTableFiller.FillContent(peFile);
        }
        
        void IContentVisualizer.ShowExceptionTable(PEFile peFile)
        {
            InitDefaultContentLayout();
            ExceptionTableFiller excepTableFiller = new ExceptionTableFiller(this);
            excepTableFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowTLSTable(PEFile peFile)
        {
            InitDefaultContentLayout();
            TLSTableFiller tlsTableFiller = new TLSTableFiller(this);
            tlsTableFiller.FillContent(peFile);
        }

        void IContentVisualizer.ShowDotNetTable(PEFile peFile)
        {
            InitDefaultContentLayout();
            DotnetTableFiller dotnetTableFiller = new DotnetTableFiller(this);
            dotnetTableFiller.FillContent(peFile);
        }

        /******************************************************************************
         * Standard Resource Handling
         ******************************************************************************/
        public void ShowStdResIconGroupDirEntry(PEFile peFile, PeIconGroupDirEntry resIconGroupDir)
        {
            InitDefaultContentLayout();
            ResIconGroupEntryDirFiller iconDirFiller = new ResIconGroupEntryDirFiller(this, resIconGroupDir);
            iconDirFiller.FillContent(peFile);
        }

        public void ShowStdResGroupGroupDirEntry(PEFile peFile, PeCursorGroupDirEntry resCursorGroupDir)
        {
            InitDefaultContentLayout();
            ResCursorGroupEntryDirFiller cursorDirFiller = new ResCursorGroupEntryDirFiller(this, resCursorGroupDir);
            cursorDirFiller.FillContent(peFile);
        }


        /******************************************************************************
         * Add dynamically items
         ******************************************************************************/
        public void AddStdResIconGroupElements(PEFileTreeView.PEFileTreeViewControl peFileTreeView, PEFile peFile,
            PEFileTreeView.PESectionNode peSectionNode, PeIconGroupResource resIconGroup)
        {
            foreach (PeIconGroupDirEntry iconDir in resIconGroup)
            {
                peFileTreeView.AddFileSection(peFile, peSectionNode, iconDir.ToString());
            }
            peSectionNode.Expand();
        }

        public void AddStdResCursorGroupElements(PEFileTreeView.PEFileTreeViewControl peFileTreeView, PEFile peFile,
            PEFileTreeView.PESectionNode peSectionNode, PeCursorGroupResource resCursorGroup)
        {
            foreach (PeCursorGroupDirEntry cursorDir in resCursorGroup)
            {
                peFileTreeView.AddFileSection(peFile, peSectionNode, cursorDir.ToString());
            }
            peSectionNode.Expand();
        }

        /******************************************************************************
         * Selection changed event handling
         ******************************************************************************/
        /// <summary>
        /// Called when the selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ContentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // column header clicked, ignore event
                return;
            }
            if (Rows[e.RowIndex].Tag != null)
            {
                IPeElem elem = (IPeElem)Rows[e.RowIndex].Tag;
                SelectedRowChanged(this, new SectionRowChangeEvents(e, elem));
            }
        }

        public event SelectedRowChangedDelegate SelectedRowChanged;


        /******************************************************************************
         * Context Menu handling
         ******************************************************************************/
        private int currentContextMenuRow;
        private int currentContextMenuColumn;
        private ContextMenu gridContextMenu;

        void ContentDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HitTestInfo hitInfo = this.HitTest(e.X, e.Y);
                currentContextMenuRow = this.HitTest(e.X, e.Y).RowIndex;
                currentContextMenuColumn = this.HitTest(e.X, e.Y).ColumnIndex;

                gridContextMenu.Show(this, new Point(e.X, e.Y));
            }
        }


        void copyMenuItem_Click(object sender, EventArgs e)
        {
            if (currentContextMenuRow != -1 && currentContextMenuColumn != -1)
            {
                object cellContent = this.Rows[currentContextMenuRow].Cells[currentContextMenuColumn].Value;
                if (cellContent != null)
                {
                    Clipboard.SetText(cellContent.ToString());
                    //Console.Out.WriteLine("TEST:" + Clipboard.GetText());
                    currentContextMenuRow = currentContextMenuColumn = -1;
                }
            }
        }



        /******************************************************************************
         * Special Context Menu events
         ******************************************************************************/
        public delegate void ContextMenuFlcItemClickedDelegate(object sender, ContextMenuFlcClickedEventArgs e);
        public event ContextMenuFlcItemClickedDelegate ContextMenuSendToFlcItemClicked;
        public event ContextMenuFlcItemClickedDelegate ContextMenuGotoHexBoxFlcClicked;

        public class ContextMenuFlcClickedEventArgs : EventArgs
        {
            public enum FLCVALUETYPE { VA, RVA, FILEOFFSET }

            public string Value { get; internal set; }
            public FLCVALUETYPE Type { get; internal set; }

            public ContextMenuFlcClickedEventArgs(string value, FLCVALUETYPE type)
                : base()
            {
                this.Value = value;
                this.Type = type;
            }
        }

        void sendToFlcMenuItem_Click(object sender, EventArgs e)
        {
            if (ContextMenuSendToFlcItemClicked != null && currentContextMenuRow != -1 && currentContextMenuColumn != -1)
            {
                object cellContent = this.Rows[currentContextMenuRow].Cells[currentContextMenuColumn].Value;
                if (cellContent != null)
                {
                    if (sender.Equals(flcAsVaMenuItem))
                    {
                        ContextMenuSendToFlcItemClicked(sender, new ContextMenuFlcClickedEventArgs(cellContent.ToString(),
                            ContextMenuFlcClickedEventArgs.FLCVALUETYPE.VA));
                    } else if (sender.Equals(flcAsRVaMenuItem))
                    {
                        ContextMenuSendToFlcItemClicked(sender, new ContextMenuFlcClickedEventArgs(cellContent.ToString(),
                            ContextMenuFlcClickedEventArgs.FLCVALUETYPE.RVA));
                    } else if (sender.Equals(flcAsFileOffMenuItem))
                    {
                        ContextMenuSendToFlcItemClicked(sender, new ContextMenuFlcClickedEventArgs(cellContent.ToString(),
                            ContextMenuFlcClickedEventArgs.FLCVALUETYPE.FILEOFFSET));
                    } 
                }
            }
        }

        void goToHexBoxMenuItem_Click(object sender, EventArgs e)
        {
            if (ContextMenuGotoHexBoxFlcClicked != null && currentContextMenuRow != -1 && currentContextMenuColumn != -1)
            {
                object cellContent = this.Rows[currentContextMenuRow].Cells[currentContextMenuColumn].Value;
                if (cellContent != null)
                {
                    if (sender.Equals(hexBoxAsVaMenuItem))
                    {
                        ContextMenuGotoHexBoxFlcClicked(sender, new ContextMenuFlcClickedEventArgs(cellContent.ToString(),
                            ContextMenuFlcClickedEventArgs.FLCVALUETYPE.VA));
                    }
                    else if (sender.Equals(hexBoxAsRVaMenuItem))
                    {
                        ContextMenuGotoHexBoxFlcClicked(sender, new ContextMenuFlcClickedEventArgs(cellContent.ToString(),
                            ContextMenuFlcClickedEventArgs.FLCVALUETYPE.RVA));
                    }
                    else if (sender.Equals(hexBoxAsFileOffMenuItem))
                    {
                        ContextMenuGotoHexBoxFlcClicked(sender, new ContextMenuFlcClickedEventArgs(cellContent.ToString(),
                            ContextMenuFlcClickedEventArgs.FLCVALUETYPE.FILEOFFSET));
                    }
                }
            }
        }



    }
}
