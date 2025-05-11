using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner.Dialogs
{
  partial class OptionsDialog
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.tabControl1 = new TabControl();
      this.drivesTabPage = new TabPage();
      this.drivesListView = new ListView();
      this.columnHeader3 = ((ColumnHeader)(new ColumnHeader()));
      this.columnHeader4 = ((ColumnHeader)(new ColumnHeader()));
      this.columnHeader5 = ((ColumnHeader)(new ColumnHeader()));
      this.columnHeader6 = ((ColumnHeader)(new ColumnHeader()));
      this.tabPageRemoval = new TabPage();
      this.groupBox2 = new GroupBox();
      this.label1 = new Label();
      this.checkBoxAutoSysRestore = new CheckBox();
      this.groupBox1 = new GroupBox();
      this.radioButtonRecycle = new RadioButton();
      this.buttonBrowse = new Button();
      this.textBoxMoveFolder = new TextBox();
      this.radioButtonRemove = new RadioButton();
      this.radioButtonMove = new RadioButton();
      this.tabPageSearch = new TabPage();
      
      this.groupBox8 = new GroupBox();
      this.checkBoxAutoClean = new CheckBox();
      this.groupBox7 = new GroupBox();
      this.radioButtonFilterAgg = new RadioButton();
      this.radioButtonFilterMed = new RadioButton();
      this.radioButtonFilterSafe = new RadioButton();
      this.label2 = new Label();
      this.textBoxSearchFilters = new TextBox();
      this.groupBox6 = new GroupBox();
      this.checkBoxZeroLength = new CheckBox();
      this.checkBoxWriteProtected = new CheckBox();
      this.tabPageAdvanced = new TabPage();
      this.groupBox5 = new GroupBox();
      this.label6 = new Label();
      this.numericUpDownSizeAtMost = new NumericUpDown();
      this.label7 = new Label();
      this.label5 = new Label();
      this.numericUpDownSizeAtLeast = new NumericUpDown();
      this.label4 = new Label();
      this.checkBoxSize = new CheckBox();
      this.groupBox4 = new GroupBox();
      this.dateTimePickerBefore = new DateTimePicker();
      this.checkBoxFindBefore = new CheckBox();
      this.checkBoxFindAfter = new CheckBox();
      this.dateTimePickerAfter = new DateTimePicker();
      this.radioButtonFindAccessed = new RadioButton();
      this.radioButtonFindModified = new RadioButton();
      this.radioButtonFindCreated = new RadioButton();
      this.groupBox3 = new GroupBox();
      this.checkBoxSystem = new CheckBox();
      this.checkBoxArchive = new CheckBox();
      this.checkBoxReadOnly = new CheckBox();
      this.checkBoxHidden = new CheckBox();
      this.tabPageIncFolders = new TabPage();
      this.buttonIncFoldersAdd = new Button();
      this.buttonIncFoldersRemove = new Button();
      this.listViewIncFolders = new ListView();
      this.columnHeader7 = ((ColumnHeader)(new ColumnHeader()));
      this.tabPageExcludeFolders = new TabPage();
      this.buttonFoldersAdd = new Button();
      this.buttonFoldersRemove = new Button();
      this.listViewExcludeFolders = new ListView();
      this.columnHeader1 = ((ColumnHeader)(new ColumnHeader()));
      this.tabPageExcludeFiles = new TabPage();
      this.buttonFilesAdd = new Button();
      this.buttonFilesRemove = new Button();
      this.listViewFiles = new ListView();
      this.columnHeader2 = ((ColumnHeader)(new ColumnHeader()));
      this.buttonCancel = new Button();
      this.buttonOk = new Button();
      this.tabControl1.SuspendLayout();
      this.drivesTabPage.SuspendLayout();
      this.tabPageRemoval.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPageSearch.SuspendLayout();
      this.groupBox8.SuspendLayout();
      this.groupBox7.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.tabPageAdvanced.SuspendLayout();
      this.groupBox5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeAtMost)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeAtLeast)).BeginInit();
      this.groupBox4.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.tabPageIncFolders.SuspendLayout();
      this.tabPageExcludeFolders.SuspendLayout();
      this.tabPageExcludeFiles.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.drivesTabPage);
      this.tabControl1.Controls.Add(this.tabPageRemoval);
      this.tabControl1.Controls.Add(this.tabPageSearch);
      this.tabControl1.Controls.Add(this.tabPageAdvanced);
      this.tabControl1.Controls.Add(this.tabPageIncFolders);
      this.tabControl1.Controls.Add(this.tabPageExcludeFolders);
      this.tabControl1.Controls.Add(this.tabPageExcludeFiles);
      this.tabControl1.Location = new System.Drawing.Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(346, 238);
      this.tabControl1.TabIndex = 0;
      // 
      // drivesTabPage
      // 
      this.drivesTabPage.Controls.Add(this.drivesListView);
      this.drivesTabPage.Location = new System.Drawing.Point(4, 22);
      this.drivesTabPage.Name = "drivesTabPage";
      this.drivesTabPage.Size = new System.Drawing.Size(338, 212);
      this.drivesTabPage.TabIndex = 6;
      this.drivesTabPage.Text = "Drives";
      this.drivesTabPage.UseVisualStyleBackColor = true;
      // 
      // drivesListView
      // 
      this.drivesListView.CheckBoxes = true;
      this.drivesListView.Columns.AddRange(new ColumnHeader[] {
      this.columnHeader3,
      this.columnHeader4,
      this.columnHeader5,
      this.columnHeader6});
      this.drivesListView.Dock = DockStyle.Fill;
      this.drivesListView.Location = new System.Drawing.Point(0, 0);
      this.drivesListView.Name = "drivesListView";
      this.drivesListView.Size = new System.Drawing.Size(338, 212);
      this.drivesListView.TabIndex = 1;
      this.drivesListView.UseCompatibleStateImageBehavior = false;
      this.drivesListView.View = View.Details;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Drive";
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Drive Format";
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Capacity";
      this.columnHeader5.Width = 81;
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "Free Space";
      this.columnHeader6.Width = 83;
      // 
      // tabPageRemoval
      // 
      this.tabPageRemoval.Controls.Add(this.groupBox2);
      this.tabPageRemoval.Controls.Add(this.groupBox1);
      this.tabPageRemoval.Location = new System.Drawing.Point(4, 22);
      this.tabPageRemoval.Name = "tabPageRemoval";
      this.tabPageRemoval.Padding = new Padding(3);
      this.tabPageRemoval.Size = new System.Drawing.Size(338, 212);
      this.tabPageRemoval.TabIndex = 3;
      this.tabPageRemoval.Text = "Removal";
      this.tabPageRemoval.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.checkBoxAutoSysRestore);
      this.groupBox2.Location = new System.Drawing.Point(6, 131);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(326, 75);
      this.groupBox2.TabIndex = 8;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "System Restore";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 39);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(298, 26);
      this.label1.TabIndex = 1;
      this.label1.Text = "This will backup essential system files in order to prevent your \r\ncomputer from " +
          "crashing. ";
      // 
      // checkBoxAutoSysRestore
      // 
      this.checkBoxAutoSysRestore.AutoSize = true;
      this.checkBoxAutoSysRestore.Location = new System.Drawing.Point(6, 19);
      this.checkBoxAutoSysRestore.Name = "checkBoxAutoSysRestore";
      this.checkBoxAutoSysRestore.Size = new System.Drawing.Size(263, 17);
      this.checkBoxAutoSysRestore.TabIndex = 0;
      this.checkBoxAutoSysRestore.Text = "Create restore points automatically (recommended)";
      this.checkBoxAutoSysRestore.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.radioButtonRecycle);
      this.groupBox1.Controls.Add(this.buttonBrowse);
      this.groupBox1.Controls.Add(this.textBoxMoveFolder);
      this.groupBox1.Controls.Add(this.radioButtonRemove);
      this.groupBox1.Controls.Add(this.radioButtonMove);
      this.groupBox1.Location = new System.Drawing.Point(6, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(326, 119);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select the mode of removing files from the disk";
      // 
      // radioButtonRecycle
      // 
      this.radioButtonRecycle.AutoSize = true;
      this.radioButtonRecycle.Location = new System.Drawing.Point(6, 42);
      this.radioButtonRecycle.Name = "radioButtonRecycle";
      this.radioButtonRecycle.Size = new System.Drawing.Size(160, 17);
      this.radioButtonRecycle.TabIndex = 2;
      this.radioButtonRecycle.TabStop = true;
      this.radioButtonRecycle.Text = "Send junk files to recycle bin";
      this.radioButtonRecycle.UseVisualStyleBackColor = true;
      // 
      // buttonBrowse
      // 
      this.buttonBrowse.Location = new System.Drawing.Point(290, 88);
      this.buttonBrowse.Name = "buttonBrowse";
      this.buttonBrowse.Size = new System.Drawing.Size(30, 23);
      this.buttonBrowse.TabIndex = 6;
      this.buttonBrowse.Text = "...";
      this.buttonBrowse.UseVisualStyleBackColor = true;
      // 
      // textBoxMoveFolder
      // 
      this.textBoxMoveFolder.Location = new System.Drawing.Point(6, 88);
      this.textBoxMoveFolder.Name = "textBoxMoveFolder";
      this.textBoxMoveFolder.ReadOnly = true;
      this.textBoxMoveFolder.Size = new System.Drawing.Size(278, 20);
      this.textBoxMoveFolder.TabIndex = 5;
      // 
      // radioButtonRemove
      // 
      this.radioButtonRemove.AutoSize = true;
      this.radioButtonRemove.Location = new System.Drawing.Point(6, 19);
      this.radioButtonRemove.Name = "radioButtonRemove";
      this.radioButtonRemove.Size = new System.Drawing.Size(276, 17);
      this.radioButtonRemove.TabIndex = 3;
      this.radioButtonRemove.TabStop = true;
      this.radioButtonRemove.Text = "Remove the junk files from hard drive (recommended)";
      this.radioButtonRemove.UseVisualStyleBackColor = true;
      // 
      // radioButtonMove
      // 
      this.radioButtonMove.AutoSize = true;
      this.radioButtonMove.Location = new System.Drawing.Point(6, 65);
      this.radioButtonMove.Name = "radioButtonMove";
      this.radioButtonMove.Size = new System.Drawing.Size(177, 17);
      this.radioButtonMove.TabIndex = 4;
      this.radioButtonMove.TabStop = true;
      this.radioButtonMove.Text = "Move files to the specified folder";
      this.radioButtonMove.UseVisualStyleBackColor = true;
      // 
      // tabPageSearch
      // 
      this.tabPageSearch.Controls.Add(this.groupBox8);
      this.tabPageSearch.Controls.Add(this.groupBox7);
      this.tabPageSearch.Controls.Add(this.groupBox6);
      this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
      this.tabPageSearch.Name = "tabPageSearch";
      this.tabPageSearch.Padding = new Padding(3);
      this.tabPageSearch.Size = new System.Drawing.Size(338, 212);
      this.tabPageSearch.TabIndex = 4;
      this.tabPageSearch.Text = "Searching";
      this.tabPageSearch.UseVisualStyleBackColor = true;
      // 
      // groupBox8
      // 
      this.groupBox8.Controls.Add(this.checkBoxAutoClean);
      this.groupBox8.Location = new System.Drawing.Point(6, 149);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new System.Drawing.Size(326, 41);
      this.groupBox8.TabIndex = 2;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "Power Users";
      // 
      // checkBoxAutoClean
      // 
      this.checkBoxAutoClean.AutoSize = true;
      this.checkBoxAutoClean.Location = new System.Drawing.Point(6, 19);
      this.checkBoxAutoClean.Name = "checkBoxAutoClean";
      this.checkBoxAutoClean.Size = new System.Drawing.Size(311, 17);
      this.checkBoxAutoClean.TabIndex = 0;
      this.checkBoxAutoClean.Text = "Automatically remove files after scanning (not recommended)";
      this.checkBoxAutoClean.UseVisualStyleBackColor = true;
      // 
      // groupBox7
      // 
      this.groupBox7.Controls.Add(this.radioButtonFilterAgg);
      this.groupBox7.Controls.Add(this.radioButtonFilterMed);
      this.groupBox7.Controls.Add(this.radioButtonFilterSafe);
      this.groupBox7.Controls.Add(this.label2);
      this.groupBox7.Controls.Add(this.textBoxSearchFilters);
      this.groupBox7.Location = new System.Drawing.Point(6, 78);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new System.Drawing.Size(326, 65);
      this.groupBox7.TabIndex = 1;
      this.groupBox7.TabStop = false;
      this.groupBox7.Text = "Search Filters";
      // 
      // radioButtonFilterAgg
      // 
      this.radioButtonFilterAgg.AutoSize = true;
      this.radioButtonFilterAgg.Location = new System.Drawing.Point(153, 40);
      this.radioButtonFilterAgg.Name = "radioButtonFilterAgg";
      this.radioButtonFilterAgg.Size = new System.Drawing.Size(77, 17);
      this.radioButtonFilterAgg.TabIndex = 12;
      this.radioButtonFilterAgg.TabStop = true;
      this.radioButtonFilterAgg.Text = "Aggressive";
      this.radioButtonFilterAgg.UseVisualStyleBackColor = true;
      this.radioButtonFilterAgg.CheckedChanged += new System.EventHandler(this.FilterRadioChanged);
      // 
      // radioButtonFilterMed
      // 
      this.radioButtonFilterMed.AutoSize = true;
      this.radioButtonFilterMed.Location = new System.Drawing.Point(94, 40);
      this.radioButtonFilterMed.Name = "radioButtonFilterMed";
      this.radioButtonFilterMed.Size = new System.Drawing.Size(62, 17);
      this.radioButtonFilterMed.TabIndex = 11;
      this.radioButtonFilterMed.TabStop = true;
      this.radioButtonFilterMed.Text = "Medium";
      this.radioButtonFilterMed.UseVisualStyleBackColor = true;
      this.radioButtonFilterMed.CheckedChanged += new System.EventHandler(this.FilterRadioChanged);
      // 
      // radioButtonFilterSafe
      // 
      this.radioButtonFilterSafe.AutoSize = true;
      this.radioButtonFilterSafe.Location = new System.Drawing.Point(49, 40);
      this.radioButtonFilterSafe.Name = "radioButtonFilterSafe";
      this.radioButtonFilterSafe.Size = new System.Drawing.Size(47, 17);
      this.radioButtonFilterSafe.TabIndex = 10;
      this.radioButtonFilterSafe.TabStop = true;
      this.radioButtonFilterSafe.Text = "Safe";
      this.radioButtonFilterSafe.UseVisualStyleBackColor = true;
      this.radioButtonFilterSafe.CheckedChanged += new System.EventHandler(this.FilterRadioChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 42);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(37, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Mode:";
      // 
      // textBoxSearchFilters
      // 
      this.textBoxSearchFilters.Location = new System.Drawing.Point(6, 19);
      this.textBoxSearchFilters.Name = "textBoxSearchFilters";
      this.textBoxSearchFilters.Size = new System.Drawing.Size(314, 20);
      this.textBoxSearchFilters.TabIndex = 8;
      // 
      // groupBox6
      // 
      this.groupBox6.Controls.Add(this.checkBoxZeroLength);
      this.groupBox6.Controls.Add(this.checkBoxWriteProtected);
      this.groupBox6.Location = new System.Drawing.Point(6, 6);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new System.Drawing.Size(326, 66);
      this.groupBox6.TabIndex = 0;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Search Options";
      // 
      // checkBoxZeroLength
      // 
      this.checkBoxZeroLength.AutoSize = true;
      this.checkBoxZeroLength.Location = new System.Drawing.Point(6, 42);
      this.checkBoxZeroLength.Name = "checkBoxZeroLength";
      this.checkBoxZeroLength.Size = new System.Drawing.Size(215, 17);
      this.checkBoxZeroLength.TabIndex = 1;
      this.checkBoxZeroLength.Text = "Assume zero length files as junk (slower)";
      this.checkBoxZeroLength.UseVisualStyleBackColor = true;
      // 
      // checkBoxWriteProtected
      // 
      this.checkBoxWriteProtected.AutoSize = true;
      this.checkBoxWriteProtected.Location = new System.Drawing.Point(6, 19);
      this.checkBoxWriteProtected.Name = "checkBoxWriteProtected";
      this.checkBoxWriteProtected.Size = new System.Drawing.Size(278, 17);
      this.checkBoxWriteProtected.TabIndex = 0;
      this.checkBoxWriteProtected.Text = "Ignore write protected and files in use (recommended)";
      this.checkBoxWriteProtected.UseVisualStyleBackColor = true;
      // 
      // tabPageAdvanced
      // 
      this.tabPageAdvanced.Controls.Add(this.groupBox5);
      this.tabPageAdvanced.Controls.Add(this.groupBox4);
      this.tabPageAdvanced.Controls.Add(this.groupBox3);
      this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
      this.tabPageAdvanced.Name = "tabPageAdvanced";
      this.tabPageAdvanced.Padding = new Padding(3);
      this.tabPageAdvanced.Size = new System.Drawing.Size(338, 212);
      this.tabPageAdvanced.TabIndex = 0;
      this.tabPageAdvanced.Text = "Advanced";
      this.tabPageAdvanced.UseVisualStyleBackColor = true;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.label6);
      this.groupBox5.Controls.Add(this.numericUpDownSizeAtMost);
      this.groupBox5.Controls.Add(this.label7);
      this.groupBox5.Controls.Add(this.label5);
      this.groupBox5.Controls.Add(this.numericUpDownSizeAtLeast);
      this.groupBox5.Controls.Add(this.label4);
      this.groupBox5.Controls.Add(this.checkBoxSize);
      this.groupBox5.Location = new System.Drawing.Point(6, 126);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(326, 42);
      this.groupBox5.TabIndex = 6;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Size";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(299, 20);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(21, 13);
      this.label6.TabIndex = 6;
      this.label6.Text = "KB";
      // 
      // numericUpDownSizeAtMost
      // 
      this.numericUpDownSizeAtMost.Location = new System.Drawing.Point(250, 16);
      this.numericUpDownSizeAtMost.Name = "numericUpDownSizeAtMost";
      this.numericUpDownSizeAtMost.Size = new System.Drawing.Size(48, 20);
      this.numericUpDownSizeAtMost.TabIndex = 5;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(204, 20);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(45, 13);
      this.label7.TabIndex = 4;
      this.label7.Text = "At most:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(149, 20);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(21, 13);
      this.label5.TabIndex = 3;
      this.label5.Text = "KB";
      // 
      // numericUpDownSizeAtLeast
      // 
      this.numericUpDownSizeAtLeast.Location = new System.Drawing.Point(100, 16);
      this.numericUpDownSizeAtLeast.Name = "numericUpDownSizeAtLeast";
      this.numericUpDownSizeAtLeast.Size = new System.Drawing.Size(48, 20);
      this.numericUpDownSizeAtLeast.TabIndex = 2;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(54, 20);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(45, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "At least:";
      // 
      // checkBoxSize
      // 
      this.checkBoxSize.AutoSize = true;
      this.checkBoxSize.Location = new System.Drawing.Point(6, 19);
      this.checkBoxSize.Name = "checkBoxSize";
      this.checkBoxSize.Size = new System.Drawing.Size(49, 17);
      this.checkBoxSize.TabIndex = 0;
      this.checkBoxSize.Text = "Size:";
      this.checkBoxSize.UseVisualStyleBackColor = true;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.dateTimePickerBefore);
      this.groupBox4.Controls.Add(this.checkBoxFindBefore);
      this.groupBox4.Controls.Add(this.checkBoxFindAfter);
      this.groupBox4.Controls.Add(this.dateTimePickerAfter);
      this.groupBox4.Controls.Add(this.radioButtonFindAccessed);
      this.groupBox4.Controls.Add(this.radioButtonFindModified);
      this.groupBox4.Controls.Add(this.radioButtonFindCreated);
      this.groupBox4.Location = new System.Drawing.Point(6, 53);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(326, 67);
      this.groupBox4.TabIndex = 5;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Find Files";
      // 
      // dateTimePickerBefore
      // 
      this.dateTimePickerBefore.Checked = false;
      this.dateTimePickerBefore.Format = DateTimePickerFormat.Short;
      this.dateTimePickerBefore.Location = new System.Drawing.Point(235, 38);
      this.dateTimePickerBefore.Name = "dateTimePickerBefore";
      this.dateTimePickerBefore.ShowUpDown = true;
      this.dateTimePickerBefore.Size = new System.Drawing.Size(85, 20);
      this.dateTimePickerBefore.TabIndex = 7;
      // 
      // checkBoxFindBefore
      // 
      this.checkBoxFindBefore.AutoSize = true;
      this.checkBoxFindBefore.Location = new System.Drawing.Point(181, 42);
      this.checkBoxFindBefore.Name = "checkBoxFindBefore";
      this.checkBoxFindBefore.Size = new System.Drawing.Size(57, 17);
      this.checkBoxFindBefore.TabIndex = 6;
      this.checkBoxFindBefore.Text = "Before";
      this.checkBoxFindBefore.UseVisualStyleBackColor = true;
      // 
      // checkBoxFindAfter
      // 
      this.checkBoxFindAfter.AutoSize = true;
      this.checkBoxFindAfter.Location = new System.Drawing.Point(6, 41);
      this.checkBoxFindAfter.Name = "checkBoxFindAfter";
      this.checkBoxFindAfter.Size = new System.Drawing.Size(48, 17);
      this.checkBoxFindAfter.TabIndex = 5;
      this.checkBoxFindAfter.Text = "After";
      this.checkBoxFindAfter.UseVisualStyleBackColor = true;
      // 
      // dateTimePickerAfter
      // 
      this.dateTimePickerAfter.Checked = false;
      this.dateTimePickerAfter.Format = DateTimePickerFormat.Short;
      this.dateTimePickerAfter.Location = new System.Drawing.Point(54, 39);
      this.dateTimePickerAfter.Name = "dateTimePickerAfter";
      this.dateTimePickerAfter.ShowUpDown = true;
      this.dateTimePickerAfter.Size = new System.Drawing.Size(85, 20);
      this.dateTimePickerAfter.TabIndex = 4;
      // 
      // radioButtonFindAccessed
      // 
      this.radioButtonFindAccessed.AutoSize = true;
      this.radioButtonFindAccessed.Location = new System.Drawing.Point(145, 19);
      this.radioButtonFindAccessed.Name = "radioButtonFindAccessed";
      this.radioButtonFindAccessed.Size = new System.Drawing.Size(72, 17);
      this.radioButtonFindAccessed.TabIndex = 2;
      this.radioButtonFindAccessed.TabStop = true;
      this.radioButtonFindAccessed.Text = "Accessed";
      this.radioButtonFindAccessed.UseVisualStyleBackColor = true;
      // 
      // radioButtonFindModified
      // 
      this.radioButtonFindModified.AutoSize = true;
      this.radioButtonFindModified.Location = new System.Drawing.Point(74, 19);
      this.radioButtonFindModified.Name = "radioButtonFindModified";
      this.radioButtonFindModified.Size = new System.Drawing.Size(65, 17);
      this.radioButtonFindModified.TabIndex = 1;
      this.radioButtonFindModified.TabStop = true;
      this.radioButtonFindModified.Text = "Modified";
      this.radioButtonFindModified.UseVisualStyleBackColor = true;
      // 
      // radioButtonFindCreated
      // 
      this.radioButtonFindCreated.AutoSize = true;
      this.radioButtonFindCreated.Location = new System.Drawing.Point(6, 19);
      this.radioButtonFindCreated.Name = "radioButtonFindCreated";
      this.radioButtonFindCreated.Size = new System.Drawing.Size(62, 17);
      this.radioButtonFindCreated.TabIndex = 0;
      this.radioButtonFindCreated.TabStop = true;
      this.radioButtonFindCreated.Text = "Created";
      this.radioButtonFindCreated.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.checkBoxSystem);
      this.groupBox3.Controls.Add(this.checkBoxArchive);
      this.groupBox3.Controls.Add(this.checkBoxReadOnly);
      this.groupBox3.Controls.Add(this.checkBoxHidden);
      this.groupBox3.Location = new System.Drawing.Point(6, 3);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(326, 44);
      this.groupBox3.TabIndex = 4;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Attributes";
      // 
      // checkBoxSystem
      // 
      this.checkBoxSystem.AutoSize = true;
      this.checkBoxSystem.Location = new System.Drawing.Point(219, 19);
      this.checkBoxSystem.Name = "checkBoxSystem";
      this.checkBoxSystem.Size = new System.Drawing.Size(60, 17);
      this.checkBoxSystem.TabIndex = 3;
      this.checkBoxSystem.Text = "System";
      this.checkBoxSystem.UseVisualStyleBackColor = true;
      // 
      // checkBoxArchive
      // 
      this.checkBoxArchive.AutoSize = true;
      this.checkBoxArchive.Location = new System.Drawing.Point(154, 19);
      this.checkBoxArchive.Name = "checkBoxArchive";
      this.checkBoxArchive.Size = new System.Drawing.Size(62, 17);
      this.checkBoxArchive.TabIndex = 2;
      this.checkBoxArchive.Text = "Archive";
      this.checkBoxArchive.UseVisualStyleBackColor = true;
      // 
      // checkBoxReadOnly
      // 
      this.checkBoxReadOnly.AutoSize = true;
      this.checkBoxReadOnly.Location = new System.Drawing.Point(72, 19);
      this.checkBoxReadOnly.Name = "checkBoxReadOnly";
      this.checkBoxReadOnly.Size = new System.Drawing.Size(76, 17);
      this.checkBoxReadOnly.TabIndex = 1;
      this.checkBoxReadOnly.Text = "Read Only";
      this.checkBoxReadOnly.UseVisualStyleBackColor = true;
      // 
      // checkBoxHidden
      // 
      this.checkBoxHidden.AutoSize = true;
      this.checkBoxHidden.Location = new System.Drawing.Point(6, 19);
      this.checkBoxHidden.Name = "checkBoxHidden";
      this.checkBoxHidden.Size = new System.Drawing.Size(60, 17);
      this.checkBoxHidden.TabIndex = 0;
      this.checkBoxHidden.Text = "Hidden";
      this.checkBoxHidden.UseVisualStyleBackColor = true;
      // 
      // tabPageIncFolders
      // 
      this.tabPageIncFolders.Controls.Add(this.buttonIncFoldersAdd);
      this.tabPageIncFolders.Controls.Add(this.buttonIncFoldersRemove);
      this.tabPageIncFolders.Controls.Add(this.listViewIncFolders);
      this.tabPageIncFolders.Location = new System.Drawing.Point(4, 22);
      this.tabPageIncFolders.Name = "tabPageIncFolders";
      this.tabPageIncFolders.Size = new System.Drawing.Size(338, 212);
      this.tabPageIncFolders.TabIndex = 7;
      this.tabPageIncFolders.Text = "Included Folders";
      this.tabPageIncFolders.UseVisualStyleBackColor = true;
      // 
      // buttonIncFoldersAdd
      // 
      this.buttonIncFoldersAdd.Location = new System.Drawing.Point(233, 186);
      this.buttonIncFoldersAdd.Name = "buttonIncFoldersAdd";
      this.buttonIncFoldersAdd.Size = new System.Drawing.Size(37, 23);
      this.buttonIncFoldersAdd.TabIndex = 2;
      this.buttonIncFoldersAdd.Text = "Add";
      this.buttonIncFoldersAdd.UseVisualStyleBackColor = true;
      this.buttonIncFoldersAdd.Click += new System.EventHandler(this.buttonIncFoldersAdd_Click);
      // 
      // buttonIncFoldersRemove
      // 
      this.buttonIncFoldersRemove.Location = new System.Drawing.Point(276, 186);
      this.buttonIncFoldersRemove.Name = "buttonIncFoldersRemove";
      this.buttonIncFoldersRemove.Size = new System.Drawing.Size(59, 23);
      this.buttonIncFoldersRemove.TabIndex = 1;
      this.buttonIncFoldersRemove.Text = "Remove";
      this.buttonIncFoldersRemove.UseVisualStyleBackColor = true;
      this.buttonIncFoldersRemove.Click += new System.EventHandler(this.buttonIncFoldersRemove_Click);
      // 
      // listViewIncFolders
      // 
      this.listViewIncFolders.Columns.AddRange(new ColumnHeader[] {
      this.columnHeader7});
      this.listViewIncFolders.LabelEdit = true;
      this.listViewIncFolders.Location = new System.Drawing.Point(3, 3);
      this.listViewIncFolders.Name = "listViewIncFolders";
      this.listViewIncFolders.Size = new System.Drawing.Size(332, 177);
      this.listViewIncFolders.TabIndex = 0;
      this.listViewIncFolders.UseCompatibleStateImageBehavior = false;
      this.listViewIncFolders.View = View.Details;
      // 
      // columnHeader7
      // 
      this.columnHeader7.Text = "List of folders to include";
      this.columnHeader7.Width = 129;
      // 
      // tabPageExcludeFolders
      // 
      this.tabPageExcludeFolders.Controls.Add(this.buttonFoldersAdd);
      this.tabPageExcludeFolders.Controls.Add(this.buttonFoldersRemove);
      this.tabPageExcludeFolders.Controls.Add(this.listViewExcludeFolders);
      this.tabPageExcludeFolders.Location = new System.Drawing.Point(4, 22);
      this.tabPageExcludeFolders.Name = "tabPageExcludeFolders";
      this.tabPageExcludeFolders.Size = new System.Drawing.Size(338, 212);
      this.tabPageExcludeFolders.TabIndex = 2;
      this.tabPageExcludeFolders.Text = "Excluded Folders";
      this.tabPageExcludeFolders.UseVisualStyleBackColor = true;
      // 
      // buttonFoldersAdd
      // 
      this.buttonFoldersAdd.Location = new System.Drawing.Point(234, 186);
      this.buttonFoldersAdd.Name = "buttonFoldersAdd";
      this.buttonFoldersAdd.Size = new System.Drawing.Size(38, 23);
      this.buttonFoldersAdd.TabIndex = 2;
      this.buttonFoldersAdd.Text = "Add";
      this.buttonFoldersAdd.UseVisualStyleBackColor = true;
      this.buttonFoldersAdd.Click += new System.EventHandler(this.buttonFoldersAdd_Click);
      // 
      // buttonFoldersRemove
      // 
      this.buttonFoldersRemove.Location = new System.Drawing.Point(278, 186);
      this.buttonFoldersRemove.Name = "buttonFoldersRemove";
      this.buttonFoldersRemove.Size = new System.Drawing.Size(57, 23);
      this.buttonFoldersRemove.TabIndex = 1;
      this.buttonFoldersRemove.Text = "Remove";
      this.buttonFoldersRemove.UseVisualStyleBackColor = true;
      this.buttonFoldersRemove.Click += new System.EventHandler(this.buttonFoldersRemove_Click);
      // 
      // listViewExcludeFolders
      // 
      this.listViewExcludeFolders.Columns.AddRange(new ColumnHeader[] {
      this.columnHeader1});
      this.listViewExcludeFolders.LabelEdit = true;
      this.listViewExcludeFolders.Location = new System.Drawing.Point(3, 3);
      this.listViewExcludeFolders.MultiSelect = false;
      this.listViewExcludeFolders.Name = "listViewExcludeFolders";
      this.listViewExcludeFolders.Size = new System.Drawing.Size(332, 177);
      this.listViewExcludeFolders.TabIndex = 0;
      this.listViewExcludeFolders.UseCompatibleStateImageBehavior = false;
      this.listViewExcludeFolders.View = View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "List of folders to exclude";
      this.columnHeader1.Width = 133;
      // 
      // tabPageExcludeFiles
      // 
      this.tabPageExcludeFiles.Controls.Add(this.buttonFilesAdd);
      this.tabPageExcludeFiles.Controls.Add(this.buttonFilesRemove);
      this.tabPageExcludeFiles.Controls.Add(this.listViewFiles);
      this.tabPageExcludeFiles.Location = new System.Drawing.Point(4, 22);
      this.tabPageExcludeFiles.Name = "tabPageExcludeFiles";
      this.tabPageExcludeFiles.Size = new System.Drawing.Size(338, 212);
      this.tabPageExcludeFiles.TabIndex = 5;
      this.tabPageExcludeFiles.Text = "Excluded Files";
      this.tabPageExcludeFiles.UseVisualStyleBackColor = true;
      // 
      // buttonFilesAdd
      // 
      this.buttonFilesAdd.Location = new System.Drawing.Point(234, 186);
      this.buttonFilesAdd.Name = "buttonFilesAdd";
      this.buttonFilesAdd.Size = new System.Drawing.Size(38, 23);
      this.buttonFilesAdd.TabIndex = 2;
      this.buttonFilesAdd.Text = "Add";
      this.buttonFilesAdd.UseVisualStyleBackColor = true;
      this.buttonFilesAdd.Click += new System.EventHandler(this.buttonFilesAdd_Click);
      // 
      // buttonFilesRemove
      // 
      this.buttonFilesRemove.Location = new System.Drawing.Point(278, 186);
      this.buttonFilesRemove.Name = "buttonFilesRemove";
      this.buttonFilesRemove.Size = new System.Drawing.Size(57, 23);
      this.buttonFilesRemove.TabIndex = 1;
      this.buttonFilesRemove.Text = "Remove";
      this.buttonFilesRemove.UseVisualStyleBackColor = true;
      this.buttonFilesRemove.Click += new System.EventHandler(this.buttonFilesRemove_Click);
      // 
      // listViewFiles
      // 
      this.listViewFiles.Columns.AddRange(new ColumnHeader[] {
      this.columnHeader2});
      this.listViewFiles.LabelEdit = true;
      this.listViewFiles.Location = new System.Drawing.Point(3, 3);
      this.listViewFiles.MultiSelect = false;
      this.listViewFiles.Name = "listViewFiles";
      this.listViewFiles.Size = new System.Drawing.Size(332, 177);
      this.listViewFiles.TabIndex = 0;
      this.listViewFiles.UseCompatibleStateImageBehavior = false;
      this.listViewFiles.View = View.Details;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "List of files to exclude";
      this.columnHeader2.Width = 120;
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(307, 256);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(51, 23);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // buttonOk
      // 
      this.buttonOk.Location = new System.Drawing.Point(263, 256);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new System.Drawing.Size(38, 23);
      this.buttonOk.TabIndex = 2;
      this.buttonOk.Text = "OK";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
      // 
      // Options
      // 
      this.AcceptButton = this.buttonOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(370, 284);
      this.Controls.Add(this.buttonOk);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.tabControl1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Options";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Little Disk Cleaner - Options";
      this.Load += new System.EventHandler(this.Options_Load);
      this.tabControl1.ResumeLayout(false);
      this.drivesTabPage.ResumeLayout(false);
      this.tabPageRemoval.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabPageSearch.ResumeLayout(false);
      this.groupBox8.ResumeLayout(false);
      this.groupBox8.PerformLayout();
      this.groupBox7.ResumeLayout(false);
      this.groupBox7.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.tabPageAdvanced.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeAtMost)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeAtLeast)).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.tabPageIncFolders.ResumeLayout(false);
      this.tabPageExcludeFolders.ResumeLayout(false);
      this.tabPageExcludeFiles.ResumeLayout(false);
      this.ResumeLayout(false);
    }
    /************************************************/
    private TabControl tabControl1 = null;
    private TabPage drivesTabPage = null;
    private ListView drivesListView = null;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private TabPage tabPageRemoval;
    private GroupBox groupBox2;
    private Label label1;
    private CheckBox checkBoxAutoSysRestore;
    private GroupBox groupBox1;
    private RadioButton radioButtonRecycle;
    private Button buttonBrowse;
    private TextBox textBoxMoveFolder;
    private RadioButton radioButtonRemove;
    private RadioButton radioButtonMove;
    private TabPage tabPageSearch;
    private GroupBox groupBox8;
    private CheckBox checkBoxAutoClean;
    private GroupBox groupBox7;
    private RadioButton radioButtonFilterAgg;
    private RadioButton radioButtonFilterMed;
    private RadioButton radioButtonFilterSafe;
    private Label label2;
    private TextBox textBoxSearchFilters;
    private GroupBox groupBox6;
    private CheckBox checkBoxZeroLength;
    private CheckBox checkBoxWriteProtected;
    private TabPage tabPageAdvanced;
    private GroupBox groupBox5;
    private Label label6;
    private NumericUpDown numericUpDownSizeAtMost;
    private Label label7;
    private Label label5;
    private NumericUpDown numericUpDownSizeAtLeast;
    private Label label4;
    private CheckBox checkBoxSize;
    private GroupBox groupBox4;
    private DateTimePicker dateTimePickerBefore;
    private CheckBox checkBoxFindBefore;
    private CheckBox checkBoxFindAfter;
    private DateTimePicker dateTimePickerAfter;
    private RadioButton radioButtonFindAccessed;
    private RadioButton radioButtonFindModified;
    private RadioButton radioButtonFindCreated;
    private GroupBox groupBox3;
    private CheckBox checkBoxSystem;
    private CheckBox checkBoxArchive;
    private CheckBox checkBoxReadOnly;
    private CheckBox checkBoxHidden;
    private TabPage tabPageIncFolders;
    private Button buttonIncFoldersAdd;
    private Button buttonIncFoldersRemove;
    private ListView listViewIncFolders;
    private ColumnHeader columnHeader7;
    private TabPage tabPageExcludeFolders;
    private Button buttonFoldersAdd;
    private Button buttonFoldersRemove;
    private ListView listViewExcludeFolders;
    private ColumnHeader columnHeader1;
    private TabPage tabPageExcludeFiles;
    private Button buttonFilesAdd;
    private Button buttonFilesRemove;
    private ListView listViewFiles;
    private ColumnHeader columnHeader2;
    private Button buttonOk;
    private Button buttonCancel;
  }
}