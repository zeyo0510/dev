using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VariIcons
{
	/// <summary>
	/// Zusammenfassung für Splashform2.
	/// </summary>
	public class SplashBase:Form
	{
		#region variables
		private string _applicationName="Application",
			_subversion="REload",
			_subtitle="created by",
			_inittext="";
		private StringFormat _fmt=new StringFormat(StringFormatFlags.NoWrap);
		#endregion
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashBase));
			this.SuspendLayout();
			// 
			// Splashform2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(350, 220);
			this.ControlBox = false;
			this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Splashform2";
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);

		}
	
		public SplashBase()
		{
			this.InitializeComponent();

			//set styles
			this.SetStyle(ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint, true);
		}
		/// <summary>
		/// Gets the Main and Subversion dot separated
		/// </summary>
		private string GetMainVersion()
		{
			string ret=Application.ProductVersion;
			if(ret==null || ret=="") return "0.0";
			string[] segments=ret.Split('.');
			if(segments.Length<3) return ret;
			ret=string.Join(".",segments,0,2);
			return ret;
		}
		#region overrides
		//paint all text
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			if(this.Icon!=null)
			{
				using(Icon bigger=new Icon(this.Icon,48,48))
				{
					//e.Graphics.DrawImageUnscaled(img.Bitmap,120-img.Bitmap.Value,112-img.Bitmap.Height);
				e.Graphics.DrawIconUnstretched(bigger,new Rectangle(72,64,48,48));
				}
			}
			e.Graphics.TextRenderingHint=
				System.Drawing.Text.TextRenderingHint.AntiAlias;
			RectangleF layout=new RectangleF(117f,78f,0f,0f);
			//big written application Name
			if(this._applicationName!=null && this._applicationName!="")
			{
				using(Font fnt=new Font(this.Font.Name,30f,GraphicsUnit.Pixel))
				{
					layout.Size=e.Graphics.MeasureString(this._applicationName,fnt,0,this._fmt);
					e.Graphics.DrawString(this._applicationName,fnt,Brushes.Black,layout,this._fmt);
					//padding removal
					layout.X=layout.Right-7f;
				}
			}
			//subversion title, such as reload or revolution
			if(this._subversion!=null && this._subversion!="")
			{
				using(Font fnt=new Font(this.Font.Name,12f,GraphicsUnit.Pixel))
				{
					layout.Size=e.Graphics.MeasureString(this._subversion,fnt,0,this._fmt);
					layout.Y=94f;
					e.Graphics.DrawString(this._subversion,fnt,Brushes.Black,layout,this._fmt);
				}
			}
			//subtitle, such as created by...
			if(this._subtitle!=null && this._subtitle!="")
			{
				using(Font fnt=new Font(this.Font.Name,12f,GraphicsUnit.Pixel))
				{
					layout.Size=new SizeF(225f,14f);
					layout.Location=new PointF(125f,112f);
					e.Graphics.DrawString(this._subtitle,fnt,Brushes.Black,layout,this._fmt);
				}
			}
			//initialization text
			if(this._inittext!=null && this._inittext!="")
			{
				using(Font fnt=new Font(this.Font.Name,12f,GraphicsUnit.Pixel))
				{
					layout.Size=new SizeF(225f,50f);
					layout.Location=new PointF(125f,142f);
					e.Graphics.DrawString(this._inittext,fnt,Brushes.Black,layout,this._fmt);
				}
			}
			//main version
			string mainversion="Version "+GetMainVersion();
#if DEBUG
			mainversion+=" Debug";
#endif
			using(Font fnt=new Font(this.Font.Name,13f,GraphicsUnit.Pixel))
			{
				layout.Size=new SizeF(340f,15f);
				layout.Location=new PointF(5f,202f);
					this._fmt.Alignment=StringAlignment.Far;
				e.Graphics.DrawString(mainversion,fnt,Brushes.Black,layout,this._fmt);
					this._fmt.Alignment=StringAlignment.Near;
			}
		}
		//make sure size isn't altered
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Image img=this.BackgroundImage;
			if(img!=null)
			{
				width=img.Width;
				height=img.Height;
			}
			else
			{
				width=300;
				height=200;
			}
			base.SetBoundsCore (x, y, width, height, specified);
		}
		#endregion
		#region properties hiding
		//hide from editor
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		EditorBrowsable(EditorBrowsableState.Never),
		Browsable(false)]
		public override Image BackgroundImage
		{
			get{return base.BackgroundImage;}
			set
			{
				base.BackgroundImage=value;
				this.Size=new Size(20,20);
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		EditorBrowsable(EditorBrowsableState.Never),
		Browsable(false)]
		public new FormBorderStyle FormBorderStyle
		{
			get{return base.FormBorderStyle;}
			set{base.FormBorderStyle=value;}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		EditorBrowsable(EditorBrowsableState.Never),
		Browsable(false)]
		public new bool MaximizeBox
		{
			get{return base.MaximizeBox;}
			set{base.MaximizeBox=value;}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		EditorBrowsable(EditorBrowsableState.Never),
		Browsable(false)]
		public new bool MinimizeBox
		{
			get{return base.MinimizeBox;}
			set{base.MinimizeBox=value;}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		EditorBrowsable(EditorBrowsableState.Never),
		Browsable(false)]
		public new bool ShowInTaskbar
		{
			get{return false;}
			set{base.ShowInTaskbar=false;}
		}
		#endregion
		#region splashform properties
		/// <summary>
		/// Application Icon
		/// </summary>
		[DefaultValue(null),
		Category("Splashform"),
		Description("Application Icon")]
		public new Icon Icon
		{
			get{return base.Icon;}
			set{base.Icon=value;
			this.Refresh();}
		}
		/// <summary>
		/// Application Name (big letters)
		/// </summary>
		[DefaultValue("Application"),
		Category("Splashform"),
		Description("Application Name (big letters)")]
		public string ApplicationName
		{
			get{return this._applicationName;}
			set{this._applicationName=value; this.Refresh();}
		}
		/// <summary>
		/// Version Sub (smaller letters)
		/// </summary>
		[DefaultValue("REload"),
		Category("Splashform"),
		Description("Version Sub (smaller letters)")]
		public string SubVersion
		{
			get{return this._subversion;}
			set{this._subversion=value; this.Refresh();}
		}
		/// <summary>
		/// Title Sub (small letters)
		/// </summary>
		[DefaultValue("created by"),
		Category("Splashform"),
		Description("Title Sub (small letters)")]
		public string SubTitle
		{
			get{return this._subtitle;}
			set{this._subtitle=value; this.Refresh();}
		}
		/// <summary>
		/// Initialization Text (small letters)
		/// </summary>
		[DefaultValue(""),
		Category("Splashform"),
		Description("Initialization Text (small letters)")]
		public string InitText
		{
			get{return this._inittext;}
			set
			{
				this._inittext=value;
				this.Invalidate(new Rectangle(125,142,225,50));
				this.Update();
			}
		}
		#endregion
	}
}
