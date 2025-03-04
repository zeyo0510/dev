﻿using System;
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

      mcMine.Mines = mines;
    }
    /************************************************/
    private void MainForm_Load(object sender, System.EventArgs e)
    {
      Application.AddMessageFilter(this);
      mpMine.Reset += new EventHandler(reset);
      mcMine.DigOrMark += new EventHandler(AfterDigOrMark);
      reset(this, e);
    }
    /************************************************/
    private void newMenuItem_Click(object sender, System.EventArgs e)
    {
      reset(this, e);
    }
    /************************************************/
    private void beginnerMenuItem_Click(object sender, System.EventArgs e)
    {
      beginnerMenuItem.Checked = true;
      intermediateMenuItem.Checked = false;
      expertMenuItem.Checked = false;
      customMenuItem.Checked = false;
      mines.Clear(9, 9, 10);
      reset(this, EventArgs.Empty);
    }
    /************************************************/
    private void intermediateMenuItem_Click(object sender, System.EventArgs e)
    {
      beginnerMenuItem.Checked = false;
      intermediateMenuItem.Checked = true;
      expertMenuItem.Checked = false;
      customMenuItem.Checked = false;
      mines.Clear(16, 16, 40);
      reset(this, EventArgs.Empty);
    }
    /************************************************/
    private void expertMenuItem_Click(object sender, System.EventArgs e)
    {
      beginnerMenuItem.Checked = false;
      intermediateMenuItem.Checked = false;
      expertMenuItem.Checked = true;
      customMenuItem.Checked = false;
      mines.Clear(30, 16, 99);
      reset(this, EventArgs.Empty);
    }
    /************************************************/
    private void customMenuItem_Click(object sender, System.EventArgs e)
    {
      int height = mines.Height;
      int width = mines.Width;
      int mineCount = mines.Count;
      if (CustomDialog.ShowSelf(this, PointToScreen(mpMine.Location), ref width, ref height, ref mineCount))
      {
        beginnerMenuItem.Checked = false;
        intermediateMenuItem.Checked = false;
        expertMenuItem.Checked = false;
        customMenuItem.Checked = true;
        mines.Clear(width, height, mineCount);
        reset(this, EventArgs.Empty);
      }
    }
    /************************************************/
    private void marksMenuItem_Click(object sender, System.EventArgs e)
    {
      MenuItem mi = sender as MenuItem;
      if (mi != null)
      {
        mi.Checked = !mi.Checked;
        mines.AllowMarkDoubt = mi.Checked;
      }
    }
    /************************************************/
    private void exitMenuItem_Click(object sender, System.EventArgs e)
    {
      Close();
    }
    /************************************************/
    private void aboutMenuItem_Click(object sender, System.EventArgs e)
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
    private void reset(object sender, EventArgs e)
    {
      prevGameState = GameState.NotStarted;
      mines.Clear();
      mcMine.AdjustSize();
      ClientSize = mpMine.GetWindowClientSize(mcMine.Size);
      mcMine.Refresh();
      mpMine.StopTimer();
      mpMine.RemainMineCount = mines.MineRemainCount;
      mpMine.CountSecond = 0;
      mpMine.ChangeFace(1);
    }

    private GameState prevGameState = GameState.NotStarted;

    private void AfterDigOrMark(object sender, EventArgs e)
    {
      if (mines.GameState != prevGameState)
      {
        if (mines.GameState == GameState.Processing)
          mpMine.StartTimer();

        prevGameState = mines.GameState;

        if (mines.GameState == GameState.Complete || mines.GameState == GameState.Fail)
        {
          mpMine.StopTimer();
          if (mines.GameState == GameState.Complete)
            mpMine.ChangeFace(4);
          else
            mpMine.ChangeFace(3);
        }
      }
      mpMine.RemainMineCount = mines.MineRemainCount;
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
          if (mcMine.Enabled && ctrl.Name != "rbReset" && ctrl.FindForm().GetType() != typeof(CustomDialog))
            mpMine.ChangeFace(2);
          break;
        case WM_LBUTTONUP:
          if (mcMine.Enabled && ctrl.Name != "rbReset" && ctrl.FindForm().GetType() != typeof(CustomDialog))
            mpMine.ChangeFace(1);
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