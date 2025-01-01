using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PEFileTreeView.Properties;


namespace PEFileTreeView
{
    public class PEFileTreeViewControl : UserControl
    {
        private DoubleBufferedTreeView treeView;

        private ImageList treeViewImageList;

        private static readonly string STANDARD_ICON_KEY = "SectionIconDefault";

        /// <summary>
        /// context menu when node was right clicked
        /// </summary>
        private ContextMenuStrip contextMenuStrip;
        /// <summary>
        /// hold which node was right-clicked at last to access it in context menu
        /// </summary>
        private TreeNode lastRightClickedNode;
    
        /// <summary>
        /// Constructor
        /// </summary>
        public PEFileTreeViewControl()
        {
            InitializeComponent();
            this.ResizeRedraw = true;

            // create the icon list for the tree
            treeViewImageList = new ImageList();
            treeViewImageList.ImageSize = new Size(16, 16);
            treeViewImageList.ColorDepth = ColorDepth.Depth32Bit;
            treeView.ImageList = treeViewImageList;
            // add standard icon to image list
            treeViewImageList.Images.Add(STANDARD_ICON_KEY, GetDefaultIcon());

            // add event handler
            treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);

            // add context menu
            treeView.MouseUp += new MouseEventHandler(treeView_MouseUp);

            contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem collapseItem = new ToolStripMenuItem("Collapse node");
            collapseItem.Click += new EventHandler(collapseItem_Click);
            contextMenuStrip.Items.Add(collapseItem);

            ToolStripMenuItem expandItem = new ToolStripMenuItem("Expand node");
            expandItem.Click += new EventHandler(expandItem_Click);
            contextMenuStrip.Items.Add(expandItem);

            ToolStripMenuItem expandAllItem = new ToolStripMenuItem("Expand all nodes");
            expandAllItem.Click += new EventHandler(expandAllItem_Click);
            contextMenuStrip.Items.Add(expandAllItem);

            treeView.ContextMenuStrip = contextMenuStrip;
        }

        /// <summary>
        /// Get or sets the size of the icons in the treeview
        /// </summary>
        public Size IconSize
        {
            get { return treeViewImageList.ImageSize; }
            set { treeViewImageList.ImageSize = value; }
        }

        /// <summary>
        /// Add a new PE file to the treeview.
        /// </summary>
        /// <param name="newFile"></param>
        public void AddPEFile(IPEFile newFile)
        {
            PEFileNode newNode = new PEFileNode(newFile);
            // add new icon to image list, using the filename as key
            treeViewImageList.Images.Add(newFile.FileName, GetIcon(newFile));
            newNode.ImageKey = newFile.FileName;
            newNode.SelectedImageKey = newFile.FileName;
            // add new node to tree
            treeView.Nodes.Add(newNode);
        }

        /// <summary>
        /// Add new section under given PE file. Ensure that a section is added only once.
        /// </summary>
        /// <param name="peFile"></param>
        /// <param name="section"></param>
        public void AddFileSection(IPEFile peFile, string section)
        {
            TreeNode tn = GetPENode(peFile);
            if (!tn.Nodes.ContainsKey(section))
            {
                PESectionNode newSubNode = new PESectionNode(peFile, section);
                newSubNode.ImageKey = STANDARD_ICON_KEY;
                newSubNode.SelectedImageKey = STANDARD_ICON_KEY;
                newSubNode.Name = section;
                tn.Nodes.Add(newSubNode);
                newSubNode.EnsureVisible();
            }
        }

