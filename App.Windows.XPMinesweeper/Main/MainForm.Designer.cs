using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using App.Windows.XPMinesweeper.Controls;
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
      ResourceManager resources = new ResourceManager(typeof(MainForm));
      /************************************************/
      this.topMainMenu           = new MainMenu();
      this.gameMenuItem          = new MenuItem();
      this.newMenuItem           = new MenuItem();
      this.beginnerMenuItem      = new MenuItem();
      this.intermediateMenuItem  = new MenuItem();
      this.expertMenuItem        = new MenuItem();
      this.customMenuItem        = new MenuItem();
      this.marksMenuItem         = new MenuItem();
      this.colorMenuItem         = new MenuItem();
      this.soundMenuItem         = new MenuItem();
      this.besttimesMenuItem     = new MenuItem();
      this.exitMenuItem          = new MenuItem();
      this.helpMenuItem          = new MenuItem();
      this.contentsMenuItem      = new MenuItem();
      this.search4helponMenuItem = new MenuItem();
      this.usinghelpMenuItem     = new MenuItem();
      this.aboutMenuItem         = new MenuItem();
      this.mpMine = new MinePlayer();
      this.mcMine = new MineControl();
      /************************************************/
      this.mpMine.SuspendLayout();
      this.SuspendLayout();
      /************************************************/
      // topMainMenu
      {
        this.topMainMenu.Name = "topMainMenu";
        /************************************************/
        this.topMainMenu.MenuItems.Add(this.gameMenuItem);
        this.topMainMenu.MenuItems.Add(this.helpMenuItem);
      }
      // gameMenuItem
      {
        this.gameMenuItem.Text = "&Game";
        /************************************************/
        this.gameMenuItem.MenuItems.Add(this.newMenuItem);
        this.gameMenuItem.MenuItems.Add(new MenuItem("-"));
        this.gameMenuItem.MenuItems.Add(this.beginnerMenuItem);
        this.gameMenuItem.MenuItems.Add(this.intermediateMenuItem);
        this.gameMenuItem.MenuItems.Add(this.expertMenuItem);
        this.gameMenuItem.MenuItems.Add(this.customMenuItem);
        this.gameMenuItem.MenuItems.Add(new MenuItem("-"));
        this.gameMenuItem.MenuItems.Add(this.marksMenuItem);
        this.gameMenuItem.MenuItems.Add(this.colorMenuItem);
        this.gameMenuItem.MenuItems.Add(this.soundMenuItem);
        this.gameMenuItem.MenuItems.Add(new MenuItem("-"));
        this.gameMenuItem.MenuItems.Add(this.besttimesMenuItem);
        this.gameMenuItem.MenuItems.Add(new MenuItem("-"));
        this.gameMenuItem.MenuItems.Add(this.exitMenuItem);
      }
      // newMenuItem
      {
        this.newMenuItem.Name     = "newMenuItem";
        this.newMenuItem.Shortcut = Shortcut.F2;
        this.newMenuItem.Text     = "&New";
        /************************************************/
        this.newMenuItem.Click += this.newMenuItem_Click;
      }
      // beginnerMenuItem
      {
        this.beginnerMenuItem.Name     = "beginnerMenuItem";
        this.beginnerMenuItem.Shortcut = Shortcut.None;
        this.beginnerMenuItem.Text     = "&Beginner";
        /************************************************/
        this.beginnerMenuItem.Click += this.beginnerMenuItem_Click;
      }
      // intermediateMenuItem
      {
        this.intermediateMenuItem.Name     = "intermediateMenuItem";
        this.intermediateMenuItem.Shortcut = Shortcut.None;
        this.intermediateMenuItem.Text     = "&Intermediate";
        /************************************************/
        this.intermediateMenuItem.Click += this.intermediateMenuItem_Click;
      }
      // expertMenuItem
      {
        this.expertMenuItem.Name     = "expertMenuItem";
        this.expertMenuItem.Shortcut = Shortcut.None;
        this.expertMenuItem.Text     = "&Expert";
        /************************************************/
        this.expertMenuItem.Click += this.expertMenuItem_Click;
      }
      // customMenuItem
      {
        this.customMenuItem.Name     = "customMenuItem";
        this.customMenuItem.Shortcut = Shortcut.None;
        this.customMenuItem.Text     = "&Custom...";
        /************************************************/
        this.customMenuItem.Click += this.customMenuItem_Click;
      }
      // marksMenuItem
      {
        this.marksMenuItem.Name     = "marksMenuItem";
        this.marksMenuItem.Shortcut = Shortcut.None;
        this.marksMenuItem.Text     = "&Marks (?)";
        /************************************************/
        this.marksMenuItem.Click += this.marksMenuItem_Click;
      }
      // colorMenuItem
      {
        this.colorMenuItem.Name     = "colorMenuItem";
        this.colorMenuItem.Shortcut = Shortcut.None;
        this.colorMenuItem.Text     = "Co&lor";
      }
      // soundMenuItem
      {
        this.soundMenuItem.Name     = "soundMenuItem";
        this.soundMenuItem.Shortcut = Shortcut.None;
        this.soundMenuItem.Text     = "&Sound";
      }
      // besttimesMenuItem
      {
        this.besttimesMenuItem.Name = "besttimesMenuItem";
        this.besttimesMenuItem.Shortcut = Shortcut.None;
        this.besttimesMenuItem.Text = "Best &Times...";
      }
      // exitMenuItem
      {
        this.exitMenuItem.Name     = "exitMenuItem";
        this.exitMenuItem.Shortcut = Shortcut.None;
        this.exitMenuItem.Text     = "E&xit";
        /************************************************/
        this.exitMenuItem.Click += this.exitMenuItem_Click;
      }
      // helpMenuItem
      {
        this.helpMenuItem.Name     = "helpMenuItem";
        this.helpMenuItem.Shortcut = Shortcut.None;
        this.helpMenuItem.Text     = "&Help";
        /************************************************/
        this.helpMenuItem.MenuItems.Add(this.contentsMenuItem);
        this.helpMenuItem.MenuItems.Add(this.search4helponMenuItem);
        this.helpMenuItem.MenuItems.Add(this.usinghelpMenuItem);
        this.helpMenuItem.MenuItems.Add(new MenuItem("-"));
        this.helpMenuItem.MenuItems.Add(this.aboutMenuItem);
      }
      // contentsMenuItem
      {
        this.contentsMenuItem.Name     = "contentsMenuItem";
        this.contentsMenuItem.Shortcut = Shortcut.F1;
        this.contentsMenuItem.Text     = "&Contents";
      }
      // search4helponMenuItem
      {
        this.search4helponMenuItem.Name     = "search4helponMenuItem";
        this.search4helponMenuItem.Shortcut = Shortcut.None;
        this.search4helponMenuItem.Text     = "&Search for Help on...";
      }
      // usinghelpMenuItem
      {
        this.usinghelpMenuItem.Name     = "usinghelpMenuItem";
        this.usinghelpMenuItem.Shortcut = Shortcut.None;
        this.usinghelpMenuItem.Text     = "Using &Help";
      }
      // aboutMenuItem
      {
        this.aboutMenuItem.Name     = "aboutMenuItem";
        this.aboutMenuItem.Shortcut = Shortcut.None;
        this.aboutMenuItem.Text     = "&About...";
        /************************************************/
        this.aboutMenuItem.Click += this.aboutMenuItem_Click;
      }
      // mpMine
      this.mpMine.Name = "mpMine";
      this.mpMine.CountSecond = 0;
      this.mpMine.Dock = DockStyle.Fill;
      this.mpMine.RemainMineCount = 0;
      this.mpMine.Panel2.Controls.Add(this.mcMine);
      // mcMine
      this.mcMine.Name = "mcMine";
      this.mcMine.BackColor = Color.Silver;
      this.mcMine.Mines = null;
      this.mcMine.Size = new Size(272, 168);
      // MainForm
