using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsEx;
using VariIconsReload.Components;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// user tool for zooming and panning
	/// </summary>
	public class NavigatorTool : Tool
	{
		public NavigatorTool()
		{
			this.Layers.Add(new NavigatorLayer());
		}
	}
	/// <summary>
	/// navigator tool is used to zoom and pan
	/// </summary>
	public class NavigatorLayer : ToolLayer
	{
		private Point _startpoint;
		//deactivate layerview scrolling by mouse
		protected override void OnParentChange(LayerView value)
		{
			if (value != null)
				value.SetScrollFlags(ScrollableView.ScrollFlags.MouseWheelScroll, false);
			if (Owner != null)
				Owner.SetScrollFlags(ScrollableView.ScrollFlags.MouseWheelScroll, true);
			base.OnParentChange(value);
		}
		//start panning
		protected override void OnMouseDown(object sender, MouseEventArgs e)
		{
			Point offset = Owner.RealOffset;
			_startpoint = new Point(
				e.X - offset.X,
				e.Y - offset.Y);
		}
		//pan
		protected override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None ||
				Owner.Cursor != Cursors.Hand) return;
			Owner.AutoScrollPosition = new Point(_startpoint.X - e.X,
				_startpoint.Y - e.Y);
		}
		//zoom or scroll
		protected override void OnMouseWheel(object sender, MouseEventArgs e)
		{
			if (Control.ModifierKeys == Keys.Alt)
			{
				if ((Owner.DisplayMode & DisplayMode.Stretch) != 0)
					return;
				Owner.SetScrollFlags(ScrollableView.ScrollFlags.OptimizedScroll, false);
				Owner.SuspendRefresh();
				//calculate distance of mouse to corner of image
				PointF distance = (PointF)Owner.AutoScrollPosition;
				distance.X = (float)e.X - distance.X;
				distance.Y = (float)e.Y - distance.Y;
				//
				distance = Owner.RealZoom.Unscale(distance);
				int zoomindex = ScaleFactor.GetNearestCommonZoom(Owner.Zoom);
				if (e.Delta > 0)
				{
					if (zoomindex < ScaleFactor.CommonZooms.Length - 1)
						Owner.Zoom = ScaleFactor.CommonZooms[++zoomindex];
				}
				else
				{
					if (zoomindex > 0)
						Owner.Zoom = ScaleFactor.CommonZooms[--zoomindex];
				}
				distance = Owner.RealZoom.Scale(distance);
				//
				Owner.AutoScrollPosition = new Point(
					(int)(-(float)e.X + distance.X),
					(int)(-(float)e.Y + distance.Y));
				//
				Owner.ResumeRefresh();
				Owner.SetScrollFlags(ScrollableView.ScrollFlags.OptimizedScroll, true);
			}
			else if (Control.ModifierKeys == Keys.Control)
				//scroll horizontally
				Owner.AutoScrollPosition = new Point(
					-Owner.AutoScrollPosition.X - e.Delta,
					-Owner.AutoScrollPosition.Y);
			else
				//scroll vertically
				Owner.AutoScrollPosition = new Point(
						-Owner.AutoScrollPosition.X,
						-Owner.AutoScrollPosition.Y - e.Delta);
		}
		//enables pan cursor
		protected override void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
				Owner.Cursor = Cursors.Hand;
		}
		//restores original cursor
		protected override void OnKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
				Owner.Cursor = Cursors.Default;
		}
	}
}
