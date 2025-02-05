using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    private IContainer components = null;
    /************************************************/
    private Timer guiTimer = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing && (this.components != null))
      {
        this.components.Dispose();
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
      this.openFileDialog1 = new OpenFileDialog();
      this.saveFileDialog1 = new SaveFileDialog();
      this.fontDialog1 = new FontDialog();
      this.printDialog1 = new PrintDialog();
      this.pageSetupDialog1 = new PageSetupDialog();
      this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.topMainMenu = new MainMenu(this.components);
      this.fileMenuItem = new MenuItem();
      this.newMenuItem = new MenuItem();
      this.openMenuItem = new MenuItem();
      this.saveMenuItem = new MenuItem();
      this.saveasMenuItem = new MenuItem();
      this.pagesetupMenuItem = new MenuItem();
      this.printMenuItem = new MenuItem();
      this.exitMenuItem = new MenuItem();
      this.editMenuItem = new MenuItem();
      this.undoMenuItem = new MenuItem();
      this.cutMenuItem = new MenuItem();
      this.copyMenuItem = new MenuItem();
      this.pasteMenuItem = new MenuItem();
      this.deleteMenuItem = new MenuItem();
      this.findMenuItem = new MenuItem();
      this.findnextMenuItem = new MenuItem();
      this.replaceMenuItem1 = new MenuItem();
      this.gotoMenuItem1 = new MenuItem();
      this.selectallMenuItem1 = new MenuItem();
      this.timedateMenuItem1 = new MenuItem();
      this.formatMenuItem = new MenuItem();
      this.wordwrapMenuItem = new MenuItem();
      this.fontMenuItem = new MenuItem();
      this.viewMenuItem = new MenuItem();
      this.statusbarMenuItem = new MenuItem();
      this.helpMenuItem = new MenuItem();
      this.helptopicsMenuItem = new MenuItem();
      this.aboutMenuItem = new MenuItem();
      this.notepadTextBox = new TextBox();
      this.bottomStatusBar = new StatusBar();
      this.statusBarPanel1 = new StatusBarPanel();
      this.lncolStatusBarPanel = new StatusBarPanel();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.lncolStatusBarPanel)).BeginInit();
      this.SuspendLayout();
      // 
      // fontDialog1
      // 
      this.fontDialog1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.fontDialog1.ShowEffects = false;
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
      // 
      // topMainMenu
      // 
      this.topMainMenu.MenuItems.AddRange(new MenuItem[] {
      this.fileMenuItem,
      this.editMenuItem,
      this.formatMenuItem,
      this.viewMenuItem,
      this.helpMenuItem});
      // 
      // fileMenuItem
      // 
      this.fileMenuItem.Index = 0;
      this.fileMenuItem.MenuItems.AddRange(new MenuItem[] {
      this.newMenuItem,
      this.openMenuItem,
      this.saveMenuItem,
      this.saveasMenuItem,
      new MenuItem("-"),
      this.pagesetupMenuItem,
      this.printMenuItem,
      new MenuItem("-"),
      this.exitMenuItem});
      this.fileMenuItem.Text = "文件(&F)";
      // 
      // newMenuItem
      // 
      this.newMenuItem.Index = 0;
      this.newMenuItem.Shortcut = Shortcut.CtrlN;
      this.newMenuItem.Text = "新建(&N)";
      this.newMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
      // 
      // openMenuItem
      // 
      this.openMenuItem.Index = 1;
      this.openMenuItem.Shortcut = Shortcut.CtrlO;
      this.openMenuItem.Text = "打开(&O)";
      this.openMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
      // 
      // saveMenuItem
      // 
      this.saveMenuItem.Index = 2;
      this.saveMenuItem.Shortcut = Shortcut.CtrlS;
      this.saveMenuItem.Text = "保存(&S)";
      this.saveMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
      // 
      // saveasMenuItem
      // 
      this.saveasMenuItem.Index = 3;
      this.saveasMenuItem.Text = "另存为(&A)";
      this.saveasMenuItem.Click += new System.EventHandler(this.另存为AToolStripMenuItem_Click);
      // 
      // pagesetupMenuItem
      // 
      this.pagesetupMenuItem.Index = 5;
      this.pagesetupMenuItem.Text = "页面设置(&U)";
      this.pagesetupMenuItem.Click += new System.EventHandler(this.页面设置ToolStripMenuItem_Click);
      // 
      // printMenuItem
      // 
      this.printMenuItem.Index = 6;
      this.printMenuItem.Shortcut = Shortcut.CtrlP;
      this.printMenuItem.Text = "打印(&P)";
      this.printMenuItem.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
      // 
      // exitMenuItem
      // 
      this.exitMenuItem.Index = 8;
      this.exitMenuItem.Text = "退出(&X)";
      this.exitMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
      // 
      // editMenuItem
      // 
      this.editMenuItem.Index = 1;
      this.editMenuItem.MenuItems.AddRange(new MenuItem[] {
      this.undoMenuItem,
      new MenuItem("-"),
      this.cutMenuItem,
      this.copyMenuItem,
      this.pasteMenuItem,
      this.deleteMenuItem,
      new MenuItem("-"),
      this.findMenuItem,
      this.findnextMenuItem,
      this.replaceMenuItem1,
      this.gotoMenuItem1,
      new MenuItem("-"),
      this.selectallMenuItem1,
      this.timedateMenuItem1});
      this.editMenuItem.Text = "编辑(&E)";
      // 
      // undoMenuItem
      // 
      this.undoMenuItem.Index = 0;
      this.undoMenuItem.Shortcut = Shortcut.CtrlZ;
      this.undoMenuItem.Text = "撤销(&U)";
      this.undoMenuItem.Click += new System.EventHandler(this.撤销ToolStripMenuItem_Click);
      // 
      // cutMenuItem
      // 
      this.cutMenuItem.Index = 2;
      this.cutMenuItem.Shortcut = Shortcut.CtrlX;
      this.cutMenuItem.Text = "剪切(&T)";
      this.cutMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem1_Click);
      // 
      // copyMenuItem
      // 
      this.copyMenuItem.Index = 3;
      this.copyMenuItem.Shortcut = Shortcut.CtrlC;
      this.copyMenuItem.Text = "复制(&C)";
      this.copyMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
      // 
      // pasteMenuItem
      // 
      this.pasteMenuItem.Index = 4;
      this.pasteMenuItem.Shortcut = Shortcut.CtrlP;
      this.pasteMenuItem.Text = "粘贴(&P)";
      this.pasteMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem1_Click);
      // 
      // deleteMenuItem
      // 
      this.deleteMenuItem.Index = 5;
      this.deleteMenuItem.Shortcut = Shortcut.Del;
      this.deleteMenuItem.Text = "删除(&L)";
      this.deleteMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem1_Click);
      // 
      // findMenuItem
      // 
      this.findMenuItem.Index = 7;
      this.findMenuItem.Shortcut = Shortcut.CtrlF;
      this.findMenuItem.Text = "查找(&F)";
      this.findMenuItem.Click += new System.EventHandler(this.查找ToolStripMenuItem_Click);
      // 
      // findnextMenuItem
      // 
      this.findnextMenuItem.Index = 8;
      this.findnextMenuItem.Shortcut = Shortcut.F3;
      this.findnextMenuItem.Text = "查找下一个(&N)";
      this.findnextMenuItem.Click += new System.EventHandler(this.查找下一个ToolStripMenuItem_Click);
      // 
      // replaceMenuItem1
      // 
      this.replaceMenuItem1.Index = 9;
      this.replaceMenuItem1.Shortcut = Shortcut.CtrlH;
      this.replaceMenuItem1.Text = "替换 (&R)";
      this.replaceMenuItem1.Click += new System.EventHandler(this.定位ToolStripMenuItem_Click);
      // 
      // gotoMenuItem1
      // 
      this.gotoMenuItem1.Index = 10;
      this.gotoMenuItem1.Shortcut = Shortcut.CtrlG;
      this.gotoMenuItem1.Text = "转到(&G)";
      this.gotoMenuItem1.Click += new System.EventHandler(this.转到ToolStripMenuItem_Click);
      // 
      // selectallMenuItem1
      // 
      this.selectallMenuItem1.Index = 12;
      this.selectallMenuItem1.Shortcut = Shortcut.CtrlA;
      this.selectallMenuItem1.Text = "全选(&A)";
      this.selectallMenuItem1.Click += new System.EventHandler(this.全选ToolStripMenuItem2_Click);
      // 
      // timedateMenuItem1
      // 
      this.timedateMenuItem1.Index = 13;
      this.timedateMenuItem1.Shortcut = Shortcut.F5;
      this.timedateMenuItem1.Text = "时间/日期(&D)";
      this.timedateMenuItem1.Click += new System.EventHandler(this.时间日期ToolStripMenuItem_Click);
      // 
      // menuItem20
      // 
      this.formatMenuItem.Index = 2;
      this.formatMenuItem.MenuItems.AddRange(new MenuItem[] {
      this.wordwrapMenuItem,
      this.fontMenuItem});
      this.formatMenuItem.Text = "格式(&O)";
      // 
      // wordwrapMenuItem
      // 
      this.wordwrapMenuItem.Index = 0;
      this.wordwrapMenuItem.Text = "自动换行(&W)";
      this.wordwrapMenuItem.Click += new System.EventHandler(this.自动换行ToolStripMenuItem_Click);
      // 
      // fontMenuItem
      // 
      this.fontMenuItem.Index = 1;
      this.fontMenuItem.Text = "字体(&F)...";
      this.fontMenuItem.Click += new System.EventHandler(this.字体ToolStripMenuItem_Click);
      // 
      // viewMenuItem
      // 
      this.viewMenuItem.Index = 3;
      this.viewMenuItem.MenuItems.AddRange(new MenuItem[] {
      this.statusbarMenuItem});
      this.viewMenuItem.Text = "查看(&V)";
      // 
      // statusbarMenuItem
      // 
      this.statusbarMenuItem.Index = 0;
      this.statusbarMenuItem.Text = "状态栏(&S)";
      this.statusbarMenuItem.Click += new System.EventHandler(this.状态栏SToolStripMenuItem_Click);
      // 
      // helpMenuItem
      // 
      this.helpMenuItem.Index = 4;
      this.helpMenuItem.MenuItems.AddRange(new MenuItem[] {
      this.helptopicsMenuItem,
      new MenuItem("-"),
      this.aboutMenuItem});
      this.helpMenuItem.Text = "帮助(&H)";
      // 
      // helptopicsMenuItem
      // 
      this.helptopicsMenuItem.Index = 0;
      this.helptopicsMenuItem.Text = "查看帮助(&H)";
      this.helptopicsMenuItem.Click += new System.EventHandler(this.menuItem57_Click);
      // 
      // aboutMenuItem
      // 
      this.aboutMenuItem.Index = 2;
      this.aboutMenuItem.Text = "关于记事本(&A)";
      this.aboutMenuItem.Click += new System.EventHandler(this.关于记事本ToolStripMenuItem_Click);
      // 
      // notepadTextBox
      // 
      this.notepadTextBox.AllowDrop = true;
      this.notepadTextBox.Dock = DockStyle.Fill;
      this.notepadTextBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.notepadTextBox.HideSelection = false;
      this.notepadTextBox.ImeMode = ImeMode.On;
      this.notepadTextBox.Location = new System.Drawing.Point(0, 0);
      this.notepadTextBox.Multiline = true;
      this.notepadTextBox.Name = "notepadTextBox";
      this.notepadTextBox.ScrollBars = ScrollBars.Both;
      this.notepadTextBox.Size = new System.Drawing.Size(584, 350);
      this.notepadTextBox.TabIndex = 4;
      this.notepadTextBox.WordWrap = false;
      this.notepadTextBox.TextChanged += new System.EventHandler(this.mtBox1_TextChanged);
      this.notepadTextBox.DragDrop += new DragEventHandler(this.mtBox1_DragDrop);
      this.notepadTextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(this.mtBox1_PreviewKeyDown);
      this.notepadTextBox.Click += new System.EventHandler(this.mtBox1_Click);
      this.notepadTextBox.DragEnter += new DragEventHandler(this.mtBox1_DragEnter);
      // 
      // guiTimer
      // 
      this.guiTimer.Enabled = true;
      this.guiTimer.Interval = 1;
      this.guiTimer.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // bottomStatusBar
      // 
      this.bottomStatusBar.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.bottomStatusBar.Location = new System.Drawing.Point(0, 350);
      this.bottomStatusBar.Name = "statusBar1";
      this.bottomStatusBar.Panels.AddRange(new StatusBarPanel[] {
      this.statusBarPanel1,
      this.lncolStatusBarPanel});
      this.bottomStatusBar.RightToLeft = RightToLeft.No;
      this.bottomStatusBar.ShowPanels = true;
      this.bottomStatusBar.Size = new System.Drawing.Size(584, 22);
      this.bottomStatusBar.TabIndex = 6;
      this.bottomStatusBar.Resize += new System.EventHandler(this.statusBar1_Resize);
      // 
      // statusBarPanel1
      // 
      this.statusBarPanel1.Alignment = HorizontalAlignment.Right;
      this.statusBarPanel1.Name = "statusBarPanel1";
      this.statusBarPanel1.Width = 440;
      // 
      // lncolStatusBarPanel
      // 
      this.lncolStatusBarPanel.BorderStyle = StatusBarPanelBorderStyle.None;
      this.lncolStatusBarPanel.Name = "lncolStatusBarPanel";
      this.lncolStatusBarPanel.Width = 150;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 372);
      this.Controls.Add(this.notepadTextBox);
      this.Controls.Add(this.bottomStatusBar);
      this.IsMdiContainer = true;
      this.Menu = this.topMainMenu;
      this.Name = "Form1";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "无标题 - 记事本";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.Shown += new System.EventHandler(this.Form1_Shown);
      this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.lncolStatusBarPanel)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
    /************************************************/
    private MainMenu topMainMenu = null;
    private MenuItem fileMenuItem = null;
    private MenuItem newMenuItem = null;
    private MenuItem openMenuItem = null;
    private MenuItem saveMenuItem = null;
    private MenuItem saveasMenuItem = null;
    private MenuItem pagesetupMenuItem = null;
    private MenuItem printMenuItem = null;
    private MenuItem exitMenuItem = null;
    private MenuItem editMenuItem = null;
    private MenuItem undoMenuItem = null;
    private MenuItem cutMenuItem = null;
    private MenuItem copyMenuItem = null;
    private MenuItem pasteMenuItem = null;
    private MenuItem deleteMenuItem = null;
    private MenuItem findMenuItem = null;
    private MenuItem findnextMenuItem = null;
    private MenuItem replaceMenuItem1 = null;
    private MenuItem gotoMenuItem1 = null;
    private MenuItem selectallMenuItem1 = null;
    private MenuItem timedateMenuItem1 = null;
    private MenuItem formatMenuItem = null;
    private MenuItem wordwrapMenuItem = null;
    private MenuItem fontMenuItem = null;
    private MenuItem viewMenuItem = null;
    private MenuItem statusbarMenuItem = null;
    private MenuItem helpMenuItem = null;
    private MenuItem helptopicsMenuItem = null;
    private MenuItem aboutMenuItem = null;
    
    public TextBox notepadTextBox = null;
    
    private StatusBar bottomStatusBar = null;
    private StatusBarPanel statusBarPanel1;
    private StatusBarPanel lncolStatusBarPanel = null;
    
    
    private FontDialog fontDialog1;
    public SaveFileDialog saveFileDialog1;
    public OpenFileDialog openFileDialog1;
    private PrintDialog printDialog1;
    private PageSetupDialog pageSetupDialog1;
    private PrintDocument printDocument1;
  }
}