/*
 * Copyright (C)  2011  Axel Kesseler
 * 
 * This software is free and you can use it for any purpose. Furthermore, 
 * you are free to copy, to modify and/or to redistribute this software.
 * 
 * In addition, this software is distributed in the hope that it will be 
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 */

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using plexdata.Controls;
using plexdata.Utilities.Resources;

namespace plexdata.ResourcesViewer
{
    public partial class MainForm : Form
    {
        private bool adjustSplitterDistance = false;

        public MainForm()
        {
            this.InitializeComponent();
            this.Icon = Program.GetFormIcon();

            this.sblFilename.Text = String.Empty;

            this.trvResources.Expanded += new EventHandler<ResourceTreeViewEventArgs>(ResourceTreeView_Expanded);
            this.trvResources.Selected += new EventHandler<ResourceTreeViewEventArgs>(ResourceTreeView_Selected);
            this.trvResources.TreeEmpty += new EventHandler<EventArgs>(ResourceTreeView_TreeEmpty);

            Control root = this.CreateRootView();
            this.splLayouter.Panel2.Controls.Clear(); // Clear controls set by the desinger.
            this.splLayouter.Panel2.Controls.Add(root);
            this.splLayouter.Panel2.Controls.Add(this.CreateBitmapView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateIconsView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateIconView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateCursorsView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateCursorView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateStringView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateVersionView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateHtmlView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateBinaryView(root.Dock, root.TabIndex));
            this.splLayouter.Panel2.Controls.Add(this.CreateTextView(root.Dock, root.TabIndex));
            this.ShowNodeView(root);
        }

        #region Resource tree view event handling.

        private void ResourceTreeView_Expanded(object sender, ResourceTreeViewEventArgs args)
        {
            // TODO: Do something meaningful right here...
        }

