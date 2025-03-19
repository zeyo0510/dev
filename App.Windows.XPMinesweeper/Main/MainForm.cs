using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using App.Windows.XPMinesweeper.Controls;
using App.Windows.XPMinesweeper.Core;
using App.Windows.XPMinesweeper.Dialogs;
/************************************************/
namespace App.Windows.XPMinesweeper.Main
{
  internal partial class MainForm : Form, IMessageFilter
  {
    private Mines mines = new Mines();
    
    public MainForm()
    {
      this.InitializeComponent();

      this.minePlayer1.MineControl.Mines = mines;
    }
    /************************************************/
    private void MainForm_Load(object sender, EventArgs e)
    {
      Application.AddMessageFilter(this);
      minePlayer1.Reset += new EventHandler(reset);
      this.minePlayer1.MineControl.DigOrMark += new EventHandler(AfterDigOrMark);
      reset(this, e);
    }
    /************************************************/
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void newMenuItem_Click(object sender, EventArgs e)
    {
      reset(this, e);
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void beginnerMenuItem_Click(object sender, EventArgs e)
    {
      this.minePlayer1.Beginner();
      reset(this, EventArgs.Empty);
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void intermediateMenuItem_Click(object sender, EventArgs e)
    {
      this.minePlayer1.Intermediate();
      reset(this, EventArgs.Empty);
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void expertMenuItem_Click(object sender, EventArgs e)
    {
      this.minePlayer1.Expert();
      reset(this, EventArgs.Empty);
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void customMenuItem_Click(object sender, EventArgs e)
    {
      int width  = this.mines.Width;
      int height = this.mines.Height;
      int count  = this.mines.Count;
      /************************************************/
      if (CustomDialog.ShowSelf(this, PointToScreen(minePlayer1.Location), ref width, ref height, ref count))
      {
        this.minePlayer1.Custom(width, height, count);
        reset(this, EventArgs.Empty);
        /************************************************/
        this.UpdateUI();
      }
    }
    /************************************************/
    private void marksMenuItem_Click(object sender, EventArgs e)
    {
      this.mines.AllowMarkDoubt = !this.mines.AllowMarkDoubt;
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void exitMenuItem_Click(object sender, EventArgs e)
    {
      this.CloseApp();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void aboutMenuItem_Click(object sender, EventArgs e)
    {
      Icon ico = new Icon(GetResource("Mine.ico"), 32, 32);
      try
      {
        ShellAbout(Handle, Text, "by Icebird", ico.Handle);
      }
      finally
      {
        ico.Dispose();
        ico = null;
      }
    }
    /************************************************/
    private void minePlayer1_SizeChanged(object sender, EventArgs e)
    {
      base.ClientSize = this.minePlayer1.Size;
    }
    
    
    
    
    /************************************************/
    private void reset(object sender, EventArgs e)
    {
      prevGameState = GameState.NotStarted;
      /************************************************/
      this.minePlayer1.New();
    }
    /************************************************/
    private GameState prevGameState = GameState.NotStarted;

    private void AfterDigOrMark(object sender, EventArgs e)
    {
      if (this.mines.GameState != this.prevGameState)
      {
        if (this.mines.GameState == GameState.Processing)
        {
          this.minePlayer1.StartTimer();
        }
        if (this.mines.GameState == GameState.Complete)
        {
          this.minePlayer1.StopTimer();
          this.minePlayer1.ChangeFace(4);
        }
        if (this.mines.GameState == GameState.Fail)
        {
          this.minePlayer1.StopTimer();
          this.minePlayer1.ChangeFace(3);
        }
        /************************************************/
        this.prevGameState = this.mines.GameState;
      }
      /************************************************/
      this.minePlayer1.RemainMineCount = this.mines.MineRemainCount;
    }

    #region IMessageFilter 傖埜
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_LBUTTONUP = 0x0202;

    public bool PreFilterMessage(ref Message m)
    {
      Control ctrl = Form.FromHandle(m.HWnd);
      switch (m.Msg)
      {
        case WM_LBUTTONDOWN:
          if (this.minePlayer1.MineControl.Enabled && ctrl.Name != "rbReset" && ctrl.FindForm().GetType() != typeof(CustomDialog))
            minePlayer1.ChangeFace(2);
          break;
        case WM_LBUTTONUP:
          if (this.minePlayer1.MineControl.Enabled && ctrl.Name != "rbReset" && ctrl.FindForm().GetType() != typeof(CustomDialog))
            minePlayer1.ChangeFace(1);
          break;
      }
      return false;
    }
    #endregion

    [DllImport("shell32.dll", EntryPoint="ShellAbout")]
    private static extern int ShellAbout(IntPtr hwnd, string szApp, string szOtherStuff, IntPtr hIcon);

    /// <summary>
    /// 植訧埭DLL笢腕剒猁腔訧埭
    /// </summary>
    public Stream GetResource(string fileName)
    {
      if (fileName == null || fileName.Length == 0)
        return null;

      Stream stream = null;
      Type resourceType = this.GetType();
      string resourceName = "App.Windows.XPMinesweeper.Resources." + fileName.Replace("\\", ".");
      System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(resourceType);
      if (assembly == null)
        throw new MineException("拸楊蚾婥訧埭恅璃: " + resourceType.Namespace + ".dll");
      stream = System.Reflection.Assembly.GetAssembly(resourceType).GetManifestResourceStream(resourceName);
      if (stream == null)
        throw new MineException("拸楊腕訧埭: " + fileName);
      return stream;
    }
  }
}