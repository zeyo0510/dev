using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    private IContainer components = null;
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
      this.components = new System.ComponentModel.Container();
      this.openFileDialog1 = new OpenFileDialog();
      this.saveFileDialog1 = new SaveFileDialog();
      this.fontDialog1 = new FontDialog();
      this.printDialog1 = new PrintDialog();
      this.pageSetupDialog1 = new PageSetupDialog();
      this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.mainMenu1 = new MainMenu(this.components);
      this.menuItem1 = new MenuItem();
      this.menuItem31 = new MenuItem();
      this.menuItem32 = new MenuItem();
      this.menuItem33 = new MenuItem();
      this.menuItem34 = new MenuItem();
      this.menuItem35 = new MenuItem();
      this.menuItem36 = new MenuItem();
      this.menuItem37 = new MenuItem();
      this.menuItem38 = new MenuItem();
      this.menuItem39 = new MenuItem();
      this.menuItem8 = new MenuItem();
      this.撤销UToolStripMenuItem = new MenuItem();
      this.menuItem41 = new MenuItem();
      this.剪切ToolStripMenuItem = new MenuItem();
      this.复制zToolStripMenuItem = new MenuItem();
      this.menuItem44 = new MenuItem();
      this.删除ToolStripMenuItem = new MenuItem();
      this.menuItem46 = new MenuItem();
      this.查找ToolStripMenuItem = new MenuItem();
      this.查找下一个ToolStripMenuItem = new MenuItem();
      this.menuItem49 = new MenuItem();
      this.menuItem50 = new MenuItem();
      this.menuItem51 = new MenuItem();
      this.menuItem52 = new MenuItem();
      this.menuItem53 = new MenuItem();
      this.menuItem20 = new MenuItem();
      this.自动换行ToolStripMenuItem = new MenuItem();
      this.menuItem55 = new MenuItem();
      this.menuItem24 = new MenuItem();
      this.状态栏SToolStripMenuItem = new MenuItem();
      this.menuItem26 = new MenuItem();
      this.menuItem57 = new MenuItem();
      this.menuItem58 = new MenuItem();
      this.menuItem59 = new MenuItem();
      this.menuItem2 = new MenuItem();
      this.menuItem3 = new MenuItem();
      this.menuItem4 = new MenuItem();
      this.menuItem5 = new MenuItem();
      this.menuItem7 = new MenuItem();
      this.menuItem6 = new MenuItem();
      this.menuItem9 = new MenuItem();
      this.menuItem28 = new MenuItem();
      this.menuItem10 = new MenuItem();
      this.menuItem11 = new MenuItem();
      this.menuItem12 = new MenuItem();
      this.menuItem13 = new MenuItem();
      this.menuItem29 = new MenuItem();
      this.menuItem14 = new MenuItem();
      this.menuItem15 = new MenuItem();
      this.menuItem16 = new MenuItem();
      this.menuItem17 = new MenuItem();
      this.menuItem30 = new MenuItem();
      this.menuItem18 = new MenuItem();
      this.menuItem19 = new MenuItem();
      this.menuItem21 = new MenuItem();
      this.menuItem22 = new MenuItem();
      this.menuItem23 = new MenuItem();
      this.menuItem25 = new MenuItem();
      this.menuItem27 = new MenuItem();
      this.mtBox1 = new TextBox();
      this.timer1 = new Timer(this.components);
      this.statusBar1 = new StatusBar();
      this.statusBarPanel1 = new StatusBarPanel();
      this.statusBarPanel2 = new StatusBarPanel();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
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
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new MenuItem[] {
      this.menuItem1,
      this.menuItem8,
      this.menuItem20,
      this.menuItem24,
      this.menuItem26});
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 0;
      this.menuItem1.MenuItems.AddRange(new MenuItem[] {
      this.menuItem31,
      this.menuItem32,
      this.menuItem33,
      this.menuItem34,
      this.menuItem35,
      this.menuItem36,
      this.menuItem37,
      this.menuItem38,
      this.menuItem39});
      this.menuItem1.Text = "文件(&F)";
      // 
      // menuItem31
      // 
      this.menuItem31.Index = 0;
      this.menuItem31.Shortcut = Shortcut.CtrlN;
      this.menuItem31.Text = "新建(&N)";
      this.menuItem31.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
      // 
      // menuItem32
      // 
      this.menuItem32.Index = 1;
      this.menuItem32.Shortcut = Shortcut.CtrlO;
      this.menuItem32.Text = "打开(&O)";
      this.menuItem32.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
      // 
      // menuItem33
      // 
      this.menuItem33.Index = 2;
      this.menuItem33.Shortcut = Shortcut.CtrlS;
      this.menuItem33.Text = "保存(&S)";
      this.menuItem33.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
      // 
      // menuItem34
      // 
      this.menuItem34.Index = 3;
      this.menuItem34.Text = "另存为(&A)";
      this.menuItem34.Click += new System.EventHandler(this.另存为AToolStripMenuItem_Click);
      // 
      // menuItem35
      // 
      this.menuItem35.Index = 4;
      this.menuItem35.Text = "-";
      // 
      // menuItem36
      // 
      this.menuItem36.Index = 5;
      this.menuItem36.Text = "页面设置(&U)";
      this.menuItem36.Click += new System.EventHandler(this.页面设置ToolStripMenuItem_Click);
      // 
      // menuItem37
      // 
      this.menuItem37.Index = 6;
      this.menuItem37.Shortcut = Shortcut.CtrlP;
      this.menuItem37.Text = "打印(&P)";
      this.menuItem37.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
      // 
      // menuItem38
      // 
      this.menuItem38.Index = 7;
      this.menuItem38.Text = "-";
      // 
      // menuItem39
      // 
      this.menuItem39.Index = 8;
      this.menuItem39.Text = "退出(&X)";
      this.menuItem39.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
      // 
      // menuItem8
      // 
      this.menuItem8.Index = 1;
      this.menuItem8.MenuItems.AddRange(new MenuItem[] {
      this.撤销UToolStripMenuItem,
      this.menuItem41,
      this.剪切ToolStripMenuItem,
      this.复制zToolStripMenuItem,
      this.menuItem44,
      this.删除ToolStripMenuItem,
      this.menuItem46,
      this.查找ToolStripMenuItem,
      this.查找下一个ToolStripMenuItem,
      this.menuItem49,
      this.menuItem50,
      this.menuItem51,
      this.menuItem52,
      this.menuItem53});
      this.menuItem8.Text = "编辑(&E)";
      // 
      // 撤销UToolStripMenuItem
      // 
      this.撤销UToolStripMenuItem.Index = 0;
      this.撤销UToolStripMenuItem.Shortcut = Shortcut.CtrlZ;
      this.撤销UToolStripMenuItem.Text = "撤销(&U)";
      this.撤销UToolStripMenuItem.Click += new System.EventHandler(this.撤销ToolStripMenuItem_Click);
      // 
      // menuItem41
      // 
      this.menuItem41.Index = 1;
      this.menuItem41.Text = "-";
      // 
      // 剪切ToolStripMenuItem
      // 
      this.剪切ToolStripMenuItem.Index = 2;
      this.剪切ToolStripMenuItem.Shortcut = Shortcut.CtrlX;
      this.剪切ToolStripMenuItem.Text = "剪切(&T)";
      this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem1_Click);
      // 
      // 复制zToolStripMenuItem
      // 
      this.复制zToolStripMenuItem.Index = 3;
      this.复制zToolStripMenuItem.Shortcut = Shortcut.CtrlC;
      this.复制zToolStripMenuItem.Text = "复制(&C)";
      this.复制zToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
      // 
      // menuItem44
      // 
      this.menuItem44.Index = 4;
      this.menuItem44.Shortcut = Shortcut.CtrlP;
      this.menuItem44.Text = "粘贴(&P)";
      this.menuItem44.Click += new System.EventHandler(this.粘贴ToolStripMenuItem1_Click);
      // 
      // 删除ToolStripMenuItem
      // 
      this.删除ToolStripMenuItem.Index = 5;
      this.删除ToolStripMenuItem.Shortcut = Shortcut.Del;
      this.删除ToolStripMenuItem.Text = "删除(&L)";
      this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem1_Click);
      // 
      // menuItem46
      // 
      this.menuItem46.Index = 6;
      this.menuItem46.Text = "-";
      // 
      // 查找ToolStripMenuItem
      // 
      this.查找ToolStripMenuItem.Index = 7;
      this.查找ToolStripMenuItem.Shortcut = Shortcut.CtrlF;
      this.查找ToolStripMenuItem.Text = "查找(&F)";
      this.查找ToolStripMenuItem.Click += new System.EventHandler(this.查找ToolStripMenuItem_Click);
      // 
      // 查找下一个ToolStripMenuItem
      // 
      this.查找下一个ToolStripMenuItem.Index = 8;
      this.查找下一个ToolStripMenuItem.Shortcut = Shortcut.F3;
      this.查找下一个ToolStripMenuItem.Text = "查找下一个(&N)";
      this.查找下一个ToolStripMenuItem.Click += new System.EventHandler(this.查找下一个ToolStripMenuItem_Click);
      // 
      // menuItem49
      // 
      this.menuItem49.Index = 9;
      this.menuItem49.Shortcut = Shortcut.CtrlH;
      this.menuItem49.Text = "替换 (&R)";
      this.menuItem49.Click += new System.EventHandler(this.定位ToolStripMenuItem_Click);
      // 
      // menuItem50
      // 
      this.menuItem50.Index = 10;
      this.menuItem50.Shortcut = Shortcut.CtrlG;
      this.menuItem50.Text = "转到(&G)";
      this.menuItem50.Click += new System.EventHandler(this.转到ToolStripMenuItem_Click);
      // 
      // menuItem51
      // 
      this.menuItem51.Index = 11;
      this.menuItem51.Text = "-";
      // 
      // menuItem52
      // 
      this.menuItem52.Index = 12;
      this.menuItem52.Shortcut = Shortcut.CtrlA;
      this.menuItem52.Text = "全选(&A)";
      this.menuItem52.Click += new System.EventHandler(this.全选ToolStripMenuItem2_Click);
      // 
      // menuItem53
      // 
      this.menuItem53.Index = 13;
      this.menuItem53.Shortcut = Shortcut.F5;
      this.menuItem53.Text = "时间/日期(&D)";
      this.menuItem53.Click += new System.EventHandler(this.时间日期ToolStripMenuItem_Click);
      // 
      // menuItem20
      // 
      this.menuItem20.Index = 2;
      this.menuItem20.MenuItems.AddRange(new MenuItem[] {
      this.自动换行ToolStripMenuItem,
      this.menuItem55});
      this.menuItem20.Text = "格式(&O)";
      // 
      // 自动换行ToolStripMenuItem
      // 
      this.自动换行ToolStripMenuItem.Index = 0;
      this.自动换行ToolStripMenuItem.Text = "自动换行(&W)";
      this.自动换行ToolStripMenuItem.Click += new System.EventHandler(this.自动换行ToolStripMenuItem_Click);
      // 
      // menuItem55
      // 
      this.menuItem55.Index = 1;
      this.menuItem55.Text = "字体(&F)...";
      this.menuItem55.Click += new System.EventHandler(this.字体ToolStripMenuItem_Click);
      // 
      // menuItem24
      // 
      this.menuItem24.Index = 3;
      this.menuItem24.MenuItems.AddRange(new MenuItem[] {
      this.状态栏SToolStripMenuItem});
      this.menuItem24.Text = "查看(&V)";
      // 
      // 状态栏SToolStripMenuItem
      // 
      this.状态栏SToolStripMenuItem.Index = 0;
      this.状态栏SToolStripMenuItem.Text = "状态栏(&S)";
      this.状态栏SToolStripMenuItem.Click += new System.EventHandler(this.状态栏SToolStripMenuItem_Click);
      // 
      // menuItem26
      // 
      this.menuItem26.Index = 4;
      this.menuItem26.MenuItems.AddRange(new MenuItem[] {
      this.menuItem57,
      this.menuItem58,
      this.menuItem59});
      this.menuItem26.Text = "帮助(&H)";
      // 
      // menuItem57
      // 
      this.menuItem57.Index = 0;
      this.menuItem57.Text = "查看帮助(&H)";
      this.menuItem57.Click += new System.EventHandler(this.menuItem57_Click);
      // 
      // menuItem58
      // 
      this.menuItem58.Index = 1;
      this.menuItem58.Text = "-";
      // 
      // menuItem59
      // 
      this.menuItem59.Index = 2;
      this.menuItem59.Text = "关于记事本(&A)";
      this.menuItem59.Click += new System.EventHandler(this.关于记事本ToolStripMenuItem_Click);
      // 
      // menuItem2
      // 
      this.menuItem2.Index = -1;
      this.menuItem2.Shortcut = Shortcut.CtrlN;
      this.menuItem2.Text = "新建(&N)";
      // 
      // menuItem3
      // 
      this.menuItem3.Index = -1;
      this.menuItem3.Shortcut = Shortcut.CtrlO;
      this.menuItem3.Text = "打开(&O)";
      // 
      // menuItem4
      // 
      this.menuItem4.Index = -1;
      this.menuItem4.Shortcut = Shortcut.CtrlS;
      this.menuItem4.Text = "保存(&S)";
      // 
      // menuItem5
      // 
      this.menuItem5.Index = -1;
      this.menuItem5.Text = "另存为(&A)";
      // 
      // menuItem7
      // 
      this.menuItem7.Index = -1;
      this.menuItem7.Text = "-";
      // 
      // menuItem6
      // 
      this.menuItem6.Index = -1;
      this.menuItem6.Text = "退出(&X)";
      // 
      // menuItem9
      // 
      this.menuItem9.Enabled = false;
      this.menuItem9.Index = -1;
      this.menuItem9.Shortcut = Shortcut.CtrlZ;
      this.menuItem9.Text = "撤消";
      // 
      // menuItem28
      // 
      this.menuItem28.Index = -1;
      this.menuItem28.Text = "-";
      // 
      // menuItem10
      // 
      this.menuItem10.Enabled = false;
      this.menuItem10.Index = -1;
      this.menuItem10.Shortcut = Shortcut.CtrlX;
      this.menuItem10.Text = "剪切(&T)";
      // 
      // menuItem11
      // 
      this.menuItem11.Enabled = false;
      this.menuItem11.Index = -1;
      this.menuItem11.Shortcut = Shortcut.CtrlC;
      this.menuItem11.Text = "复制(&C)";
      // 
      // menuItem12
      // 
      this.menuItem12.Index = -1;
      this.menuItem12.Shortcut = Shortcut.CtrlV;
      this.menuItem12.Text = "粘贴(&P)";
      // 
      // menuItem13
      // 
      this.menuItem13.Enabled = false;
      this.menuItem13.Index = -1;
      this.menuItem13.Shortcut = Shortcut.Del;
      this.menuItem13.Text = "删除(&L)";
      // 
      // menuItem29
      // 
      this.menuItem29.Index = -1;
      this.menuItem29.Text = "-";
      // 
      // menuItem14
      // 
      this.menuItem14.Enabled = false;
      this.menuItem14.Index = -1;
      this.menuItem14.Shortcut = Shortcut.CtrlF;
      this.menuItem14.Text = "查找(&F)";
      // 
      // menuItem15
      // 
      this.menuItem15.Enabled = false;
      this.menuItem15.Index = -1;
      this.menuItem15.Shortcut = Shortcut.F3;
      this.menuItem15.Text = "查找下一个(&N)";
      // 
      // menuItem16
      // 
      this.menuItem16.Enabled = false;
      this.menuItem16.Index = -1;
      this.menuItem16.Text = "替换(&R)";
      // 
      // menuItem17
      // 
      this.menuItem17.Enabled = false;
      this.menuItem17.Index = -1;
      this.menuItem17.Text = "转到(&G)";
      // 
      // menuItem30
      // 
      this.menuItem30.Index = -1;
      this.menuItem30.Text = "-";
      // 
      // menuItem18
      // 
      this.menuItem18.Index = -1;
      this.menuItem18.Shortcut = Shortcut.CtrlA;
      this.menuItem18.Text = "全选(&A)";
      // 
      // menuItem19
      // 
      this.menuItem19.Index = -1;
      this.menuItem19.Text = "时间(&D)";
      // 
      // menuItem21
      // 
      this.menuItem21.Checked = true;
      this.menuItem21.Index = -1;
      this.menuItem21.Text = "自动换行(&W)";
      // 
      // menuItem22
      // 
      this.menuItem22.Index = -1;
      this.menuItem22.Text = "字体(&F)";
      // 
      // menuItem23
      // 
      this.menuItem23.Index = -1;
      this.menuItem23.Text = "颜色(&C)";
      // 
      // menuItem25
      // 
      this.menuItem25.Index = -1;
      this.menuItem25.Text = "状态拦(&S)";
      // 
      // menuItem27
      // 
      this.menuItem27.Index = -1;
      this.menuItem27.Text = "关于我们(&A)";
      // 
      // mtBox1
      // 
      this.mtBox1.AllowDrop = true;
      this.mtBox1.Dock = DockStyle.Fill;
      this.mtBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.mtBox1.HideSelection = false;
      this.mtBox1.ImeMode = ImeMode.On;
      this.mtBox1.Location = new System.Drawing.Point(0, 0);
      this.mtBox1.Multiline = true;
      this.mtBox1.Name = "mtBox1";
      this.mtBox1.ScrollBars = ScrollBars.Both;
      this.mtBox1.Size = new System.Drawing.Size(584, 350);
      this.mtBox1.TabIndex = 4;
      this.mtBox1.WordWrap = false;
      this.mtBox1.TextChanged += new System.EventHandler(this.mtBox1_TextChanged);
      this.mtBox1.DragDrop += new DragEventHandler(this.mtBox1_DragDrop);
      this.mtBox1.PreviewKeyDown += new PreviewKeyDownEventHandler(this.mtBox1_PreviewKeyDown);
      this.mtBox1.Click += new System.EventHandler(this.mtBox1_Click);
      this.mtBox1.DragEnter += new DragEventHandler(this.mtBox1_DragEnter);
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Interval = 1;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // statusBar1
      // 
      this.statusBar1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.statusBar1.Location = new System.Drawing.Point(0, 350);
      this.statusBar1.Name = "statusBar1";
      this.statusBar1.Panels.AddRange(new StatusBarPanel[] {
      this.statusBarPanel1,
      this.statusBarPanel2});
      this.statusBar1.RightToLeft = RightToLeft.No;
      this.statusBar1.ShowPanels = true;
      this.statusBar1.Size = new System.Drawing.Size(584, 22);
      this.statusBar1.TabIndex = 6;
      this.statusBar1.Resize += new System.EventHandler(this.statusBar1_Resize);
      // 
      // statusBarPanel1
      // 
      this.statusBarPanel1.Alignment = HorizontalAlignment.Right;
      this.statusBarPanel1.Name = "statusBarPanel1";
      this.statusBarPanel1.Width = 440;
      // 
      // statusBarPanel2
      // 
      this.statusBarPanel2.BorderStyle = StatusBarPanelBorderStyle.None;
      this.statusBarPanel2.Name = "statusBarPanel2";
      this.statusBarPanel2.Width = 150;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 372);
      this.Controls.Add(this.mtBox1);
      this.Controls.Add(this.statusBar1);
      this.IsMdiContainer = true;
      this.Menu = this.mainMenu1;
      this.Name = "Form1";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "无标题 - 记事本";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.Shown += new System.EventHandler(this.Form1_Shown);
      this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
    /************************************************/
    private FontDialog fontDialog1;
    public SaveFileDialog saveFileDialog1;
    public OpenFileDialog openFileDialog1;
    private PrintDialog printDialog1;
    private PageSetupDialog pageSetupDialog1;
    private System.Drawing.Printing.PrintDocument printDocument1;
    private MainMenu mainMenu1;
    private MenuItem menuItem1;
    private MenuItem menuItem8;
    private MenuItem menuItem20;
    private MenuItem menuItem24;
    private MenuItem menuItem26;
    private MenuItem menuItem2;
    private MenuItem menuItem3;
    private MenuItem menuItem4;
    private MenuItem menuItem5;
    private MenuItem menuItem7;
    private MenuItem menuItem6;
    private MenuItem menuItem9;
    private MenuItem menuItem28;
    private MenuItem menuItem10;
    private MenuItem menuItem11;
    private MenuItem menuItem12;
    private MenuItem menuItem13;
    private MenuItem menuItem29;
    private MenuItem menuItem14;
    private MenuItem menuItem15;
    private MenuItem menuItem16;
    private MenuItem menuItem17;
    private MenuItem menuItem30;
    private MenuItem menuItem18;
    private MenuItem menuItem19;
    private MenuItem menuItem21;
    private MenuItem menuItem22;
    private MenuItem menuItem23;
    private MenuItem menuItem25;
    private MenuItem menuItem27;
    private MenuItem menuItem31;
    private MenuItem menuItem32;
    private MenuItem menuItem33;
    private MenuItem menuItem34;
    private MenuItem menuItem35;
    private MenuItem menuItem36;
    private MenuItem menuItem37;
    private MenuItem menuItem38;
    private MenuItem menuItem39;
    private MenuItem 撤销UToolStripMenuItem;
    private MenuItem menuItem41;
    private MenuItem 剪切ToolStripMenuItem;
    private MenuItem 复制zToolStripMenuItem;
    private MenuItem menuItem44;
    private MenuItem 删除ToolStripMenuItem;
    private MenuItem menuItem46;
    private MenuItem 查找ToolStripMenuItem;
    private MenuItem 查找下一个ToolStripMenuItem;
    private MenuItem menuItem49;
    private MenuItem menuItem50;
    private MenuItem menuItem51;
    private MenuItem menuItem52;
    private MenuItem menuItem53;
    private MenuItem 自动换行ToolStripMenuItem;
    private MenuItem menuItem55;
    private MenuItem 状态栏SToolStripMenuItem;
    private MenuItem menuItem57;
    private MenuItem menuItem58;
    private MenuItem menuItem59;
    public TextBox mtBox1;
    private Timer timer1;
    private StatusBar statusBar1;
    private StatusBarPanel statusBarPanel1;
    private StatusBarPanel statusBarPanel2;
  }
}