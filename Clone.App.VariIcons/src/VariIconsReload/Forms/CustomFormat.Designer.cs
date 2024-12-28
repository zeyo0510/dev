namespace VariIconsReload.Forms
{
	partial class CustomFormat
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rd32;
		private System.Windows.Forms.RadioButton rd24;
		private System.Windows.Forms.RadioButton rd8;
		private System.Windows.Forms.RadioButton rd4;
		private System.Windows.Forms.RadioButton rd1;
		private System.Windows.Forms.GroupBox groupBox2;
		private ControlsEx.ValueControls.HValueScrollBar scrWidth;
		private ControlsEx.ValueControls.HValueScrollBar scrHeight;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cbWidth;
		private System.Windows.Forms.ComboBox cbHeight;
		private System.Windows.Forms.CheckBox cbLock;
		private System.Windows.Forms.Label line;
		private System.Windows.Forms.ToolTip toolTip;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-MainTab geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomFormat));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rd32 = new System.Windows.Forms.RadioButton();
			this.rd24 = new System.Windows.Forms.RadioButton();
			this.rd8 = new System.Windows.Forms.RadioButton();
			this.rd4 = new System.Windows.Forms.RadioButton();
			this.rd1 = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbWidth = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.scrWidth = new ControlsEx.ValueControls.HValueScrollBar();
			this.scrHeight = new ControlsEx.ValueControls.HValueScrollBar();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cbHeight = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.cbLock = new System.Windows.Forms.CheckBox();
			this.line = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rd32);
			this.groupBox1.Controls.Add(this.rd24);
			this.groupBox1.Controls.Add(this.rd8);
			this.groupBox1.Controls.Add(this.rd4);
			this.groupBox1.Controls.Add(this.rd1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// rd32
			// 
			this.rd32.Checked = true;
			resources.ApplyResources(this.rd32, "rd32");
			this.rd32.Name = "rd32";
			this.rd32.TabStop = true;
			this.toolTip.SetToolTip(this.rd32, resources.GetString("rd32.ToolTip"));
			this.rd32.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
			// 
			// rd24
			// 
			resources.ApplyResources(this.rd24, "rd24");
			this.rd24.Name = "rd24";
			this.toolTip.SetToolTip(this.rd24, resources.GetString("rd24.ToolTip"));
			this.rd24.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
			// 
			// rd8
			// 
			resources.ApplyResources(this.rd8, "rd8");
			this.rd8.Name = "rd8";
			this.toolTip.SetToolTip(this.rd8, resources.GetString("rd8.ToolTip"));
			this.rd8.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
			// 
			// rd4
			// 
			resources.ApplyResources(this.rd4, "rd4");
			this.rd4.Name = "rd4";
			this.toolTip.SetToolTip(this.rd4, resources.GetString("rd4.ToolTip"));
			this.rd4.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
			// 
			// rd1
			// 
			resources.ApplyResources(this.rd1, "rd1");
			this.rd1.Name = "rd1";
			this.toolTip.SetToolTip(this.rd1, resources.GetString("rd1.ToolTip"));
			this.rd1.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbWidth);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.scrWidth);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// cbWidth
			// 
			this.cbWidth.FormattingEnabled = true;
			this.cbWidth.Items.AddRange(new object[] {
            resources.GetString("cbWidth.Items"),
            resources.GetString("cbWidth.Items1"),
            resources.GetString("cbWidth.Items2"),
            resources.GetString("cbWidth.Items3"),
            resources.GetString("cbWidth.Items4")});
			resources.ApplyResources(this.cbWidth, "cbWidth");
			this.cbWidth.Name = "cbWidth";
			this.toolTip.SetToolTip(this.cbWidth, resources.GetString("cbWidth.ToolTip"));
			this.cbWidth.Leave += new System.EventHandler(this.cbWidth_Leave);
			this.cbWidth.TextChanged += new System.EventHandler(this.cbWidth_TextChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// scrWidth
			// 
			resources.ApplyResources(this.scrWidth, "scrWidth");
			this.scrWidth.Maximum = 256;
			this.scrWidth.Minimum = 1;
			this.scrWidth.Name = "scrWidth";
			this.scrWidth.Value = 16;
			this.scrWidth.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.scrWidth_ValueChanged);
			// 
			// scrHeight
			// 
			resources.ApplyResources(this.scrHeight, "scrHeight");
			this.scrHeight.Maximum = 256;
			this.scrHeight.Minimum = 1;
			this.scrHeight.Name = "scrHeight";
			this.scrHeight.Value = 16;
			this.scrHeight.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.scrHeight_ValueChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cbHeight);
			this.groupBox3.Controls.Add(this.scrHeight);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// cbHeight
			// 
			this.cbHeight.FormattingEnabled = true;
			this.cbHeight.Items.AddRange(new object[] {
            resources.GetString("cbHeight.Items"),
            resources.GetString("cbHeight.Items1"),
            resources.GetString("cbHeight.Items2"),
            resources.GetString("cbHeight.Items3"),
            resources.GetString("cbHeight.Items4")});
			resources.ApplyResources(this.cbHeight, "cbHeight");
			this.cbHeight.Name = "cbHeight";
			this.toolTip.SetToolTip(this.cbHeight, resources.GetString("cbHeight.ToolTip"));
			this.cbHeight.Leave += new System.EventHandler(this.cbHeight_Leave);
			this.cbHeight.TextChanged += new System.EventHandler(this.cbHeight_TextChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
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
			// cbLock
			// 
			resources.ApplyResources(this.cbLock, "cbLock");
			this.cbLock.Name = "cbLock";
			this.toolTip.SetToolTip(this.cbLock, resources.GetString("cbLock.ToolTip"));
			this.cbLock.UseVisualStyleBackColor = true;
			this.cbLock.CheckedChanged += new System.EventHandler(this.cbLock_CheckedChanged);
			// 
			// line
			// 
			resources.ApplyResources(this.line, "line");
			this.line.Name = "line";
			// 
			// CustomFormat
			// 
			this.AcceptButton = this.btnOK;
			resources.ApplyResources(this, "$this");
			this.CancelButton = this.btnCancel;
			this.Controls.Add(this.cbLock);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.line);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CustomFormat";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
