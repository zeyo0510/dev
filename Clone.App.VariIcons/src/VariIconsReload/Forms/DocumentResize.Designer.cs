namespace VariIconsReload.Forms
{
	partial class DocumentResize
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentResize));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbRelative = new System.Windows.Forms.CheckBox();
			this.ancNE = new System.Windows.Forms.RadioButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.ancE = new System.Windows.Forms.RadioButton();
			this.ancSE = new System.Windows.Forms.RadioButton();
			this.ancS = new System.Windows.Forms.RadioButton();
			this.ancSW = new System.Windows.Forms.RadioButton();
			this.ancW = new System.Windows.Forms.RadioButton();
			this.ancNW = new System.Windows.Forms.RadioButton();
			this.ancCT = new System.Windows.Forms.RadioButton();
			this.ancN = new System.Windows.Forms.RadioButton();
			this.cmbHeightUnit = new System.Windows.Forms.ComboBox();
			this.cmbWidthUnit = new System.Windows.Forms.ComboBox();
			this.cbLock = new System.Windows.Forms.CheckBox();
			this.udHeight = new ControlsEx.ValueControls.ValueUpDown();
			this.udWidth = new ControlsEx.ValueControls.ValueUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbScale = new System.Windows.Forms.CheckBox();
			this.cmbScale = new System.Windows.Forms.ComboBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Name = "btnCancel";
			// 
			// btnOK
			// 
			resources.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Name = "btnOK";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbRelative);
			this.groupBox2.Controls.Add(this.ancNE);
			this.groupBox2.Controls.Add(this.ancE);
			this.groupBox2.Controls.Add(this.ancSE);
			this.groupBox2.Controls.Add(this.ancS);
			this.groupBox2.Controls.Add(this.ancSW);
			this.groupBox2.Controls.Add(this.ancW);
			this.groupBox2.Controls.Add(this.ancNW);
			this.groupBox2.Controls.Add(this.ancCT);
			this.groupBox2.Controls.Add(this.ancN);
			this.groupBox2.Controls.Add(this.cmbHeightUnit);
			this.groupBox2.Controls.Add(this.cmbWidthUnit);
			this.groupBox2.Controls.Add(this.cbLock);
			this.groupBox2.Controls.Add(this.udHeight);
			this.groupBox2.Controls.Add(this.udWidth);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label1);
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// cbRelative
			// 
			resources.ApplyResources(this.cbRelative, "cbRelative");
			this.cbRelative.Name = "cbRelative";
			this.cbRelative.UseVisualStyleBackColor = true;
			this.cbRelative.CheckedChanged += new System.EventHandler(this.cbRelative_CheckedChanged);
			// 
			// ancNE
			// 
			resources.ApplyResources(this.ancNE, "ancNE");
			this.ancNE.ImageList = this.imageList1;
			this.ancNE.Name = "ancNE";
			this.ancNE.UseVisualStyleBackColor = true;
			this.ancNE.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "N");
			this.imageList1.Images.SetKeyName(1, "NE");
			this.imageList1.Images.SetKeyName(2, "E");
			this.imageList1.Images.SetKeyName(3, "SE");
			this.imageList1.Images.SetKeyName(4, "S");
			this.imageList1.Images.SetKeyName(5, "SW");
			this.imageList1.Images.SetKeyName(6, "W");
			this.imageList1.Images.SetKeyName(7, "NW");
			// 
			// ancE
			// 
			resources.ApplyResources(this.ancE, "ancE");
			this.ancE.ImageList = this.imageList1;
			this.ancE.Name = "ancE";
			this.ancE.UseVisualStyleBackColor = true;
			this.ancE.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// ancSE
			// 
			resources.ApplyResources(this.ancSE, "ancSE");
			this.ancSE.ImageList = this.imageList1;
			this.ancSE.Name = "ancSE";
			this.ancSE.UseVisualStyleBackColor = true;
			this.ancSE.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// ancS
			// 
			resources.ApplyResources(this.ancS, "ancS");
			this.ancS.ImageList = this.imageList1;
			this.ancS.Name = "ancS";
			this.ancS.UseVisualStyleBackColor = true;
			this.ancS.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// ancSW
			// 
			resources.ApplyResources(this.ancSW, "ancSW");
			this.ancSW.ImageList = this.imageList1;
			this.ancSW.Name = "ancSW";
			this.ancSW.UseVisualStyleBackColor = true;
			this.ancSW.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// ancW
			// 
			resources.ApplyResources(this.ancW, "ancW");
			this.ancW.ImageList = this.imageList1;
			this.ancW.Name = "ancW";
			this.ancW.UseVisualStyleBackColor = true;
			this.ancW.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// ancNW
			// 
			resources.ApplyResources(this.ancNW, "ancNW");
			this.ancNW.ImageList = this.imageList1;
			this.ancNW.Name = "ancNW";
			this.ancNW.UseVisualStyleBackColor = true;
			this.ancNW.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// ancCT
			// 
			resources.ApplyResources(this.ancCT, "ancCT");
			this.ancCT.Checked = true;
			this.ancCT.ImageList = this.imageList1;
			this.ancCT.Name = "ancCT";
			this.ancCT.TabStop = true;
			this.ancCT.UseVisualStyleBackColor = true;
			this.ancCT.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// ancN
			// 
			resources.ApplyResources(this.ancN, "ancN");
			this.ancN.ImageList = this.imageList1;
			this.ancN.Name = "ancN";
			this.ancN.UseVisualStyleBackColor = true;
			this.ancN.CheckedChanged += new System.EventHandler(this.anc_CheckedChanged);
			// 
			// cmbHeightUnit
			// 
			this.cmbHeightUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHeightUnit.FormattingEnabled = true;
			this.cmbHeightUnit.Items.AddRange(new object[] {
            resources.GetString("cmbHeightUnit.Items"),
            resources.GetString("cmbHeightUnit.Items1")});
			resources.ApplyResources(this.cmbHeightUnit, "cmbHeightUnit");
			this.cmbHeightUnit.Name = "cmbHeightUnit";
			this.cmbHeightUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
			// 
			// cmbWidthUnit
			// 
			this.cmbWidthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWidthUnit.FormattingEnabled = true;
			this.cmbWidthUnit.Items.AddRange(new object[] {
            resources.GetString("cmbWidthUnit.Items"),
            resources.GetString("cmbWidthUnit.Items1")});
			resources.ApplyResources(this.cmbWidthUnit, "cmbWidthUnit");
			this.cmbWidthUnit.Name = "cmbWidthUnit";
			this.cmbWidthUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
			// 
			// cbLock
			// 
			resources.ApplyResources(this.cbLock, "cbLock");
			this.cbLock.Name = "cbLock";
			this.cbLock.UseVisualStyleBackColor = true;
			this.cbLock.CheckedChanged += new System.EventHandler(this.cbLock_CheckedChanged);
			// 
			// udHeight
			// 
			resources.ApplyResources(this.udHeight, "udHeight");
			this.udHeight.Maximum = 256;
			this.udHeight.Minimum = 1;
			this.udHeight.Name = "udHeight";
			this.udHeight.TrackerOrientation = System.Windows.Forms.Orientation.Vertical;
			this.udHeight.Value = 1;
			this.udHeight.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.ud_ValueChanged);
			// 
			// udWidth
			// 
			resources.ApplyResources(this.udWidth, "udWidth");
			this.udWidth.Maximum = 256;
			this.udWidth.Minimum = 1;
			this.udWidth.Name = "udWidth";
			this.udWidth.TrackerOrientation = System.Windows.Forms.Orientation.Vertical;
			this.udWidth.Value = 1;
			this.udWidth.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.ud_ValueChanged);
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// cbScale
			// 
			resources.ApplyResources(this.cbScale, "cbScale");
			this.cbScale.Name = "cbScale";
			this.cbScale.UseVisualStyleBackColor = true;
			this.cbScale.CheckedChanged += new System.EventHandler(this.cbScale_CheckedChanged);
			// 
			// cmbScale
			// 
			this.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.cmbScale, "cmbScale");
			this.cmbScale.FormattingEnabled = true;
			this.cmbScale.Items.AddRange(new object[] {
            resources.GetString("cmbScale.Items"),
            resources.GetString("cmbScale.Items1"),
            resources.GetString("cmbScale.Items2")});
			this.cmbScale.Name = "cmbScale";
			// 
			// DocumentResize
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmbScale);
			this.Controls.Add(this.cbScale);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DocumentResize";
			this.ShowInTaskbar = false;
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox groupBox2;
		private ControlsEx.ValueControls.ValueUpDown udHeight;
		private ControlsEx.ValueControls.ValueUpDown udWidth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox cbRelative;
		private System.Windows.Forms.RadioButton ancNE;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.RadioButton ancE;
		private System.Windows.Forms.RadioButton ancSE;
		private System.Windows.Forms.RadioButton ancS;
		private System.Windows.Forms.RadioButton ancSW;
		private System.Windows.Forms.RadioButton ancW;
		private System.Windows.Forms.RadioButton ancNW;
		private System.Windows.Forms.RadioButton ancCT;
		private System.Windows.Forms.RadioButton ancN;
		private System.Windows.Forms.ComboBox cmbHeightUnit;
		private System.Windows.Forms.ComboBox cmbWidthUnit;
		private System.Windows.Forms.CheckBox cbLock;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cbScale;
		private System.Windows.Forms.ComboBox cmbScale;
	}
}