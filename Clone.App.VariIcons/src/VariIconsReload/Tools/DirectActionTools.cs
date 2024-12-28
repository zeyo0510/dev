using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DrawingEx.ColorManagement.ColorModels;
using DrawingEx.Imaging;
using VariIconsReload.BrushModel;
using VariIconsReload.Components;
using VariIconsReload.Properties;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// tool for simple operations on click
	/// </summary>
	public abstract class DirectActionTool : Tool
	{
		#region types
		/// <summary>
		/// a layer that posts an event on mouseup
		/// </summary>
		private class DirectActionLayer : CrossLayer
		{
			private DirectActionTool _tool;
			public DirectActionLayer(DirectActionTool tool)
			{
				if (tool == null)
					throw new ArgumentNullException("tool");
				_tool = tool;
			}
			//update location and status
			protected override void OnMouseMove(object sender, MouseEventArgs e)
			{
				if (ApplyCursorPosition(e))
					_tool.RaiseToolStatusChanged(
						new Rectangle(Location, new Size(1, 1)));
			}
			//do the action
			protected override void OnMouseUp(object sender, MouseEventArgs e)
			{
				_tool.DoAction(Location, e.Button);
			}
			//update status
			protected override void OnMouseLeave(object sender, EventArgs e)
			{
				base.OnMouseLeave(sender, e);
				_tool.RaiseToolStatusChanged();
			}
		}
		#endregion
		private DirectActionLayer _layer;
		public DirectActionTool()
		{
			Layers.Add(_layer = new DirectActionLayer(this));
		}
		//override this
		protected abstract void DoAction(Point location, MouseButtons btn);
		//owner, cann be NULL
		protected LayerView LayerView
		{
			get { return _layer.Owner; }
		}
	}
	/// <summary>
	/// picks a color and switches back to the old tool
	/// </summary>
	public class PickTool : DirectActionTool
	{
		#region variables
		private BrushManager _brushes;
		private ToolInfo _previous;
		#endregion
		#region ctor
		public PickTool()
		{
			_brushes = BrushManager.RegisterInstance(this);
			if (ToolManager.Instance != null)
				_previous = ToolManager.Instance.CurrentInfo;
		}
		public override void Dispose()
		{
			BrushManager.UnregisterInstance(this);
			_brushes = null;
			base.Dispose();
		}
		#endregion
		#region controller
		//pick color and switch back to old tool
		protected override void DoAction(Point location, MouseButtons btn)
		{
			if (Content != null &&
				new Rectangle(Point.Empty, Content.Bitmap.Size).Contains(location))
			{
				_brushes.PickColor(
					Content.Bitmap.GetPixel(location.X, location.Y),
					btn == MouseButtons.Left);
				if (ToolManager.Instance != null)
					ToolManager.Instance.CurrentInfo = _previous;
			}
		}
		#endregion
	}
	/// <summary>
	/// fills a region with gradient
	/// </summary>
	public class FloodTool : DirectActionTool
	{
		#region variables
		private BrushManager _brushes;
		private ToleranceOption _tolerance;
		#endregion
		#region ctor
		public FloodTool()
		{
			ConfigOptions.Add(_tolerance = new ToleranceOption());
			_brushes = BrushManager.RegisterInstance(this);
		}
		public override void Dispose()
		{
			BrushManager.UnregisterInstance(this);
			_brushes = null;
			base.Dispose();
		}
		#endregion
		#region controller
		//gets the distance between two colors
		private int Distance(ColorBgra a, ColorBgra b)
		{
			return Math.Max((int)a.A ^ (int)b.A,
				Math.Max((int)a.R ^ (int)b.R,
				Math.Max((int)a.G ^ (int)b.G,
						(int)a.B ^ (int)b.B)));
		}
		//performs a quick flood fill and mask stamping
		private void FloodFill(Bitmap bmp, Point location, int tolerance, WrapperBrush brush)
		{
			if (bmp == null || brush == null ||
				!new Rectangle(Point.Empty, bmp.Size).Contains(location))
				return;

			using (ImageLock img = new ImageLock(bmp))
			{
				//for bounds
				int maxx = int.MinValue, minx = int.MaxValue,
					maxy = int.MinValue, miny = int.MaxValue;
				//
				ColorBgra master = img.GetPixel(location.X, location.Y);
				//backtracking flood fill algorithm
				bool[,] mask = new bool[img.Width, img.Height];
				Point[] previous = new Point[img.Width * img.Height];
				int i = 0; previous[0] = location;
				do
				{
					Point curr = previous[i];
					mask[curr.X, curr.Y] = true;
					img.SetPixel(curr.X, curr.Y, ColorBgra.Black);
					//move left
					if (curr.X > 0 && !mask[curr.X - 1, curr.Y] &&
						Distance(master, img.GetPixel(curr.X - 1, curr.Y)) <= tolerance)
						curr.X--;
					//move right
					else if (curr.X < img.Width - 1 && !mask[curr.X + 1, curr.Y] &&
					   Distance(master, img.GetPixel(curr.X + 1, curr.Y)) <= tolerance)
						curr.X++;
					//move up
					else if (curr.Y > 0 && !mask[curr.X, curr.Y - 1] &&
						Distance(master, img.GetPixel(curr.X, curr.Y - 1)) <= tolerance)
						curr.Y--;
					//move down
					else if (curr.Y < img.Height - 1 && !mask[curr.X, curr.Y + 1] &&
						Distance(master, img.GetPixel(curr.X, curr.Y + 1)) <= tolerance)
						curr.Y++;
					//move back
					else { i--; continue; }
					//store step
					previous[++i] = curr;
					if (curr.X > maxx) maxx = curr.X;
					if (curr.X < minx) minx = curr.X;
					if (curr.Y > maxy) maxy = curr.Y;
					if (curr.Y < miny) miny = curr.Y;
				} while (i >= 0);
				//apply mask
				if (minx >= maxx || miny > maxy)
					return;
				using (Bitmap gradient = new Bitmap(maxx - minx + 1, maxy - miny + 1))
				{
					//create gradient
					using (Graphics gr = Graphics.FromImage(gradient))
					{
						Rectangle bounds = new Rectangle(0, 0, gradient.Width, gradient.Height);
						gr.FillRectangle(brush.GetOutBrush(bounds), bounds);
					}
					//stamp gradient to target
					using (ImageLock grad_lock = new ImageLock(gradient))
					{
						for (int x = 0; x < grad_lock.Width; x++)
							for (int y = 0; y < grad_lock.Height; y++)
								if (mask[x + minx, y + miny])
									img.SetPixel(x + minx, y + miny, grad_lock.GetPixel(x, y));
					}
				}
			}
		}
		//trigger floodfill
		protected override void DoAction(Point location, MouseButtons btn)
		{
			if (Content == null || LayerView == null || _brushes == null)
				return;
			FloodFill(Content.Bitmap, location, _tolerance.Value,
				btn == MouseButtons.Left ? _brushes.BrushA : _brushes.BrushB);
			RaiseActionCommitted();
		}
		#endregion
	}
	/// <summary>
	/// layer for displaying remember points
	/// </summary>
	public class PointSelectLayer : Layer
	{
		private List<Point> _points;
		public PointSelectLayer()
		{
			_points = new List<Point>();
			this.Order = 200;
		}
		#region controller
		//paint points
		protected internal override void OnPaint(PaintEventArgs e)
		{
			if (_points.Count < 1)
				return;
			float w = Owner.RealZoom.Unscale(1f);
			using (Pen pn = new Pen(Color.FromArgb(200, 255, 255, 255), w * 3f))
			{
				//draw base
				foreach (Point pt in _points)
					e.Graphics.DrawRectangle(pn, pt.X, pt.Y, 1, 1);
				//draw top
				pn.Color = Color.Red;
				pn.Width = w;
				pn.DashPattern = new float[] { w * 4f, w * 4f };
				foreach (Point pt in _points)
					e.Graphics.DrawRectangle(pn, pt.X, pt.Y, 1, 1);
			}
		}
		//refresh rectangle of point on screen
		private void InvalidatePoint(Point pt)
		{
			if (Owner != null)
			{
				Owner.Invalidate(Rectangle.Inflate(Rectangle.Round(
					Owner.ContentToClient(new Rectangle(pt.X, pt.Y, 1, 1))), 4, 4));
			}
		}
		#endregion
		#region public members
		/// <summary>
		/// toogles the mark of the given point
		/// </summary>
		public void TogglePoint(Point pt)
		{
			int i = _points.IndexOf(pt);
			if (i == -1)
				_points.Add(pt);
			else
				_points.RemoveAt(i);
			InvalidatePoint(pt);
		}
		/// <summary>
		/// removes all marks
		/// </summary>
		public void Clear()
		{
			foreach (Point pt in _points)
				InvalidatePoint(pt);
			_points.Clear();
		}
		#endregion
	}
	/// <summary>
	/// tool that toggles the marker points
	/// </summary>
	public class PointSelectTool : DirectActionTool
	{
		private ActionOption _action;
		public PointSelectTool()
		{
			ConfigOptions.Add(_action = new ActionOption(
				Resources.PointSelectTool_ClearAction, null));
			_action.Action += new EventHandler(ClearAction);
		}
		//search for other layer
		private PointSelectLayer FindLayer()
		{
			if (LayerView == null)
				return null;
			foreach (Layer layer in LayerView.Layers)
			{
				PointSelectLayer sel = layer as PointSelectLayer;
				if (sel != null)
					return sel;
			}
			return null;
		}
		//clear all marks
		void ClearAction(object sender, EventArgs e)
		{
			PointSelectLayer sel = FindLayer();
			if (sel != null)
				sel.Clear();
		}
		//toggle marks
		protected override void DoAction(Point location, MouseButtons btn)
		{
			PointSelectLayer sel = FindLayer();
			if (sel != null)
				sel.TogglePoint(location);
		}
	}
}
