﻿using System;
using System.Windows.Forms;
using App.Windows.DotnetFrameworkViewer.Main;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer
{
  internal sealed class App
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}