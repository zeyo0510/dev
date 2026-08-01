using System.ComponentModel;
using System.Runtime.CompilerServices;

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
			base.ResumeLayout(false);
		}
	}
}