        /// <summary>
        /// Add a new (pefile) subnode to treeview.
        /// </summary>
        /// <param name="peFile">pe file to add new node to</param>
        /// <param name="parentNodeName">direct parent node to add new node to</param>
        /// <param name="newSectionName">name of new node</param>
        public void AddFileSection(IPEFile peFile, string parentNodeName, string newSectionName)
        {
            TreeNode tn = GetPENode(peFile);
            if (tn != null)
            {
                // found corresponding PE file, find parentNode
                TreeNode parentNode = GetSectionNode(tn, parentNodeName);
                if (parentNode != null && !parentNode.Nodes.ContainsKey(newSectionName))
                {
                    PESectionNode newSubNode = new PESectionNode(peFile, newSectionName);
                    newSubNode.ImageKey = STANDARD_ICON_KEY;
                    newSubNode.SelectedImageKey = STANDARD_ICON_KEY;
                    newSubNode.Name = newSectionName;
                    parentNode.Nodes.Add(newSubNode);
                    //newSubNode.EnsureVisible();
                }
                else
                {
                    throw new InvalidProgramException("Duplicate element");
                }
            }
        }

        public void AddFileSection(IPEFile peFile, PESectionNode parentNode, string newSectionName)
        {
            PESectionNode newSubNode = new PESectionNode(peFile, newSectionName);
            newSubNode.ImageKey = STANDARD_ICON_KEY;
            newSubNode.SelectedImageKey = STANDARD_ICON_KEY;
            newSubNode.Name = newSectionName;
            parentNode.Nodes.Add(newSubNode);
        }

        /// <summary>
        /// Delete completely all nodes of treeview.
        /// </summary>
        public void Clear()
        {
            treeView.Nodes.Clear();
        }

        /// <summary>
        /// Get the standard icon used for file sections from resource stream.
        /// </summary>
        /// <returns></returns>
        private Icon GetDefaultIcon()
        {
            return Icon.FromHandle(Properties.Resources.SectionIconDefault2.GetHicon());
        }

        /// <summary>
        /// Returns the main icon of given PE file.
        /// </summary>
        /// <param name="peFile"></param>
        /// <returns></returns>
        private Icon GetIcon(IPEFile peFile)
        {
            Icon ico = Icon.ExtractAssociatedIcon(peFile.FilePath);
            return ico;
        }

