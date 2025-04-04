using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EConTech.Windows.MACUI
{
	public class MACTrackBarDesigner : ControlDesigner
	{
		public override SelectionRules SelectionRules
		{
			get
			{
				MACTrackBar mACTrackBar = Control as MACTrackBar;
				if (mACTrackBar != null && mACTrackBar.AutoSize)
				{
					if (mACTrackBar.Orientation == Orientation.Horizontal)
					{
						return base.SelectionRules & ~SelectionRules.TopSizeable & ~SelectionRules.BottomSizeable;
					}
					return base.SelectionRules & ~SelectionRules.LeftSizeable & ~SelectionRules.RightSizeable;
				}
				return base.SelectionRules;
			}
		}

		protected override void PostFilterProperties(IDictionary P_0)
		{
			P_0.Remove("AllowDrop");
			P_0.Remove("BackgroundImage");
			P_0.Remove("ContextMenu");
			P_0.Remove("Text");
			P_0.Remove("TextAlign");
			P_0.Remove("RightToLeft");
		}

		protected override void PostFilterEvents(IDictionary P_0)
		{
			P_0.Remove("Click");
			P_0.Remove("DoubleClick");
			P_0.Remove("Paint");
			P_0.Remove("ChangeUICues");
			P_0.Remove("ImeModeChanged");
			P_0.Remove("QueryAccessibilityHelp");
			P_0.Remove("StyleChanged");
			P_0.Remove("SystemColorsChanged");
			P_0.Remove("DragDrop");
			P_0.Remove("DragEnter");
			P_0.Remove("DragLeave");
			P_0.Remove("DragOver");
			P_0.Remove("GiveFeedback");
			P_0.Remove("QueryContinueDrag");
			P_0.Remove("DragDrop");
			P_0.Remove("Layout");
			P_0.Remove("Move");
			P_0.Remove("Resize");
			P_0.Remove("BackColorChanged");
			P_0.Remove("BackgroundImageChanged");
			P_0.Remove("BindingContextChanged");
			P_0.Remove("CausesValidationChanged");
			P_0.Remove("CursorChanged");
			P_0.Remove("FontChanged");
			P_0.Remove("ForeColorChanged");
			P_0.Remove("RightToLeftChanged");
			P_0.Remove("SizeChanged");
			P_0.Remove("TextChanged");
			base.PostFilterEvents(P_0);
		}
	}
}
