namespace notepad
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.撤销UToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.复制zToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.查找ToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.查找下一个ToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.menuItem50 = new System.Windows.Forms.MenuItem();
            this.menuItem51 = new System.Windows.Forms.MenuItem();
            this.menuItem52 = new System.Windows.Forms.MenuItem();
            this.menuItem53 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.自动换行ToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem55 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.状态栏SToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem57 = new System.Windows.Forms.MenuItem();
            this.menuItem58 = new System.Windows.Forms.MenuItem();
            this.menuItem59 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.mtBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
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
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem8,
            this.menuItem20,
            this.menuItem24,
            this.menuItem26});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            this.menuItem31.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItem31.Text = "新建(&N)";
            this.menuItem31.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // menuItem32
            // 
            this.menuItem32.Index = 1;
            this.menuItem32.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem32.Text = "打开(&O)";
            this.menuItem32.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // menuItem33
            // 
            this.menuItem33.Index = 2;
            this.menuItem33.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
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
            this.menuItem37.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
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
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            this.撤销UToolStripMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
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
            this.剪切ToolStripMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.剪切ToolStripMenuItem.Text = "剪切(&T)";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem1_Click);
            // 
            // 复制zToolStripMenuItem
            // 
            this.复制zToolStripMenuItem.Index = 3;
            this.复制zToolStripMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.复制zToolStripMenuItem.Text = "复制(&C)";
            this.复制zToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 4;
            this.menuItem44.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuItem44.Text = "粘贴(&P)";
            this.menuItem44.Click += new System.EventHandler(this.粘贴ToolStripMenuItem1_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Index = 5;
            this.删除ToolStripMenuItem.Shortcut = System.Windows.Forms.Shortcut.Del;
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
            this.查找ToolStripMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.查找ToolStripMenuItem.Text = "查找(&F)";
            this.查找ToolStripMenuItem.Click += new System.EventHandler(this.查找ToolStripMenuItem_Click);
            // 
            // 查找下一个ToolStripMenuItem
            // 
            this.查找下一个ToolStripMenuItem.Index = 8;
            this.查找下一个ToolStripMenuItem.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.查找下一个ToolStripMenuItem.Text = "查找下一个(&N)";
            this.查找下一个ToolStripMenuItem.Click += new System.EventHandler(this.查找下一个ToolStripMenuItem_Click);
            // 
            // menuItem49
            // 
            this.menuItem49.Index = 9;
            this.menuItem49.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.menuItem49.Text = "替换 (&R)";
            this.menuItem49.Click += new System.EventHandler(this.定位ToolStripMenuItem_Click);
            // 
            // menuItem50
            // 
            this.menuItem50.Index = 10;
            this.menuItem50.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
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
            this.menuItem52.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.menuItem52.Text = "全选(&A)";
            this.menuItem52.Click += new System.EventHandler(this.全选ToolStripMenuItem2_Click);
            // 
            // menuItem53
            // 
            this.menuItem53.Index = 13;
            this.menuItem53.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuItem53.Text = "时间/日期(&D)";
            this.menuItem53.Click += new System.EventHandler(this.时间日期ToolStripMenuItem_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 2;
            this.menuItem20.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            this.menuItem24.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            this.menuItem26.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItem2.Text = "新建(&N)";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem3.Text = "打开(&O)";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = -1;
            this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
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
            this.menuItem9.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
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
            this.menuItem10.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.menuItem10.Text = "剪切(&T)";
            // 
            // menuItem11
            // 
            this.menuItem11.Enabled = false;
            this.menuItem11.Index = -1;
            this.menuItem11.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItem11.Text = "复制(&C)";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = -1;
            this.menuItem12.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.menuItem12.Text = "粘贴(&P)";
            // 
            // menuItem13
            // 
            this.menuItem13.Enabled = false;
            this.menuItem13.Index = -1;
            this.menuItem13.Shortcut = System.Windows.Forms.Shortcut.Del;
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
            this.menuItem14.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuItem14.Text = "查找(&F)";
            // 
            // menuItem15
            // 
            this.menuItem15.Enabled = false;
            this.menuItem15.Index = -1;
            this.menuItem15.Shortcut = System.Windows.Forms.Shortcut.F3;
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
            this.menuItem18.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
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
            this.mtBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mtBox1.HideSelection = false;
            this.mtBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mtBox1.Location = new System.Drawing.Point(0, 0);
            this.mtBox1.Multiline = true;
            this.mtBox1.Name = "mtBox1";
            this.mtBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mtBox1.Size = new System.Drawing.Size(584, 350);
            this.mtBox1.TabIndex = 4;
            this.mtBox1.WordWrap = false;
            this.mtBox1.TextChanged += new System.EventHandler(this.mtBox1_TextChanged);
            this.mtBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.mtBox1_DragDrop);
            this.mtBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.mtBox1_PreviewKeyDown);
            this.mtBox1.Click += new System.EventHandler(this.mtBox1_Click);
            this.mtBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.mtBox1_DragEnter);
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
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2});
            this.statusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(584, 22);
            this.statusBar1.TabIndex = 6;
            this.statusBar1.Resize += new System.EventHandler(this.statusBar1_Resize);
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 440;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 372);
            this.Controls.Add(this.mtBox1);
            this.Controls.Add(this.statusBar1);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无标题 - 记事本";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FontDialog fontDialog1;
        public System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.MenuItem menuItem24;
        private System.Windows.Forms.MenuItem menuItem26;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem28;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem29;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem30;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem22;
        private System.Windows.Forms.MenuItem menuItem23;
        private System.Windows.Forms.MenuItem menuItem25;
        private System.Windows.Forms.MenuItem menuItem27;
        private System.Windows.Forms.MenuItem menuItem31;
        private System.Windows.Forms.MenuItem menuItem32;
        private System.Windows.Forms.MenuItem menuItem33;
        private System.Windows.Forms.MenuItem menuItem34;
        private System.Windows.Forms.MenuItem menuItem35;
        private System.Windows.Forms.MenuItem menuItem36;
        private System.Windows.Forms.MenuItem menuItem37;
        private System.Windows.Forms.MenuItem menuItem38;
        private System.Windows.Forms.MenuItem menuItem39;
        private System.Windows.Forms.MenuItem 撤销UToolStripMenuItem;
        private System.Windows.Forms.MenuItem menuItem41;
        private System.Windows.Forms.MenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.MenuItem 复制zToolStripMenuItem;
        private System.Windows.Forms.MenuItem menuItem44;
        private System.Windows.Forms.MenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.MenuItem menuItem46;
        private System.Windows.Forms.MenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.MenuItem 查找下一个ToolStripMenuItem;
        private System.Windows.Forms.MenuItem menuItem49;
        private System.Windows.Forms.MenuItem menuItem50;
        private System.Windows.Forms.MenuItem menuItem51;
        private System.Windows.Forms.MenuItem menuItem52;
        private System.Windows.Forms.MenuItem menuItem53;
        private System.Windows.Forms.MenuItem 自动换行ToolStripMenuItem;
        private System.Windows.Forms.MenuItem menuItem55;
        private System.Windows.Forms.MenuItem 状态栏SToolStripMenuItem;
        private System.Windows.Forms.MenuItem menuItem57;
        private System.Windows.Forms.MenuItem menuItem58;
        private System.Windows.Forms.MenuItem menuItem59;
        public System.Windows.Forms.TextBox mtBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel2;
    }
}

