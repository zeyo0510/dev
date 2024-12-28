using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ControlsEx.ValueControls;
using DrawingEx.ColorManagement.ColorModels;
using DrawingEx.ColorManagement.ColorModels.Selection;
using VariIconsReload.Properties;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// ColorManager Page, for picking fore and background colors
	/// </summary>
	public partial class ColorSelectionPage : DockingFrames.UserFramePage
	{
		#region variables
		private ColorSelectionModule _module;
		#endregion
		public ColorSelectionPage()
		{
			this.SetStyle(ControlStyles.Selectable, true);
			InitializeComponent();
			//
			_module = new ColorSelectionModuleHSV_H();
			_module.ColorChanged += new EventHandler(_module_ColorChanged);
			_module.ColorChangedFinal += new EventHandler(_module_ColorChangedFinal);
			_module.ColorSelectionFader = cFader;
			_module.ColorSelectionPlane = cPlane;
			_module.XYZ = XYZ.FromRGB(new RGB(cbtn.Color));
			//
			ShiftKeyFilter.AddHandler(
				new EventHandler(filter_ShiftStateChanged));
			//settings save handler
			LoadSettings();
			Settings.Default.SettingsSaving +=
				new SettingsSavingEventHandler(SettingsSaving);
		}

		private void UserDispose()
		{
			ShiftKeyFilter.RemoveHandler(
				new EventHandler(filter_ShiftStateChanged));
		}
		#region persistence
		//load color history from settings
		void LoadSettings()
		{
			try
			{
				using (StringReader rdr = new StringReader(
					Settings.Default.ColorList))
					palHistory.Colors.ReadXml(XmlReader.Create(rdr));
				mnuLock.Checked = Settings.Default.ColorListLock;
			}
			catch { }
		}
		//save history to settings
		void SettingsSaving(object sender, CancelEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			XmlWriter wrt = XmlWriter.Create(sb);
			palHistory.Colors.WriteXml(wrt);
			wrt.Close();
			//
			Settings.Default.ColorList = sb.ToString();
			Settings.Default.ColorListLock = mnuLock.Checked;
		}
		#endregion
		#region shift filter
		void filter_ShiftStateChanged(object sender, EventArgs e)
		{
			if (this.Selected)
			{
				_module.ColorSelectionFader.Refresh();
				_module.ColorSelectionPlane.Refresh();
			}
		}
		#endregion
		#region controller
		private void UpdateColor(object sender, Color col)
		{
			UpdateColor(sender, col, true);
		}
		private void UpdateColor(object sender, Color col, bool raise)
		{
			if (sender != palHistory)
			{
				if (mnuLock.Checked)
					palHistory.SelectedColor = col;
				else
					palHistory.SelectOrAddColor(col);
			} if (sender != palStd)
				palStd.SelectedColor = col;
			if (sender != _module)
				_module.XYZ = XYZ.FromRGB(new RGB(col));
			//changed fore color
			if (sender != cbtn)
				cbtn.Color = col;
			if (raise)
				RaiseColorChanged();
		}
		//changed slider or plane, update buttons
		private void _module_ColorChanged(object sender, EventArgs e)
		{
			cbtn.Color = _module.XYZ.ToRGB().ToArgb();
		}
		//changed slider or plane, raise events
		private void _module_ColorChangedFinal(object sender, EventArgs e)
		{
			UpdateColor(_module, _module.XYZ.ToRGB().ToArgb());
		}
		//activate foregorund editing or show dialog
		private void cbtn_Click(object sender, EventArgs e)
		{
			//show colordialog
			colorDialogEx1.Color = cbtn.Color;
			if (colorDialogEx1.ShowDialog() == DialogResult.OK &&
				colorDialogEx1.Color != cbtn.Color)
				UpdateColor(cbtn, colorDialogEx1.Color);
		}
		//picked foreground color off screen
		private void cbtnA_ColorChanged(object sender, EventArgs e)
		{
			UpdateColor(cbtn, cbtn.Color);
		}

		private void palStd_SelectedColorChanged(object sender, EventArgs e)
		{
			if (palStd.SelectedColor.A > 0)
				UpdateColor(palStd, palStd.SelectedColor);
		}

		private void palHistory_SelectedColorChanged(object sender, EventArgs e)
		{
			if (palHistory.SelectedColor.A > 0)
				UpdateColor(palHistory, palHistory.SelectedColor);
		}

		private void scrAlpha_ValueChanged(ValueControl sender, ValueChangedEventArgs e)
		{
			RaiseColorChanged();
		}

		private void mnuClear_Click(object sender, EventArgs e)
		{
			palHistory.Colors.Clear();
			palHistory.SelectedIndex = -1;
		}
		#endregion
		/// <summary>
		/// gets or sets the foregorund color
		/// </summary>
		[DefaultValue(typeof(Color), "White")]
		public Color Color
		{
			get { return Color.FromArgb(scrAlpha.Value, cbtn.Color); }
			set
			{
				scrAlpha.Value = value.A;
				//
				value = Color.FromArgb(255, value);
				if (cbtn.Color == value)
					return;
				cbtn.Color = value;
				UpdateColor(cbtn, value, false);
			}
		}
		#region events
		private void RaiseColorChanged()
		{
			if (ColorChanged != null)
				ColorChanged(this, EventArgs.Empty);
		}
		/// <summary>
		/// raised when the foregorund color changes
		/// due to user interaction
		/// </summary>
		public event EventHandler ColorChanged;
		#endregion


	}
}
