using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace a
{
	public class AudioSessionManagerPanel : FlowLayoutPanel
	{
		[CompilerGenerated]
		private int _Thickness;

		[CompilerGenerated]
		private Color _PenColor;

		private IContainer components;

		[DefaultValue(3)]
		[Localizable(true)]
		public int Thickness
		{
			[CompilerGenerated]
			get
			{
				return _Thickness;
			}
			[CompilerGenerated]
			set
			{
				_Thickness = value;
			}
		}

		[DefaultValue("")]
		[Localizable(true)]
		public Color PenColor
		{
			[CompilerGenerated]
			get
			{
				return _PenColor;
			}
			[CompilerGenerated]
			set
			{
				_PenColor = value;
			}
		}

		public AudioSessionManagerPanel()
		{
			InitializeComponent();
		}

		private void _OnPaint(object P_0, PaintEventArgs P_1)
		{
		}

		private void Method1(object P_0, EventArgs P_1)
		{
		}

		protected override void Dispose(bool P_0)
		{
			if (P_0 && components != null)
			{
				components.Dispose();
			}
			base.Dispose(P_0);
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			this.AutoScroll = true;
			base.Paint += new PaintEventHandler(_OnPaint);
			base.ResumeLayout(false);
		}
	}
}
