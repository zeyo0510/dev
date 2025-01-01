namespace StartupEdit
{
    partial class FrmMainM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainM));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("All Enabled Entries", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("startupfolder");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("startupreg");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("MSConfig", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Run");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("RunOnce");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Current User", 9, 9, new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Run");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("RunOnce");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("RunOnceEx");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("RunServices");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("RunServicesOnce");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Local Machine", 10, 10, new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Registry", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("All Users");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Current User");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Startup Folder", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("All Disabled Entries", 8, 8);
            this.ViewMain = new System.Windows.Forms.ListView();
            this.AmoodName = new System.Windows.Forms.ColumnHeader();
            this.AmoodData = new System.Windows.Forms.ColumnHeader();
            this.AmoodRoot = new System.Windows.Forms.ColumnHeader();
            this.AmoodType = new System.Windows.Forms.ColumnHeader();
            this.AmoodStatus = new System.Windows.Forms.ColumnHeader();
            this.AmoodPath = new System.Windows.Forms.ColumnHeader();
            this.IsdalMain = new System.Windows.Forms.ContextMenu();
            this.IsdalMainView = new System.Windows.Forms.MenuItem();
            this.IsdalMainViewLarg = new System.Windows.Forms.MenuItem();
            this.IsdalMainViewSmall = new System.Windows.Forms.MenuItem();
            this.IsdalMainViewList = new System.Windows.Forms.MenuItem();
            this.IsdalMainViewDetails = new System.Windows.Forms.MenuItem();
            this.IsdalMainFasel1 = new System.Windows.Forms.MenuItem();
            this.IsdalMainEdit = new System.Windows.Forms.MenuItem();
            this.IsdalMainRemove = new System.Windows.Forms.MenuItem();
            this.IsdalMainFasel2 = new System.Windows.Forms.MenuItem();
            this.IsdalMainProp = new System.Windows.Forms.MenuItem();
            this.IsdalMainExplore = new System.Windows.Forms.MenuItem();
            this.IsdalMainExecute = new System.Windows.Forms.MenuItem();
            this.IsdalMainFasel3 = new System.Windows.Forms.MenuItem();
            this.IsdalMainEnable = new System.Windows.Forms.MenuItem();
            this.IsdalMainDisable = new System.Windows.Forms.MenuItem();
            this.IsdalMainFasel4 = new System.Windows.Forms.MenuItem();
            this.IsdalMainRefresh = new System.Windows.Forms.MenuItem();
            this.IsdalMainFasel5 = new System.Windows.Forms.MenuItem();
            this.IsdalMainAdd = new System.Windows.Forms.MenuItem();
            this.IsdalMainFasel6 = new System.Windows.Forms.MenuItem();
            this.IsdalMainAbout = new System.Windows.Forms.MenuItem();
            this.IsdalMainOptions = new System.Windows.Forms.MenuItem();
            this.IsdalMainFasel7 = new System.Windows.Forms.MenuItem();
            this.IsdalMainClose = new System.Windows.Forms.MenuItem();
            this.Main_SBar = new System.Windows.Forms.StatusBar();
            this.PanelScanning = new System.Windows.Forms.StatusBarPanel();
            this.PanelEntriesFound = new System.Windows.Forms.StatusBarPanel();
            this.PanelOSVersion = new System.Windows.Forms.StatusBarPanel();
            this.PanelOptions = new System.Windows.Forms.StatusBarPanel();
            this.TreeMain = new System.Windows.Forms.TreeView();
            this.IsdalTreeView = new System.Windows.Forms.ContextMenu();
            this.IsdalTreeExpandAll = new System.Windows.Forms.MenuItem();
            this.IsdalTreeCollapseAll = new System.Windows.Forms.MenuItem();
            this.IsdalTreeFasel1 = new System.Windows.Forms.MenuItem();
            this.ImageCont = new System.Windows.Forms.ImageList(this.components);
            this.Fasel1 = new System.Windows.Forms.Splitter();
            this.TheBar = new System.Windows.Forms.ToolBar();
            this.BarFile = new System.Windows.Forms.ToolBarButton();
            this.IsdalFile = new System.Windows.Forms.ContextMenu();
            this.IsdalFileAdd = new System.Windows.Forms.MenuItem();
            this.IsdalFileFasel1 = new System.Windows.Forms.MenuItem();
            this.IsdalFileOptions = new System.Windows.Forms.MenuItem();
            this.IsdalFileFasel2 = new System.Windows.Forms.MenuItem();
            this.IsdalFileExit = new System.Windows.Forms.MenuItem();
            this.BarSep1 = new System.Windows.Forms.ToolBarButton();
            this.BarEdit = new System.Windows.Forms.ToolBarButton();
            this.IsdalEdit = new System.Windows.Forms.ContextMenu();
            this.IsdalEditDisable = new System.Windows.Forms.MenuItem();
            this.IsdalEditDelete = new System.Windows.Forms.MenuItem();
            this.IsdalEditFasel1 = new System.Windows.Forms.MenuItem();
            this.IsdalEditEnable = new System.Windows.Forms.MenuItem();
            this.IsdalEditFasel2 = new System.Windows.Forms.MenuItem();
            this.IsdalEditEdit = new System.Windows.Forms.MenuItem();
            this.BarSep2 = new System.Windows.Forms.ToolBarButton();
            this.BarView = new System.Windows.Forms.ToolBarButton();
            this.IsdalView = new System.Windows.Forms.ContextMenu();
            this.IsdalViewLarg = new System.Windows.Forms.MenuItem();
            this.IsdalViewSmall = new System.Windows.Forms.MenuItem();
            this.IsdalViewList = new System.Windows.Forms.MenuItem();
            this.IsdalViewDetails = new System.Windows.Forms.MenuItem();
            this.IsdalViewFasel1 = new System.Windows.Forms.MenuItem();
            this.IsdalViewRefresh = new System.Windows.Forms.MenuItem();
            this.BarSep3 = new System.Windows.Forms.ToolBarButton();
            this.BarTools = new System.Windows.Forms.ToolBarButton();
            this.IsdalTools = new System.Windows.Forms.ContextMenu();
            this.IsdalToolsExec = new System.Windows.Forms.MenuItem();
            this.IsdalToolsExplore = new System.Windows.Forms.MenuItem();
            this.IsdalToolsFasel1 = new System.Windows.Forms.MenuItem();
            this.IsdalToolsProp = new System.Windows.Forms.MenuItem();
            this.IsdalToolsFasel2 = new System.Windows.Forms.MenuItem();
            this.IsdalToolsFileViewer = new System.Windows.Forms.MenuItem();
            this.IsdalToolsTurnOff = new System.Windows.Forms.MenuItem();
            this.BarSep4 = new System.Windows.Forms.ToolBarButton();
            this.BarHelp = new System.Windows.Forms.ToolBarButton();
            this.IsdalHelp = new System.Windows.Forms.ContextMenu();
            this.IsdalHelpAbout = new System.Windows.Forms.MenuItem();
            this.BarSep5 = new System.Windows.Forms.ToolBarButton();
            ((System.ComponentModel.ISupportInitialize)(this.PanelScanning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelEntriesFound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelOSVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewMain
            // 
            this.ViewMain.AutoArrange = false;
            this.ViewMain.BackColor = System.Drawing.Color.LightGray;
            this.ViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AmoodName,
            this.AmoodData,
            this.AmoodRoot,
            this.AmoodType,
            this.AmoodStatus,
            this.AmoodPath});
            this.ViewMain.ContextMenu = this.IsdalMain;
            this.ViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewMain.FullRowSelect = true;
            this.ViewMain.GridLines = true;
            this.ViewMain.Location = new System.Drawing.Point(163, 52);
            this.ViewMain.MultiSelect = false;
            this.ViewMain.Name = "ViewMain";
            this.ViewMain.Size = new System.Drawing.Size(671, 323);
            this.ViewMain.TabIndex = 1;
            this.ViewMain.UseCompatibleStateImageBehavior = false;
            this.ViewMain.View = System.Windows.Forms.View.Details;
            this.ViewMain.SelectedIndexChanged += new System.EventHandler(this.ViewMain_SelectedIndexChanged);
            this.ViewMain.DoubleClick += new System.EventHandler(this.ViewMain_DoubleClick);
            this.ViewMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ViewMain_ColumnClick);
            this.ViewMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewMain_KeyDown);
            // 
            // AmoodName
            // 
            this.AmoodName.Text = "Entry Name";
            this.AmoodName.Width = 140;
            // 
            // AmoodData
            // 
            this.AmoodData.Text = "Entry Data (Command)";
            this.AmoodData.Width = 206;
            // 
            // AmoodRoot
            // 
            this.AmoodRoot.Text = "Root";
            // 
            // AmoodType
            // 
            this.AmoodType.Text = "Type";
            // 
            // AmoodStatus
            // 
            this.AmoodStatus.Text = "Status";
            // 
            // AmoodPath
            // 
            this.AmoodPath.Text = "Full Path";
            // 
            // IsdalMain
            // 
            this.IsdalMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalMainView,
            this.IsdalMainFasel1,
            this.IsdalMainEdit,
            this.IsdalMainRemove,
            this.IsdalMainFasel2,
            this.IsdalMainProp,
            this.IsdalMainExplore,
            this.IsdalMainExecute,
            this.IsdalMainFasel3,
            this.IsdalMainEnable,
            this.IsdalMainDisable,
            this.IsdalMainFasel4,
            this.IsdalMainRefresh,
            this.IsdalMainFasel5,
            this.IsdalMainAdd,
            this.IsdalMainFasel6,
            this.IsdalMainAbout,
            this.IsdalMainOptions,
            this.IsdalMainFasel7,
            this.IsdalMainClose});
            this.IsdalMain.Popup += new System.EventHandler(this.IsdalMain_Popup);
            // 
            // IsdalMainView
            // 
            this.IsdalMainView.Index = 0;
            this.IsdalMainView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalMainViewLarg,
            this.IsdalMainViewSmall,
            this.IsdalMainViewList,
            this.IsdalMainViewDetails});
            this.IsdalMainView.Shortcut = System.Windows.Forms.Shortcut.Ins;
            this.IsdalMainView.Text = "&View";
            // 
            // IsdalMainViewLarg
            // 
            this.IsdalMainViewLarg.Index = 0;
            this.IsdalMainViewLarg.Text = "&Larg Icons";
            this.IsdalMainViewLarg.Click += new System.EventHandler(this.IsdalMainLarge_Click);
            // 
            // IsdalMainViewSmall
            // 
            this.IsdalMainViewSmall.Index = 1;
            this.IsdalMainViewSmall.Text = "Small Icons";
            this.IsdalMainViewSmall.Click += new System.EventHandler(this.IsdalMainSmall_Click);
            // 
            // IsdalMainViewList
            // 
            this.IsdalMainViewList.Index = 2;
            this.IsdalMainViewList.Text = "L&ist";
            this.IsdalMainViewList.Click += new System.EventHandler(this.IsdalMainList_Click);
            // 
            // IsdalMainViewDetails
            // 
            this.IsdalMainViewDetails.Checked = true;
            this.IsdalMainViewDetails.Index = 3;
            this.IsdalMainViewDetails.Text = "&Details";
            this.IsdalMainViewDetails.Click += new System.EventHandler(this.IsdalMainDetails_Click);
            // 
            // IsdalMainFasel1
            // 
            this.IsdalMainFasel1.Index = 1;
            this.IsdalMainFasel1.Text = "-";
            // 
            // IsdalMainEdit
            // 
            this.IsdalMainEdit.Index = 2;
            this.IsdalMainEdit.Text = "&Edit Entry";
            this.IsdalMainEdit.Click += new System.EventHandler(this.IsdalMainEdit_Click);
            // 
            // IsdalMainRemove
            // 
            this.IsdalMainRemove.Index = 3;
            this.IsdalMainRemove.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.IsdalMainRemove.Text = "&Remove Entry";
            this.IsdalMainRemove.Click += new System.EventHandler(this.IsdalMainRemove_Click);
            // 
            // IsdalMainFasel2
            // 
            this.IsdalMainFasel2.Index = 4;
            this.IsdalMainFasel2.Text = "-";
            // 
            // IsdalMainProp
            // 
            this.IsdalMainProp.DefaultItem = true;
            this.IsdalMainProp.Index = 5;
            this.IsdalMainProp.Text = "&Entry\'s Properties";
            this.IsdalMainProp.Click += new System.EventHandler(this.IsdalMainProp_Click);
            // 
            // IsdalMainExplore
            // 
            this.IsdalMainExplore.Index = 6;
            this.IsdalMainExplore.Text = "E&xplore Entry\'s Folder";
            this.IsdalMainExplore.Click += new System.EventHandler(this.IsdalMainExplore_Click);
            // 
            // IsdalMainExecute
            // 
            this.IsdalMainExecute.Index = 7;
            this.IsdalMainExecute.Text = "Execu&te Entry";
            this.IsdalMainExecute.Click += new System.EventHandler(this.IsdalMainExecute_Click);
            // 
            // IsdalMainFasel3
            // 
            this.IsdalMainFasel3.Index = 8;
            this.IsdalMainFasel3.Text = "-";
            // 
            // IsdalMainEnable
            // 
            this.IsdalMainEnable.Index = 9;
            this.IsdalMainEnable.Text = "Ena&ble Entry";
            this.IsdalMainEnable.Click += new System.EventHandler(this.IsdalMainEnable_Click);
            // 
            // IsdalMainDisable
            // 
            this.IsdalMainDisable.Index = 10;
            this.IsdalMainDisable.Text = "Disab&le Entry";
            this.IsdalMainDisable.Click += new System.EventHandler(this.IsdalMainDisable_Click);
            // 
            // IsdalMainFasel4
            // 
            this.IsdalMainFasel4.Index = 11;
            this.IsdalMainFasel4.Text = "-";
            // 
            // IsdalMainRefresh
            // 
            this.IsdalMainRefresh.Index = 12;
            this.IsdalMainRefresh.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.IsdalMainRefresh.Text = "&Refresh";
            this.IsdalMainRefresh.Click += new System.EventHandler(this.IsdalMainRefresh_Click);
            // 
            // IsdalMainFasel5
            // 
            this.IsdalMainFasel5.Index = 13;
            this.IsdalMainFasel5.Text = "-";
            // 
            // IsdalMainAdd
            // 
            this.IsdalMainAdd.Index = 14;
            this.IsdalMainAdd.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.IsdalMainAdd.Text = "&Add Entry";
            this.IsdalMainAdd.Click += new System.EventHandler(this.IsdalMainAdd_Click);
            // 
            // IsdalMainFasel6
            // 
            this.IsdalMainFasel6.Index = 15;
            this.IsdalMainFasel6.Text = "-";
            // 
            // IsdalMainAbout
            // 
            this.IsdalMainAbout.Index = 16;
            this.IsdalMainAbout.Text = "A&bout";
            this.IsdalMainAbout.Click += new System.EventHandler(this.IsdalMainAbout_Click);
            // 
            // IsdalMainOptions
            // 
            this.IsdalMainOptions.Index = 17;
            this.IsdalMainOptions.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.IsdalMainOptions.Text = "&Options";
            this.IsdalMainOptions.Click += new System.EventHandler(this.IsdalMainOptions_Click);
            // 
            // IsdalMainFasel7
            // 
            this.IsdalMainFasel7.Index = 18;
            this.IsdalMainFasel7.Text = "-";
            // 
            // IsdalMainClose
            // 
            this.IsdalMainClose.Index = 19;
            this.IsdalMainClose.Text = "&Close";
            this.IsdalMainClose.Click += new System.EventHandler(this.IsdalMainClose_Click);
            // 
            // Main_SBar
            // 
            this.Main_SBar.Location = new System.Drawing.Point(0, 375);
            this.Main_SBar.Name = "Main_SBar";
            this.Main_SBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.PanelScanning,
            this.PanelEntriesFound,
            this.PanelOSVersion,
            this.PanelOptions});
            this.Main_SBar.ShowPanels = true;
            this.Main_SBar.Size = new System.Drawing.Size(834, 22);
            this.Main_SBar.TabIndex = 3;
            this.Main_SBar.Text = "Main_SBar";
            this.Main_SBar.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.Main_SBar_PanelClick);
            // 
            // PanelScanning
            // 
            this.PanelScanning.Name = "PanelScanning";
            this.PanelScanning.Text = "Please Wait ! Currently Scanning..........";
            this.PanelScanning.ToolTipText = "Double Click to refresh the view or press \"F5\"";
            this.PanelScanning.Width = 235;
            // 
            // PanelEntriesFound
            // 
            this.PanelEntriesFound.Name = "PanelEntriesFound";
            this.PanelEntriesFound.Text = "Total Entries Found \"1234\"";
            this.PanelEntriesFound.Width = 150;
            // 
            // PanelOSVersion
            // 
            this.PanelOSVersion.Name = "PanelOSVersion";
            this.PanelOSVersion.Text = "Microsoft Windows Nt 5.12.12 Profile Loaded ret";
            this.PanelOSVersion.Width = 300;
            // 
            // PanelOptions
            // 
            this.PanelOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("PanelOptions.Icon")));
            this.PanelOptions.Name = "PanelOptions";
            this.PanelOptions.Text = "Program Options";
            this.PanelOptions.ToolTipText = "Double click to set program options or press \"F2\"";
            this.PanelOptions.Width = 120;
            // 
            // TreeMain
            // 
            this.TreeMain.BackColor = System.Drawing.Color.LightGray;
            this.TreeMain.ContextMenu = this.IsdalTreeView;
            this.TreeMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.TreeMain.FullRowSelect = true;
            this.TreeMain.HideSelection = false;
            this.TreeMain.ImageIndex = 0;
            this.TreeMain.ImageList = this.ImageCont;
            this.TreeMain.Indent = 20;
            this.TreeMain.ItemHeight = 26;
            this.TreeMain.LineColor = System.Drawing.Color.Peru;
            this.TreeMain.Location = new System.Drawing.Point(0, 52);
            this.TreeMain.Name = "TreeMain";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "All Enabled Entries";
            treeNode2.Name = "";
            treeNode2.Text = "startupfolder";
            treeNode3.Name = "";
            treeNode3.Text = "startupreg";
            treeNode4.Name = "";
            treeNode4.Text = "MSConfig";
            treeNode5.Name = "";
            treeNode5.Text = "Run";
            treeNode6.Name = "";
            treeNode6.Text = "RunOnce";
            treeNode7.ImageIndex = 9;
            treeNode7.Name = "";
            treeNode7.SelectedImageIndex = 9;
            treeNode7.Text = "Current User";
            treeNode8.Name = "";
            treeNode8.Text = "Run";
            treeNode9.Name = "";
            treeNode9.Text = "RunOnce";
            treeNode10.Name = "";
            treeNode10.Text = "RunOnceEx";
            treeNode11.Name = "";
            treeNode11.Text = "RunServices";
            treeNode12.Name = "";
            treeNode12.Text = "RunServicesOnce";
            treeNode13.ImageIndex = 10;
            treeNode13.Name = "";
            treeNode13.SelectedImageIndex = 10;
            treeNode13.Text = "Local Machine";
            treeNode14.ImageIndex = 1;
            treeNode14.Name = "";
            treeNode14.SelectedImageIndex = 1;
            treeNode14.Text = "Registry";
            treeNode15.Name = "";
            treeNode15.Text = "All Users";
            treeNode16.Name = "";
            treeNode16.Text = "Current User";
            treeNode17.Name = "";
            treeNode17.Text = "Startup Folder";
            treeNode18.ImageIndex = 8;
            treeNode18.Name = "";
            treeNode18.SelectedImageIndex = 8;
            treeNode18.Text = "All Disabled Entries";
            this.TreeMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode14,
            treeNode17,
            treeNode18});
            this.TreeMain.SelectedImageIndex = 7;
            this.TreeMain.Size = new System.Drawing.Size(160, 323);
            this.TreeMain.TabIndex = 4;
            this.TreeMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeMain_AfterSelect);
            // 
            // IsdalTreeView
            // 
            this.IsdalTreeView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalTreeExpandAll,
            this.IsdalTreeCollapseAll,
            this.IsdalTreeFasel1});
            // 
            // IsdalTreeExpandAll
            // 
            this.IsdalTreeExpandAll.Index = 0;
            this.IsdalTreeExpandAll.Text = "&Expand All";
            this.IsdalTreeExpandAll.Click += new System.EventHandler(this.IsdalTreeExpandAll_Click);
            // 
            // IsdalTreeCollapseAll
            // 
            this.IsdalTreeCollapseAll.Index = 1;
            this.IsdalTreeCollapseAll.Text = "&Collapse All";
            this.IsdalTreeCollapseAll.Click += new System.EventHandler(this.IsdalTreeCollapseAll_Click);
            // 
            // IsdalTreeFasel1
            // 
            this.IsdalTreeFasel1.Index = 2;
            this.IsdalTreeFasel1.Text = "-";
            // 
            // ImageCont
            // 
            this.ImageCont.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageCont.ImageStream")));
            this.ImageCont.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageCont.Images.SetKeyName(0, "");
            this.ImageCont.Images.SetKeyName(1, "");
            this.ImageCont.Images.SetKeyName(2, "");
            this.ImageCont.Images.SetKeyName(3, "");
            this.ImageCont.Images.SetKeyName(4, "");
            this.ImageCont.Images.SetKeyName(5, "");
            this.ImageCont.Images.SetKeyName(6, "");
            this.ImageCont.Images.SetKeyName(7, "");
            this.ImageCont.Images.SetKeyName(8, "");
            this.ImageCont.Images.SetKeyName(9, "");
            this.ImageCont.Images.SetKeyName(10, "");
            // 
            // Fasel1
            // 
            this.Fasel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Fasel1.Location = new System.Drawing.Point(160, 52);
            this.Fasel1.Name = "Fasel1";
            this.Fasel1.Size = new System.Drawing.Size(3, 323);
            this.Fasel1.TabIndex = 5;
            this.Fasel1.TabStop = false;
            // 
            // TheBar
            // 
            this.TheBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.TheBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TheBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.BarFile,
            this.BarSep1,
            this.BarEdit,
            this.BarSep2,
            this.BarView,
            this.BarSep3,
            this.BarTools,
            this.BarSep4,
            this.BarHelp,
            this.BarSep5});
            this.TheBar.DropDownArrows = true;
            this.TheBar.ImageList = this.ImageCont;
            this.TheBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TheBar.Location = new System.Drawing.Point(0, 0);
            this.TheBar.Name = "TheBar";
            this.TheBar.ShowToolTips = true;
            this.TheBar.Size = new System.Drawing.Size(834, 52);
            this.TheBar.TabIndex = 6;
            this.TheBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.TheBar_ButtonClick);
            // 
            // BarFile
            // 
            this.BarFile.DropDownMenu = this.IsdalFile;
            this.BarFile.ImageIndex = 7;
            this.BarFile.Name = "BarFile";
            this.BarFile.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.BarFile.Text = "&File";
            // 
            // IsdalFile
            // 
            this.IsdalFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalFileAdd,
            this.IsdalFileFasel1,
            this.IsdalFileOptions,
            this.IsdalFileFasel2,
            this.IsdalFileExit});
            this.IsdalFile.Popup += new System.EventHandler(this.IsdalFile_Popup);
            // 
            // IsdalFileAdd
            // 
            this.IsdalFileAdd.Index = 0;
            this.IsdalFileAdd.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.IsdalFileAdd.Text = "&Add New Entry";
            this.IsdalFileAdd.Click += new System.EventHandler(this.IsdalFileAdd_Click);
            // 
            // IsdalFileFasel1
            // 
            this.IsdalFileFasel1.Index = 1;
            this.IsdalFileFasel1.Text = "-";
            // 
            // IsdalFileOptions
            // 
            this.IsdalFileOptions.Index = 2;
            this.IsdalFileOptions.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.IsdalFileOptions.Text = "&Options";
            this.IsdalFileOptions.Click += new System.EventHandler(this.IsdalFileOptions_Click);
            // 
            // IsdalFileFasel2
            // 
            this.IsdalFileFasel2.Index = 3;
            this.IsdalFileFasel2.Text = "-";
            // 
            // IsdalFileExit
            // 
            this.IsdalFileExit.Index = 4;
            this.IsdalFileExit.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.IsdalFileExit.Text = "E&xit";
            this.IsdalFileExit.Click += new System.EventHandler(this.IsdalFileExit_Click);
            // 
            // BarSep1
            // 
            this.BarSep1.Name = "BarSep1";
            this.BarSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // BarEdit
            // 
            this.BarEdit.DropDownMenu = this.IsdalEdit;
            this.BarEdit.ImageIndex = 0;
            this.BarEdit.Name = "BarEdit";
            this.BarEdit.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.BarEdit.Text = "&Edit";
            this.BarEdit.ToolTipText = "Way Of View";
            // 
            // IsdalEdit
            // 
            this.IsdalEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalEditDisable,
            this.IsdalEditDelete,
            this.IsdalEditFasel1,
            this.IsdalEditEnable,
            this.IsdalEditFasel2,
            this.IsdalEditEdit});
            this.IsdalEdit.Popup += new System.EventHandler(this.IsdalEdit_Popup);
            // 
            // IsdalEditDisable
            // 
            this.IsdalEditDisable.Index = 0;
            this.IsdalEditDisable.Text = "D&isable Entry";
            this.IsdalEditDisable.Click += new System.EventHandler(this.IsdalEditDisable_Click);
            // 
            // IsdalEditDelete
            // 
            this.IsdalEditDelete.Index = 1;
            this.IsdalEditDelete.Text = "&Remove Entry";
            this.IsdalEditDelete.Click += new System.EventHandler(this.IsdalEditDelete_Click);
            // 
            // IsdalEditFasel1
            // 
            this.IsdalEditFasel1.Index = 2;
            this.IsdalEditFasel1.Text = "-";
            // 
            // IsdalEditEnable
            // 
            this.IsdalEditEnable.Index = 3;
            this.IsdalEditEnable.Text = "E&nable Entry";
            this.IsdalEditEnable.Click += new System.EventHandler(this.IsdalEditEnable_Click);
            // 
            // IsdalEditFasel2
            // 
            this.IsdalEditFasel2.Index = 4;
            this.IsdalEditFasel2.Text = "-";
            // 
            // IsdalEditEdit
            // 
            this.IsdalEditEdit.Index = 5;
            this.IsdalEditEdit.Text = "&Edit Entry";
            this.IsdalEditEdit.Click += new System.EventHandler(this.IsdalEditEdit_Click);
            // 
            // BarSep2
            // 
            this.BarSep2.Name = "BarSep2";
            this.BarSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // BarView
            // 
            this.BarView.DropDownMenu = this.IsdalView;
            this.BarView.ImageIndex = 6;
            this.BarView.Name = "BarView";
            this.BarView.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.BarView.Text = "&View";
            // 
            // IsdalView
            // 
            this.IsdalView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalViewLarg,
            this.IsdalViewSmall,
            this.IsdalViewList,
            this.IsdalViewDetails,
            this.IsdalViewFasel1,
            this.IsdalViewRefresh});
            this.IsdalView.Popup += new System.EventHandler(this.IsdalView_Popup);
            // 
            // IsdalViewLarg
            // 
            this.IsdalViewLarg.Index = 0;
            this.IsdalViewLarg.Text = "Larg Icons";
            this.IsdalViewLarg.Click += new System.EventHandler(this.IsdalViewLarg_Click);
            // 
            // IsdalViewSmall
            // 
            this.IsdalViewSmall.Index = 1;
            this.IsdalViewSmall.Text = "Small Icons";
            this.IsdalViewSmall.Click += new System.EventHandler(this.IsdalViewSmall_Click);
            // 
            // IsdalViewList
            // 
            this.IsdalViewList.Index = 2;
            this.IsdalViewList.Text = "List";
            this.IsdalViewList.Click += new System.EventHandler(this.IsdalViewList_Click);
            // 
            // IsdalViewDetails
            // 
            this.IsdalViewDetails.Checked = true;
            this.IsdalViewDetails.Index = 3;
            this.IsdalViewDetails.Text = "Details";
            this.IsdalViewDetails.Click += new System.EventHandler(this.IsdalViewDitails_Click);
            // 
            // IsdalViewFasel1
            // 
            this.IsdalViewFasel1.Index = 4;
            this.IsdalViewFasel1.Text = "-";
            // 
            // IsdalViewRefresh
            // 
            this.IsdalViewRefresh.Index = 5;
            this.IsdalViewRefresh.Text = "&Refresh";
            this.IsdalViewRefresh.Click += new System.EventHandler(this.IsdalViewRefresh_Click);
            // 
            // BarSep3
            // 
            this.BarSep3.Name = "BarSep3";
            this.BarSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // BarTools
            // 
            this.BarTools.DropDownMenu = this.IsdalTools;
            this.BarTools.ImageIndex = 3;
            this.BarTools.Name = "BarTools";
            this.BarTools.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.BarTools.Text = "&Tools";
            // 
            // IsdalTools
            // 
            this.IsdalTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalToolsExec,
            this.IsdalToolsExplore,
            this.IsdalToolsFasel1,
            this.IsdalToolsProp,
            this.IsdalToolsFasel2,
            this.IsdalToolsFileViewer,
            this.IsdalToolsTurnOff});
            this.IsdalTools.Popup += new System.EventHandler(this.IsdalTools_Popup);
            // 
            // IsdalToolsExec
            // 
            this.IsdalToolsExec.Index = 0;
            this.IsdalToolsExec.Text = "&Execute Entry";
            this.IsdalToolsExec.Click += new System.EventHandler(this.IsdalToolsExec_Click);
            // 
            // IsdalToolsExplore
            // 
            this.IsdalToolsExplore.Index = 1;
            this.IsdalToolsExplore.Text = "Explo&re Entry\'s Folder";
            this.IsdalToolsExplore.Click += new System.EventHandler(this.IsdalToolsExplore_Click);
            // 
            // IsdalToolsFasel1
            // 
            this.IsdalToolsFasel1.Index = 2;
            this.IsdalToolsFasel1.Text = "-";
            // 
            // IsdalToolsProp
            // 
            this.IsdalToolsProp.Index = 3;
            this.IsdalToolsProp.Text = "Entry\'s &Properties";
            this.IsdalToolsProp.Click += new System.EventHandler(this.IsdalToolsProp_Click);
            // 
            // IsdalToolsFasel2
            // 
            this.IsdalToolsFasel2.Index = 4;
            this.IsdalToolsFasel2.Text = "-";
            // 
            // IsdalToolsFileViewer
            // 
            this.IsdalToolsFileViewer.Index = 5;
            this.IsdalToolsFileViewer.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.IsdalToolsFileViewer.Text = "&File Viewer";
            this.IsdalToolsFileViewer.Click += new System.EventHandler(this.IsdalToolsFileEditor_Click);
            // 
            // IsdalToolsTurnOff
            // 
            this.IsdalToolsTurnOff.Index = 6;
            this.IsdalToolsTurnOff.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.IsdalToolsTurnOff.Text = "T&urn Off Computer";
            this.IsdalToolsTurnOff.Click += new System.EventHandler(this.IsdalToolsTurnOff_Click);
            // 
            // BarSep4
            // 
            this.BarSep4.Name = "BarSep4";
            this.BarSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // BarHelp
            // 
            this.BarHelp.DropDownMenu = this.IsdalHelp;
            this.BarHelp.ImageIndex = 4;
            this.BarHelp.Name = "BarHelp";
            this.BarHelp.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.BarHelp.Text = "&Help";
            // 
            // IsdalHelp
            // 
            this.IsdalHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.IsdalHelpAbout});
            this.IsdalHelp.Popup += new System.EventHandler(this.IsdalHelp_Popup);
            // 
            // IsdalHelpAbout
            // 
            this.IsdalHelpAbout.Index = 0;
            this.IsdalHelpAbout.Text = "&About";
            this.IsdalHelpAbout.Click += new System.EventHandler(this.IsdalHelpAbout_Click);
            // 
            // BarSep5
            // 
            this.BarSep5.Name = "BarSep5";
            this.BarSep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // FrmMainM
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(834, 397);
            this.Controls.Add(this.ViewMain);
            this.Controls.Add(this.Fasel1);
            this.Controls.Add(this.TreeMain);
            this.Controls.Add(this.TheBar);
            this.Controls.Add(this.Main_SBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.KeyPreview = true;
            this.Name = "FrmMainM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartupEdit Ahmad Shaban";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.PanelScanning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelEntriesFound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelOSVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelOptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ViewMain;
        private System.Windows.Forms.TreeView TreeMain;
        internal System.Windows.Forms.ContextMenu IsdalMain;
        internal System.Windows.Forms.MenuItem IsdalMainView;
        internal System.Windows.Forms.MenuItem IsdalMainViewLarg;
        internal System.Windows.Forms.MenuItem IsdalMainViewSmall;
        internal System.Windows.Forms.MenuItem IsdalMainViewList;
        internal System.Windows.Forms.MenuItem IsdalMainViewDetails;
        private System.Windows.Forms.MenuItem IsdalMainFasel1;
        private System.Windows.Forms.MenuItem IsdalMainEdit;
        private System.Windows.Forms.MenuItem IsdalMainRemove;
        private System.Windows.Forms.MenuItem IsdalMainFasel2;
        private System.Windows.Forms.MenuItem IsdalMainProp;
        private System.Windows.Forms.MenuItem IsdalMainExplore;
        private System.Windows.Forms.MenuItem IsdalMainExecute;
        private System.Windows.Forms.MenuItem IsdalMainFasel3;
        private System.Windows.Forms.MenuItem IsdalMainEnable;
        private System.Windows.Forms.MenuItem IsdalMainDisable;
        private System.Windows.Forms.MenuItem IsdalMainFasel4;
        private System.Windows.Forms.MenuItem IsdalMainRefresh;
        private System.Windows.Forms.MenuItem IsdalMainFasel5;
        private System.Windows.Forms.MenuItem IsdalMainAdd;
        private System.Windows.Forms.MenuItem IsdalMainFasel6;
        private System.Windows.Forms.MenuItem IsdalMainAbout;
        private System.Windows.Forms.MenuItem IsdalMainOptions;
        private System.Windows.Forms.MenuItem IsdalMainFasel7;
        private System.Windows.Forms.MenuItem IsdalMainClose;
        private System.Windows.Forms.StatusBar Main_SBar;
        private System.Windows.Forms.StatusBarPanel PanelEntriesFound;
        private System.Windows.Forms.ColumnHeader AmoodName;
        private System.Windows.Forms.ColumnHeader AmoodData;
        private System.Windows.Forms.ToolBar TheBar;
        private System.Windows.Forms.ToolBarButton BarSep1;
        private System.Windows.Forms.ToolBarButton BarSep2;
        private System.Windows.Forms.ToolBarButton BarSep3;
        private System.Windows.Forms.ToolBarButton BarSep4;
        private System.Windows.Forms.ToolBarButton BarSep5;
        internal System.Windows.Forms.ContextMenu IsdalView;
        internal System.Windows.Forms.MenuItem IsdalViewLarg;
        internal System.Windows.Forms.MenuItem IsdalViewSmall;
        internal System.Windows.Forms.MenuItem IsdalViewList;
        internal System.Windows.Forms.MenuItem IsdalViewDetails;
        private System.Windows.Forms.ColumnHeader AmoodRoot;
        private System.Windows.Forms.ColumnHeader AmoodType;
        private System.Windows.Forms.ColumnHeader AmoodStatus;
        private System.Windows.Forms.ToolBarButton BarFile;
        private System.Windows.Forms.ContextMenu IsdalEdit;
        private System.Windows.Forms.MenuItem IsdalEditEdit;
        private System.Windows.Forms.MenuItem IsdalEditDelete;
        private System.Windows.Forms.MenuItem IsdalEditEnable;
        private System.Windows.Forms.MenuItem IsdalEditDisable;
        private System.Windows.Forms.MenuItem IsdalEditFasel1;
        private System.Windows.Forms.ContextMenu IsdalFile;
        private System.Windows.Forms.MenuItem IsdalFileExit;
        private System.Windows.Forms.ToolBarButton BarEdit;
        private System.Windows.Forms.ToolBarButton BarView;
        private System.Windows.Forms.ToolBarButton BarTools;
        private System.Windows.Forms.ContextMenu IsdalTools;
        private System.Windows.Forms.MenuItem IsdalToolsExec;
        private System.Windows.Forms.MenuItem IsdalToolsExplore;
        private System.Windows.Forms.MenuItem IsdalToolsProp;
        private System.Windows.Forms.MenuItem IsdalToolsFasel1;
        private System.Windows.Forms.MenuItem IsdalToolsFasel2;
        private System.Windows.Forms.MenuItem IsdalToolsTurnOff;
        private System.Windows.Forms.ToolBarButton BarHelp;
        private System.Windows.Forms.ContextMenu IsdalHelp;
        private System.Windows.Forms.MenuItem IsdalHelpAbout;
        private System.Windows.Forms.StatusBarPanel PanelScanning;
        private System.Windows.Forms.MenuItem IsdalFileAdd;
        private System.Windows.Forms.MenuItem IsdalFileFasel1;
        private System.Windows.Forms.MenuItem IsdalEditFasel2;
        private System.Windows.Forms.MenuItem IsdalFileFasel2;
        private System.Windows.Forms.MenuItem IsdalFileOptions;
        private System.Windows.Forms.StatusBarPanel PanelOSVersion;
        private System.Windows.Forms.MenuItem IsdalViewRefresh;
        private System.Windows.Forms.MenuItem IsdalViewFasel1;
        private System.Windows.Forms.ContextMenu IsdalTreeView;
        private System.Windows.Forms.MenuItem IsdalTreeExpandAll;
        private System.Windows.Forms.MenuItem IsdalTreeCollapseAll;
        private System.Windows.Forms.MenuItem IsdalTreeFasel1;
        private System.Windows.Forms.ColumnHeader AmoodPath;
        private System.Windows.Forms.Splitter Fasel1;
        private System.Windows.Forms.MenuItem IsdalToolsFileViewer;
        private System.Windows.Forms.StatusBarPanel PanelOptions;
        private System.Windows.Forms.ImageList ImageCont;







    }
}
