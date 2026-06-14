using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class BrowserComboBox
  {
    class ComboEditWindow : NativeWindow
    {
      private const int LEFTMARGIN = 0x1;
      private BrowserComboBox owner;
      private Rectangle imageRect;
      private int margin = 0;

      public ComboEditWindow(BrowserComboBox owner)
      {
        this.owner = owner;

        WinAPI.COMBOBOXINFO info = new WinAPI.COMBOBOXINFO();
        info.cbSize = Marshal.SizeOf(typeof(WinAPI.COMBOBOXINFO));
        WinAPI.GetComboBoxInfo(owner.ComboBox.Handle, ref info);

        if (!this.Handle.Equals(IntPtr.Zero))
        {
            this.ReleaseHandle();
        }
        this.AssignHandle(info.hwndEdit);
      }

      public Rectangle ImageRect
      {
        get
        {
          return imageRect;
        }
      }

      private void SetMargin(int margin)
      {
        if (this.margin != margin)
        {
          this.margin = margin;

          if (owner == null)
            return;

          WinAPI.SendMessage(this.Handle, WinAPI.WM.SETMARGINS, LEFTMARGIN, new IntPtr(margin));
        }
      }

      private void DrawImage()
      {
        if (owner.CurrentItem != null)
        {
          SetMargin(owner.CurrentItem.Image.Width + 3);

          Icon icon = owner.CurrentItem.Image;
          Bitmap image = new Bitmap(icon.Width, icon.Height);
          
          Graphics imageGfx = Graphics.FromImage(image);
          imageRect = new Rectangle(new Point(0, 0), image.Size);
          imageGfx.FillRectangle(Brushes.White, imageRect);
          imageGfx.DrawIcon(icon, 0, 0);
          imageGfx.Flush();

          Graphics gfx = Graphics.FromHwnd(this.Handle);

          if (owner.RightToLeft == RightToLeft.Yes)
          {
              gfx.DrawImage(image, (int)gfx.VisibleClipBounds.Width - icon.Width, 0);
          }
          else if (owner.RightToLeft == RightToLeft.No)
          {
              gfx.DrawImage(image, 0, 0);
          }

          gfx.Flush();
          gfx.Dispose();
        }
      }
      
      /************************************************/

      protected override void WndProc(ref Message m)
      {
        switch (m.Msg)
        {
          case (int)WinAPI.WM.PAINT:
              base.WndProc(ref m);
              DrawImage();
              break;
          case (int)WinAPI.WM.LBUTTONDOWN:
              base.WndProc(ref m);
              DrawImage();
              break;
          case (int)WinAPI.WM.KEYDOWN:
              base.WndProc(ref m);
              DrawImage();
              break;
          case (int)WinAPI.WM.KEYUP:
              base.WndProc(ref m);
              DrawImage();
              break;
          case (int)WinAPI.WM.CHAR:
              base.WndProc(ref m);
              DrawImage();
              break;
          case (int)WinAPI.WM.GETTEXTLENGTH:
              base.WndProc(ref m);
              DrawImage();
              break;
          case (int)WinAPI.WM.GETTEXT:
              base.WndProc(ref m);
              DrawImage();
              break;
          default:
              base.WndProc(ref m);
              break;
        }
      }
    }
  }
}