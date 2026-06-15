using System;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitDragDrop()
    {
      if (AllowDrop)
      {
        tvDropWrapper = new BrowserTVDropWrapper(this);
        lvDropWrapper = new BrowserLVDropWrapper(this);

        tvDropWrapper.Drop += new DropEventHandler(DropWrapper_Drop);
        lvDropWrapper.Drop += new DropEventHandler(DropWrapper_Drop);

        tvDragWrapper = new BrowserTVDragWrapper(this);
        lvDragWrapper = new BrowserLVDragWrapper(this);

        tvDragWrapper.DragStart += new DragEnterEventHandler(DragWrapper_DragStart);
        lvDragWrapper.DragStart += new DragEnterEventHandler(DragWrapper_DragStart);

        tvDragWrapper.DragEnd += new EventHandler(DragWrapper_DragEnd);
        lvDragWrapper.DragEnd += new EventHandler(DragWrapper_DragEnd);
      }
    }
  }
}