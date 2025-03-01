using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Main
{
  partial class MainForm
  {
    private Container components = null;
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
      /************************************************/
      this.mmMainMenu = new MainMenu();
      this.miGame = new MenuItem();
      this.miGameStart = new MenuItem();
      this.miS1 = new MenuItem();
      this.miNovice = new MenuItem();
      this.miMaster = new MenuItem();
      this.miExpert = new MenuItem();
      this.miCustomGame = new MenuItem();
      this.miS2 = new MenuItem();
      this.miMark = new MenuItem();
      this.miColor = new MenuItem();
      this.miSound = new MenuItem();
      this.miS3 = new MenuItem();
      this.miHiscore = new MenuItem();
      this.miS4 = new MenuItem();
      this.miExit = new MenuItem();
      this.miHelp = new MenuItem();
      this.miIndex = new MenuItem();
      this.miSearchTopic = new MenuItem();
      this.miUsingHelp = new MenuItem();
      this.miS5 = new MenuItem();
      this.miAbout = new MenuItem();
      this.mpMine = new Minesweeper.MinePanel();
      this.mcMine = new Minesweeper.MineControl();
      /************************************************/
      this.mpMine.SuspendLayout();
      this.SuspendLayout();
      /************************************************/
      // 
      // mmMainMenu
      // 
      this.mmMainMenu.MenuItems.AddRange(new MenuItem[] {
                                             this.miGame,
                                             this.miHelp});
      // 
      // miGame
      // 
      this.miGame.Index = 0;
      this.miGame.MenuItems.AddRange(new MenuItem[] {
                                           this.miGameStart,
                                           this.miS1,
                                           this.miNovice,
                                           this.miMaster,
                                           this.miExpert,
                                           this.miCustomGame,
                                           this.miS2,
                                           this.miMark,
                                           this.miColor,
                                           this.miSound,
                                           this.miS3,
                                           this.miHiscore,
                                           this.miS4,
                                           this.miExit});
      this.miGame.Text = "蚔牁(&G)";
      // 
      // miGameStart
      // 
      this.miGameStart.Index = 0;
      this.miGameStart.Shortcut = Shortcut.F2;
      this.miGameStart.Text = "羲擁(&N)";
      this.miGameStart.Click += new System.EventHandler(this.miGameStart_Click);
      // 
      // miS1
      // 
      this.miS1.Index = 1;
      this.miS1.Text = "-";
      // 
      // miNovice
      // 
      this.miNovice.Checked = true;
      this.miNovice.Index = 2;
      this.miNovice.Text = "場撰(&B)";
      this.miNovice.Click += new System.EventHandler(this.miNovice_Click);
      // 
      // miMaster
      // 
      this.miMaster.Index = 3;
      this.miMaster.Text = "笢撰(&I)";
      this.miMaster.Click += new System.EventHandler(this.miMaster_Click);
      // 
      // miExpert
      // 
      this.miExpert.Index = 4;
      this.miExpert.Text = "詢撰(&E)";
      this.miExpert.Click += new System.EventHandler(this.miExpert_Click);
      // 
      // miCustomGame
      // 
      this.miCustomGame.Index = 5;
      this.miCustomGame.Text = "赻隅砱(&C)...";
      this.miCustomGame.Click += new System.EventHandler(this.miCustomGame_Click);
      // 
      // miS2
      // 
      this.miS2.Index = 6;
      this.miS2.Text = "-";
      // 
      // miMark
      // 
      this.miMark.Checked = true;
      this.miMark.Index = 7;
      this.miMark.Text = "梓暮(?)(&M)";
      this.miMark.Click += new System.EventHandler(this.miMark_Click);
      // 
      // miColor
      // 
      this.miColor.Checked = true;
      this.miColor.Enabled = false;
      this.miColor.Index = 8;
      this.miColor.Text = "晇伎(&L)";
      // 
      // miSound
      // 
      this.miSound.Enabled = false;
      this.miSound.Index = 9;
      this.miSound.Text = "汒秞(&S)";
      // 
      // miS3
      // 
      this.miS3.Index = 10;
      this.miS3.Text = "-";
      // 
      // miHiscore
      // 
      this.miHiscore.Enabled = false;
      this.miHiscore.Index = 11;
      this.miHiscore.Text = "禸濘荎倯埤(&T)...";
      // 
      // miS4
      // 
      this.miS4.Index = 12;
      this.miS4.Text = "-";
      // 
      // miExit
      // 
      this.miExit.Index = 13;
      this.miExit.Text = "豖堤(&X)";
      this.miExit.Click += new System.EventHandler(this.miExit_Click);
      // 
      // miHelp
      // 
      this.miHelp.Index = 1;
      this.miHelp.MenuItems.AddRange(new MenuItem[] {
                                           this.miIndex,
                                           this.miSearchTopic,
                                           this.miUsingHelp,
                                           this.miS5,
                                           this.miAbout});
      this.miHelp.Text = "堆翑(&H)";
      // 
      // miIndex
      // 
      this.miIndex.Enabled = false;
      this.miIndex.Index = 0;
      this.miIndex.Shortcut = Shortcut.F1;
      this.miIndex.Text = "醴翹(&C)";
      // 
      // miSearchTopic
      // 
      this.miSearchTopic.Enabled = false;
      this.miSearchTopic.Index = 1;
      this.miSearchTopic.Text = "脤梑堆翑翋枙(&S)...";
      // 
      // miUsingHelp
      // 
      this.miUsingHelp.Enabled = false;
      this.miUsingHelp.Index = 2;
      this.miUsingHelp.Text = "妏蚚堆翑(&H)";
      // 
      // miS5
      // 
      this.miS5.Index = 3;
      this.miS5.Text = "-";
      // 
      // miAbout
      // 
      this.miAbout.Index = 4;
      this.miAbout.Text = "壽衾禸濘(&A)...";
      this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
      // 
      // mpMine
      // 
      this.mpMine.BackColor = System.Drawing.Color.Silver;
      this.mpMine.Controls.Add(this.mcMine);
      this.mpMine.CountSecond = 0;
      this.mpMine.Dock = DockStyle.Fill;
      this.mpMine.Location = new System.Drawing.Point(0, 0);
      this.mpMine.Name = "mpMine";
      this.mpMine.RemainMineCount = 0;
      this.mpMine.Size = new System.Drawing.Size(322, 256);
      this.mpMine.TabIndex = 1;
      // 
      // mcMine
      // 
      this.mcMine.BackColor = System.Drawing.Color.Silver;
      this.mcMine.Location = new System.Drawing.Point(24, 72);
      this.mcMine.Mines = null;
      this.mcMine.Name = "mcMine";
      this.mcMine.Size = new System.Drawing.Size(272, 168);
      this.mcMine.TabIndex = 4;
      // MainForm
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(322, 256);
      this.Controls.Add(this.mpMine);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Menu = this.mmMainMenu;
      this.Name = "MainForm";
      this.Text = "禸濘";
      this.SizeChanged += new System.EventHandler(this.frmMinesweeper_SizeChanged);
      this.Load += new System.EventHandler(this.frmMinesweeper_Load);
      this.mpMine.ResumeLayout(false);
      this.ResumeLayout(false);
    }
    
    private MenuItem miGame;
    private MenuItem miHelp;
    private MainMenu mmMainMenu;
    private MenuItem miIndex;
    private MenuItem miSearchTopic;
    private MenuItem miUsingHelp;
    private MenuItem miAbout;
    private MenuItem miS1;
    private MenuItem miS2;
    private MenuItem miS3;
    private MenuItem miS4;
    private MenuItem miS5;
    private MenuItem miGameStart;
    private MenuItem miExpert;
    private MenuItem miCustomGame;
    private MenuItem miMark;
    private MenuItem miColor;
    private MenuItem miSound;
    private MenuItem miHiscore;
    private MenuItem miExit;
    private Minesweeper.MinePanel mpMine;
    private Minesweeper.MineControl mcMine;
    private MenuItem miNovice;
    private MenuItem miMaster;
  }
}