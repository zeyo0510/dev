using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using MDC.Collections;
using MDC.RegViewer.Editors;
using MDC.RegViewer.Properties;
using MDC.RegViewer.Registry;
using MDC.UI.Controls;
using Microsoft.Win32;

namespace MDC.RegViewer
{
	internal class MainForm : Form
	{
		private const bool PROEDTN = false;

		private IContainer components;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem editToolStripMenuItem;

		private ToolStripMenuItem helpToolStripMenuItem;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private TabControl tabControl1;

		private TabPage tbExplorer;

		private ListView2 lstValues;

		private ColumnHeader chValName;

		private ColumnHeader chValType;

		private ColumnHeader chValData;

		private Splitter splitter1;

		private TreeView tvwKeys;

		private TabPage tbSearch;

		private GroupBox gbSearch;

		private CheckBox chkLookAtData;

		private CheckBox chkMatchCase;

		private Label label3;

		private Label label1;

		private ComboBox cmbSearch;

		private Label label2;

		private CheckBox chkLookAtValues;

		private CheckBox chkLookAtKeys;

		private Button btnFind;

		private TextBox txtPattern;

		private ListView2 lstResults;

		private ColumnHeader chKeyPath;

		private ColumnHeader chKeyValue;

		private ColumnHeader chKeyData;

		private ToolStripMenuItem aboutRegViewerToolStripMenuItem;

		private ToolStripSeparator toolStripMenuSeperatorCopy;

		private ToolStripMenuItem copyKeyNameToolStripMenuItem;

		private ToolStripMenuItem findToolStripMenuItem;

		private ToolStripMenuItem exportToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem copyKeyNamePopupMenuItem;

		private TextBox txtBranch;

		private Label label4;

		private ToolStripMenuItem findPopupMenuItem;

		private CheckBox chkUseRegex;

		private ToolStripMenuItem refreshToolStripMenuItem;

		private ToolStripMenuItem refreshPopupMenuItem;

		private ImageList imlIcons;

		private ToolStripMenuItem expandPopupMenuItem;

		private ToolStripMenuItem jumpToKeyToolStripMenuItem;

		private ToolStripMenuItem exportPopupMenuItem;

		private ToolStripSeparator popupMenuSeperatorCopyKeyName;

		private ToolStripMenuItem MDCWebsiteToolStripMenuItem;

		private ToolStripMenuItem modifyPopupMenuItem;

		private ToolStripSeparator popupMenuSeperatorModify;

		private ToolStripMenuItem modifyToolStripMenuItem;

		private ToolStripSeparator toolStripMenuSeperatorModify;

		private ToolStripSeparator popupMenuSeparatorNew;

		private ToolStripMenuItem favoritesToolStripMenuItem;

		private ToolStripMenuItem addToFavoritesToolStripMenuItem;

		private ToolStripMenuItem removeFavoriteToolStripMenuItem;

		private ToolStripSeparator toolStripMenuSeperatorFavorites;

		private ToolStripMenuItem toolStripMenuItem1;

		private RegSearcher searcher;

		private DateTime searchStartTime;

		private bool searchStarted;

		private Settings settings;

		private EventDictionary<string, string> favorites;