        private void ResourceTreeView_Selected(object sender, ResourceTreeViewEventArgs args)
        {
            Cursor previous = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Update status bar with current filename.
                this.sblFilename.Text = args.GetFilename();

                if (args.IsRootNode())
                {
                    this.UpdateRootView((args.Node as ResourceTreeView.RootNode).Filename);
                }
                else if (args.IsTypeNode())
                {
                    ResourceTreeView.TypeNode node = (args.Node as ResourceTreeView.TypeNode);
                    if (node != null)
                    {
                        ResourcesLocator locator = args.GetResourcesLocator();
                        ResourceType type = node.ResourceType;
                        ResourceValue[] values = node.ResourceValues;

                        switch (type.Type)
                        {
                            case ResourceTypes.RT_BITMAP:

                                this.UpdateBitmapView(locator, type, values[0]);
                                break;

                            case ResourceTypes.RT_GROUP_ICON:

                                this.UpdateIconsView(locator, type, values);
                                break;

                            case ResourceTypes.RT_GROUP_CURSOR:

                                this.UpdateCursorsView(locator, type, values);
                                break;

                            case ResourceTypes.RT_STRING:

                                this.UpdateStringView(locator, type, values);
                                break;

                            case ResourceTypes.RT_VERSION:

                                this.UpdateVersionView(locator, type, values[0]);
                                break;

                            case ResourceTypes.RT_HTML:

                                this.UpdateHtmlView(locator, type, values[0]);
                                break;

                            default:

                                if (node.Nodes.Count > 0)
                                {
                                    if ((node.Nodes[0] as ResourceTreeView.ValueNode).IsBinary)
                                    {
                                        this.UpdateBinaryView(locator, type, values[0]);
                                    }
                                    else
                                    {
                                        this.UpdateTextView(locator, type, values[0]);
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (args.IsValueNode())
                {
                    ResourceTreeView.ValueNode node = (args.Node as ResourceTreeView.ValueNode);
                    if (node != null)
                    {
                        ResourcesLocator locator = args.GetResourcesLocator();
                        ResourceType type = node.ResourceType;
                        ResourceValue value = node.ResourceValue;

                        switch (type.Type)
                        {
                            case ResourceTypes.RT_BITMAP:

                                this.UpdateBitmapView(locator, type, value);
                                break;

                            case ResourceTypes.RT_GROUP_ICON:

                                this.UpdateIconView(locator, type, value);
                                break;

                            case ResourceTypes.RT_GROUP_CURSOR:

                                this.UpdateCursorView(locator, type, value);
                                break;

                            case ResourceTypes.RT_STRING:

                                this.UpdateSingleStringView(locator, type, value);
                                break;

                            case ResourceTypes.RT_VERSION:

                                this.UpdateVersionView(locator, type, value);
                                break;

                            case ResourceTypes.RT_HTML:

                                this.UpdateHtmlView(locator, type, value);
                                break;

                            default:
                                if (node.IsBinary)
                                {
                                    this.UpdateBinaryView(locator, type, value);
                                }
                                else
                                {
                                    this.UpdateTextView(locator, type, value);
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.Print(exception.ToString());
            }
            finally
            {
                this.Cursor = previous;
            }
        }

        private void ResourceTreeView_TreeEmpty(object sender, EventArgs args)
        {
            this.UpdateRootView(String.Empty);
        }

        #endregion // Resource tree view event handling.

        #region Resource display control creation section.

        private Control CreateRootView()
        {
            Label control = new Label();
            control.BackColor = Color.White;
            control.Dock = DockStyle.Fill;
            control.Location = new Point(0, 0);
            control.Padding = new Padding(10);
            control.BorderStyle = BorderStyle.Fixed3D;
            control.TabIndex = 2;
            control.Font = new Font(FontFamily.GenericMonospace, Control.DefaultFont.Size);
            control.Text = "Open a file...";
            control.TextAlign = ContentAlignment.MiddleCenter;
            control.Tag = ResourceTreeView.ROOT_NODE;

            return control;
        }

        private Control CreateBitmapView(DockStyle dock, int index)
        {
            Panel control = new Panel();
            control.Dock = dock;
            control.TabIndex = index;
            control.AutoScroll = true;
            control.BorderStyle = BorderStyle.FixedSingle;
            control.Tag = ResourceTreeView.BITMAP_NODE;

            PictureBox picture = new PictureBox();
            picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.CenterImage;

            control.Controls.Add(picture);

            return control;
        }

        private Control CreateIconsView(DockStyle dock, int index)
        {
            // Create an owner draw list view and draw icons with border!
            // see http://www.codeproject.com/KB/cs/IconExtractor.aspx.

            ListView control = new ListView();

            PropertyInfo property = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(control, true, null);

            control.Dock = dock;
            control.View = View.LargeIcon;
            control.TabIndex = index;
            control.ShowItemToolTips = false; // by intention!
            control.Tag = ResourceTreeView.ICON_NODE;
            control.OwnerDraw = true;
            control.UseCompatibleStateImageBehavior = false;

            control.LargeImageList = new ImageList();
            control.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            control.LargeImageList.ImageSize = new Size(32, 32);

            control.DrawItem += new DrawListViewItemEventHandler(IconView_DrawItem);

            return control;
        }

        private Control CreateIconView(DockStyle dock, int index)
        {
            // Create an owner draw list view and draw icons with border!
            // see http://www.codeproject.com/KB/cs/IconExtractor.aspx.

            ListView control = new ListView();

            PropertyInfo property = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(control, true, null);

            control.Dock = dock;
            control.View = View.Tile;
            control.TabIndex = index;
            control.ShowItemToolTips = true;
            control.Tag = ResourceTreeView.ICON_ITEM;
            control.OwnerDraw = true;
            control.UseCompatibleStateImageBehavior = false;

            control.DrawItem += new DrawListViewItemEventHandler(IconView_DrawItem);

            return control;
        }

        private Control CreateCursorsView(DockStyle dock, int index)
        {
            // Create an owner draw list view and draw cursors with border!
            // see http://www.codeproject.com/KB/cs/IconExtractor.aspx.

            ListView control = new ListView();

            PropertyInfo property = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(control, true, null);

            control.Dock = dock;
            control.View = View.LargeIcon;
            control.TabIndex = index;
            control.ShowItemToolTips = false; // by intention!
            control.Tag = ResourceTreeView.CURSOR_NODE;
            control.OwnerDraw = true;
            control.UseCompatibleStateImageBehavior = false;

            control.LargeImageList = new ImageList();
            control.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            control.LargeImageList.ImageSize = new Size(32, 32);

            control.DrawItem += new DrawListViewItemEventHandler(CursorView_DrawItem);

            return control;
        }

        private Control CreateCursorView(DockStyle dock, int index)
        {
            // Create an owner draw list view and draw icons with border!
            // see http://www.codeproject.com/KB/cs/IconExtractor.aspx.

            ListView control = new ListView();

            PropertyInfo property = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(control, true, null);

            control.Dock = dock;
            control.View = View.Tile;
            control.TabIndex = index;
            control.ShowItemToolTips = true;
            control.Tag = ResourceTreeView.CURSOR_ITEM;
            control.OwnerDraw = true;
            control.UseCompatibleStateImageBehavior = false;

            control.DrawItem += new DrawListViewItemEventHandler(CursorView_DrawItem);

            return control;
        }

        private Control CreateStringView(DockStyle dock, int index)
        {
            ListView control = new ListView();

            PropertyInfo property = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(control, true, null);

            control.Dock = dock;
            control.TabIndex = index;
            control.FullRowSelect = true;
            control.View = View.Details;
            control.Tag = ResourceTreeView.STRING_NODE;

            control.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader(), new ColumnHeader() });
            control.Columns[0].Text = "Block ID";
            control.Columns[1].Text = "String ID";
            control.Columns[2].Text = "String";

            control.Columns[0].Width = -2; // Hier nötig ???
            control.Columns[1].Width = -2; // Hier nötig ???
            control.Columns[2].Width = -2; // Hier nötig ???
            control.Columns[0].TextAlign = HorizontalAlignment.Center;
            control.Columns[1].TextAlign = HorizontalAlignment.Center;

            return control;
        }

        private Control CreateVersionView(DockStyle dock, int index)
        {
            TextBox control = new TextBox();
            control.Dock = dock;
            control.TabIndex = index;
            control.ReadOnly = true;
            control.Multiline = true;
            control.WordWrap = false;
            control.ScrollBars = ScrollBars.Both;
            control.Font = new Font(FontFamily.GenericMonospace, Control.DefaultFont.Size);
            control.Tag = ResourceTreeView.VERSION_NODE;

            return control;
        }

        private Control CreateHtmlView(DockStyle dock, int index)
        {
            WebBrowser control = new WebBrowser();
            control.Dock = dock;
            control.TabIndex = index;
            control.Tag = ResourceTreeView.HTML_NODE;

            return control;
        }

        private Control CreateBinaryView(DockStyle dock, int index)
        {
            HexViewer control = new HexViewer();
            control.Dock = dock;
            control.TabIndex = index;
            control.Font = new Font(FontFamily.GenericMonospace, Control.DefaultFont.Size);
            control.Tag = ResourceTreeView.BINARY_NODE;

            return control;
        }

        private Control CreateTextView(DockStyle dock, int index)
        {
            TextBox control = new TextBox();
            control.Dock = dock;
            control.TabIndex = index;
            control.ReadOnly = true;
            control.Multiline = true;
            control.WordWrap = true;
            control.ScrollBars = ScrollBars.Both;
            control.Font = new Font(FontFamily.GenericMonospace, Control.DefaultFont.Size);
            control.Tag = ResourceTreeView.TEXT_NODE;

            return control;
        }

        #endregion // Resource display control creation section.

        #region Resource display control update section.

        private void UpdateRootView(string filename)
        {
            Label control = (this.GetNodeView(ResourceTreeView.ROOT_NODE) as Label);
            if (control != null)
            {
                if (String.IsNullOrEmpty(filename))
                {
                    control.TextAlign = ContentAlignment.MiddleCenter;
                    control.Text = "Open a file...";
                }
                else
                {
                    control.TextAlign = ContentAlignment.TopLeft;
                    try
                    {
                        const int padding = 12;

                        FileInfo info = new FileInfo(filename);
                        List<string> content = new List<string>();

                        // Collect information...
                        content.Add("File Name:".PadRight(padding) + info.Name + "\r\n");
                        content.Add("Location:".PadRight(padding) + info.DirectoryName + "\r\n");
                        content.Add("Attributes:".PadRight(padding) + info.Attributes + "\r\n");
                        content.Add("File Size:".PadRight(padding) + String.Format("{0:#,0}", info.Length) + " Bytes\r\n");
                        content.Add("Created:".PadRight(padding) + info.CreationTimeUtc + " (UTC)\r\n");
                        content.Add("Changed:".PadRight(padding) + info.LastWriteTimeUtc + " (UTC)\r\n");
                        content.Add("Accessed:".PadRight(padding) + info.LastAccessTimeUtc + " (UTC)\r\n");

                        // Flush display.
                        string result = String.Empty;
                        foreach (string current in content)
                        {
                            result += current;
                        }
                        control.Text = result;
                    }
                    catch (Exception exception)
                    {
                        Debug.Print(exception.ToString());
                        control.Text = String.Empty;
                    }
                }
                this.ChooseViewColor(control);
                this.ShowNodeView(control);
            }
        }

        private void UpdateBitmapView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            Panel control = (this.GetNodeView(ResourceTreeView.BITMAP_NODE) as Panel);
            if (control != null && locator != null)
            {
                PictureBox picture = (control.Controls[0] as PictureBox);
                if (picture != null)
                {
                    MemoryStream stream = locator.GetResource(type, value);
                    if (stream != null && stream.Length > 0)
                    {
                        picture.Image = new Bitmap(stream);
                        if (picture.Image != null && picture.Image.Height < control.Height && picture.Image.Width < control.Width)
                        {
                            picture.Dock = DockStyle.Fill;
                            picture.SizeMode = PictureBoxSizeMode.CenterImage;
                        }
                        else
                        {
                            picture.Dock = DockStyle.None;
                            picture.SizeMode = PictureBoxSizeMode.AutoSize;
                        }
                    }
                }
                this.ChooseViewColor(control);
                this.ShowNodeView(control);
            }
        }

        private void UpdateIconsView(ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            ListView control = (this.GetNodeView(ResourceTreeView.ICON_NODE) as ListView);
            if (control != null && locator != null)
            {
                try
                {
                    // Cleanup current image list.
                    // NOTE: It would be a good idea to dispose all images explicitly 
                    //       but this will take a lot of time. Therefore, I trust in  
                    //       the garbage collector that it does its job properly.
                    if (control.LargeImageList != null && control.LargeImageList.Images.Count > 0)
                    {
                        control.LargeImageList.Images.Clear();
                    }

                    ImageList images = new ImageList();
                    images.ColorDepth = control.LargeImageList.ColorDepth;
                    images.ImageSize = control.LargeImageList.ImageSize;

                    List<IconListViewItem> items = new List<IconListViewItem>();
                    foreach (ResourceValue value in values)
                    {
                        MemoryStream stream = locator.GetResource(type, value);
                        if (stream != null && stream.Length > 0)
                        {
                            IconListViewItem item = new IconListViewItem(new Icon(stream), value.Name);
                            images.Images.Add(value.Name, item.Icon);
                            item.ImageKey = value.Name;
                            items.Add(item);

                        }
                    }

                    control.BeginUpdate();
                    control.Items.Clear();
                    control.LargeImageList = images;
                    control.Items.AddRange(items.ToArray());

                    this.ChooseViewColor(control);
                    this.ShowNodeView(control);
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                finally
                {
                    control.EndUpdate();
                }
            }
        }

        private void UpdateIconView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            ListView control = (this.GetNodeView(ResourceTreeView.ICON_ITEM) as ListView);
            if (control != null && locator != null)
            {
                try
                {
                    List<ListViewItem> items = new List<ListViewItem>();
                    using (MemoryStream stream = locator.GetResource(type, value))
                    {
                        if (stream != null && stream.Length > 0)
                        {
                            Icon[] icons = ImageSplitter.SplitIcon(stream);

                            int width = 0;
                            int height = 0;
                            for (int index = 0; index < icons.Length; index++)
                            {
                                // Recalculate needed max-size!
                                if (icons[index].Width > width) { width = icons[index].Width; }
                                if (icons[index].Height > height) { height = icons[index].Height; }

                                // Add current icon to icons list.
                                items.Add(new IconListViewItem(icons[index]));
                            }

                            // Sort icon list.
                            items.Sort();

                            // Set size to be used to display all icons.
                            control.TileSize = new Size(width + 20, height + 20); // Add padding!
                        }
                    }

                    control.BeginUpdate();
                    control.Items.Clear();
                    control.Items.AddRange(items.ToArray());

                    this.ChooseViewColor(control);
                    this.ShowNodeView(control);
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                finally
                {
                    control.EndUpdate();
                }
            }
        }

        private void UpdateCursorsView(ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            ListView control = (this.GetNodeView(ResourceTreeView.CURSOR_NODE) as ListView);
            if (control != null && locator != null)
            {
                try
                {
                    // Cleanup current image list.
                    // NOTE: It would be a good idea to dispose all images explicitly 
                    //       but this will take a lot of time. Therefore, I trust in  
                    //       the garbage collector that it does its job properly.
                    if (control.LargeImageList != null && control.LargeImageList.Images.Count > 0)
                    {
                        control.LargeImageList.Images.Clear();
                    }

                    ImageList images = new ImageList();
                    images.ColorDepth = control.LargeImageList.ColorDepth;
                    images.ImageSize = control.LargeImageList.ImageSize;

                    List<ListViewItem> items = new List<ListViewItem>();
                    foreach (ResourceValue value in values)
                    {
                        MemoryStream stream = locator.GetResource(type, value);
                        if (stream != null && stream.Length > 0)
                        {
                            CursorListViewItem item = new CursorListViewItem(ImageSplitter.CursorToIcon(stream), value.Name);
                            images.Images.Add(value.Name, item.Cursor);
                            item.ImageKey = value.Name;
                            items.Add(item);
                        }
                    }

                    control.BeginUpdate();
                    control.Items.Clear();
                    control.LargeImageList = images;
                    control.Items.AddRange(items.ToArray());

                    this.ChooseViewColor(control);
                    this.ShowNodeView(control);
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                finally
                {
                    control.EndUpdate();
                }
            }
        }

        private void UpdateCursorView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            ListView control = (this.GetNodeView(ResourceTreeView.CURSOR_ITEM) as ListView);
            if (control != null && locator != null)
            {
                try
                {
                    List<ListViewItem> items = new List<ListViewItem>();
                    using (MemoryStream stream = locator.GetResource(type, value))
                    {
                        if (stream != null && stream.Length > 0)
                        {
                            Icon[] cursors = ImageSplitter.SplitCursor(stream);

                            int width = 0;
                            int height = 0;
                            for (int index = 0; index < cursors.Length; index++)
                            {
                                // Recalculate needed max-size!
                                if (cursors[index].Width > width) { width = cursors[index].Width; }
                                if (cursors[index].Height > height) { height = cursors[index].Height; }

                                // Add current cursor to cursors list.
                                items.Add(new CursorListViewItem(cursors[index]));
                            }

                            // Sort cursor list.
                            items.Sort();

                            // Set size to be used to display all cursors.
                            control.TileSize = new Size(width + 20, height + 20); // Add padding!
                        }
                    }

                    control.BeginUpdate();
                    control.Items.Clear();
                    control.Items.AddRange(items.ToArray());

                    this.ChooseViewColor(control);
                    this.ShowNodeView(control);
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                finally
                {
                    control.EndUpdate();
                }
            }
        }

        private void UpdateStringView(ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            ListView control = (this.GetNodeView(ResourceTreeView.STRING_NODE) as ListView);
            if (control != null && locator != null)
            {
                try
                {
                    control.BeginUpdate();
                    control.Items.Clear();

                    foreach (ResourceValue value in values)
                    {
                        MemoryStream stream = locator.GetResource(type, value);
                        if (stream != null && stream.Length > 0)
                        {
                            BinaryReader reader = new BinaryReader(stream);
                            while (stream.Position < stream.Length)
                            {
                                // Keep binary reader's order!
                                int stringID = reader.ReadInt32();
                                int length = reader.ReadInt32();
                                char[] buffer = reader.ReadChars(length);

                                control.Items.Add(new ListViewItem(new string[] { 
                                value.ToString(), 
                                stringID.ToString(), 
                                new string(buffer) 
                            }));
                            }
                        }
                    }
                    control.Columns[0].Width = -2;
                    control.Columns[1].Width = -2;
                    control.Columns[2].Width = -2;

                    this.ChooseViewColor(control);
                    this.ShowNodeView(control);
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                finally
                {
                    control.EndUpdate();
                }
            }
        }

        private void UpdateSingleStringView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            ListView control = (this.GetNodeView(ResourceTreeView.STRING_NODE) as ListView);
            if (control != null && locator != null)
            {
                try
                {
                    control.BeginUpdate();
                    control.Items.Clear();

                    MemoryStream stream = locator.GetResource(type, value);
                    if (stream != null && stream.Length > 0)
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        while (stream.Position < stream.Length)
                        {
                            // Keep binary reader's order!
                            int stringID = reader.ReadInt32();
                            int length = reader.ReadInt32();
                            char[] buffer = reader.ReadChars(length);

                            control.Items.Add(new ListViewItem(new string[] { 
                                value.ToString(), 
                                stringID.ToString(), 
                                new string(buffer) 
                            }));
                        }
                    }

                    control.Columns[0].Width = -2;
                    control.Columns[1].Width = -2;
                    control.Columns[2].Width = -2;

                    this.ChooseViewColor(control);
                    this.ShowNodeView(control);
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                finally
                {
                    control.EndUpdate();
                }
            }
        }

        private void UpdateVersionView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            TextBox control = (this.GetNodeView(ResourceTreeView.VERSION_NODE) as TextBox);
            if (control != null && locator != null)
            {
                MemoryStream stream = locator.GetResource(type, value);
                if (stream != null && stream.Length > 0)
                {
                    VersionInfo version = new VersionInfo();
                    if (version.Read(VersionInfo.GetReader(stream.ToArray())))
                    {
                        control.Lines = version.GetResults();
                    }
                }
                this.ChooseViewColor(control);
                this.ShowNodeView(control);
            }
        }

        private void UpdateHtmlView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            WebBrowser control = (this.GetNodeView(ResourceTreeView.HTML_NODE) as WebBrowser);
            if (control != null && locator != null)
            {
                MemoryStream stream = locator.GetResource(type, value);
                if (stream != null && stream.Length > 0)
                {
                    control.DocumentText = Encoding.UTF8.GetString(stream.ToArray());
                }
                this.ChooseViewColor(control);
                this.ShowNodeView(control);
            }
        }

        private void UpdateBinaryView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            HexViewer control = (this.GetNodeView(ResourceTreeView.BINARY_NODE) as HexViewer);
            if (control != null && locator != null)
            {
                MemoryStream stream = locator.GetResource(type, value);
                if (stream != null && stream.Length > 0)
                {
                    control.Buffer = stream.ToArray();
                    control.BytesPerLine = ApplicationSettings.Instance.Settings.BinaryLineLength;
                }
                this.ChooseViewColor(control);
                this.ShowNodeView(control);
            }
        }

        private void UpdateTextView(ResourcesLocator locator, ResourceType type, ResourceValue value)
        {
            TextBox control = (this.GetNodeView(ResourceTreeView.TEXT_NODE) as TextBox);
            if (control != null && locator != null)
            {
                MemoryStream stream = locator.GetResource(type, value);
                if (stream != null && stream.Length > 0)
                {
                    control.Text = Encoding.UTF8.GetString(stream.ToArray());
                }
                this.ChooseViewColor(control);
                this.ShowNodeView(control);
            }
        }

        #endregion // Resource display control update section.

        #region Resource data export section.

        private void ExportRootData(string fullpath)
        {
            // That's really easy because the current label content has to be saved!
            Label control = (this.GetNodeView(ResourceTreeView.ROOT_NODE) as Label);
            if (control != null)
            {
                using (StreamWriter writer = File.CreateText(fullpath))
                {
                    writer.Write(control.Text);
                    writer.Close();
                }
            }
        }

        private void ExportBitmapData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            // Export as is!
            this.ExportBinaryData(fullpath, locator, type, values);
        }

        private void ExportIconData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            // Export as is!
            this.ExportBinaryData(fullpath, locator, type, values);
        }

        private void ExportCursorData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            // Export as is!
            this.ExportBinaryData(fullpath, locator, type, values);
        }

        private void ExportStringData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            // String tabel is exported at once!
            Cursor old = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string directory = Path.GetDirectoryName(fullpath);
                string filename = Path.GetFileNameWithoutExtension(fullpath) + "_";
                string extension = Path.GetExtension(fullpath);
                string file = fullpath;

                // If only one resources was selected then use resource ID in the filename.
                if (values.Length == 1)
                {
                    file = Path.Combine(directory, filename + values[0].Name + extension);
                }

                using (StreamWriter output = File.CreateText(file))
                {
                    foreach (ResourceValue value in values)
                    {
                        using (MemoryStream stream = locator.GetResource(type, value))
                        {
                            if (stream != null && stream.Length > 0)
                            {
                                // Write header.
                                output.WriteLine("BlockID\tStringID\tString");

                                BinaryReader reader = new BinaryReader(stream);
                                while (stream.Position < stream.Length)
                                {
                                    // Keep binary reader's order!
                                    int stringID = reader.ReadInt32();
                                    int length = reader.ReadInt32();
                                    string buffer = new string(reader.ReadChars(length));

                                    // Replace '\t', '\r' and '\n' with "\\t", "\\r" and "\\n"
                                    buffer = buffer.Replace("\t", "\\t");
                                    buffer = buffer.Replace("\r", "\\r");
                                    buffer = buffer.Replace("\n", "\\n");

                                    // Write output.
                                    output.WriteLine(String.Format("{0}\t{1}\t{2}", value, stringID, buffer));
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                this.Cursor = old;
            }
        }

        private void ExportVersionData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            // Version information are exported at once!
            Cursor old = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string directory = Path.GetDirectoryName(fullpath);
                string filename = Path.GetFileNameWithoutExtension(fullpath) + "_";
                string extension = Path.GetExtension(fullpath);
                string file = fullpath;

                // If only one resources was selected then use resource ID in the filename.
                if (values.Length == 1)
                {
                    file = Path.Combine(directory, filename + values[0].Name + extension);
                }

                using (StreamWriter output = File.CreateText(file))
                {
                    foreach (ResourceValue value in values)
                    {
                        using (MemoryStream stream = locator.GetResource(type, value))
                        {
                            VersionInfo version = new VersionInfo();
                            if (version.Read(VersionInfo.GetReader(stream.ToArray())))
                            {
                                output.WriteLine(String.Format("---------- Version Information (ID = {0}) ----------", value.Value.ToInt32()));
                                foreach (string line in version.GetResults())
                                {
                                    output.WriteLine(line);
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                this.Cursor = old;
            }
        }

        private void ExportHtmlData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            // Export as is!
            this.ExportBinaryData(fullpath, locator, type, values);
        }

        private void ExportBinaryData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            Cursor old = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string directory = Path.GetDirectoryName(fullpath);
                string filename = Path.GetFileNameWithoutExtension(fullpath) + "_";
                string extension = Path.GetExtension(fullpath);
                foreach (ResourceValue value in values)
                {
                    string file = Path.Combine(directory, filename + value.Name + extension);
                    using (MemoryStream stream = locator.GetResource(type, value))
                    using (FileStream output = File.Create(file))
                    {
                        byte[] buffer = stream.ToArray();
                        output.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            finally
            {
                this.Cursor = old;
            }
        }

        private void ExportTextData(string fullpath, ResourcesLocator locator, ResourceType type, ResourceValue[] values)
        {
            // Export as is!
            this.ExportBinaryData(fullpath, locator, type, values);
        }

        #endregion // Resource data export section.

        #region Control access function section.

        private Control GetNodeView()
        {
            Debug.Assert(this.splLayouter.Panel2.Controls.Count != 0);

            // Return top most control (set by a BringToFront() call).
            return this.splLayouter.Panel2.Controls[0];
        }

        private Control GetNodeView(string name)
        {
            foreach (Control current in this.splLayouter.Panel2.Controls)
            {
                if (current.Tag.Equals(name))
                {
                    return current;
                }
            }
            return null;
        }

        private void ShowNodeView(Control control)
        {
            // Hiding or showing the controls is needed to 
            // accomplish an appropriated tab-stop behaviour.

            if (control != null) { control.Show(); control.BringToFront(); }

            foreach (Control current in this.splLayouter.Panel2.Controls)
            {
                if (!current.Equals(control))
                {
                    current.Hide();
                }
            }
        }

        #endregion // Control access function section.

        #region General form event handlers

        private void MainForm_Load(object sender, EventArgs args)
        {
            // If loading configuration fails the form starts with default size and centred!
            if (ApplicationSettings.Instance.Load())
            {
                ApplicationSettings settings = ApplicationSettings.Instance;

                // Set size before set location because location depends on size!
                this.WindowState = settings.MainWindow.State;
                this.Size = settings.MainWindow.Size;
                this.Location = settings.MainWindow.Location;

                // Adjust current color (especially for the placeholder)
                this.ChooseViewColor(this.trvResources);
                this.ChooseViewColor(this.GetNodeView());

                // Bad but needed to adjust visible controls before 
                // loading a file (which takes some time).
                Application.DoEvents();

                if (settings.Settings.AutoLoadLastFiles)
                {
                    // Do application events before loading a large file list!
                    this.LoadFiles(settings.Settings.LastFilenames);
                }

                // TODO: Load other options.
            }
        }

        private void MainForm_Shown(object sender, EventArgs args)
        {
            // REMARK: Fixing the splitter's first panel avoids indeed 
            //         flickering of the Tree View. This should be good 
            //         enough for this program. But also read the remarks 
            //         in the Resource Tree View's constructor.
            this.splLayouter.FixedPanel = FixedPanel.Panel1;

            // BUG: Extensive bug fixing for WinForm's SplitContainer!
            //      The SplitContainer crashes with an InvalidOperationException 
            //      when the surrounding form is loaded minimized. In this case 
            //      it is impossible to adjust the SplitContainer's property! 
            //      Therefore, it is unfortunately necessary to use this funny 
            //      workaround! See also MainForm_SizeChanged() event handler.
            if (this.WindowState != FormWindowState.Minimized)
            {
                // Yes, the SplitterDistance can be adjusted!
                this.splLayouter.SplitterDistance = ApplicationSettings.Instance.MainWindow.SplitterDistance;
            }
            else
            {
                this.adjustSplitterDistance = true;
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs args)
        {
            if (this.adjustSplitterDistance && this.WindowState != FormWindowState.Minimized)
            {
                try // Use try catch anyway!
                {
                    this.splLayouter.SplitterDistance = ApplicationSettings.Instance.MainWindow.SplitterDistance;
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                this.adjustSplitterDistance = false;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs args)
        {
            // Set size before set location because location depends on size!
            ApplicationSettings.Instance.MainWindow.State = this.WindowState;
            ApplicationSettings.Instance.MainWindow.Size = this.Size;
            ApplicationSettings.Instance.MainWindow.Location = this.Location;
            ApplicationSettings.Instance.MainWindow.SplitterDistance = this.splLayouter.SplitterDistance;

            // Keep list of last loaded file even if no files are currently loaded!
            string[] loadedFiles = this.trvResources.LoadedFiles;
            if (loadedFiles.Length > 0)
            {
                ApplicationSettings.Instance.Settings.LastFilenames = this.trvResources.LoadedFiles;
            }

            // TODO: Add other options before saving.
            ApplicationSettings.Instance.Save();
        }

        #endregion // General form event handlers

        #region Toolbar event handlers

        private void ToolbarQuit_Click(object sender, EventArgs args)
        {
            this.Close();
        }

        private void ToolbarOpen_Click(object sender, EventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "All Resource Files (*.exe, *.dll)|*.exe;*.dll|Executable Files (*.exe)|*.exe|Dynamic Link Libraries (*.dll)|*.dll|All Files (*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.RestoreDirectory = true;
            dialog.InitialDirectory = this.trvResources.LastLoadedPath;

            if (DialogResult.OK == dialog.ShowDialog(this))
            {
                bool success = false;

                // Validate file to load!
                using (ResourcesLocator helper = new ResourcesLocator())
                {
                    if (helper.Load(dialog.FileName))
                    {
                        if (helper.Enumerate())
                        {
                            this.LoadFile(dialog.FileName);
                            success = true;
                        }
                    }
                }

                if (!success)
                {
                    string filename = dialog.FileName;
                    try { filename = Path.GetFileName(dialog.FileName); }
                    catch { }

                    string message = "Loading file \"" + filename + "\" failed.";
                    string caption = this.Text + " (Error)";
                    MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ToolbarClose_Click(object sender, EventArgs args)
        {
            this.trvResources.CloseRecource();
        }

        private void ToolbarExport_Click(object sender, EventArgs args)
        {
            try
            {
                ResourceTreeView.BaseNode node = this.trvResources.GetCurrentNode();

                if (node == null)
                {
                    // TODO: Inform somebody.
                    return;
                }

                // Get pre-selected filename but without quotation marks.
                string filename = String.Empty;
                string directory = Path.GetDirectoryName(node.GetRootNode().Filename);
                string filter = "All Files (*.*)|*.*";
                bool isBinary = false;

                if (node.IsRootNode())
                {
                    // Adjust settings to be used with SaveAs settings.
                    filename = Path.GetFileNameWithoutExtension(node.GetRootNode().Filename) + ".info";
                    filter = "File Information (*.txt)|*.txt|All Files (*.*)|*.*";

                    // Ask for the filename (including path) to be used.
                    filename = this.GetExportFilename(filter, filename, directory);
                    if (!String.IsNullOrEmpty(filename))
                    {
                        this.ExportRootData(filename);
                    }

                    return; // Done!
                }
                else if (!node.IsTypeNode() && !node.IsValueNode())
                {
                    // TODO: Inform somebody.
                    return;
                }

                ResourceType type = this.trvResources.GetCurrentResourceType();

                #region Determine required file filter.

                switch (type.Type)
                {
                    case ResourceTypes.RT_BITMAP:
                        filter = "Bitmap Files (*.bmp)|*.bmp|" + filter;
                        break;
                    case ResourceTypes.RT_GROUP_ICON:
                        filter = "Icon Files (*.ico)|*.ico|" + filter;
                        break;
                    case ResourceTypes.RT_GROUP_CURSOR:
                        filter = "Cursor Files (*.cur)|*.cur|" + filter;
                        break;
                    case ResourceTypes.RT_STRING:
                        filter = "String Tables (*.tab)|*.tab|" + filter;
                        break;
                    case ResourceTypes.RT_VERSION:
                        filter = "Version Information (*.ver)|*.ver|" + filter;
                        break;
                    case ResourceTypes.RT_HTML:
                        filter = "HTML Files (*.html)|*.html|" + filter;
                        break;
                    default:
                        ResourceTreeView.ValueNode helper = null;
                        if (node.IsValueNode())
                        {
                            helper = (node as ResourceTreeView.ValueNode);
                        }
                        else
                        {
                            helper = (node.FirstNode as ResourceTreeView.ValueNode);
                        }

                        if (helper != null)
                        {
                            isBinary = helper.IsBinary;
                            if (isBinary)
                            {
                                filter = "Binary Files (*.bin)|*.bin|" + filter;
                            }
                            else
                            {
                                filter = "Plain Text Files (*.txt)|*.txt|" + filter;
                            }
                        }
                        else
                        {
                            // TODO: Handle this issue.
                            return;
                        }
                        break;
                }

                #endregion // Determine required file filter.

                // Adjust settings to be used with SaveAs settings.
                filename = Path.GetFileNameWithoutExtension(node.GetRootNode().Filename) + " ";
                if (node.IsValueNode())
                {
                    filename += node.Parent.Text.Replace("\"", "");
                }
                else
                {
                    filename += node.Text.Replace("\"", "");
                }
                filename = filename.Replace(" ", "_").ToLower();

                // Ask for the filename (including path) to be used.
                filename = this.GetExportFilename(filter, filename, directory);
                if (String.IsNullOrEmpty(filename))
                {
                    return; // No file name chosen.
                }

                ResourcesLocator locator = this.trvResources.GetCurrentResourcesLocator();
                ResourceValue[] values = this.trvResources.GetCurrentResourceValues();
                if (node.IsValueNode())
                {
                    values = new ResourceValue[] { this.trvResources.GetCurrentResourceValue() };
                }

                #region Execute type depending data export.

                switch (type.Type)
                {
                    case ResourceTypes.RT_BITMAP:
                        this.ExportBitmapData(filename, locator, type, values);
                        break;
                    case ResourceTypes.RT_GROUP_ICON:
                        this.ExportIconData(filename, locator, type, values);
                        break;
                    case ResourceTypes.RT_GROUP_CURSOR:
                        this.ExportCursorData(filename, locator, type, values);
                        break;
                    case ResourceTypes.RT_STRING:
                        this.ExportStringData(filename, locator, type, values);
                        break;
                    case ResourceTypes.RT_VERSION:
                        this.ExportVersionData(filename, locator, type, values);
                        break;
                    case ResourceTypes.RT_HTML:
                        this.ExportHtmlData(filename, locator, type, values);
                        break;
                    default:
                        if (isBinary)
                        {
                            this.ExportBinaryData(filename, locator, type, values);
                        }
                        else
                        {
                            this.ExportTextData(filename, locator, type, values);
                        }
                        break;
                }

                #endregion // Execute type depending data export.
            }
            catch (Exception exception)
            {
                Debug.Print(exception.ToString());
            }
        }

        private void ToolbarSettings_Click(object sender, EventArgs args)
        {
            ApplicationSettingsDialog dialog = new ApplicationSettingsDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.SettingsChanged();
            }
        }

        private void ToolbarAbout_Click(object sender, EventArgs args)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(this);
        }

        #endregion // Toolbar event handlers

        #region Drawing handler functions.

        private void IconView_DrawItem(object sender, DrawListViewItemEventArgs args)
        {
            IconListViewItem item = (args.Item as IconListViewItem);

            // Draw default if item is not valid.
            if (item == null)
            {
                args.DrawDefault = true;
                return;
            }

            // Draw default if neither Tile nor LargeIcon view!
            if (item.ListView.View != View.Tile && item.ListView.View != View.LargeIcon)
            {
                args.DrawDefault = true;
                return;
            }

            // First of all draw the background!
            args.DrawBackground();

            Rectangle bounds = new Rectangle(args.Bounds.X, args.Bounds.Y, args.Bounds.Width - 1, args.Bounds.Height - 1);
            Rectangle focus = bounds;
            Color highlight = SystemColors.Highlight;

            // Set clip region to be used.
            args.Graphics.Clip = new Region(new Rectangle(bounds.X, bounds.Y, bounds.Width + 1, bounds.Height + 1));

            if (item.ListView.View == View.LargeIcon)
            {
                // Make focus rectangle a bit smaller in this case!
                focus.Inflate(-(args.Bounds.Width - args.Bounds.Height) / 2 + 10, 0);
            }

            // Draw focus rectangle if needed.
            if (args.Item.Focused)
            {
                // Use pen color from configuration.
                using (Pen pen = new Pen(highlight))
                {
                    args.Graphics.DrawRectangle(pen, focus);
                }
            }
            else
            {
                // Prepare current icon background.
                using (SolidBrush brush = new SolidBrush(ApplicationSettings.Instance.Settings.GetViewBackground()))
                {
                    args.Graphics.FillRectangle(brush, bounds);
                }

                // Use this feature only with tile views!
                if (item.ListView.View == View.Tile)
                {
                    // Use pen color from configuration.
                    using (Pen pen = new Pen(ApplicationSettings.Instance.Settings.GetImageBorderColor()))
                    {
                        args.Graphics.DrawRectangle(pen, focus);
                    }
                }
            }

            // Do it...
            int cx, cy, x, y, delta;
            if (item.ListView.View == View.Tile)
            {
                // Draw the icon centered...
                cx = item.Icon.Width;
                cy = item.Icon.Height;
                x = bounds.X + (bounds.Width - cx) / 2 + 1;
                y = bounds.Y + (bounds.Height - cy) / 2 + 1;
                args.Graphics.DrawIcon(item.Icon, new Rectangle(x, y, cx, cy));
            }
            else if (item.ListView.View == View.LargeIcon)
            {
                Size textField = args.Graphics.MeasureString(item.Text, item.Font).ToSize();
                cx = bounds.Width;
                cy = textField.Height;
                x = bounds.X + (bounds.Width - cx) / 2 + 1;
                y = bounds.Y + bounds.Height - cy;
                delta = bounds.Height - cy + 2;

                // Draw the text centered...
                using (Brush brush = new SolidBrush(item.ForeColor))
                {
                    TextRenderer.DrawText(
                        args.Graphics,
                        item.Text,
                        item.Font,
                        new Rectangle(x, y, cx, cy),
                        item.ForeColor,
                        TextFormatFlags.HorizontalCenter
                    );
                }

                // Draw the icon centered...
                cx = item.Icon.Width;
                cy = item.Icon.Height;
                x = bounds.X + (bounds.Width - cx) / 2 + 1;
                y = bounds.Y + (delta - cy) / 2;
                args.Graphics.DrawIcon(item.Icon, x, y);
            }

            if (args.Item.Selected)
            {
                // Draw selection state onto current image using a half transparent brush (nice).
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, highlight)))
                {
                    focus = new Rectangle(focus.Location, new Size(focus.Width + 1, focus.Height + 1));
                    args.Graphics.FillRectangle(brush, focus);
                }
            }
        }

        private void CursorView_DrawItem(object sender, DrawListViewItemEventArgs args)
        {
            CursorListViewItem item = (args.Item as CursorListViewItem);

            // Draw default if item is not valid.
            if (item == null)
            {
                args.DrawDefault = true;
                return;
            }

            // Draw default if neither Tile nor LargeIcon view!
            if (item.ListView.View != View.Tile && item.ListView.View != View.LargeIcon)
            {
                args.DrawDefault = true;
                return;
            }

            // First of all draw the background!
            args.DrawBackground();

            Rectangle bounds = new Rectangle(args.Bounds.X, args.Bounds.Y, args.Bounds.Width - 1, args.Bounds.Height - 1);
            Rectangle focus = bounds;
            Color highlight = SystemColors.Highlight;

            // Set clip region to be used.
            args.Graphics.Clip = new Region(new Rectangle(bounds.X, bounds.Y, bounds.Width + 1, bounds.Height + 1));

            if (item.ListView.View == View.LargeIcon)
            {
                // Make focus rectangle a bit smaller in this case!
                focus.Inflate(-(args.Bounds.Width - args.Bounds.Height) / 2 + 10, 0);
            }

            // Draw focus rectangle if needed.
            if (args.Item.Focused)
            {
                // Use pen color from configuration.
                using (Pen pen = new Pen(highlight))
                {
                    args.Graphics.DrawRectangle(pen, focus);
                }
            }
            else
            {
                // Prepare current icon background.
                using (SolidBrush brush = new SolidBrush(ApplicationSettings.Instance.Settings.GetViewBackground()))
                {
                    args.Graphics.FillRectangle(brush, bounds);
                }

                // Use this feature only with tile views!
                if (item.ListView.View == View.Tile)
                {
                    // Use pen color from configuration.
                    using (Pen pen = new Pen(ApplicationSettings.Instance.Settings.GetImageBorderColor()))
                    {
                        args.Graphics.DrawRectangle(pen, focus);
                    }
                }
            }

            // Do it...
            int cx, cy, x, y, delta;
            if (item.ListView.View == View.Tile)
            {
                // Draw the icon centered...
                cx = item.Cursor.Width;
                cy = item.Cursor.Height;
                x = bounds.X + (bounds.Width - cx) / 2 + 1;
                y = bounds.Y + (bounds.Height - cy) / 2 + 1;

                using (Bitmap bitmap = CursorListViewItem.CursorToBitmap(item.Cursor))
                {
                    args.Graphics.DrawImage(bitmap, new Rectangle(x, y, cx, cy));
                }
            }
            else if (item.ListView.View == View.LargeIcon)
            {
                Size textField = args.Graphics.MeasureString(item.Text, item.Font).ToSize();
                cx = bounds.Width;
                cy = textField.Height;
                x = bounds.X + (bounds.Width - cx) / 2 + 1;
                y = bounds.Y + bounds.Height - cy;
                delta = bounds.Height - cy + 2;

                // Draw the text centered...
                using (Brush brush = new SolidBrush(item.ForeColor))
                {
                    TextRenderer.DrawText(
                        args.Graphics,
                        item.Text,
                        item.Font,
                        new Rectangle(x, y, cx, cy),
                        item.ForeColor,
                        TextFormatFlags.HorizontalCenter
                    );
                }

                // Draw the icon centered...
                cx = item.Cursor.Width;
                cy = item.Cursor.Height;
                x = bounds.X + (bounds.Width - cx) / 2 + 1;
                y = bounds.Y + (delta - cy) / 2;

                using (Bitmap bitmap = CursorListViewItem.CursorToBitmap(item.Cursor))
                {
                    args.Graphics.DrawImage(bitmap, new Rectangle(x, y, cx, cy));
                }
            }

            if (args.Item.Selected)
            {
                // Draw selection state onto current image using a half transparent brush (nice).
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, highlight)))
                {
                    focus = new Rectangle(focus.Location, new Size(focus.Width + 1, focus.Height + 1));
                    args.Graphics.FillRectangle(brush, focus);
                }
            }
        }

        #endregion // Drawing handler functions.

        #region Helper and conversion classes and functions.

        private void LoadFiles(string[] filenames)
        {
            foreach (string filename in filenames)
            {
                this.LoadFile(filename);
            }
        }

        private void LoadFile(string filename)
        {
            Cursor old = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.sblFilename.Text = filename;
                this.trvResources.OpenRecource(filename);
            }
            catch (Exception exception)
            {
                Debug.Print(exception.ToString());
                this.sblFilename.Text = "";
            }
            finally
            {
                this.Cursor = old;
            }
        }

        private string GetExportFilename(string filter, string filename, string directory)
        {
            string result = String.Empty;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = false; // Allow overwriting existing files without warning!
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.RestoreDirectory = true;
            dialog.InitialDirectory = String.IsNullOrEmpty(ApplicationSettings.Instance.Settings.ExportPath) ? (String.IsNullOrEmpty(directory) ? this.trvResources.LastLoadedPath : directory) : ApplicationSettings.Instance.Settings.ExportPath;
            dialog.Filter = String.IsNullOrEmpty(filter) ? "All Files (*.*)|*.*" : filter;
            dialog.FileName = filename;

            if (DialogResult.OK == dialog.ShowDialog(this))
            {
                result = dialog.FileName;
                try
                {
                    ApplicationSettings.Instance.Settings.ExportPath = Path.GetDirectoryName(dialog.FileName);
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
            }

            return result;
        }

        private void SettingsChanged()
        {
            // Set tree view's color.
            this.ChooseViewColor(this.trvResources);

            // Set current control's color.
            Control current = this.GetNodeView();
            this.ChooseViewColor(current);

            // Setup binary line length if current control is binary.
            if (current.Tag.Equals(ResourceTreeView.BINARY_NODE))
            {
                ResourcesLocator locator = this.trvResources.GetCurrentResourcesLocator();
                ResourceType type = this.trvResources.GetCurrentResourceType();
                ResourceValue value = this.trvResources.GetCurrentResourceValue();
                if (locator != null && type != null && value != null)
                {
                    this.UpdateBinaryView(locator, type, value);
                }
            }
            // Update cursor background color.
            else if (current.Tag.Equals(ResourceTreeView.CURSOR_NODE))
            {
                ResourcesLocator locator = this.trvResources.GetCurrentResourcesLocator();
                ResourceType type = this.trvResources.GetCurrentResourceType();
                ResourceValue[] values = this.trvResources.GetCurrentResourceValues();
                if (locator != null && type != null && values != null)
                {
                    this.UpdateCursorsView(locator, type, values);
                }
            }

            // Reflect additional (especially border) color changes.
            if (current.Tag.Equals(ResourceTreeView.CURSOR_NODE) ||
                current.Tag.Equals(ResourceTreeView.CURSOR_ITEM) ||
                current.Tag.Equals(ResourceTreeView.ICON_NODE) ||
                current.Tag.Equals(ResourceTreeView.ICON_ITEM))
            {
                current.Refresh();
            }
        }

        private void ChooseViewColor(Control control)
        {
            this.ChooseViewColor(control, false);
        }

        private void ChooseViewColor(Control control, bool settingsChanged)
        {
            if (control != null)
            {
                control.BackColor = ApplicationSettings.Instance.Settings.GetViewBackground();
                control.ForeColor = ApplicationSettings.Instance.Settings.GetViewForeground();
            }
        }

        // Comparable and therefore sortabel icon list view item.
        private class IconListViewItem : ListViewItem, IComparable
        {
            private Icon icon = null;
            private string compare = String.Empty;

            public IconListViewItem(Icon icon)
                : base()
            {
                if (icon == null)
                {
                    throw new ArgumentNullException("icon");
                }
                this.Icon = icon;
            }

            public IconListViewItem(Icon icon, string text)
                : this(icon)
            {
                this.Text = text;
            }

            ~IconListViewItem()
            {
                if (this.icon != null)
                {
                    this.icon.Dispose();
                }
            }

            public Icon Icon
            {
                get { return this.icon; }
                private set
                {
                    this.icon = value;
                    int depth = ImageSplitter.GetIconBitDepth(this.icon);
                    this.compare = String.Format("{0:D4}{1:D4}{2:D4}", this.icon.Width, this.icon.Height, depth);
                    this.Text = String.Format("{0}\u00D7{1}, {2} Bit", this.icon.Width, this.icon.Height, depth);
                    this.ToolTipText = this.Text;
                }
            }

            #region IComparable member implementation.

            public int CompareTo(object other)
            {
                if (other is IconListViewItem)
                {
                    return (this.compare.CompareTo((other as IconListViewItem).compare));
                }
                else
                {
                    return this.CompareTo(other);
                }
            }

            #endregion // IComparable member implementation.
        }

        // Comparable and therefore sortabel icon list view item.
        private class CursorListViewItem : ListViewItem, IComparable
        {
            private Icon cursor = null;
            private string compare = String.Empty;

            public CursorListViewItem(Icon cursor)
                : base()
            {
                if (cursor == null)
                {
                    throw new ArgumentNullException("cursor");
                }
                this.Cursor = cursor;
            }

            public CursorListViewItem(Icon cursor, string text)
                : this(cursor)
            {
                this.Text = text;
            }

            ~CursorListViewItem()
            {
                if (this.cursor != null)
                {
                    this.cursor.Dispose();
                }
            }

            public Icon Cursor
            {
                get { return this.cursor; }
                private set
                {
                    this.cursor = value;
                    int depth = ImageSplitter.GetCursorBitDepth(this.cursor);
                    Point hotspot = ImageSplitter.GetCursorHotSpot(this.cursor);
                    this.compare = String.Format("{0:D4}{1:D4}{2:D4}{3:D4}{4:D4}", this.cursor.Width, this.cursor.Height, depth, hotspot.X, hotspot.Y);
                    this.Text = String.Format("{0}\u00D7{1}, {2} Bit, Hot Spot at x={3}, y={4}", this.cursor.Width, this.cursor.Height, depth, hotspot.X, hotspot.Y);
                    this.ToolTipText = this.Text;
                }
            }

            public static Bitmap CursorToBitmap(Icon cursor)
            {
                Bitmap bitmap = new Bitmap(1, 1);
                try
                {
                    bitmap = new Bitmap(cursor.Size.Width, cursor.Size.Height);

                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    using (Brush brush = new SolidBrush(ApplicationSettings.Instance.Settings.GetCursorBackground()))
                    {
                        // Fill background!
                        Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                        // Copy cursor image.
                        graphics.FillRectangle(brush, rectangle);
                        // BUG: The given icon (cursor) has probably a bug 
                        //      because function DrawIcon() (and DrawImage() 
                        //      too) causes an ArgumentException() internally 
                        //      which can be seen only in the Visual Studio 
                        //      output but not here (no catch).
                        graphics.DrawIcon(cursor, rectangle);
                    }
                }
                catch (Exception exception)
                {
                    Debug.Print(exception.ToString());
                }
                return bitmap;
            }

            #region IComparable member implementation.

            public int CompareTo(object other)
            {
                if (other is CursorListViewItem)
                {
                    return (this.compare.CompareTo((other as CursorListViewItem).compare));
                }
                else
                {
                    return this.CompareTo(other);
                }
            }

            #endregion // IComparable member implementation.
        }

        #endregion // Helper and conversion functions.
    }
}
