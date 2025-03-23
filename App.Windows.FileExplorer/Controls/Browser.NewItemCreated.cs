using System;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    internal bool NewItemCreated {
      get { return newItemCreated; }
      set { newItemCreated = value; }
    }
  }
}