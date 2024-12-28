using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DrawingEx.ColorManagement;

namespace VariIconsReload.Components
{
	public class ToolStripColorButton : ToolStripButton
	{
		private Color _color = Color.Black;
		private Bitmap _img;
		public ToolStripColorButton()
		{
			base.Image = _img = new Bitmap(16, 16);
			base.DisplayStyle = ToolStripItemDisplayStyle.Image;
			UpdateColor();
		}
		//update color image
		void UpdateColor()
		{
			using (Graphics gr = Graphics.FromImage(_img))
			{
				gr.Clear(Color.Transparent);
				using (SolidBrush brs = new SolidBrush(_color))
					gr.FillRectangle(brs, 3, 3, 10, 10);
				gr.DrawRectangle(Pens.Gray, 3, 3, 10, 10);
			}
			base.Image = _img;
			this.Invalidate();
		}
		//show color dialog
		protected override void OnClick(System.EventArgs e)
		{
			//base.OnClick(e);
			using (ColorDialogEx frm = new ColorDialogEx())
			{
				frm.Color = _color;
				if (frm.ShowDialog() == DialogResult.OK &&
					frm.Color != _color)
				{
					_color = frm.Color;
					UpdateColor();
					if (ColorChanged != null)
						ColorChanged(this, e);
				}
			}
		}
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image
		{
			get { return null; }
			set { }
		}
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ToolStripItemDisplayStyle DisplayStyle
		{
			get { return ToolStripItemDisplayStyle.Image; }
			set { }
		}
		[DefaultValue(typeof(Color), "Black")]
		public Color Color
		{
			get { return _color; }
			set
			{
				if (_color == value)
					return;
				_color = value;
				UpdateColor();
			}
		}
		public event EventHandler ColorChanged;
	}
}