//      this.AutoScaleBaseSize = new Size(6, 14);
      base.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(322, 256);
      this.Controls.Add(this.mpMine);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
//      this.MaximizeBox = false;
      this.Menu = this.topMainMenu;
      this.Name = "MainForm";
      this.Text = "禸濘";
      this.Load += new System.EventHandler(this.MainForm_Load);
      /************************************************/
      this.mpMine.ResumeLayout(false);
      this.ResumeLayout(false);
    }
    /************************************************/
    private MainMenu topMainMenu           = null;
    private MenuItem gameMenuItem          = null;
    private MenuItem newMenuItem           = null;
    private MenuItem beginnerMenuItem      = null;
    private MenuItem intermediateMenuItem  = null;
    private MenuItem expertMenuItem        = null;
    private MenuItem customMenuItem        = null;
    private MenuItem marksMenuItem         = null;
    private MenuItem colorMenuItem         = null;
    private MenuItem soundMenuItem         = null;
    private MenuItem besttimesMenuItem     = null;
    private MenuItem exitMenuItem          = null;
    private MenuItem helpMenuItem          = null;
    private MenuItem contentsMenuItem      = null;
    private MenuItem search4helponMenuItem = null;
    private MenuItem usinghelpMenuItem     = null;
    private MenuItem aboutMenuItem         = null;
    
    private MinePlayer mpMine;
    private MineControl mcMine;
  }
}