        /// <summary>
        /// Get the corresponding treeview node from the PE file.
        /// </summary>
        /// <param name="peFile"></param>
        /// <returns></returns>
        private TreeNode GetPENode(IPEFile peFile)
        {
            foreach (TreeNode tn in treeView.Nodes)
            {
                if (tn.Tag.Equals(peFile))
                {
                    return tn;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the treeview node with given name that is under the branch of the given tree node.
        /// </summary>
        /// <param name="indirectParentFileNode">the node to start searching. So this must be a parent node
        /// of the node to find, however it does not have to be the direct parent node</param>
        /// <param name="sectionName">name of node to find</param>
        /// <returns></returns>
        private TreeNode GetSectionNode(TreeNode indirectParentFileNode, string sectionName)
        {
            foreach (TreeNode tn in indirectParentFileNode.Nodes)
            {
                if (tn.Text.Equals(sectionName))
                {
                    return tn;
                }

                TreeNode node = GetSectionNode(tn, sectionName);
                if (node != null)
                    return node;
            }
            return null;
        }

        #region Selected Section Changed Event
        
        // delegate for callback when selected section changed
        public delegate void SelectedSectionChangedDelegate(object sender, SectionSelectionChangeEvents e);
        // event when selected section changed
        public event SelectedSectionChangedDelegate SelectedSectionChanged;

        /// <summary>
        /// Internally handle the treeview selection change to fire the section change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void processSelectedSectionEvents(object sender, TreeViewEventArgs e)
        {
            if (SelectedSectionChanged != null)
            {
                SectionSelectionChangeEvents ssce = new SectionSelectionChangeEvents(e);
                SelectedSectionChanged(sender, ssce);
            }
        }
        
        /// <summary>
        /// EventArgs class for the section changed event.
        /// </summary>
        public class SectionSelectionChangeEvents : TreeViewEventArgs
        {
            private PESectionNode peNode;

            public SectionSelectionChangeEvents(TreeViewEventArgs e) : base(e.Node) 
            {
                peNode = (PESectionNode)e.Node;
            }

            public PESectionNode PESectionNode
            {
                get { return peNode; }
            }
        }

        #endregion

        #region Selected PE file Changed Event
        public delegate void SelectedPEFileChangedDelegate(object sender, PEFileSelectionChangeEvents e);

        public event SelectedPEFileChangedDelegate SelectedPEFileChanged;

        /// <summary>
        /// Internally called when a new PE file root node was selected. Handles registered events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void processSelectedFileEvents(object sender, TreeViewEventArgs e)
        {
            if (SelectedPEFileChanged != null)
            {
                PEFileSelectionChangeEvents psce = new PEFileSelectionChangeEvents(e);
                SelectedPEFileChanged(sender, psce);
            }
        }

        /// <summary>
        /// Event class if selected PE file root node has changed.
        /// </summary>
        public class PEFileSelectionChangeEvents : TreeViewEventArgs
        {
            private PEFileNode peNode;

            public PEFileSelectionChangeEvents(TreeViewEventArgs e)
                : base(e.Node)
            {
                peNode = (PEFileNode)e.Node;
            }

            public PEFileNode PEFileNode
            {
                get { return peNode; }
            }
        }

        /// <summary>
        /// Called when the selected node has changed.
        /// Splits the event to differ between a PE file root node has been selected or 
        /// a file section (sub) node has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                processSelectedSectionEvents(sender, e);
            }
            else
            {
                processSelectedFileEvents(sender, e);
            }
        }
        #endregion

        #region Context Menu

        /// <summary>
        /// Called when treeview was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                lastRightClickedNode = treeView.GetNodeAt(e.X, e.Y);
                treeView.SelectedNode = lastRightClickedNode;
                if (lastRightClickedNode != null)
                {
                    contextMenuStrip.Show(treeView, e.Location);
                }
            }
        }

        /// <summary>
        /// Called when user clicked collapse option in context menu of treeview node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void collapseItem_Click(object sender, EventArgs e)
        {
            if (lastRightClickedNode != null)
            {
                if (lastRightClickedNode.IsExpanded)
                {
                    lastRightClickedNode.Collapse();
                }
                lastRightClickedNode = null;
            }
        }

        /// <summary>
        /// Called when the user clicked expand node in context menu of treeview node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void expandItem_Click(object sender, EventArgs e)
        {
            if (lastRightClickedNode != null)
            {
                if (!lastRightClickedNode.IsExpanded)
                {
                    lastRightClickedNode.Expand();
                }
                lastRightClickedNode = null;
            }
        }

        /// <summary>
        /// Called when the user clicked expand all nodes in context menu of treeview node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void expandAllItem_Click(object sender, EventArgs e)
        {
            if (lastRightClickedNode != null)
            {
                if (!lastRightClickedNode.IsExpanded)
                {
                    lastRightClickedNode.ExpandAll();
                }
                lastRightClickedNode = null;
            }
        }

        #endregion

        #region Generated code
        private void InitializeComponent()
        {
            this.treeView = new DoubleBufferedTreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView1";
            this.treeView.Size = new System.Drawing.Size(245, 307);
            this.treeView.TabIndex = 0;
            // 
            // PEFileTreeView
            // 
            this.Controls.Add(this.treeView);
            this.Name = "PEFileTreeView";
            this.Size = new System.Drawing.Size(245, 307);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Select a node in treeview.
        /// </summary>
        /// <param name="nodeName">name of node to select. Must be unique.</param>
        public void SelectNode(string nodeName)
        {
            TreeNode[] foundNodes = treeView.Nodes.Find(nodeName, true);
            if (foundNodes.Length == 1)
            {
                treeView.SelectedNode = foundNodes[0];
                foundNodes[0].EnsureVisible();
                treeView.Focus();
            }

        }
    }
}
