﻿using System;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer.Main
{
  internal partial class MainForm : Form
  {
    public MainForm()
    {
      this.InitializeComponent();
    }
    /************************************************/
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      /************************************************/
      this.AdjuestClientSize();
      /************************************************/
      this.BuildItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ExitApp();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.CopyItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/    
    private void selectallToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SelectAllItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void selectnoneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SelectNoneItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void invertselectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.InvertSelectionItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.BuildItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ToggleStatusBar();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void messageToolStripStatusLabel_TextChanged(object sender, EventArgs e)
    {
      // do nothing...
    }
  }
}