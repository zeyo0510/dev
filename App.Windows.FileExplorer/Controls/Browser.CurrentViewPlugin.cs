using System;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    internal IViewPlugin CurrentViewPlugin {
      get { return currentViewPlugin; }
      set { currentViewPlugin = value; }
    }
  }
}