		private ImageList rootImageList = new ImageList();

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDC.RegViewer.MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jumpToKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSeperatorModify = new System.Windows.Forms.ToolStripSeparator();
			this.copyKeyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSeperatorCopy = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFavoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSeperatorFavorites = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.MDCWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutRegViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tbExplorer = new System.Windows.Forms.TabPage();
			this.lstValues = new MDC.UI.Controls.ListView2();
			this.chValName = new System.Windows.Forms.ColumnHeader();
			this.chValType = new System.Windows.Forms.ColumnHeader();
			this.chValData = new System.Windows.Forms.ColumnHeader();
			this.imlIcons = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.tvwKeys = new System.Windows.Forms.TreeView();
			this.tbSearch = new System.Windows.Forms.TabPage();
			this.lstResults = new MDC.UI.Controls.ListView2();
			this.chKeyPath = new System.Windows.Forms.ColumnHeader();
			this.chKeyValue = new System.Windows.Forms.ColumnHeader();
			this.chKeyData = new System.Windows.Forms.ColumnHeader();
			this.gbSearch = new System.Windows.Forms.GroupBox();
			this.chkUseRegex = new System.Windows.Forms.CheckBox();
			this.txtBranch = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkLookAtData = new System.Windows.Forms.CheckBox();
			this.chkMatchCase = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbSearch = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chkLookAtValues = new System.Windows.Forms.CheckBox();
			this.chkLookAtKeys = new System.Windows.Forms.CheckBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.txtPattern = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.modifyPopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.popupMenuSeperatorModify = new System.Windows.Forms.ToolStripSeparator();
			this.expandPopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.findPopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshPopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.popupMenuSeparatorNew = new System.Windows.Forms.ToolStripSeparator();
			this.exportPopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.popupMenuSeperatorCopyKeyName = new System.Windows.Forms.ToolStripSeparator();
			this.copyKeyNamePopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tbExplorer.SuspendLayout();
			this.tbSearch.SuspendLayout();
			this.gbSearch.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.fileToolStripMenuItem, this.editToolStripMenuItem, this.favoritesToolStripMenuItem, this.helpToolStripMenuItem });
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(609, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.jumpToKeyToolStripMenuItem, this.exportToolStripMenuItem, this.exitToolStripMenuItem });
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.jumpToKeyToolStripMenuItem.Name = "jumpToKeyToolStripMenuItem";
			this.jumpToKeyToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.jumpToKeyToolStripMenuItem.Text = "&Goto To Key...";
			this.jumpToKeyToolStripMenuItem.Click += new System.EventHandler(jumpToKeyToolStripMenuItem_Click);
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exportToolStripMenuItem.Text = "&Export...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(exportToolStripMenuItem_Click);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(exitToolStripMenuItem_Click);
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.modifyToolStripMenuItem, this.toolStripMenuSeperatorModify, this.copyKeyNameToolStripMenuItem, this.toolStripMenuSeperatorCopy, this.findToolStripMenuItem, this.refreshToolStripMenuItem });
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(editToolStripMenuItem_DropDownOpening);
			this.modifyToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
			this.modifyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.modifyToolStripMenuItem.Text = "&View";
			this.modifyToolStripMenuItem.Click += new System.EventHandler(modifyToolStripMenuItem_Click);
			this.toolStripMenuSeperatorModify.Name = "toolStripMenuSeperatorModify";
			this.toolStripMenuSeperatorModify.Size = new System.Drawing.Size(156, 6);
			this.copyKeyNameToolStripMenuItem.Name = "copyKeyNameToolStripMenuItem";
			this.copyKeyNameToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.copyKeyNameToolStripMenuItem.Text = "&Copy Key Name";
			this.copyKeyNameToolStripMenuItem.Click += new System.EventHandler(copyKeyNameToolStripMenuItem_Click);
			this.toolStripMenuSeperatorCopy.Name = "toolStripMenuSeperatorCopy";
			this.toolStripMenuSeperatorCopy.Size = new System.Drawing.Size(156, 6);
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control;
			this.findToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.findToolStripMenuItem.Text = "&Find";
			this.findToolStripMenuItem.Click += new System.EventHandler(findToolStripMenuItem_Click);
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(refreshToolStripMenuItem_Click);
			this.favoritesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.addToFavoritesToolStripMenuItem, this.removeFavoriteToolStripMenuItem, this.toolStripMenuSeperatorFavorites });
			this.favoritesToolStripMenuItem.Name = "favoritesToolStripMenuItem";
			this.favoritesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
			this.favoritesToolStripMenuItem.Text = "F&avorites";
			this.favoritesToolStripMenuItem.DropDownOpening += new System.EventHandler(favoritesToolStripMenuItem_DropDownOpening);
			this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
			this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.addToFavoritesToolStripMenuItem.Text = "&Add to Favorites";
			this.addToFavoritesToolStripMenuItem.Click += new System.EventHandler(addToFavoritesToolStripMenuItem_Click);
			this.removeFavoriteToolStripMenuItem.Name = "removeFavoriteToolStripMenuItem";
			this.removeFavoriteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.removeFavoriteToolStripMenuItem.Text = "&Remove Favorite";
			this.removeFavoriteToolStripMenuItem.Click += new System.EventHandler(removeFavoriteToolStripMenuItem_Click);
			this.toolStripMenuSeperatorFavorites.Name = "toolStripMenuSeperatorFavorites";
			this.toolStripMenuSeperatorFavorites.Size = new System.Drawing.Size(159, 6);
			this.toolStripMenuSeperatorFavorites.Visible = false;
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.toolStripMenuItem1, this.MDCWebsiteToolStripMenuItem, this.aboutRegViewerToolStripMenuItem });
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
			this.toolStripMenuItem1.Text = "Help (&Online)";
			this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
			this.MDCWebsiteToolStripMenuItem.Name = "MDCWebsiteToolStripMenuItem";
			this.MDCWebsiteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.MDCWebsiteToolStripMenuItem.Text = "&Contact";
			this.MDCWebsiteToolStripMenuItem.Click += new System.EventHandler(MDCWebsiteToolStripMenuItem_Click);
			this.aboutRegViewerToolStripMenuItem.Name = "aboutRegViewerToolStripMenuItem";
			this.aboutRegViewerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.aboutRegViewerToolStripMenuItem.Text = "&About RegViewer";
			this.aboutRegViewerToolStripMenuItem.Click += new System.EventHandler(aboutRegViewerToolStripMenuItem_Click);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripStatusLabel1 });
			this.statusStrip1.Location = new System.Drawing.Point(0, 384);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(609, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			this.tabControl1.Controls.Add(this.tbExplorer);
			this.tabControl1.Controls.Add(this.tbSearch);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.tabControl1.Location = new System.Drawing.Point(0, 24);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(609, 360);
			this.tabControl1.TabIndex = 5;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(tabControl1_SelectedIndexChanged);
			this.tbExplorer.Controls.Add(this.lstValues);
			this.tbExplorer.Controls.Add(this.splitter1);
			this.tbExplorer.Controls.Add(this.tvwKeys);
			this.tbExplorer.Location = new System.Drawing.Point(4, 22);
			this.tbExplorer.Name = "tbExplorer";
			this.tbExplorer.Padding = new System.Windows.Forms.Padding(3);
			this.tbExplorer.Size = new System.Drawing.Size(601, 334);
			this.tbExplorer.TabIndex = 0;
			this.tbExplorer.Text = "Explore";
			this.tbExplorer.UseVisualStyleBackColor = true;
			this.lstValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.chValName, this.chValType, this.chValData });
			this.lstValues.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstValues.HideSelection = false;
			this.lstValues.Location = new System.Drawing.Point(233, 3);
			this.lstValues.Name = "lstValues";
			this.lstValues.Size = new System.Drawing.Size(365, 328);
			this.lstValues.SmallImageList = this.rootImageList;
			this.lstValues.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstValues.TabIndex = 1;
			this.lstValues.UseCompatibleStateImageBehavior = false;
			this.lstValues.View = System.Windows.Forms.View.Details;
			this.lstValues.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(lstValues_AfterLabelEdit);
			this.lstValues.DoubleClick += new System.EventHandler(lstValues_DoubleClick);
			this.lstValues.KeyDown += new System.Windows.Forms.KeyEventHandler(lstValues_KeyDown);
			this.lstValues.MouseUp += new System.Windows.Forms.MouseEventHandler(lstValues_MouseUp);
			this.chValName.Text = "Name";
			this.chValName.Width = 120;
			this.chValType.Text = "Type";
			this.chValType.Width = 120;
			this.chValData.Text = "Data";
			this.chValData.Width = 114;
			this.imlIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imlIcons.ImageStream");
			this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.imlIcons.Images.SetKeyName(0, "ascii");
			this.imlIcons.Images.SetKeyName(1, "binary");
			this.imlIcons.Images.SetKeyName(2, "fold_close");
			this.imlIcons.Images.SetKeyName(3, "fold_open");
			this.splitter1.Location = new System.Drawing.Point(229, 3);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(4, 328);
			this.splitter1.TabIndex = 16;
			this.splitter1.TabStop = false;
			this.tvwKeys.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwKeys.HideSelection = false;
			this.tvwKeys.ImageKey = "fold_close";
			this.tvwKeys.ImageList = this.rootImageList;
			this.tvwKeys.Location = new System.Drawing.Point(3, 3);
			this.tvwKeys.Name = "tvwKeys";
			this.tvwKeys.SelectedImageKey = "fold_open";
			this.tvwKeys.Size = new System.Drawing.Size(226, 328);
			this.tvwKeys.TabIndex = 0;
			this.tvwKeys.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(tvwKeys_AfterLabelEdit);
			this.tvwKeys.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(tvwKeys_BeforeExpand);
			this.tvwKeys.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tvwKeys_AfterSelect);
			this.tvwKeys.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvwKeys_NodeMouseClick);
			this.tvwKeys.KeyDown += new System.Windows.Forms.KeyEventHandler(tvwKeys_KeyDown);
			this.tvwKeys.MouseUp += new System.Windows.Forms.MouseEventHandler(tvwKeys_MouseUp);
			this.tbSearch.Controls.Add(this.lstResults);
			this.tbSearch.Controls.Add(this.gbSearch);
			this.tbSearch.Location = new System.Drawing.Point(4, 22);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Padding = new System.Windows.Forms.Padding(3);
			this.tbSearch.Size = new System.Drawing.Size(601, 334);
			this.tbSearch.TabIndex = 1;
			this.tbSearch.Text = "Search";
			this.tbSearch.UseVisualStyleBackColor = true;
			this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.chKeyPath, this.chKeyValue, this.chKeyData });
			this.lstResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstResults.FullRowSelect = true;
			this.lstResults.GridLines = true;
			this.lstResults.HideSelection = false;
			this.lstResults.Location = new System.Drawing.Point(3, 120);
			this.lstResults.Name = "lstResults";
			this.lstResults.Size = new System.Drawing.Size(595, 211);
			this.lstResults.TabIndex = 9;
			this.lstResults.UseCompatibleStateImageBehavior = false;
			this.lstResults.View = System.Windows.Forms.View.Details;
			this.lstResults.DoubleClick += new System.EventHandler(lstResults_DoubleClick);
			this.lstResults.KeyDown += new System.Windows.Forms.KeyEventHandler(lstResults_KeyDown);
			this.lstResults.MouseUp += new System.Windows.Forms.MouseEventHandler(lstResults_MouseUp);
			this.chKeyPath.Text = "Path";
			this.chKeyPath.Width = 270;
			this.chKeyValue.Text = "Value";
			this.chKeyValue.Width = 240;
			this.chKeyData.Text = "Data";
			this.chKeyData.Width = 76;
			this.gbSearch.Controls.Add(this.chkUseRegex);
			this.gbSearch.Controls.Add(this.txtBranch);
			this.gbSearch.Controls.Add(this.label4);
			this.gbSearch.Controls.Add(this.chkLookAtData);
			this.gbSearch.Controls.Add(this.chkMatchCase);
			this.gbSearch.Controls.Add(this.label3);
			this.gbSearch.Controls.Add(this.label1);
			this.gbSearch.Controls.Add(this.cmbSearch);
			this.gbSearch.Controls.Add(this.label2);
			this.gbSearch.Controls.Add(this.chkLookAtValues);
			this.gbSearch.Controls.Add(this.chkLookAtKeys);
			this.gbSearch.Controls.Add(this.btnFind);
			this.gbSearch.Controls.Add(this.txtPattern);
			this.gbSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbSearch.Location = new System.Drawing.Point(3, 3);
			this.gbSearch.Name = "gbSearch";
			this.gbSearch.Size = new System.Drawing.Size(595, 117);
			this.gbSearch.TabIndex = 3;
			this.gbSearch.TabStop = false;
			this.chkUseRegex.AutoSize = true;
			this.chkUseRegex.Location = new System.Drawing.Point(380, 83);
			this.chkUseRegex.Name = "chkUseRegex";
			this.chkUseRegex.Size = new System.Drawing.Size(80, 17);
			this.chkUseRegex.TabIndex = 9;
			this.chkUseRegex.Text = "Use &RegEx";
			this.chkUseRegex.UseVisualStyleBackColor = true;
			this.txtBranch.Location = new System.Drawing.Point(292, 49);
			this.txtBranch.Name = "txtBranch";
			this.txtBranch.Size = new System.Drawing.Size(198, 20);
			this.txtBranch.TabIndex = 4;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(242, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 53;
			this.label4.Text = "Branch:";
			this.chkLookAtData.AutoSize = true;
			this.chkLookAtData.Location = new System.Drawing.Point(186, 84);
			this.chkLookAtData.Name = "chkLookAtData";
			this.chkLookAtData.Size = new System.Drawing.Size(49, 17);
			this.chkLookAtData.TabIndex = 7;
			this.chkLookAtData.Text = "&Data";
			this.chkLookAtData.UseVisualStyleBackColor = true;
			this.chkLookAtData.CheckedChanged += new System.EventHandler(CheckedChanged);
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.Location = new System.Drawing.Point(292, 83);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(83, 17);
			this.chkMatchCase.TabIndex = 8;
			this.chkMatchCase.Text = "Match &Case";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 52;
			this.label3.Text = "Look at:";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 50;
			this.label1.Text = "Find what:";
			this.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSearch.FormattingEnabled = true;
			this.cmbSearch.Items.AddRange(new object[6] { "All Hives", "HKEY_CLASSES_ROOT", "HKEY_CURRENT_USER", "HKEY_LOCAL_MACHINE", "HKEY_USERS", "HKEY_CURRENT_CONFIG" });
			this.cmbSearch.Location = new System.Drawing.Point(59, 48);
			this.cmbSearch.Name = "cmbSearch";
			this.cmbSearch.Size = new System.Drawing.Size(176, 21);
			this.cmbSearch.TabIndex = 0;
			this.cmbSearch.SelectedValueChanged += new System.EventHandler(cmbSearch_SelectedValueChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 51;
			this.label2.Text = "Search:";
			this.chkLookAtValues.AutoSize = true;
			this.chkLookAtValues.Location = new System.Drawing.Point(122, 84);
			this.chkLookAtValues.Name = "chkLookAtValues";
			this.chkLookAtValues.Size = new System.Drawing.Size(58, 17);
			this.chkLookAtValues.TabIndex = 6;
			this.chkLookAtValues.Text = "&Values";
			this.chkLookAtValues.UseVisualStyleBackColor = true;
			this.chkLookAtValues.CheckedChanged += new System.EventHandler(CheckedChanged);
			this.chkLookAtKeys.AutoSize = true;
			this.chkLookAtKeys.Location = new System.Drawing.Point(59, 84);
			this.chkLookAtKeys.Name = "chkLookAtKeys";
			this.chkLookAtKeys.Size = new System.Drawing.Size(49, 17);
			this.chkLookAtKeys.TabIndex = 5;
			this.chkLookAtKeys.Text = "&Keys";
			this.chkLookAtKeys.UseVisualStyleBackColor = true;
			this.chkLookAtKeys.CheckedChanged += new System.EventHandler(CheckedChanged);
			this.btnFind.Enabled = false;
			this.btnFind.Location = new System.Drawing.Point(496, 20);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(93, 30);
			this.btnFind.TabIndex = 10;
			this.btnFind.Text = "F&ind";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(btnFind_Click);
			this.txtPattern.Location = new System.Drawing.Point(59, 20);
			this.txtPattern.Name = "txtPattern";
			this.txtPattern.Size = new System.Drawing.Size(431, 20);
			this.txtPattern.TabIndex = 2;
			this.txtPattern.TextChanged += new System.EventHandler(txtPattern_TextChanged);
			this.txtPattern.KeyDown += new System.Windows.Forms.KeyEventHandler(txtPattern_KeyDown);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.modifyPopupMenuItem, this.popupMenuSeperatorModify, this.expandPopupMenuItem, this.findPopupMenuItem, this.refreshPopupMenuItem, this.popupMenuSeparatorNew, this.exportPopupMenuItem, this.popupMenuSeperatorCopyKeyName, this.copyKeyNamePopupMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(160, 154);
			this.modifyPopupMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.modifyPopupMenuItem.Name = "modifyPopupMenuItem";
			this.modifyPopupMenuItem.Size = new System.Drawing.Size(159, 22);
			this.modifyPopupMenuItem.Text = "&View";
			this.modifyPopupMenuItem.Click += new System.EventHandler(modifyToolStripMenuItem_Click);
			this.popupMenuSeperatorModify.Name = "popupMenuSeperatorModify";
			this.popupMenuSeperatorModify.Size = new System.Drawing.Size(156, 6);
			this.expandPopupMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.expandPopupMenuItem.Name = "expandPopupMenuItem";
			this.expandPopupMenuItem.Size = new System.Drawing.Size(159, 22);
			this.expandPopupMenuItem.Text = "Expand";
			this.expandPopupMenuItem.Click += new System.EventHandler(expandPopupMenuItem_Click);
			this.findPopupMenuItem.Name = "findPopupMenuItem";
			this.findPopupMenuItem.Size = new System.Drawing.Size(159, 22);
			this.findPopupMenuItem.Text = "&Find";
			this.findPopupMenuItem.Click += new System.EventHandler(findToolStripMenuItem_Click);
			this.refreshPopupMenuItem.Name = "refreshPopupMenuItem";
			this.refreshPopupMenuItem.Size = new System.Drawing.Size(159, 22);
			this.refreshPopupMenuItem.Text = "Refresh";
			this.refreshPopupMenuItem.Click += new System.EventHandler(refreshToolStripMenuItem_Click);
			this.popupMenuSeparatorNew.Name = "popupMenuSeparatorNew";
			this.popupMenuSeparatorNew.Size = new System.Drawing.Size(156, 6);
			this.exportPopupMenuItem.Name = "exportPopupMenuItem";
			this.exportPopupMenuItem.Size = new System.Drawing.Size(159, 22);
			this.exportPopupMenuItem.Text = "&Export";
			this.exportPopupMenuItem.Click += new System.EventHandler(exportToolStripMenuItem_Click);
			this.popupMenuSeperatorCopyKeyName.Name = "popupMenuSeperatorCopyKeyName";
			this.popupMenuSeperatorCopyKeyName.Size = new System.Drawing.Size(156, 6);
			this.copyKeyNamePopupMenuItem.Name = "copyKeyNamePopupMenuItem";
			this.copyKeyNamePopupMenuItem.Size = new System.Drawing.Size(159, 22);
			this.copyKeyNamePopupMenuItem.Text = "&Copy Key Name";
			this.copyKeyNamePopupMenuItem.Click += new System.EventHandler(copyKeyNameToolStripMenuItem_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(609, 406);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Location = new System.Drawing.Point(-1, -1);
			base.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(617, 433);
			base.Name = "MainForm";
			this.Text = "RegViewer, a read-only registry viewer - for personal use only";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MainForm_FormClosing);
			base.Load += new System.EventHandler(MainForm_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(MainForm_KeyDown);
			base.Resize += new System.EventHandler(MainForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tbExplorer.ResumeLayout(false);
			this.tbSearch.ResumeLayout(false);
			this.gbSearch.ResumeLayout(false);
			this.gbSearch.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public MainForm()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			rootImageList.Images.Add("ascii", Image.FromFile(baseDirectory + "\\Resources\\ascii.ico"));
			rootImageList.Images.Add("binary", Image.FromFile(baseDirectory + "\\Resources\\binary.ico"));
			rootImageList.Images.Add("fold_close", Image.FromFile(baseDirectory + "\\Resources\\fold_close.ico"));
			rootImageList.Images.Add("fold_open", Image.FromFile(baseDirectory + "\\Resources\\fold_open.ico"));
			rootImageList.Images.Add("computer", Image.FromFile(baseDirectory + "\\Resources\\computer.ico"));
			InitializeComponent();
			searcher = new RegSearcher();
			searcher.SearchComplete += searcher_SearchComplete;
			searcher.MatchFound += searcher_MatchFound;
			favorites = new EventDictionary<string, string>();
			favorites.ItemAdded += favorites_ItemAdded;
			favorites.ItemRemoved += favorites_ItemRemoved;
		}

		private void favorites_ItemRemoved(object sender, ItemEventArgs<string, string> e)
		{
			favoritesToolStripMenuItem.DropDownItems.RemoveByKey(e.Item.Key);
		}

		private void favorites_ItemAdded(object sender, ItemEventArgs<string, string> e)
		{
			AddFavoriteMenuItem(e.Item.Key, e.Item.Value);
		}

		private TreeNode CreateNode()
		{
			return new TreeNode();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			settings = Settings.Default;
			LoadSettings();
			AddRootKeys();
			LoadFavorites();
			if (settings.LastKey != string.Empty)
			{
				JumpToKey(settings.LastKey);
			}
		}

		private void LoadFavorites()
		{
			RegKey regKey = RegKey.Parse("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\Regedit\\Favorites");
			if (regKey == null)
			{
				favorites["Windows Owner"] = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
				favorites["HKLM Startup Run"] = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
				if (Environment.Is64BitOperatingSystem)
				{
					favorites["HKLM Wow6432Node Run"] = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
				}
				favorites["HKCU Startup Run"] = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
				favorites["Startup RunOnce"] = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
				return;
			}
			favorites["Windows Owner"] = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
			favorites["HKLM Startup Run"] = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
			if (Environment.Is64BitOperatingSystem)
			{
				favorites["HKLM Wow6432Node Run"] = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
			}
			favorites["HKCU Startup Run"] = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
			favorites["Startup RunOnce"] = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
			List<RegValue> values = RegExplorer.GetValues(regKey.Key);
			if (values.Count > 0)
			{
				values.ForEach(delegate(RegValue val)
				{
					string text = val.Data.ToString();
					text = text.Substring(text.IndexOf('\\') + 1);
					favorites[val.Name] = text;
				});
			}
		}

		private void AddFavoriteMenuItem(string name, string key)
		{
			ToolStripItem toolStripItem = new ToolStripMenuItem(name);
			toolStripItem.Tag = key;
			toolStripItem.Name = name;
			favoritesToolStripMenuItem.DropDownItems.Add(toolStripItem);
			toolStripItem.Click += favoriteMenuItem_Click;
		}

		private void favoriteMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			JumpToKey(toolStripMenuItem.Tag.ToString());
		}

		private void AddRootKeys()
		{
			tvwKeys.ImageList = rootImageList;
			TreeNode treeNode = new TreeNode("Computer");
			treeNode.Text = "Computer";
			treeNode.Name = "Computer";
			treeNode.Tag = "Computer";
			treeNode.ImageKey = "computer";
			treeNode.ImageIndex = 4;
			treeNode.SelectedImageIndex = 4;
			tvwKeys.Nodes.Add(treeNode);
			AddRootKey(Microsoft.Win32.Registry.ClassesRoot);
			AddRootKey(Microsoft.Win32.Registry.CurrentUser);
			AddRootKey(Microsoft.Win32.Registry.LocalMachine);
			AddRootKey(Microsoft.Win32.Registry.Users);
			AddRootKey(Microsoft.Win32.Registry.CurrentConfig);
		}

		private void AdjustControls()
		{
			int num = base.Width - gbSearch.Left * 2 - tbSearch.Left * 2 - 8;
			btnFind.Left = num - btnFind.Width - 6;
			txtPattern.Width = num - txtPattern.Left - btnFind.Width - 12;
			txtBranch.Width = num - txtBranch.Left - btnFind.Width - 12;
		}

		private void AddRootKey(RegistryKey key)
		{
			TreeNode treeNode = CreateNode(key.Name, key.Name, key);
			tvwKeys.Nodes.Add(treeNode);
			treeNode.Nodes.Add(CreateNode());
		}

		private TreeNode CreateNode(string key, string text, object tag)
		{
			TreeNode treeNode = CreateNode();
			treeNode.Text = text;
			treeNode.Name = key;
			treeNode.Tag = tag;
			return treeNode;
		}

		private void AddKeyToTree(TreeNode parent, RegKey subKey)
		{
			RegistryKey key = subKey.Key;
			TreeNode treeNode = CreateNode(key.Name, subKey.Name, key);
			parent.Nodes.Add(treeNode);
			if (key.SubKeyCount > 0)
			{
				treeNode.Nodes.Add(CreateNode());
			}
		}

		private void tvwKeys_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;
			if (node.FirstNode.Tag == null)
			{
				using (new BusyCursor(this))
				{
					LoadSubKeys(node);
				}
			}
		}

		private void LoadSubKeys(TreeNode parentNode)
		{
			tvwKeys.SuspendLayout();
			parentNode.Nodes.Clear();
			RegistryKey key = (RegistryKey)parentNode.Tag;
			List<RegKey> subKeys = RegExplorer.GetSubKeys(key);
			subKeys.OrderBy((RegKey subKey) => subKey.Name);
			foreach (RegKey item in subKeys)
			{
				AddKeyToTree(parentNode, item);
			}
			tvwKeys.ResumeLayout();
		}

		private void tvwKeys_AfterSelect(object sender, TreeViewEventArgs e)
		{
			RegistryKey key = e.Node.Tag as RegistryKey;
			LoadValues(key);
		}

		private void LoadValues(RegistryKey key)
		{
			if (key == null)
			{
				toolStripStatusLabel1.Text = "Computer " + Environment.MachineName;
				lstValues.Items.Clear();
				List<KeyValuePair<string, string>> list = RegistrySettings();
				return;
			}
			toolStripStatusLabel1.Text = key.Name;
			lstValues.Items.Clear();
			List<RegValue> values = RegExplorer.GetValues(key);
			if (values == null)
			{
				return;
			}
			if (values.Count == 0)
			{
				AddValueToList(key, CreateDefaultValue());
				return;
			}
			lstValues.SuspendLayout();
			RegValue defaultValue = CreateDefaultValue();
			if (values.SingleOrDefault((RegValue val) => val.Name == defaultValue.Name) == null)
			{
				AddValueToList(key, defaultValue);
			}
			foreach (RegValue item in values)
			{
				AddValueToList(key, item);
			}
			lstValues.ResumeLayout();
		}

		private static RegValue CreateDefaultValue()
		{
			return new RegValue(string.Empty, RegistryValueKind.String, "(value not set)");
		}

		private ListViewItem AddValueToList(RegistryKey key, RegValue value)
		{
			ListViewItem listViewItem = lstValues.Items.Add(value.Name);
			listViewItem.ImageKey = GetValueTypeIcon(value.Kind);
			listViewItem.Name = value.Name;
			listViewItem.Tag = key;
			listViewItem.SubItems.Add(value.Kind.ToDataType());
			ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(value.ToString());
			listViewSubItem.Tag = value;
			return listViewItem;
		}

		private ListViewItem AddComputerValueToList(string key, string value)
		{
			ListViewItem listViewItem = lstValues.Items.Add(key);
			listViewItem.ImageKey = "ascii";
			listViewItem.Name = key;
			listViewItem.Tag = key;
			listViewItem.SubItems.Add("REG_SZ");
			ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(value);
			listViewSubItem.Tag = value;
			return listViewItem;
		}

		private string GetValueTypeIcon(RegistryValueKind registryValueKind)
		{
			if (registryValueKind == RegistryValueKind.ExpandString || registryValueKind == RegistryValueKind.MultiString || registryValueKind == RegistryValueKind.String)
			{
				return "ascii";
			}
			return "binary";
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			AdjustControls();
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			if (btnFind.Text == "F&ind")
			{
				RegistryKey[] array;
				if (cmbSearch.Text == "All Hives")
				{
					array = new RegistryKey[cmbSearch.Items.Count];
					for (int i = 0; i < cmbSearch.Items.Count; i++)
					{
						array[i] = RegUtility.ParseRootKey(cmbSearch.Items[i].ToString());
					}
				}
				else
				{
					array = new RegistryKey[1] { RegUtility.ParseRootKey(cmbSearch.Text) };
				}
				if (txtBranch.Text != string.Empty)
				{
					array[0] = array[0].OpenSubKey(txtBranch.Text);
					if (array[0] == null)
					{
						UIUtility.DisplayError(this, Resources.Error_InvalidKey, txtBranch);
						return;
					}
				}
				RegSearchArgs searchArgs = GetSearchArgs(array);
				StartSearch();
				try
				{
					searcher.Start(searchArgs);
				}
				catch (ArgumentException ex)
				{
					toolStripStatusLabel1.Text = "Ready.";
					UIUtility.DisplayError(this, ex.Message, txtPattern);
					EnableSearch();
					return;
				}
				searchStarted = true;
			}
			else
			{
				btnFind.Enabled = false;
				searcher.Stop();
			}
		}

		private void StartSearch()
		{
			DisableSearch();
			lstResults.Items.Clear();
			toolStripStatusLabel1.Text = "Searching...";
			searchStartTime = DateTime.Now;
		}

		private RegSearchArgs GetSearchArgs(RegistryKey[] keys)
		{
			RegSearchLookAt searchTarget = GetSearchTarget();
			return new RegSearchArgs(keys, txtPattern.Text, chkMatchCase.Checked, searchTarget, chkUseRegex.Checked);
		}

		private RegSearchLookAt GetSearchTarget()
		{
			RegSearchLookAt regSearchLookAt = (RegSearchLookAt)0;
			if (chkLookAtData.Checked)
			{
				regSearchLookAt |= RegSearchLookAt.Data;
			}
			if (chkLookAtValues.Checked)
			{
				regSearchLookAt |= RegSearchLookAt.Values;
			}
			if (chkLookAtKeys.Checked)
			{
				regSearchLookAt |= RegSearchLookAt.Keys;
			}
			return regSearchLookAt;
		}

		private void searcher_MatchFound(object sender, MatchFoundEventArgs e)
		{
			AddResultToListView(e.Match);
		}

		private void DisableSearch()
		{
			btnFind.Text = "&Cancel";
			TextBox textBox = txtPattern;
			CheckBox checkBox = chkLookAtKeys;
			TextBox textBox2 = txtBranch;
			CheckBox checkBox2 = chkLookAtValues;
			CheckBox checkBox3 = chkLookAtData;
			ComboBox comboBox = cmbSearch;
			CheckBox checkBox4 = chkMatchCase;
			bool flag2 = (chkUseRegex.Enabled = false);
			bool flag4 = (checkBox4.Enabled = flag2);
			bool flag6 = (comboBox.Enabled = flag4);
			bool flag8 = (checkBox3.Enabled = flag6);
			bool flag10 = (checkBox2.Enabled = flag8);
			bool flag12 = (textBox2.Enabled = flag10);
			bool enabled = (checkBox.Enabled = flag12);
			textBox.Enabled = enabled;
		}

		private void searcher_SearchComplete(object sender, SearchCompleteEventArgs e)
		{
			double totalSeconds = DateTime.Now.Subtract(searchStartTime).TotalSeconds;
			int count = lstResults.Items.Count;
			btnFind.Enabled = true;
			EnableSearch();
			if (tabControl1.SelectedIndex == 1)
			{
				toolStripStatusLabel1.Text = string.Format("Found {0} matches in {1} seconds.", count, totalSeconds);
				searchStarted = false;
			}
		}

		private void AddResultToListView(RegSearchMatch result)
		{
			ListViewItem listViewItem = lstResults.Items.Add(result.Key);
			listViewItem.Tag = result;
			listViewItem.SubItems.Add(RegUtility.GetRegValueName(result.Value));
			listViewItem.SubItems.Add(result.Data);
		}

		private void EnableSearch()
		{
			btnFind.Text = "F&ind";
			TextBox textBox = txtPattern;
			CheckBox checkBox = chkLookAtKeys;
			TextBox textBox2 = txtBranch;
			CheckBox checkBox2 = chkLookAtValues;
			CheckBox checkBox3 = chkLookAtData;
			ComboBox comboBox = cmbSearch;
			CheckBox checkBox4 = chkMatchCase;
			bool flag2 = (chkUseRegex.Enabled = true);
			bool flag4 = (checkBox4.Enabled = flag2);
			bool flag6 = (comboBox.Enabled = flag4);
			bool flag8 = (checkBox3.Enabled = flag6);
			bool flag10 = (checkBox2.Enabled = flag8);
			bool flag12 = (textBox2.Enabled = flag10);
			bool enabled = (checkBox.Enabled = flag12);
			textBox.Enabled = enabled;
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			btnFind.Enabled = (chkLookAtKeys.Checked || chkLookAtValues.Checked || chkLookAtData.Checked) && txtPattern.Text != string.Empty;
		}

		private void txtPattern_TextChanged(object sender, EventArgs e)
		{
			btnFind.Enabled = (chkLookAtKeys.Checked || chkLookAtValues.Checked || chkLookAtData.Checked) && txtPattern.Text != string.Empty;
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl1.SelectedIndex == 1)
			{
				if (searchStarted && searcher.IsBusy)
				{
					toolStripStatusLabel1.Text = "Searching...";
				}
				else if (searchStarted)
				{
					toolStripStatusLabel1.Text = "Search complete.";
					searchStarted = false;
				}
				else
				{
					toolStripStatusLabel1.Text = "Ready.";
				}
			}
			else
			{
				toolStripStatusLabel1.Text = string.Empty;
			}
		}

		private void aboutRegViewerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AboutBox().ShowDialog(this);
		}

		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnFindAction();
		}

		private void OnFindAction()
		{
			string selectedKey = GetSelectedKey();
			if (selectedKey != string.Empty)
			{
				string hive;
				string branch;
				RegUtility.SplitKey(selectedKey, out hive, out branch);
				if (!searcher.IsBusy)
				{
					cmbSearch.SelectedItem = hive;
					txtBranch.Text = branch;
				}
			}
			tabControl1.SelectedTab = tbSearch;
			txtPattern.Focus();
		}

		private void CreatePopupMenu()
		{
			ToolStripMenuItem toolStripMenuItem = modifyPopupMenuItem;
			bool visible = (popupMenuSeperatorModify.Visible = GetModifyMenuState());
			toolStripMenuItem.Visible = visible;
			if (base.ActiveControl == tvwKeys)
			{
				expandPopupMenuItem.Visible = true;
				expandPopupMenuItem.Enabled = tvwKeys.SelectedNode.Nodes.Count > 0;
				expandPopupMenuItem.Text = (tvwKeys.SelectedNode.IsExpanded ? "Collapse" : "Expand");
			}
			else
			{
				expandPopupMenuItem.Visible = false;
			}
			refreshPopupMenuItem.Enabled = GetRefreshMenuState();
			exportPopupMenuItem.Visible = base.ActiveControl != lstValues;
			exportPopupMenuItem.Enabled = base.ActiveControl != lstResults || lstResults.SelectedItems.Count == 1;
			ToolStripMenuItem toolStripMenuItem2 = copyKeyNamePopupMenuItem;
			bool visible2 = (popupMenuSeperatorCopyKeyName.Visible = GetCopyMenuState());
			toolStripMenuItem2.Visible = visible2;
		}

		private bool GetNewMenuState()
		{
			if ((base.ActiveControl == tvwKeys || (base.ActiveControl == lstValues && lstValues.Items.Count > 0)) && tvwKeys.SelectedNode != null)
			{
				return true;
			}
			return false;
		}

		private bool GetModifyMenuState()
		{
			if (base.ActiveControl == lstValues && lstValues.SelectedItems.Count == 1)
			{
				return true;
			}
			return false;
		}

		private void lstResults_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && lstResults.SelectedItems.Count > 0)
			{
				DisplayPopupMenu(lstResults, e);
			}
		}

		private void lstValues_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				DisplayPopupMenu(lstValues, e);
			}
		}

		private void DisplayPopupMenu(Control source, MouseEventArgs e)
		{
			CreatePopupMenu();
			contextMenuStrip1.Show(source, e.X, e.Y);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Control || e.KeyCode != Keys.A)
			{
				return;
			}
			if (base.ActiveControl == lstValues)
			{
				foreach (ListViewItem item in lstValues.Items)
				{
					item.Selected = true;
				}
				return;
			}
			if (base.ActiveControl != lstResults)
			{
				return;
			}
			foreach (ListViewItem item2 in lstResults.Items)
			{
				item2.Selected = true;
			}
		}

		private void tvwKeys_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && tvwKeys.SelectedNode != null)
			{
				DisplayPopupMenu(tvwKeys, e);
			}
		}

		private string GetSelectedKey()
		{
			if (base.ActiveControl == tvwKeys && tvwKeys.SelectedNode != null)
			{
				return tvwKeys.SelectedNode.Name;
			}
			if (base.ActiveControl == lstValues && lstValues.SelectedItems.Count > 0)
			{
				return lstValues.SelectedItems[0].Tag.ToString();
			}
			if (base.ActiveControl == lstResults && lstResults.SelectedItems.Count > 0)
			{
				return lstResults.SelectedItems[0].Text;
			}
			return string.Empty;
		}

		private void copyKeyNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnCopyKeyNameAction();
		}

		private void OnCopyKeyNameAction()
		{
			string selectedKey = GetSelectedKey();
			if (selectedKey != string.Empty)
			{
				Clipboard.SetText(selectedKey);
			}
		}

		private void tvwKeys_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				tvwKeys.SelectedNode = e.Node;
			}
		}

		private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			CreateEditMenu();
		}

		private void CreateEditMenu()
		{
			ToolStripMenuItem toolStripMenuItem = modifyToolStripMenuItem;
			bool visible = (toolStripMenuSeperatorModify.Visible = GetModifyMenuState());
			toolStripMenuItem.Visible = visible;
			refreshToolStripMenuItem.Enabled = GetRefreshMenuState();
		}

		private bool GetDeleteMenuState()
		{
			if (base.ActiveControl == tvwKeys && tvwKeys.SelectedNode != null)
			{
				return tvwKeys.SelectedNode.Level != 0;
			}
			if (base.ActiveControl is ListView && ((ListView)base.ActiveControl).SelectedItems.Count > 0)
			{
				return true;
			}
			return false;
		}

		private bool GetRefreshMenuState()
		{
			if ((base.ActiveControl == tvwKeys || (base.ActiveControl == lstValues && lstValues.Items.Count > 0)) && tvwKeys.SelectedNode != null)
			{
				return true;
			}
			return false;
		}

		private bool GetCopyMenuState()
		{
			if ((base.ActiveControl == tvwKeys && tvwKeys.SelectedNode != null) || (base.ActiveControl == lstResults && lstResults.SelectedItems.Count == 1))
			{
				return true;
			}
			return false;
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnDeleteAction();
		}

		private void OnRefreshAction()
		{
			if (tvwKeys.SelectedNode == null)
			{
				return;
			}
			using (new BusyCursor(this))
			{
				if (base.ActiveControl == tvwKeys || base.ActiveControl == lstValues)
				{
					string fullPath = tvwKeys.SelectedNode.FullPath;
					string selectedValue = GetSelectedValue();
					RefreshTreeView();
					TreeNode[] array = tvwKeys.Nodes.Find(fullPath, true);
					if (array.Length > 0)
					{
						tvwKeys.SelectedNode = array[0];
						SetSelectedValue(selectedValue);
					}
				}
			}
		}

		private void RefreshValues()
		{
			string selectedValue = GetSelectedValue();
			RegistryKey registryKey = tvwKeys.SelectedNode.Tag as RegistryKey;
			if (registryKey != null)
			{
				LoadValues(registryKey);
			}
			SetSelectedValue(selectedValue);
		}

		private void SetSelectedValue(string key)
		{
			ListViewItem listViewItem = lstValues.Items[key];
			if (listViewItem != null)
			{
				listViewItem.Selected = true;
			}
		}

		private string GetSelectedValue()
		{
			if (lstValues.SelectedItems.Count == 1)
			{
				return lstValues.SelectedItems[0].Name;
			}
			return string.Empty;
		}

		private void RefreshTreeView()
		{
			if (tvwKeys.SelectedNode == null)
			{
				return;
			}
			TreeNode selectedNode;
			if (tvwKeys.SelectedNode.IsExpanded)
			{
				selectedNode = tvwKeys.SelectedNode;
			}
			else
			{
				if (tvwKeys.SelectedNode.Level == 0)
				{
					return;
				}
				selectedNode = tvwKeys.SelectedNode.Parent;
			}
			bool flag = false;
			do
			{
				try
				{
					LoadSubKeys(selectedNode);
				}
				catch (IOException)
				{
					flag = true;
					selectedNode = selectedNode.Parent;
				}
			}
			while (flag && selectedNode.Level > 0);
		}

		private void OnDeleteAction()
		{
			if (base.ActiveControl == tvwKeys)
			{
				DeleteTreeKey();
			}
			else if (base.ActiveControl == lstValues)
			{
				DeleteListValue();
			}
			else if (base.ActiveControl == lstResults)
			{
				DeleteListEntry();
			}
		}

		private void DeleteListEntry()
		{
			if (UIUtility.ConfirmAction(this, Resources.Confirm_DeleteEntries, "Entry Delete", true) && !DeleteEntries())
			{
				UIUtility.WarnUser(this, Resources.Error_DeleteEntriesFail);
			}
		}

		private void DeleteListValue()
		{
			if (UIUtility.ConfirmAction(this, Resources.Confirm_DeleteValue, "Value Delete", true) && !DeleteValues())
			{
				UIUtility.WarnUser(this, Resources.Error_DeleteValueFail);
			}
		}

		private void DeleteTreeKey()
		{
			if (UIUtility.ConfirmAction(this, Resources.Confirm_DeleteKey, "Key Delete", true))
			{
				if (RegUtility.DeleteKey(tvwKeys.SelectedNode.Tag.ToString()))
				{
					tvwKeys.SelectedNode.Remove();
				}
				else
				{
					UIUtility.WarnUser(this, Resources.Error_DeleteKeyFail);
				}
			}
		}

		private bool DeleteEntries()
		{
			bool result = true;
			foreach (ListViewItem selectedItem in lstResults.SelectedItems)
			{
				RegSearchMatch regSearchMatch = (RegSearchMatch)selectedItem.Tag;
				if (regSearchMatch.Value == "-")
				{
					if (RegUtility.DeleteKey(regSearchMatch.Key))
					{
						selectedItem.Remove();
					}
					else
					{
						result = false;
					}
				}
				else if (RegUtility.DeleteValue(regSearchMatch.Key, regSearchMatch.Value))
				{
					selectedItem.Remove();
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		private bool DeleteValues()
		{
			bool result = true;
			foreach (ListViewItem selectedItem in lstValues.SelectedItems)
			{
				if (RegUtility.DeleteValue(selectedItem.Tag.ToString(), selectedItem.Text))
				{
					selectedItem.Remove();
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			launchBrowser("http://metadataconsulting.blogspot.ca/2017/04/Registry-Viewer-App-a-read-only-registry-viewer.html");
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (searcher != null)
			{
				searcher.Stop();
			}
			SaveSettings();
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnRefreshAction();
		}

		private void tvwKeys_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void expandPopupMenuItem_Click(object sender, EventArgs e)
		{
			if (tvwKeys.SelectedNode.IsExpanded)
			{
				tvwKeys.SelectedNode.Collapse();
			}
			else
			{
				tvwKeys.SelectedNode.Expand();
			}
		}

		private void LoadSettings()
		{
			if (settings.Location.X != -1)
			{
				base.Location = settings.Location;
			}
			base.Size = settings.Size;
			chkLookAtKeys.Checked = settings.LookAtKeys;
			chkLookAtValues.Checked = settings.LookAtValues;
			chkLookAtData.Checked = settings.LookAtData;
			chkMatchCase.Checked = settings.MatchCase;
			chkUseRegex.Checked = settings.UseRegEx;
			cmbSearch.SelectedItem = settings.SearchHive.Clone();
			lstValues.Columns[0].Width = settings.ValWidth1;
			lstValues.Columns[1].Width = settings.ValWidth2;
			lstValues.Columns[2].Width = settings.ValWidth3;
			lstResults.Columns[0].Width = settings.ResWidth1;
			lstResults.Columns[1].Width = settings.ResWidth2;
			lstResults.Columns[2].Width = settings.ResWidth3;
			if (settings.Maximized)
			{
				base.WindowState = FormWindowState.Maximized;
			}
		}

		private void SaveSettings()
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				settings.Maximized = false;
				settings.Size = base.Size;
				settings.Location = base.Location;
			}
			else
			{
				settings.Maximized = true;
			}
			settings.LookAtKeys = chkLookAtKeys.Checked;
			settings.LookAtValues = chkLookAtValues.Checked;
			settings.LookAtData = chkLookAtData.Checked;
			settings.MatchCase = chkMatchCase.Checked;
			settings.UseRegEx = chkUseRegex.Checked;
			settings.SearchHive = cmbSearch.SelectedItem.ToString();
			settings.ValWidth1 = lstValues.Columns[0].Width;
			settings.ValWidth2 = lstValues.Columns[1].Width;
			settings.ValWidth3 = lstValues.Columns[2].Width;
			settings.ResWidth1 = lstResults.Columns[0].Width;
			settings.ResWidth2 = lstResults.Columns[1].Width;
			settings.ResWidth3 = lstResults.Columns[2].Width;
			if (tvwKeys.SelectedNode != null)
			{
				settings.LastKey = tvwKeys.SelectedNode.Name;
			}
			settings.Save();
		}

		private void lstResults_DoubleClick(object sender, EventArgs e)
		{
			if (lstResults.SelectedItems.Count != 1)
			{
				return;
			}
			RegSearchMatch regSearchMatch = lstResults.SelectedItems[0].Tag as RegSearchMatch;
			if (JumpToKey(regSearchMatch.Key) && regSearchMatch.Value != "-")
			{
				string regValueName = RegUtility.GetRegValueName(regSearchMatch.Value);
				ListViewItem listViewItem = lstValues.Items[regValueName];
				if (listViewItem != null)
				{
					listViewItem.Selected = true;
					lstValues.Focus();
				}
			}
		}

		private bool JumpToKey(string key)
		{
			tabControl1.SelectedTab = tbExplorer;
			string[] array = key.Split('\\');
			TreeNode treeNode = tvwKeys.Nodes[array[0]];
			if (treeNode == null)
			{
				return false;
			}
			SelectAndExpand(treeNode);
			StringBuilder stringBuilder = new StringBuilder(treeNode.Name);
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.Append('\\');
				stringBuilder.Append(array[i]);
				treeNode = treeNode.Nodes[stringBuilder.ToString()];
				if (treeNode == null)
				{
					return false;
				}
				SelectAndExpand(treeNode);
			}
			return true;
		}

		private void SelectAndExpand(TreeNode node)
		{
			node.EnsureVisible();
			tvwKeys.SelectedNode = node;
			node.Expand();
		}

		private void txtPattern_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnFind.PerformClick();
				e.SuppressKeyPress = true;
			}
		}

		private void lstValues_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode != Keys.Delete || !GetDeleteMenuState()) && e.KeyCode == Keys.Return)
			{
				OnValueEditAction();
			}
		}

		private void OnValueEditAction()
		{
			if (lstValues.SelectedItems.Count != 1)
			{
				return;
			}
			RegValue regValue = (RegValue)lstValues.SelectedItems[0].SubItems[2].Tag;
			if (regValue.ParentKey != null)
			{
				ValueEditor valueEditor = null;
				switch (regValue.Kind)
				{
				case RegistryValueKind.Binary:
					valueEditor = new BinaryEditor(regValue);
					break;
				case RegistryValueKind.MultiString:
					valueEditor = new MultiStringEditor(regValue);
					break;
				case RegistryValueKind.DWord:
				case RegistryValueKind.QWord:
					valueEditor = new DWordEditor(regValue);
					break;
				case RegistryValueKind.String:
				case RegistryValueKind.ExpandString:
					valueEditor = new StringEditor(regValue);
					break;
				}
				if (valueEditor != null && valueEditor.ShowDialog(this) == DialogResult.OK)
				{
					RefreshValues();
				}
			}
		}

		private void lstResults_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void jumpToKeyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			JumpToKeyDialog jumpToKeyDialog = new JumpToKeyDialog();
			if (jumpToKeyDialog.ShowDialog() == DialogResult.OK && !JumpToKey(jumpToKeyDialog.txtKeyPath.Text))
			{
				UIUtility.DisplayError(this, Resources.Error_InvalidKey);
			}
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnExportAction();
		}

		private void OnExportAction()
		{
			string selectedKey = GetSelectedKey();
			ExportDialog exportDialog = new ExportDialog();
			exportDialog.cmbBranch.Text = selectedKey;
			exportDialog.ShowDialog(this);
		}

		private void MDCWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			launchBrowser("http://metadataconsulting.ca/bio");
		}

		private void lstValues_DoubleClick(object sender, EventArgs e)
		{
			OnValueEditAction();
		}

		private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnValueEditAction();
		}

		private void keyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnNewKeyAction();
		}

		private void OnNewKeyAction()
		{
			if (tvwKeys.SelectedNode != null)
			{
				if (tvwKeys.HasChildren && !tvwKeys.SelectedNode.IsExpanded)
				{
					tvwKeys.SelectedNode.Expand();
				}
				RegistryKey registryKey = (RegistryKey)tvwKeys.SelectedNode.Tag;
				string newKeyName = RegUtility.GetNewKeyName(registryKey);
				string key = registryKey.Name + "\\" + newKeyName;
				TreeNode treeNode = CreateNode(key, newKeyName, new object());
				tvwKeys.SelectedNode.Nodes.Add(treeNode);
				treeNode.EnsureVisible();
				tvwKeys.LabelEdit = true;
				treeNode.BeginEdit();
			}
		}

		private void tvwKeys_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			tvwKeys.LabelEdit = false;
			string text = ((e.Label == null) ? e.Node.Text : e.Label);
			try
			{
				RegistryKey registryKey = (RegistryKey)e.Node.Parent.Tag;
				RegKey regKey = RegKey.Parse(registryKey.Name, true);
				regKey.Key.CreateSubKey(text);
				e.Node.Name = regKey.Key.Name + "\\" + text;
				e.Node.Tag = RegKey.Parse(e.Node.Name).Key;
			}
			catch
			{
				e.Node.Remove();
				UIUtility.DisplayError(this, Resources.Error_CreateKeyFail);
			}
		}

		private void stringValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnNewValueAction(RegistryValueKind.String);
		}

		private void OnNewValueAction(RegistryValueKind valueKind)
		{
			if (tvwKeys.SelectedNode != null)
			{
				RegistryKey key = (RegistryKey)tvwKeys.SelectedNode.Tag;
				string newValueName = RegUtility.GetNewValueName(key);
				ListViewItem listViewItem = AddValueToList(key, new RegValue(newValueName, valueKind, valueKind.GetDefaultData()));
				lstValues.LabelEdit = true;
				listViewItem.BeginEdit();
			}
		}

		private void binaryValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnNewValueAction(RegistryValueKind.Binary);
		}

		private void dWORDValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnNewValueAction(RegistryValueKind.DWord);
		}

		private void multiStringValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnNewValueAction(RegistryValueKind.MultiString);
		}

		private void expandableStringValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnNewValueAction(RegistryValueKind.ExpandString);
		}

		private void qWORDValuePopupMenuItem_Click(object sender, EventArgs e)
		{
			OnNewValueAction(RegistryValueKind.QWord);
		}

		private void lstValues_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			lstValues.LabelEdit = false;
			ListViewItem listViewItem = lstValues.Items[e.Item];
			string text = ((e.Label == null) ? listViewItem.Text : e.Label);
			try
			{
				RegistryKey registryKey = (RegistryKey)listViewItem.Tag;
				RegistryKey key = RegKey.Parse(registryKey.Name, true).Key;
				RegValue regValue = (RegValue)listViewItem.SubItems[2].Tag;
				key.SetValue(text, regValue.Data, regValue.Kind);
				listViewItem.Name = text;
				listViewItem.SubItems[2].Tag = new RegValue(registryKey, text);
			}
			catch
			{
				listViewItem.Remove();
				UIUtility.DisplayError(this, Resources.Error_CreateValueFail);
			}
		}

		private void favoritesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			removeFavoriteToolStripMenuItem.Enabled = favorites.Count > 0;
			addToFavoritesToolStripMenuItem.Enabled = tvwKeys.SelectedNode != null;
			toolStripMenuSeperatorFavorites.Visible = favorites.Count > 0;
		}

		private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddToFavoritesDialog addToFavoritesDialog = new AddToFavoritesDialog();
			try
			{
				RegistryKey registryKey = (RegistryKey)tvwKeys.SelectedNode.Tag;
				int num = registryKey.Name.LastIndexOf('\\');
				addToFavoritesDialog.txtName.SelectedText = ((num >= 0) ? registryKey.Name.Substring(num + 1) : registryKey.Name);
				if (addToFavoritesDialog.ShowDialog(this) == DialogResult.OK)
				{
					if (favorites.ContainsKey(addToFavoritesDialog.txtName.Text))
					{
						UIUtility.DisplayError(this, Resources.Error_AlreadyFavorite);
						return;
					}
					string value = "My Computer\\" + registryKey.Name;
					Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\Regedit\\Favorites", addToFavoritesDialog.txtName.Text, value);
					favorites[addToFavoritesDialog.txtName.Text] = registryKey.Name;
				}
			}
			catch
			{
				UIUtility.DisplayError(this, Resources.Error_CreateKeyFail);
			}
		}

		private void removeFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RemoveFavoritesDialog removeFavoritesDialog = new RemoveFavoritesDialog(favorites);
			removeFavoritesDialog.ShowDialog(this);
		}

		private void cmbSearch_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cmbSearch.Text == "All Hives")
			{
				txtBranch.Text = string.Empty;
				txtBranch.Enabled = false;
			}
			else
			{
				txtBranch.Enabled = true;
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			launchBrowser("http://metadataconsulting.blogspot.ca/2017/04/Registry-Viewer-App-a-read-only-registry-viewer.html");
		}

		public static void launchBrowser(string url)
		{
			string fileName = "iexplore.exe";
			using (RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("Progid");
					if (value != null)
					{
						if (value.ToString().ToLower().Contains("chrome"))
						{
							fileName = "chrome.exe";
						}
						else if (value.ToString().ToLower().Contains("firefox"))
						{
							fileName = "firefox.exe";
						}
						else if (value.ToString().ToLower().Contains("safari"))
						{
							fileName = "safari.exe";
						}
						else if (value.ToString().ToLower().Contains("opera"))
						{
							fileName = "opera.exe";
						}
					}
				}
			}
			Process.Start(new ProcessStartInfo(fileName, url));
		}

		public static DateTime GetWindowsInstallationDateTime(string computerName)
		{
			RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
			registryKey = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\", false);
			if (registryKey != null)
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
				long num = Convert.ToInt64(registryKey.GetValue("InstallDate").ToString());
				return dateTime.AddSeconds(num);
			}
			return DateTime.MinValue;
		}

		public static DateTime GetWMIWindowsInstallationDateTime()
		{
			DateTime result = default(DateTime);
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Registry");
			foreach (ManagementObject item in managementObjectSearcher.Get())
			{
				try
				{
					if (item["InstallDate"].ToString().Length == 25)
					{
						result = ManagementDateTimeConverter.ToDateTime(item["InstallDate"].ToString());
					}
				}
				catch (Exception)
				{
					result = DateTime.Now;
					continue;
				}
				break;
			}
			return result;
		}

		private static List<KeyValuePair<string, string>> RegistrySettings()
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			try
			{
				string empty = string.Empty;
				string machineName = Environment.MachineName;
				string text = "\\\\.\\ROOT\\cimv2";
				string text2 = "Win32_Registry";
				string s = "";
				string s2 = "";
				string text3 = "";
				ManagementClass managementClass = new ManagementClass(text + ":" + text2);
				foreach (ManagementObject instance in managementClass.GetInstances())
				{
					if (instance["Name"] != null)
					{
						int num = instance["Name"].ToString().IndexOf("|", StringComparison.Ordinal);
						if (num > 0)
						{
							text3 = instance["Name"].ToString().Remove(num, 1).Insert(num, "mounted on ");
						}
						text3 = text3.Replace("|", " (");
						text3 += ")";
						list.Add(new KeyValuePair<string, string>("1.Registry Name", text3));
					}
					if (instance["Status"] != null)
					{
						list.Add(new KeyValuePair<string, string>("2.Registry Status", instance["Status"].ToString()));
					}
					if (instance["CurrentSize"] != null)
					{
						list.Add(new KeyValuePair<string, string>("4.Registry Current Size", instance["CurrentSize"].ToString() + " Mb"));
						s = instance["CurrentSize"].ToString();
					}
					if (instance["MaximumSize"] != null)
					{
						list.Add(new KeyValuePair<string, string>("5.Registry Maximum Size", instance["MaximumSize"].ToString() + " Mb"));
						s2 = instance["MaximumSize"].ToString();
					}
				}
				int result = 0;
				int result2 = 0;
				bool flag = int.TryParse(s, out result);
				bool flag2 = int.TryParse(s2, out result2);
				if (flag && flag2)
				{
					decimal num2 = (1m - (decimal)result / (decimal)result2) * 100m;
					decimal num3 = (decimal)result / (decimal)result2 * 100m;
					list.Add(new KeyValuePair<string, string>("6.Registry Percentage Free", num2.ToString("0.##") + "%"));
					list.Add(new KeyValuePair<string, string>("7.Registry Percentage Used", num3.ToString("0.##") + "%"));
				}
				list.Add(new KeyValuePair<string, string>("3.Registry Install Date", GetWMIWindowsInstallationDateTime().ToString("d-MMM-yyyy h:mm:ss tt")));
				return list;
			}
			catch
			{
				return list;
			}
		}
	}
}
