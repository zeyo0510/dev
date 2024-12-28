using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ControlsEx;
using DockingFrames;
using DrawingEx.ColorManagement.Gradients;
using DrawingEx.Drawing3D;
using VariIconsReload.Properties;
using VariIconsSDK.Model;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// brushmanager component for keeping track of a
	/// </summary>
	public class BrushManager : Manager<BrushManager>, IDisposable
	{
		#region types
		/// <summary>
		/// used to sotre config parameters for brushes
		/// </summary>
		private class BrushParams : IXmlSerializable
		{
			//gradient params
			public ScaleFactor Scale = ScaleFactor.Identity;
			public Angle Angle = Angle.Empty;
			public PointF Offset = PointF.Empty;
			public Gradient Gradient = null;
			public GradientType Type = GradientType.Linear;
			//solid params
			public Color Color = Color.White;
			//
			public BrushParams(Color color)
			{
				this.Color = color;
			}
			public void Aquire(WrapperBrush brs)
			{
				SolidWrapperBrush sbrs;
				GradientWrapperBrush gbrs;
				if ((gbrs = brs as GradientWrapperBrush) != null)
				{
					Scale = gbrs.Scale;
					Offset = gbrs.Offset;
					Angle = gbrs.Angle;
					Gradient = gbrs.Gradient;
				}
				else if ((sbrs = brs as SolidWrapperBrush) != null)
					Color = sbrs.Color;
			}

			#region IXmlSerializable Member
			public XmlSchema GetSchema()
			{
				return null;
			}
			//reads the xml chunk to this parameters
			public void ReadXml(XmlReader reader)
			{
				TypeConverter colorconv = TypeDescriptor.GetConverter(typeof(Color));
				reader.ReadStartElement("brushparams");
				//scale
				reader.ReadStartElement("scale");
				int num = reader.ReadElementContentAsInt("numerator", "");
				Scale = new ScaleFactor(num, reader.ReadElementContentAsInt("denominator", ""));
				reader.ReadEndElement();
				//angle
				Angle = Angle.FromRadiants(reader.ReadElementContentAsDouble("angle", ""));
				//offset
				reader.ReadStartElement("offset");
				float x = reader.ReadElementContentAsFloat("x", "");
				Offset = new PointF(x, reader.ReadElementContentAsFloat("y", ""));
				reader.ReadEndElement();
				//gradient, import with xmlformat
				if (!reader.IsEmptyElement)
				{
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(reader.ReadInnerXml());
					Gradient = new XmlFormat().GradientFromXml(doc.DocumentElement);
				}
				else reader.Read();
				//gradient type
				int val = reader.ReadElementContentAsInt("gradienttype", "");
				if (!Enum.IsDefined(typeof(GradientType), val))
					throw new ArgumentException("gradienttype");
				Type = (GradientType)val;
				//color
				Color = (Color)colorconv.ConvertFromInvariantString(
					reader.ReadElementString("color"));
				//
				reader.ReadEndElement();
			}
			//writes the parameters to a xml chunk
			public void WriteXml(XmlWriter writer)
			{
				TypeConverter colorconv = TypeDescriptor.GetConverter(typeof(Color));
				writer.WriteStartElement("brushparams");
				//scale
				writer.WriteStartElement("scale");
				writer.WriteStartElement("numerator");
				writer.WriteValue(Scale.Numerator);
				writer.WriteEndElement();
				writer.WriteStartElement("denominator");
				writer.WriteValue(Scale.Denominator);
				writer.WriteEndElement();
				writer.WriteEndElement();
				//angle
				writer.WriteStartElement("angle");
				writer.WriteValue(Angle.ToRadiants());
				writer.WriteEndElement();
				//offset, there is no pointf converter
				writer.WriteStartElement("offset");
				writer.WriteStartElement("x");
				writer.WriteValue(Offset.X);
				writer.WriteEndElement();
				writer.WriteStartElement("y");
				writer.WriteValue(Offset.Y);
				writer.WriteEndElement();
				writer.WriteEndElement();
				//gradient, use xmlformat for conversion
				writer.WriteStartElement("gradient");
				if (Gradient != null)
				{
					XmlDocument doc = new XmlDocument();
					writer.WriteRaw(new XmlFormat().GradientToXml(doc, Gradient).OuterXml);
				}
				writer.WriteEndElement();
				//gradienttype
				writer.WriteStartElement("gradienttype");
				writer.WriteValue((int)Type);
				writer.WriteEndElement();
				//color
				writer.WriteElementString("color",
					colorconv.ConvertToInvariantString(Color));
				//
				writer.WriteEndElement();
			}
			#endregion
		}
		#endregion
		#region variables
		private UserFrame _frame;
		private ColorSelectionPage _colorselect;
		private GradientSelectionPage _gradientselect;
		private WrapperBrush _brsA, _brsB;
		private bool _lasteditedA;
		//history
		private BrushParams _paramsA = new BrushParams(Color.Black),
			_paramsB = new BrushParams(Color.White);
		#endregion
		#region ctor
		public BrushManager()
		{
			_frame = new UserFrame();
			_frame.Guid = "80A7C741-F392-4F1F-A6C1-A68D9F8895B3";
			//
			_colorselect = new ColorSelectionPage();
			_colorselect.ColorChanged += new EventHandler(_colorselect_ColorChanged);
			_colorselect.VisibleChanged += new EventHandler(_brushPane_VisibleChanged);
			//
			_gradientselect = new GradientSelectionPage();
			_gradientselect.GradientTypeChanged += new EventHandler(_gradientselect_GradientTypeChanged);
			_gradientselect.VisibleChanged += new EventHandler(_brushPane_VisibleChanged);
			//
			_frame.Pages.AddRange(new UserFramePage[]{
				_colorselect,_gradientselect});
			//create brushes
			_brsA = new SolidWrapperBrush(Color.Black);
			_brsB = new SolidWrapperBrush(Color.White);
			SelectBrush(true);
			//Settings
			LoadSettings();
			Settings.Default.SettingsSaving +=
				new SettingsSavingEventHandler(SettingsSaving);
		}

		public void Dispose()
		{
			if (_frame != null)
			{
				BrushA = null;
				BrushB = null;
				//
				_frame.Dispose();
				_frame = null;
			}
		}
		#endregion
		#region persistence
		//load params from settings
		void LoadSettings()
		{
			try
			{
				using (StringReader rdr = new StringReader(
					Settings.Default.BrushParamsA))
					_paramsA.ReadXml(XmlReader.Create(rdr));
				using (StringReader rdr = new StringReader(
					Settings.Default.BrushParamsB))
					_paramsB.ReadXml(XmlReader.Create(rdr));
				//
				if (Settings.Default.BrushAType)
					BrushA = CreateGradientBrush(_paramsA);
				else
					BrushA = CreateSolidBrush(_paramsA);
				if (Settings.Default.BrushBType)
					BrushB = CreateGradientBrush(_paramsB);
				else
					BrushB = CreateSolidBrush(_paramsB);
				SelectBrush(Settings.Default.BrushEditingA);
			}
			catch { }
		}
		//save params to settings
		void SettingsSaving(object sender, CancelEventArgs e)
		{
			_paramsA.Aquire(_brsA); _paramsB.Aquire(_brsB);
			Settings.Default.BrushParamsA = ParamsToXmlString(_paramsA);
			Settings.Default.BrushParamsB = ParamsToXmlString(_paramsB);
			Settings.Default.BrushAType = _brsA is GradientWrapperBrush;
			Settings.Default.BrushBType = _brsB is GradientWrapperBrush;
			Settings.Default.BrushEditingA = _lasteditedA;
		}
		//convert brush params to string
		private string ParamsToXmlString(BrushParams param)
		{
			StringBuilder sb = new StringBuilder();
			XmlWriter wrt = XmlWriter.Create(sb);
			param.WriteXml(wrt);
			wrt.Close();
			return sb.ToString();
		}
		#endregion
		#region brush creation
		//create a solidbrush from history
		private SolidWrapperBrush CreateSolidBrush()
		{
			if (_lasteditedA)
				return CreateSolidBrush(_paramsA);
			else
				return CreateSolidBrush(_paramsB);
		}
		//create a solidbrush from params
		private SolidWrapperBrush CreateSolidBrush(BrushParams param)
		{
			return new SolidWrapperBrush(param.Color);
		}
		//creates a gradient brush from the given original, or one from history
		private GradientWrapperBrush CreateGradientBrush(GradientWrapperBrush brs, GradientType type)
		{
			if (brs != null)
				return CreateGradientBrush(brs.Gradient, type, brs.Scale, brs.Angle, brs.Offset);
			else
				return CreateGradientBrush();
		}
		//creates a gradientbrush from history
		private GradientWrapperBrush CreateGradientBrush()
		{
			if (_lasteditedA)
				return CreateGradientBrush(_paramsA);
			else
				return CreateGradientBrush(_paramsB);
		}
		//creates a gradientbrush from parameters
		private GradientWrapperBrush CreateGradientBrush(BrushParams param)
		{
			return CreateGradientBrush(param.Gradient,
					param.Type, param.Scale, param.Angle, param.Offset);
		}
		//creates a gradientbrush from the given parameters
		private GradientWrapperBrush CreateGradientBrush(Gradient grd, GradientType type, ScaleFactor scale, Angle angle, PointF offset)
		{
			if (grd == null)
			{
				grd = new Gradient();
				grd.Alphas.Add(new AlphaPoint(255, 0.5));
				grd.Colors.Add(new ColorPoint(Color.Orange, 0));
				grd.Colors.Add(new ColorPoint(Color.Blue, 1));
			}
			//select new brush
			switch (type)
			{
				case GradientType.Linear:
					return new LinearGradientWrapperBrush(grd, scale, angle, offset);
				case GradientType.Angle:
					return new AngleGradientWrapperBrush(grd, scale, angle, offset);
				case GradientType.Diamond:
					return new DiamondGradientWrapperBrush(grd, scale, angle, offset);
				default:
					return new RadialGradientWrapperBrush(grd, scale, angle, offset);
			}
		}
		#endregion
		#region handlers
		//switch current brush type, if not already done
		void _brushPane_VisibleChanged(object sender, EventArgs e)
		{
			//save params of previous brush
			SelectedParams.Aquire(SelectedBrush);
			//change
			if (_colorselect.Selected && !(SelectedBrush is SolidWrapperBrush))
				SelectedBrush = CreateSolidBrush();
			else if (_gradientselect.Selected && !(SelectedBrush is GradientWrapperBrush))
				SelectedBrush = CreateGradientBrush();
		}
		//switch current gradientbrush type
		void _gradientselect_GradientTypeChanged(object sender, EventArgs e)
		{
			SelectedBrush =
				CreateGradientBrush(_gradientselect.GradientWrapperBrush, _gradientselect.GradientType);
			SelectedParams.Type = _gradientselect.GradientType;
		}
		//updates the solid color brush
		void _colorselect_ColorChanged(object sender, EventArgs e)
		{
			SolidWrapperBrush brs = SelectedBrush as SolidWrapperBrush;
			if (brs != null)
				brs.Color = _colorselect.Color;
			else
				SelectedBrush = new SolidWrapperBrush(_colorselect.Color);
			SelectedParams.Color = _colorselect.Color;
		}
		//gets or sets the currently edited brush
		private WrapperBrush SelectedBrush
		{
			get
			{
				if (_lasteditedA)
					return BrushA;
				else
					return BrushB;
			}
			set
			{
				if (_lasteditedA)
					BrushA = value;
				else
					BrushB = value;
				SelectBrush(_lasteditedA);
				RaiseBrushChanged();
			}
		}
		//gets the current brush parameters
		private BrushParams SelectedParams
		{
			get
			{
				if (_lasteditedA)
					return _paramsA;
				else
					return _paramsB;
			}
		}
		#endregion
		#region public members
		//selects A or B brush for editing
		public void SelectBrush(bool a)
		{
			_lasteditedA = a;
			WrapperBrush brs = SelectedBrush;
			GradientWrapperBrush gbrs;
			SolidWrapperBrush sbrs;
			if ((gbrs = brs as GradientWrapperBrush) != null)
			{
				_gradientselect.GradientWrapperBrush = gbrs;
				_gradientselect.SelectPage();
			}
			else if ((sbrs = brs as SolidWrapperBrush) != null)
			{
				_gradientselect.GradientWrapperBrush = null;
				_colorselect.Color = sbrs.Color;
				_colorselect.SelectPage();
			}
		}
		/// <summary>
		/// swaps the brushes, keeping the same edited
		/// </summary>
		public void SwapBrushes()
		{
			BrushParams tmp = _paramsB;
			_paramsB = _paramsA;
			_paramsA = tmp;
			WrapperBrush brs = _brsB;
			_brsB = _brsA;
			_brsA = brs;
			SelectBrush(_lasteditedA);
			RaiseBrushChanged();
		}
		/// <summary>
		/// pics a color to the given brush
		/// </summary>
		public void PickColor(Color value, bool a)
		{
			if (a)
				BrushA = new SolidWrapperBrush(value);
			else
				BrushB = new SolidWrapperBrush(value);
			SelectBrush(a);
			RaiseBrushChanged();
		}
		#endregion
		#region properties
		/// <summary>
		/// gets the gui extension frame
		/// </summary>
		public UserFrame Frame
		{
			get { return _frame; }
		}
		/// <summary>
		/// gets or sets the current foreground brush
		/// </summary>
		public WrapperBrush BrushA
		{
			get { return _brsA; }
			private set
			{
				if (value == _brsA)
					return;
				//set new brush
				WrapperBrush brs = _brsA;
				_brsA = value;
				if (brs != null)
					brs.Dispose();
			}
		}
		/// <summary>
		/// gets or sets the current background brush
		/// </summary>
		public WrapperBrush BrushB
		{
			get { return _brsB; }
			private set
			{
				if (value == _brsB)
					return;
				//set new brush
				WrapperBrush brs = _brsB;
				_brsB = value;
				if (brs != null)
					brs.Dispose();
			}
		}
		/// <summary>
		/// gets if the last edited brush is A
		/// </summary>
		public bool SelectedA
		{
			get { return _lasteditedA; }
		}
		#endregion
		private void RaiseBrushChanged()
		{
			if (BrushChanged != null)
				BrushChanged(this, EventArgs.Empty);
		}
		public event EventHandler BrushChanged;
	}
}
