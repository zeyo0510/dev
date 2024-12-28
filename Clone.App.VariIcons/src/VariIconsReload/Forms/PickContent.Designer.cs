namespace VariIconsReload.Forms
{
	partial class PickContent
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PickContent));
			this.btnOK = new System.Windows.Forms.Button();
			this.newHeight = new System.Windows.Forms.ComboBox();
			this.newWidth = new System.Windows.Forms.ComboBox();
			this.sourceY = new System.Windows.Forms.NumericUpDown();
			this.sourceX = new System.Windows.Forms.NumericUpDown();
			this.cmbScale = new System.Windows.Forms.ComboBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.cbLock = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.sourceWidth = new System.Windows.Forms.NumericUpDown();
			this.sourceHeight = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cbScale = new System.Windows.Forms.CheckBox();
			this.line1 = new System.Windows.Forms.Label();
			this.line2 = new System.Windows.Forms.Label();
			this.layerView = new VariIconsReload.Components.LayerView();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.sourceY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceX)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sourceWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceHeight)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			resources.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Name = "btnOK";
			// 
			// newHeight
			// 
			this.newHeight.FormattingEnabled = true;
			this.newHeight.Items.AddRange(new object[] {
            resources.GetString("newHeight.Items")});
			resources.ApplyResources(this.newHeight, "newHeight");
			this.newHeight.Name = "newHeight";
			this.newHeight.TextChanged += new System.EventHandler(this.newHeight_TextChanged);
			this.newHeight.Leave += new System.EventHandler(this.newHeight_Leave);
			// 
			// newWidth
			// 
			this.newWidth.FormattingEnabled = true;
			resources.ApplyResources(this.newWidth, "newWidth");
			this.newWidth.Name = "newWidth";
			this.newWidth.TextChanged += new System.EventHandler(this.newWidth_TextChanged);
			this.newWidth.Leave += new System.EventHandler(this.newWidth_Leave);
			// 
			// sourceY
			// 
			resources.ApplyResources(this.sourceY, "sourceY");
			this.sourceY.Name = "sourceY";
			this.sourceY.ValueChanged += new System.EventHandler(this.sourceY_ValueChanged);
			// 
			// sourceX
			// 
			resources.ApplyResources(this.sourceX, "sourceX");
			this.sourceX.Name = "sourceX";
			this.sourceX.ValueChanged += new System.EventHandler(this.sourceX_ValueChanged);
			// 
			// cmbScale
			// 
			this.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbScale.FormattingEnabled = true;
			this.cmbScale.Items.AddRange(new object[] {
            resources.GetString("cmbScale.Items"),
            resources.GetString("cmbScale.Items1"),
            resources.GetString("cmbScale.Items2")});
			resources.ApplyResources(this.cmbScale, "cmbScale");
			this.cmbScale.Name = "cmbScale";
			// 
			// flowLayoutPanel1
			// 
			resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Controls.Add(this.label6);
			this.flowLayoutPanel1.Controls.Add(this.newWidth);
			this.flowLayoutPanel1.Controls.Add(this.newHeight);
			this.flowLayoutPanel1.Controls.Add(this.cbLock);
			this.flowLayoutPanel1.Controls.Add(this.label7);
			this.flowLayoutPanel1.Controls.Add(this.sourceX);
			this.flowLayoutPanel1.Controls.Add(this.sourceY);
			this.flowLayoutPanel1.Controls.Add(this.sourceWidth);
			this.flowLayoutPanel1.Controls.Add(this.sourceHeight);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// cbLock
			// 
			resources.ApplyResources(this.cbLock, "cbLock");
			this.cbLock.Name = "cbLock";
			this.cbLock.UseVisualStyleBackColor = true;
			this.cbLock.CheckedChanged += new System.EventHandler(this.cbLock_CheckedChanged);
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// sourceWidth
			// 
			resources.ApplyResources(this.sourceWidth, "sourceWidth");
			this.sourceWidth.Name = "sourceWidth";
			this.sourceWidth.ValueChanged += new System.EventHandler(this.sourceWidth_ValueChanged);
			// 
			// sourceHeight
			// 
			resources.ApplyResources(this.sourceHeight, "sourceHeight");
			this.sourceHeight.Name = "sourceHeight";
			this.sourceHeight.ValueChanged += new System.EventHandler(this.sourceHeight_ValueChanged);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.btnOK, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.cmbScale, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbScale, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnCancel, 4, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// cbScale
			// 
			resources.ApplyResources(this.cbScale, "cbScale");
			this.cbScale.Checked = true;
			this.cbScale.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbScale.Name = "cbScale";
			this.cbScale.UseVisualStyleBackColor = true;
			this.cbScale.CheckedChanged += new System.EventHandler(this.cbScale_CheckedChanged);
			// 
			// line1
			// 
			this.line1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			resources.ApplyResources(this.line1, "line1");
			this.line1.Name = "line1";
			// 
			// line2
			// 
			this.line2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			resources.ApplyResources(this.line2, "line2");
			this.line2.Name = "line2";
			// 
			// layerView
			// 
			this.layerView.AutoScrollMinSize = new System.Drawing.Size(18, 18);
			this.layerView.DisplayMode = VariIconsReload.Components.DisplayMode.Center;
			resources.ApplyResources(this.layerView, "layerView");
			this.layerView.Name = "layerView";
			// 
			// btnCancel
			// 
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Name = "btnCancel";
			// 
			// PickContent
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.layerView);
			this.Controls.Add(this.line2);
			this.Controls.Add(this.line1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.MinimizeBox = false;
			this.Name = "PickContent";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			((System.ComponentModel.ISupportInitialize)(this.sourceY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceX)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sourceWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sourceHeight)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cmbScale;
		private System.Windows.Forms.NumericUpDown sourceY;
		private System.Windows.Forms.NumericUpDown sourceX;
		private System.Windows.Forms.ComboBox newHeight;
		private System.Windows.Forms.ComboBox newWidth;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Components.LayerView layerView;
		private System.Windows.Forms.CheckBox cbScale;
		private System.Windows.Forms.NumericUpDown sourceWidth;
		private System.Windows.Forms.NumericUpDown sourceHeight;
		private System.Windows.Forms.Label line1;
		private System.Windows.Forms.Label line2;
		private System.Windows.Forms.CheckBox cbLock;
		private System.Windows.Forms.Button btnCancel;
	}
}