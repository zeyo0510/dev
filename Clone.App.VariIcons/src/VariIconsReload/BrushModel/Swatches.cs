using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ControlsEx;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// a little dynamic palette for color history and selection
	/// </summary>
	[DefaultEvent("SelectedColorChanged")]
	public class Swatches : Control
	{
		#region variables
		private ColorCollection _colors;
		private SizeF _cellsize;
		private Size _layout;
		private int _capacity = 16,
			_selection = -1, _hot = -1, _newest = -1;
		#endregion
		public Swatches()
		{
			_colors = new ColorCollection(this);

			SetStyle(ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.UserMouse |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.SupportsTransparentBackColor,
				true);
		}
		#region helpers
		/// <summary>
		/// updates the rows / cols layout, so the area
		/// of each swatch is maximized
		/// </summary>
		private void UpdateLayout()
		{
			if (_capacity < 1)
				return;
			//find all filling row/col combinations
			List<Size> layouts = new List<Size>();
			for (int cols = 1; cols < _capacity; cols++)
			{
				int rem;
				int rows = Math.DivRem(_capacity, cols, out rem);
				if (rows < cols)//hit sqrt(capacity), all values contained
					break;
				if (rem != 0)//not filling
					continue;
				layouts.Add(new Size(rows, cols));
				if (rows != cols)
					layouts.Add(new Size(cols, rows));
			}
			//find best maching
			_cellsize = SizeF.Empty;
			foreach (Size layout in layouts)
			{
				SizeF cellsize = new SizeF(
					(float)this.ClientSize.Width / (float)layout.Width,
					(float)this.ClientSize.Height / (float)layout.Height);
				if (Math.Min(cellsize.Width, cellsize.Height) >
					Math.Min(_cellsize.Width, _cellsize.Height))
				{
					_cellsize = cellsize;
					_layout = layout;
				}
			}
		}
		/// <summary>
		/// invalidates the swatch at the given position, if valid
		/// </summary>
		public void Invalidate(int index)
		{
			if (index >= 0 && _layout.Width > 0)
			{
				int col, row = Math.DivRem(index, _layout.Width, out col);
				this.Invalidate(Rectangle.Inflate(Rectangle.Round(
					new RectangleF(col * _cellsize.Width, row * _cellsize.Height,
						_cellsize.Width, _cellsize.Height)), 2, 2));
			}
		}
		/// <summary>
		/// gets the index at the given position or -1
		/// </summary>
		public int GetIndexAt(Point pos)
		{
			if (!this.ClientRectangle.Contains(pos) ||
				_cellsize.Width < 1f || _cellsize.Height < 1f)
				return -1;
			int index = _layout.Width * (int)(pos.Y / _cellsize.Height) +
				(int)(pos.X / _cellsize.Width);
			//can't select colors out of range or empty spots
			if (index < -1 || index >= _colors.Count)
				return -1;
			return index;
		}
		#endregion
		#region controller
		//update layout
		protected override void OnSizeChanged(EventArgs e)
		{
			UpdateLayout();
			base.OnSizeChanged(e);
		}
		//draw all
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			RectangleF cell = new RectangleF(PointF.Empty, _cellsize);
			using (SolidBrush sld = new SolidBrush(Color.Black),
				highlight = new SolidBrush(Color.FromArgb(128, SystemColors.Highlight)))
			{
				for (int i = 0, col = 0; i < _capacity; i++)
				{
					//draw swatch
					RectangleF rct = RectangleF.Inflate(cell, -1f, -1f);
					if (i < _colors.Count)
						e.Graphics.FillRectangle(Brushes.White, rct);
					if (i == _selection || i == _hot)
					{
						e.Graphics.FillRectangle(highlight, rct);
						e.Graphics.DrawRectangle(SystemPens.Highlight,
							rct.X, rct.Y, rct.Width, rct.Height);
					}
					else
						e.Graphics.DrawRectangle(Pens.LightGray, rct.X, rct.Y, rct.Width, rct.Height);
					if (i < _colors.Count)
					{
						sld.Color = _colors[i];
						e.Graphics.FillRectangle(sld,
							RectangleF.Inflate(rct, -2f, -2f));
					}
					//move next
					cell.X += cell.Width;
					if (++col >= _layout.Width)
					{
						col = 0;
						cell.X = 0f;
						cell.Y += cell.Height;
					}
				}
			}
		}
		//change selection
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				//change selection
				int sel = GetIndexAt(e.Location);
				if (sel != _selection)
				{
					SelectedIndex = sel;
					RaiseSelectedColorChanged();
				}
			}
		}
		//change highlight
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == MouseButtons.None)
			{
				//change highlight
				int hot = GetIndexAt(e.Location);
				if (hot != _hot)
				{
					Invalidate(hot);
					Invalidate(_hot);
					_hot = hot;
					this.Update();
				}
			}
		}
		//reset highlighting
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (_hot != -1)
			{
				Invalidate(_hot);
				_hot = -1;
				this.Update();
			}
		}
		#endregion
		/// <summary>
		/// selects the given color or adds it
		/// </summary>
		public void SelectOrAddColor(Color value)
		{
			int index = _colors.FindColor(value);
			if (index != -1)
			{
				SelectedIndex = index;
				return;
			}
			//edit
			if (++_newest >= _capacity)
				_newest = 0;
			if (_newest >= _colors.Count)
				_colors.Add(value);
			else
				_colors[_newest] = value;
			SelectedIndex = _newest;
		}
		#region properties
		/// <summary>
		/// gets the colorcollection
		/// </summary>
		[Editor(typeof(ColorCollectionEditor), typeof(UITypeEditor)),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ColorCollection Colors
		{
			get { return _colors; }
		}
		/// <summary>
		/// gets or sets the selected index
		/// </summary>
		[DefaultValue(-1)]
		public int SelectedIndex
		{
			get { return _selection; }
			set
			{
				value = Math.Min(Math.Max(-1, value), _colors.Count - 1);
				if (value == _selection)
					return;
				Invalidate(_selection);
				_selection = value;
				Invalidate(_selection);
				this.Update();
			}
		}
		/// <summary>
		/// gets or sets the selected color
		/// if readonly is disabled and no color
		/// like that is in the swatch, it is added
		/// and selected, eventually overriding old
		/// colors
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		Browsable(false)]
		public Color SelectedColor
		{
			get
			{
				if (_selection == -1)
					return Color.Transparent;
				return _colors[_selection];
			}
			set
			{
				SelectedIndex = _colors.FindColor(value);
			}
		}
		/// <summary>
		/// gets the maximum number of spots
		/// </summary>
		public int Capacity
		{
			get { return _capacity; }
		}
		#endregion
		private void RaiseSelectedColorChanged()
		{
			if (SelectedColorChanged != null)
				SelectedColorChanged(this, EventArgs.Empty);
		}
		public event EventHandler SelectedColorChanged;
	}
	/// <summary>
	/// color collection with a fixed capacity
	/// </summary>
	public class ColorCollection : CollectionBase<Swatches, Color>,IXmlSerializable
	{
		public ColorCollection(Swatches owner) : base(owner) { }
		#region controller
		protected override void OnInsert(int index, Color value)
		{
			if (Count >= Owner.Capacity)
				throw new NotSupportedException("too many items");
			base.OnInsert(index, value);
		}
		protected override void OnInsertComplete(int index, Color value)
		{
			Owner.Refresh();
		}
		protected override void OnSetComplete(int index, Color oldValue, Color newValue)
		{
			Owner.Invalidate(index);
			Owner.Update();
		}
		protected override void OnClearComplete()
		{
			Owner.Refresh();
		}
		protected override void OnRemoveComplete(int index, Color value)
		{
			Owner.Refresh();
		}
		#endregion
		public int FindColor(Color value)
		{
			for (int i = 0; i < Count; i++)
				if (this[i].ToArgb() == value.ToArgb())
					return i;
			return -1;
		}
		public int Capacity
		{
			get { return Owner.Capacity; }
		}

		#region IXmlSerializable Member
		public XmlSchema GetSchema()
		{
			return null;
		}
		//clear and read collection element
		public void ReadXml(XmlReader reader)
		{
			this.Clear();
			TypeConverter colorconv = TypeDescriptor.GetConverter(
				typeof(Color));
			reader.ReadStartElement("colorcollection");
			while (reader.MoveToContent() != XmlNodeType.EndElement)
				Add((Color)colorconv.ConvertFromInvariantString(
					reader.ReadElementString("color")));
			reader.ReadEndElement();
		}
		//write collection element
		public void WriteXml(XmlWriter writer)
		{
			TypeConverter colorconv = TypeDescriptor.GetConverter(
				typeof(Color));
			writer.WriteStartElement("colorcollection");
			foreach (Color col in List)
				writer.WriteElementString("color",
					colorconv.ConvertToInvariantString(col));
			writer.WriteEndElement();
		}
		#endregion
	}
	/// <summary>
	/// collection editor respecting the maximum capacity of a colorcollection
	/// </summary>
	public class ColorCollectionEditor : CollectionEditor
	{
		private ColorCollection col;
		private Random rnd;
		public ColorCollectionEditor(Type type)
			: base(type)
		{
			rnd = new Random();
		}
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			col = value as ColorCollection;
			return base.EditValue(context, provider, value);
		}
		protected override object CreateInstance(Type itemType)
		{
			if (itemType != typeof(Color) || col == null)
				return base.CreateInstance(itemType);
			if (col.Count < col.Capacity)
				return Color.FromArgb(rnd.Next(255),
					rnd.Next(255), rnd.Next(255));
			return null;
		}
	}
}
