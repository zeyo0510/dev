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

      /// <summary>
      /// The native window's original handle is released 
      /// and the handle of the TextBox is assigned to it.
      /// </summary>
      public ComboEditWindow(BrowserComboBox owner) {
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

      public Rectangle ImageRect { get { return imageRect; } }

      /// <summary>
      /// Set the margin of the TextBox to make room for the icon
      /// </summary>
      /// <param name="margin">The margin to set</param>
      private void SetMargin(int margin) {
          if (this.margin != margin) {
              this.margin = margin;

              if (owner == null)
                  return;

              WinAPI.SendMessage(
                  this.Handle, WinAPI.WM.SETMARGINS, LEFTMARGIN,
                  new IntPtr(margin));
          }
      }

      /// <summary>
      /// Whenever the textbox is repainted, this method will draw the icon.
      /// </summary>
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

              // Gets a GDI drawing surface from the textbox.
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

      // Override the WndProc method so that we can redraw the TextBox when the textbox is repainted.
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