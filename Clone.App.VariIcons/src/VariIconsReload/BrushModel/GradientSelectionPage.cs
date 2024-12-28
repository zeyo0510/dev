using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using ControlsEx.ValueControls;
using DrawingEx.ColorManagement.ColorModels;
using DrawingEx.ColorManagement.Gradients;
using VariIconsReload.BrushModel;
using VariIconsReload.Properties;
using System.Collections.Generic;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// manager for gradient brushes.
	/// GradientList -> Gradient -> GradientWrapperBrush
	/// </summary>
	public partial class GradientSelectionPage : DockingFrames.UserFramePage
	{
		#region variables
		private GradientType _gradienttype = GradientType.Linear;
		#endregion
		public GradientSelectionPage()
		{
			InitializeComponent();
			//
			LoadSettings();
			Settings.Default.SettingsSaving +=
				new SettingsSavingEventHandler(SettingsSaving);
		}
		#region persistence
		//load gradient list from settings
		void LoadSettings()
		{
			try
			{
				new XmlFormat().Load(grdList.Gradients,
					Settings.Default.GradientList);
			}
			catch { }
		}
		//save gradient list to settings
		void SettingsSaving(object sender, CancelEventArgs e)
		{
			Settings.Default.GradientList =
				new XmlFormat().Write(grdList.Gradients);
		}
		#endregion
		#region full gradient editor
		private void tbForm_Command_Click(object sender, EventArgs e)
		{
			using (GradientCollectionEditor frm = new GradientCollectionEditor())
			{
				//sync library
				foreach (Gradient grd in grdList.Gradients)
					frm.Gradients.Add(grd);
				frm.SelectedGradient = (Gradient)grdEdit.Gradient.Clone();
				//edit gradient in place
				if (frm.ShowDialog() == DialogResult.OK)
				{
					//sync back
					SelectedGradient = frm.SelectedGradient;
					grdList.Gradients.Clear();
					foreach (Gradient grd in frm.Gradients)
						grdList.Gradients.Add(grd);
				}
			}
		}
		#endregion
		#region brush manager
		//change gradient type
		private void tbGradient_Click(object sender, EventArgs e)
		{
			GradientType newtype = _gradienttype;
			if (sender == tbGradientAngled.Command)
				newtype = GradientType.Angle;
			else if (sender == tbGradientLinear.Command)
				newtype = GradientType.Linear;
			else if (sender == tbGradientRadial.Command)
				newtype = GradientType.Radial;
			//fire event
			if (newtype != _gradienttype)
			{
				_gradienttype = newtype;
				UpdateGradientType();
				RaiseGradientTypeChanged();
			}
		}
		//applies the set gradienttype to gui
		private void UpdateGradientType()
		{
			switch (_gradienttype)
			{
				case GradientType.Linear:
					drpGradientType.Command.Image = tbGradientLinear.Command.Image;
					break;
				case GradientType.Angle:
					drpGradientType.Command.Image = tbGradientAngled.Command.Image;
					break;
				case GradientType.Diamond:
					break;
				case GradientType.Radial:
					drpGradientType.Command.Image = tbGradientRadial.Command.Image;
					break;
			}
		}
		#endregion
		#region gradient list
		//adds a default preset to list, or a clone of the current
		private void tbAdd_Command_Click(object sender, EventArgs e)
		{
			if (grdEdit.Gradient != null)
				grdList.Gradients.Add((Gradient)grdEdit.Gradient.Clone());
			else
			{
				Gradient grd = new Gradient();
				grd.Alphas.Add(new AlphaPoint(255, 0.5));
				grd.Colors.Add(new ColorPoint(Color.Orange, 0.0));
				grd.Colors.Add(new ColorPoint(Color.Blue, 1.0));
				grdList.Gradients.Add(grd);
			}
		}
		//select item for deletion
		private void grdList_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				grdList.SelectedIndex = grdList.GetIndexAtPosition(e.Location);
				deletePresetToolStripMenuItem.Enabled = grdList.SelectedIndex != -1;
			}
		}
		//restore selection on closing
		private void ctxMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
				grdList.SelectedIndex = grdList.Gradients.IndexOf(grdEdit.Gradient);
		}
		//delete seleted preset
		private void deletePresetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (grdList.SelectedIndex != -1)
				grdList.Gradients.RemoveAt(grdList.SelectedIndex);
			grdList.SelectedIndex = grdList.Gradients.IndexOf(grdEdit.Gradient);
		}
		//opens a list of presets from the file system
		private void tbOpenPresets_Command_Click(object sender, EventArgs e)
		{
			if (oDialog.ShowDialog() == DialogResult.OK)
			{
				grdList.Gradients.Clear();
				try
				{
					grdList.Gradients.Load(oDialog.FileName);
				}
				catch (Exception ex) { MessageBox.Show(ex.Message); }
			}
		}
		//export c# code
		private void tbCopyCode_Command_Click(object sender, EventArgs e)
		{
			if (grdEdit.Gradient != null)
				Clipboard.SetDataObject(
				GradientExportUtils.ExportCode(
					grdEdit.Gradient.GetColorBlend()), true);
		}
		//save gradient list
		private void tbSavePresets_Command_Click(object sender, EventArgs e)
		{
			if (sDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					grdList.Gradients.Save(sDialog.FileName);
				}
				catch (Exception ex) { MessageBox.Show(ex.Message); }
			}
		}
		//clear the presets
		private void tbClearPresets_Command_Click(object sender, EventArgs e)
		{
			grdList.Gradients.Clear();
		}
		//set new gradient and listen to changes
		private void grdList_GradientClicked(object sender, GradientEventArgs e)
		{
			if (e.Gradient != null)
				SelectedGradient = (Gradient)e.Gradient.Clone();
		}
		#endregion
		#region gradient editor
		private void grdEdit_SelectionChanged(object sender, EventArgs e)
		{
			if (grdEdit.SelectedColorIndex != -1)
			{
				btnRemoveStop.Enabled = btnColor.Enabled =
					btnColor.Visible = true;
				scrAlpha.Visible = false;
				btnColor.Color = grdEdit.Gradient.Colors[
					grdEdit.SelectedColorIndex].Color.ToRGB().ToArgb();
			}
			else if (grdEdit.SelectedAlphaIndex != -1)
			{
				btnRemoveStop.Enabled = scrAlpha.Visible = true;
				btnColor.Visible = false;
				scrAlpha.Value = (int)(grdEdit.Gradient.Alphas[
					grdEdit.SelectedAlphaIndex].Alpha * 255.0);
			}
			else
			{
				btnColor.Visible = true;
				btnColor.Enabled = scrAlpha.Visible =
					btnRemoveStop.Enabled = false;
			}
		}
		private void tbReverse_Command_Click(object sender, EventArgs e)
		{
			Gradient grd=grdEdit.Gradient;
			if (grd!= null)
			{
				grdEdit.SuspendUpdates();
				//reverse colors
				for (int i = 0, j = grd.Colors.Count - 1; i < j; i++, j--)
				{
					XYZ color = grd.Colors[i].Color;
					grd.Colors[i].Color = grd.Colors[j].Color;
					grd.Colors[j].Color = color;
				}
				//reverse alphas
				for (int i = 0, j = grd.Alphas.Count - 1; i < j; i++, j--)
				{
					double alpha = grd.Alphas[i].Alpha;
					grd.Alphas[i].Alpha = grd.Alphas[j].Alpha;
					grd.Alphas[j].Alpha = alpha;
				}
				grdEdit.ResumeUpates();
			}
		}
		private void btnRemoveStop_Click(object sender, EventArgs e)
		{
			if (grdEdit.SelectedAlphaIndex != -1)
				grdEdit.Gradient.Alphas.RemoveAt(grdEdit.SelectedAlphaIndex);
			else if (grdEdit.SelectedColorIndex != -1)
				grdEdit.Gradient.Colors.RemoveAt(grdEdit.SelectedColorIndex);
		}
		private void btnColor_Click(object sender, EventArgs e)
		{
			cDialog.Color = btnColor.Color;
			if (cDialog.ShowDialog() == DialogResult.OK)
			{
				btnColor.Color = cDialog.Color;
				btnColor_ColorChanged(this, e);
			}
		}

		private void btnColor_ColorChanged(object sender, EventArgs e)
		{
			if (grdEdit.SelectedColorIndex != -1)
				grdEdit.Gradient.Colors[grdEdit.SelectedColorIndex].Color =
					XYZ.FromRGB(new RGB(btnColor.Color));
		}

		void scrAlpha_ValueChanged(ValueControl sender, ValueChangedEventArgs e)
		{
			if (grdEdit.SelectedAlphaIndex != -1)
				grdEdit.Gradient.Alphas[grdEdit.SelectedAlphaIndex].Alpha =
					(double)(scrAlpha.Value) / 255.0;
		}
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the current gradientstyle
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GradientType GradientType
		{
			get { return _gradienttype; }
			set
			{
				if (value == _gradienttype)
					return;
				_gradienttype = value;
				UpdateGradientType();
			}
		}
		//get or set the edited gradient
		private Gradient SelectedGradient
		{
			get { return grdEdit.Gradient; }
			set
			{
				if (value == grdEdit.Gradient)
					return;
				grdEdit.Gradient = value;
				tbReverse.Enabled = value != null;
				if (brushTransform1.GradientBrush != null && value != null)
					brushTransform1.GradientBrush.Gradient = value;
				grdEdit_SelectionChanged(this, EventArgs.Empty);
			}
		}
		/// <summary>
		/// gets or sets the gradient wrapperbrush to configure
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GradientWrapperBrush GradientWrapperBrush
		{
			get { return brushTransform1.GradientBrush; }
			set
			{
				if (value == brushTransform1.GradientBrush)
					return;
				brushTransform1.GradientBrush = value;
				if (value == null)
				{
					grdEdit.Gradient = null;
					tbReverse.Enabled = false;
				}
				else
				{
					grdEdit.Gradient = value.Gradient;
					tbReverse.Enabled = true;
				}
				grdEdit_SelectionChanged(this, EventArgs.Empty);
			}
		}
		#endregion
		#region events
		private void RaiseGradientTypeChanged()
		{
			if (GradientTypeChanged != null)
				GradientTypeChanged(this, EventArgs.Empty);
		}
		/// <summary>
		/// raised when selected gradientstyle
		/// </summary>
		public event EventHandler GradientTypeChanged;
		#endregion
	}
	/// <summary>
	/// gradienttype enum
	/// </summary>
	public enum GradientType
	{
		None, Linear, Radial, Angle, Diamond
	}
}
