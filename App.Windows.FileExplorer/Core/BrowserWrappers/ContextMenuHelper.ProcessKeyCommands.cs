using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class ContextMenuHelper
  {
    public static void ProcessKeyCommands(Browser br, object sender, KeyEventArgs e)
    {
        if (e.Control && !e.Shift && !e.Alt)
        {
            switch (e.KeyCode)
            {
                case Keys.C:
                case Keys.Insert:
                case Keys.V:
                case Keys.X:
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IntPtr[] pidls;
                        ShellItem parent;
                        if (sender.Equals(br.FileView) && e.KeyCode != Keys.V)
                        {
                            pidls = new IntPtr[br.FileView.SelectedItems.Count];
                            for (int i = 0; i < pidls.Length; i++)
                            {
                                pidls[i] = ((ShellItem)br.FileView.SelectedItems[i].Tag).PIDLRel.Ptr;
                            }
                            parent = br.SelectedItem;
                        }
                        else
                        {
                            pidls = new IntPtr[1];
                            pidls[0] = br.SelectedItem.PIDLRel.Ptr;
                            parent = (br.SelectedItem.ParentItem != null ? br.SelectedItem.ParentItem : br.SelectedItem);
                        }

                        if (pidls.Length > 0)
                        {
                            string cmd;
                            if (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert)
                                cmd = "copy";
                            else if (e.KeyCode == Keys.V)
                                cmd = "paste";
                            else
                                cmd = "cut";

                            ContextMenuHelper.InvokeCommand(parent, pidls, cmd, new Point(0, 0));
                            Cursor.Current = Cursors.Default;
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.A:
                    {
                        foreach (ListViewItem item in br.FileView.Items)
                            item.Selected = true;

                        br.FileView.Focus();
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.N:
                    if (!br.CreateNewFolder())
                        System.Media.SystemSounds.Beep.Play();
                    
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Z:
                    break;

                case Keys.Y:
                    break;
            }
        }
        else
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    if (e.Shift && !e.Control && !e.Alt)
                    {
                        IntPtr[] pidls = new IntPtr[1];
                        pidls[0] = br.SelectedItem.PIDLRel.Ptr;
                        ShellItem parent = (br.SelectedItem.ParentItem != null ? br.SelectedItem.ParentItem : br.SelectedItem);
                        ContextMenuHelper.InvokeCommand(parent, pidls, "paste", new Point(0, 0));
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Delete:
                    if (!e.Control && !e.Alt)
                    {
                        IntPtr[] pidls;
                        ShellItem parent;
                        if (sender.Equals(br.FileView))
                        {
                            pidls = new IntPtr[br.FileView.SelectedItems.Count];
                            for (int i = 0; i < pidls.Length; i++)
                            {
                                pidls[i] = ((ShellItem)br.FileView.SelectedItems[i].Tag).PIDLRel.Ptr;
                            }
                            parent = br.SelectedItem;
                        }
                        else
                        {
                            pidls = new IntPtr[1];
                            pidls[0] = br.SelectedItem.PIDLRel.Ptr;
                            parent = (br.SelectedItem.ParentItem != null ? br.SelectedItem.ParentItem : br.SelectedItem);
                        }

                        if (pidls.Length > 0)
                            ContextMenuHelper.InvokeCommand(parent, pidls, "delete", new Point(0, 0));
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F4:
                    {
                        br.NavAddressBox.Focus();
                        br.NavAddressBox.DroppedDown = true;
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.F2:
                    if (sender.Equals(br.FolderView))
                    {
                        if (br.FolderView.SelectedNode != null)
                        {
                            br.FolderView.LabelEdit = true;
                            br.FolderView.SelectedNode.BeginEdit();
                        }
                    }
                    else if (sender.Equals(br.FileView))
                    {
                        if (br.FileView.SelectedOrder.Count > 0)
                        {
                            ArrayList temp = new ArrayList();
                            foreach (object obj in br.FileView.SelectedOrder)
                                temp.Add(obj);

                            ListViewItem item = br.FileView.SelectedOrder[0] as ListViewItem;
                            item.BeginEdit();

                            for (int i = temp.Count - 1; i >= 0; i--)
                            {
                                item = temp[i] as ListViewItem;
                                item.Selected = false;
                                item.Selected = true;
                            }
                        }
                    }
                    break;

                case Keys.Back:
                    {
                        if (br.FolderView.SelectedNode != null && br.FolderView.SelectedNode.Parent != null)
                            br.FolderView.SelectedNode = br.FolderView.SelectedNode.Parent;
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }
    }
  }
}