using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.TreeViewAdv.Tree
{
	internal class InputWithControl: NormalInputState
	{
		public InputWithControl(TreeViewAdv tree): base(tree)
		{
		}

		protected override void DoMouseOperation(TreeNodeAdvMouseEventArgs args)
		{
			if (Tree.SelectionMode == TreeSelectionMode.Single)
			{
				base.DoMouseOperation(args);
			}
			else if (CanSelect(args.Node))
			{
				args.Node.IsSelected = !args.Node.IsSelected;
				Tree.SelectionStart = args.Node;
			}
		}

		protected override void MouseDownAtEmptySpace(TreeNodeAdvMouseEventArgs args)
		{
		}
	}
}
