using System;
using System.Collections.Generic;
using System.Text;
using Common_Tools.TreeViewAdv.Tree.NodeControls;
using System.Drawing;

namespace Common_Tools.TreeViewAdv.Tree
{
	public struct NodeControlInfo
	{
		public static readonly NodeControlInfo Empty = new NodeControlInfo(null, Rectangle.Empty, null);

		private NodeControl _control;
		public NodeControl Control
		{
			get { return _control; }
		}

		private Rectangle _bounds;
		public Rectangle Bounds
		{
			get { return _bounds; }
		}

		private TreeNodeAdv _node;
		public TreeNodeAdv Node
		{
			get { return _node; }
		}

		public NodeControlInfo(NodeControl control, Rectangle bounds, TreeNodeAdv node)
		{
			_control = control;
			_bounds = bounds;
			_node = node;
		}
	}
}
