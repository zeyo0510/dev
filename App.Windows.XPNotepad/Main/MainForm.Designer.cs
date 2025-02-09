using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;
using App.Windows.XPNotepad.Core;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    private IContainer components = null;
    /************************************************/
    private Timer guiTimer = null;
    /************************************************/
    private MainMenu topMainMenu        = null;
    private MenuItem fileMenuItem       = null;
    private MenuItem newMenuItem        = null;
    private MenuItem openMenuItem       = null;
    private MenuItem saveMenuItem       = null;
    private MenuItem saveasMenuItem     = null;
    private MenuItem pagesetupMenuItem  = null;
    private MenuItem printMenuItem      = null;
    private MenuItem exitMenuItem       = null;
    private MenuItem editMenuItem       = null;
    private MenuItem undoMenuItem       = null;
    private MenuItem cutMenuItem        = null;
    private MenuItem copyMenuItem       = null;
    private MenuItem pasteMenuItem      = null;
    private MenuItem deleteMenuItem     = null;
    private MenuItem findMenuItem       = null;
    private MenuItem findnextMenuItem   = null;
    private MenuItem replaceMenuItem    = null;
    private MenuItem gotoMenuItem       = null;
    private MenuItem selectallMenuItem  = null;
    private MenuItem timedateMenuItem   = null;
    private MenuItem formatMenuItem     = null;
    private MenuItem wordwrapMenuItem   = null;
    private MenuItem fontMenuItem       = null;
    private MenuItem viewMenuItem       = null;
    private MenuItem statusbarMenuItem  = null;
    private MenuItem helpMenuItem       = null;
    private MenuItem helptopicsMenuItem = null;
    private MenuItem aboutMenuItem      = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
        {
          this.components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.components = new Container();
      /************************************************/
      this.guiTimer = new Timer(this.components);
      /************************************************/
      this.topMainMenu        = new MainMenu(this.components);
      this.fileMenuItem       = new MenuItem();
      this.newMenuItem        = new MenuItem();
      this.openMenuItem       = new MenuItem();
      this.saveMenuItem       = new MenuItem();
      this.saveasMenuItem     = new MenuItem();
      this.pagesetupMenuItem  = new MenuItem();
      this.printMenuItem      = new MenuItem();
      this.exitMenuItem       = new MenuItem();
      this.editMenuItem       = new MenuItem();
      this.undoMenuItem       = new MenuItem();
      this.cutMenuItem        = new MenuItem();
      this.copyMenuItem       = new MenuItem();
      this.pasteMenuItem      = new MenuItem();
      this.deleteMenuItem     = new MenuItem();
      this.findMenuItem       = new MenuItem();
      this.findnextMenuItem   = new MenuItem();
      this.replaceMenuItem    = new MenuItem();
      this.gotoMenuItem       = new MenuItem();
      this.selectallMenuItem  = new MenuItem();
      this.timedateMenuItem   = new MenuItem();
      this.formatMenuItem     = new MenuItem();
      this.wordwrapMenuItem   = new MenuItem();
      this.fontMenuItem       = new MenuItem();
      this.viewMenuItem       = new MenuItem();
      this.statusbarMenuItem  = new MenuItem();
      this.helpMenuItem       = new MenuItem();
      this.helptopicsMenuItem = new MenuItem();
      this.aboutMenuItem      = new MenuItem();
      /************************************************/
      this.openFileDialog1 = new OpenFileDialog();
      this.saveFileDialog1 = new SaveFileDialog();
      this.printDialog1 = new PrintDialog();
      this.pageSetupDialog1 = new PageSetupDialog();
      this.printDocument1 = new PrintDocument();
      /************************************************/
      this.notepadTextBox      = new NotepadTextBox();
      this.bottomStatusBar     = new StatusBar();
      this.statusBarPanel1     = new StatusBarPanel();
      this.lncolStatusBarPanel = new StatusBarPanel();
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.lncolStatusBarPanel)).BeginInit();
      this.SuspendLayout();
      /************************************************/
      // guiTimer
      {
        this.guiTimer.Enabled  = true;
        this.guiTimer.Interval = 100;
        /************************************************/
        this.guiTimer.Tick += this.guiTimer_Tick;
      }
      // topMainMenu
      {
        this.topMainMenu.Name = "topMainMenu";
        /************************************************/
        this.topMainMenu.MenuItems.Add(this.fileMenuItem);
        this.topMainMenu.MenuItems.Add(this.editMenuItem);
        this.topMainMenu.MenuItems.Add(this.formatMenuItem);
        this.topMainMenu.MenuItems.Add(this.viewMenuItem);
        this.topMainMenu.MenuItems.Add(this.helpMenuItem);
      }
      // fileMenuItem
      {
        this.fileMenuItem.Name     = "fileMenuItem";
        this.fileMenuItem.Shortcut = Shortcut.None;
        this.fileMenuItem.Text     = "&File";
        /************************************************/
        this.fileMenuItem.MenuItems.Add(this.newMenuItem);
        this.fileMenuItem.MenuItems.Add(this.openMenuItem);
        this.fileMenuItem.MenuItems.Add(this.saveMenuItem);
        this.fileMenuItem.MenuItems.Add(this.saveasMenuItem);
        this.fileMenuItem.MenuItems.Add(new MenuItem("-"));
        this.fileMenuItem.MenuItems.Add(this.pagesetupMenuItem);
        this.fileMenuItem.MenuItems.Add(this.printMenuItem);
        this.fileMenuItem.MenuItems.Add(new MenuItem("-"));
        this.fileMenuItem.MenuItems.Add(this.exitMenuItem);
        /************************************************/
        this.fileMenuItem.Popup += this.fileMenuItem_Popup;
      }
      // newMenuItem
      {
        this.newMenuItem.Name     = "newMenuItem";
        this.newMenuItem.Shortcut = Shortcut.CtrlN;
        this.newMenuItem.Text     = "&New";
        /************************************************/
        this.newMenuItem.Click += this.newMenuItem_Click;
      }
      // openMenuItem
      {
        this.openMenuItem.Name     = "openMenuItem";
        this.openMenuItem.Shortcut = Shortcut.CtrlO;
        this.openMenuItem.Text     = "&Open...";
        /************************************************/
        this.openMenuItem.Click += this.openMenuItem_Click;
      }
      // saveMenuItem
      {
        this.saveMenuItem.Name     = "saveMenuItem";
        this.saveMenuItem.Shortcut = Shortcut.CtrlS;
        this.saveMenuItem.Text     = "&Save";
        /************************************************/
        this.saveMenuItem.Click += this.saveMenuItem_Click;
      }
      // saveasMenuItem
      {
        this.saveasMenuItem.Name     = "saveasMenuItem";
        this.saveasMenuItem.Shortcut = Shortcut.None;
        this.saveasMenuItem.Text     = "Save &As...";
        /************************************************/
        this.saveasMenuItem.Click += this.saveasMenuItem_Click;
      }
      // pagesetupMenuItem
      {
        this.pagesetupMenuItem.Name     = "pagesetupMenuItem";
        this.pagesetupMenuItem.Shortcut = Shortcut.None;
        this.pagesetupMenuItem.Text     = "Page Set&up...";
        /************************************************/
        this.pagesetupMenuItem.Click += this.pagesetupMenuItem_Click;
      }
      // printMenuItem
      {
        this.printMenuItem.Name     = "printMenuItem";
        this.printMenuItem.Shortcut = Shortcut.CtrlP;
        this.printMenuItem.Text     = "&Print...";
        /************************************************/
        this.printMenuItem.Click += this.printMenuItem_Click;
      }
      // exitMenuItem
      {
        this.exitMenuItem.Name     = "exitMenuItem";
        this.exitMenuItem.Shortcut = Shortcut.None;
        this.exitMenuItem.Text     = "E&xit";
        /************************************************/
        this.exitMenuItem.Click += this.exitMenuItem_Click;
      }
      // editMenuItem
      {
        this.editMenuItem.Name     = "editMenuItem";
        this.editMenuItem.Shortcut = Shortcut.None;
        this.editMenuItem.Text     = "&Edit";
        /************************************************/
        this.editMenuItem.MenuItems.Add(this.undoMenuItem);
        this.editMenuItem.MenuItems.Add(new MenuItem("-"));
        this.editMenuItem.MenuItems.Add(this.cutMenuItem);
        this.editMenuItem.MenuItems.Add(this.copyMenuItem);
        this.editMenuItem.MenuItems.Add(this.pasteMenuItem);
        this.editMenuItem.MenuItems.Add(this.deleteMenuItem);
        this.editMenuItem.MenuItems.Add(new MenuItem("-"));
        this.editMenuItem.MenuItems.Add(this.findMenuItem);
        this.editMenuItem.MenuItems.Add(this.findnextMenuItem);
        this.editMenuItem.MenuItems.Add(this.replaceMenuItem);
        this.editMenuItem.MenuItems.Add(this.gotoMenuItem);
        this.editMenuItem.MenuItems.Add(new MenuItem("-"));
        this.editMenuItem.MenuItems.Add(this.selectallMenuItem);
        this.editMenuItem.MenuItems.Add(this.timedateMenuItem);
        /************************************************/
        this.editMenuItem.Popup += editMenuItem_Popup;
      }
      // undoMenuItem
      {
        this.undoMenuItem.Name     = "undoMenuItem";
        this.undoMenuItem.Shortcut = Shortcut.CtrlZ;
        this.undoMenuItem.Text     = "&Undo";
        /************************************************/
        this.undoMenuItem.Click += this.undoMenuItem_Click;
      }
      // cutMenuItem
      {
        this.cutMenuItem.Name     = "cutMenuItem";
        this.cutMenuItem.Shortcut = Shortcut.CtrlX;
        this.cutMenuItem.Text     = "Cut&t";
        /************************************************/
        this.cutMenuItem.Click += this.cutMenuItem_Click;
      }
      // copyMenuItem
      {
        this.copyMenuItem.Name     = "copyMenuItem";
        this.copyMenuItem.Shortcut = Shortcut.CtrlC;
        this.copyMenuItem.Text     = "&Copy";
        /************************************************/
        this.copyMenuItem.Click += this.copyMenuItem_Click;
      }
      // pasteMenuItem
      {
        this.pasteMenuItem.Name     = "pasteMenuItem";
        this.pasteMenuItem.Shortcut = Shortcut.CtrlP;
        this.pasteMenuItem.Text     = "&Psate";
        /************************************************/
        this.pasteMenuItem.Click += this.pasteMenuItem_Click;
      }
      // deleteMenuItem
      {
        this.deleteMenuItem.Name     = "deleteMenuItem";
        this.deleteMenuItem.Shortcut = Shortcut.Del;
        this.deleteMenuItem.Text     = "De&lete";
        /************************************************/
        this.deleteMenuItem.Click += this.deleteMenuItem_Click;
      }
      // findMenuItem
      {
        this.findMenuItem.Name     = "findMenuItem";
        this.findMenuItem.Shortcut = Shortcut.CtrlF;
        this.findMenuItem.Text     = "&Find...";
        /************************************************/
        this.findMenuItem.Click += this.findMenuItem_Click;
      }
      // findnextMenuItem
      {
        this.findnextMenuItem.Name     = "findnextMenuItem";
        this.findnextMenuItem.Shortcut = Shortcut.F3;
        this.findnextMenuItem.Text     = "Find &Next";
        /************************************************/
        this.findnextMenuItem.Click += this.findnextMenuItem_Click;
      }
      // replaceMenuItem
      {
        this.replaceMenuItem.Name     = "replaceMenuItem";
        this.replaceMenuItem.Shortcut = Shortcut.CtrlH;
        this.replaceMenuItem.Text     = "&Replace...";
        /************************************************/
        this.replaceMenuItem.Click += this.replaceMenuItem_Click;
      }
      // gotoMenuItem
      {
        this.gotoMenuItem.Name     = "gotoMenuItem";
        this.gotoMenuItem.Shortcut = Shortcut.CtrlG;
        this.gotoMenuItem.Text     = "&Go To...";
        /************************************************/
        this.gotoMenuItem.Click += this.gotoMenuItem_Click;
      }
      // selectallMenuItem
      {
        this.selectallMenuItem.Name     = "selectallMenuItem";
        this.selectallMenuItem.Shortcut = Shortcut.CtrlA;
        this.selectallMenuItem.Text     = "Select &All";
        /************************************************/
        this.selectallMenuItem.Click += this.selectallMenuItem_Click;
      }
      // timedateMenuItem
      {
        this.timedateMenuItem.Name     = "timedateMenuItem";
        this.timedateMenuItem.Shortcut = Shortcut.F5;
        this.timedateMenuItem.Text     = "Time/&Date";
        /************************************************/
        this.timedateMenuItem.Click += this.timedateMenuItem_Click;
      }
      // formatMenuItem
      {
        this.formatMenuItem.Name     = "formatMenuItem";
        this.formatMenuItem.Shortcut = Shortcut.None;
        this.formatMenuItem.Text     = "F&ormat";
        /************************************************/
        this.formatMenuItem.MenuItems.Add(this.wordwrapMenuItem);
        this.formatMenuItem.MenuItems.Add(this.fontMenuItem);
        /************************************************/
        this.formatMenuItem.Popup += formatMenuItem_Popup;
      }
      // wordwrapMenuItem
      {
        this.wordwrapMenuItem.Name     = "wordwrapMenuItem";
        this.wordwrapMenuItem.Shortcut = Shortcut.None;
        this.wordwrapMenuItem.Text     = "&Word Wrap";
        /************************************************/
        this.wordwrapMenuItem.Click += this.wordwrapMenuItem_Click;
      }
      // fontMenuItem
      {
        this.fontMenuItem.Name     = "fontMenuItem";
        this.fontMenuItem.Shortcut = Shortcut.None;
        this.fontMenuItem.Text     = "&Font...";
        /************************************************/
        this.fontMenuItem.Click += this.fontMenuItem_Click;
      }
      // viewMenuItem
      {
        this.viewMenuItem.Name     = "viewMenuItem";
        this.viewMenuItem.Shortcut = Shortcut.None;
        this.viewMenuItem.Text     = "&View";
        /************************************************/
        this.viewMenuItem.MenuItems.Add(this.statusbarMenuItem);
        /************************************************/
        this.viewMenuItem.Popup += viewMenuItem_Popup;
      }
      // statusbarMenuItem
      {
        this.statusbarMenuItem.Name     = "statusbarMenuItem";
        this.statusbarMenuItem.Shortcut = Shortcut.None;
        this.statusbarMenuItem.Text     = "&Status Bar";
        /************************************************/
        this.statusbarMenuItem.Click += this.statusbarMenuItem_Click;
      }
      // helpMenuItem
      {
        this.helpMenuItem.Name     = "helpMenuItem";
        this.helpMenuItem.Shortcut = Shortcut.None;
        this.helpMenuItem.Text     = "&Help";
        /************************************************/
        this.helpMenuItem.MenuItems.Add(this.helptopicsMenuItem);
        this.helpMenuItem.MenuItems.Add(new MenuItem("-"));
        this.helpMenuItem.MenuItems.Add(this.aboutMenuItem);
        /************************************************/
        this.helpMenuItem.Popup += helpMenuItem_Popup;
      }
      // helptopicsMenuItem
      {
        this.helptopicsMenuItem.Name     = "helptopicsMenuItem";
        this.helptopicsMenuItem.Shortcut = Shortcut.None;
        this.helptopicsMenuItem.Text     = "&Help Topics";
        /************************************************/
        this.helptopicsMenuItem.Click += this.helptopicsMenuItem_Click;
      }
      // aboutMenuItem
      {
        this.aboutMenuItem.Name     = "aboutMenuItem";
        this.aboutMenuItem.Shortcut = Shortcut.None;
        this.aboutMenuItem.Text     = "&About Notepad";
        /************************************************/
        this.aboutMenuItem.Click += this.aboutMenuItem_Click;
      }
      // notepadTextBox
      {
        this.notepadTextBox.Name          = "notepadTextBox";
        this.notepadTextBox.AllowDrop     = true;
        this.notepadTextBox.Dock          = DockStyle.Fill;
        this.notepadTextBox.Font          = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.notepadTextBox.HideSelection = false;
        this.notepadTextBox.ImeMode       = ImeMode.On;
        this.notepadTextBox.Location      = new System.Drawing.Point(0, 0);
        this.notepadTextBox.Multiline     = true;
        this.notepadTextBox.ScrollBars    = ScrollBars.Both;
        this.notepadTextBox.Size          = new System.Drawing.Size(584, 350);
        this.notepadTextBox.TabIndex      = 4;
        this.notepadTextBox.WordWrap      = false;
        /************************************************/
        this.notepadTextBox.TextChanged    += this.notepadTextBox_TextChanged;
        this.notepadTextBox.DragDrop       += this.notepadTextBox_DragDrop;
        this.notepadTextBox.PreviewKeyDown += this.notepadTextBox_PreviewKeyDown;
        this.notepadTextBox.Click          += this.notepadTextBox_Click;
        this.notepadTextBox.DragEnter     += this.notepadTextBox_DragEnter;
      }
      // bottomStatusBar
      {
        this.bottomStatusBar.Name        = "statusBar1";
        this.bottomStatusBar.Font        = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.bottomStatusBar.Location    = new System.Drawing.Point(0, 350);
        this.bottomStatusBar.RightToLeft = RightToLeft.No;
        this.bottomStatusBar.ShowPanels  = true;
        this.bottomStatusBar.Size        = new System.Drawing.Size(584, 22);
        this.bottomStatusBar.TabIndex    = 6;
        /************************************************/
        this.bottomStatusBar.Panels.Add(this.statusBarPanel1);
        this.bottomStatusBar.Panels.Add(this.lncolStatusBarPanel);
        /************************************************/
        this.bottomStatusBar.Resize += this.bottomStatusBar_Resize;
      }
      // statusBarPanel1
      {
        this.statusBarPanel1.Name      = "statusBarPanel1";
        this.statusBarPanel1.Alignment = HorizontalAlignment.Right;
        this.statusBarPanel1.Width     = 440;
      }
      // lncolStatusBarPanel
      {
        this.lncolStatusBarPanel.Name        = "lncolStatusBarPanel";
        this.lncolStatusBarPanel.BorderStyle = StatusBarPanelBorderStyle.None;
        this.lncolStatusBarPanel.Width       = 150;
      }
      
      
      
      
      // 
      // printDialog1
      // 
      this.printDialog1.UseEXDialog = true;
      // 
      // pageSetupDialog1
      // 
      this.pageSetupDialog1.EnableMetric = true;
      // 
      // printDocument1
      // 
      this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);


      // MainForm
      {
        base.Name = "MainForm";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        base.AutoScaleMode       = AutoScaleMode.Font;
        base.ClientSize          = new System.Drawing.Size(584, 372);
        base.IsMdiContainer      = true;
        base.Menu                = this.topMainMenu;
        base.StartPosition       = FormStartPosition.CenterScreen;
        base.Text                = "无标题 - 记事本";
        /************************************************/
        base.Controls.Add(this.notepadTextBox);
        base.Controls.Add(this.bottomStatusBar);
        /************************************************/
        base.Load += this.MainForm_Load;
        base.Shown += this.MainForm_Shown;
        base.FormClosing += this.MainForm_FormClosing;
      }
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.lncolStatusBarPanel)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
    /************************************************/
    public NotepadTextBox  notepadTextBox      = null;
    private StatusBar      bottomStatusBar     = null;
    private StatusBarPanel statusBarPanel1     = null;
    private StatusBarPanel lncolStatusBarPanel = null;
    
    
    public SaveFileDialog saveFileDialog1;
    public OpenFileDialog openFileDialog1;
    private PrintDialog printDialog1;
    private PageSetupDialog pageSetupDialog1;
    private PrintDocument printDocument1;
  }
}