using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(true)]
    public int SplitterDistance {
      get { return browseSplitter.SplitterDistance; }
      set { browseSplitter.SplitterDistance = value; }
    }
  }
}