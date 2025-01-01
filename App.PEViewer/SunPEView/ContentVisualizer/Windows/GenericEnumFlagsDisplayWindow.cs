using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SunPEView.PEModel.PEFormat.Enums;

namespace SunPEView.ContentVisualizer.Windows
{
    /// <summary>
    /// Generic window to display an bit-field enumeration.
    /// </summary>
    public partial class GenericEnumFlagsDisplayWindow : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="enumFlags"></param>
        /// <param name="value"></param>
        public GenericEnumFlagsDisplayWindow(string caption, EnumBitFlagsGeneric enumFlags, long value)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.Text = caption;

            AddGuiElements(enumFlags, value);
        }
        
        /// <summary>
        /// Add the actual checkboxes and labels elements.
        /// </summary>
        private void AddGuiElements(EnumBitFlagsGeneric enumFlags, long value)
        {
            // add panel
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            this.Controls.Add(panel);

            int index = 0;
            foreach (EnumBitFlagsGeneric e in enumFlags.GetValues())
            {
                CheckBox chkBox = new CheckBox();
                chkBox.Location = new System.Drawing.Point(10, (18 * index) + 10);
                chkBox.Size = new System.Drawing.Size(25, 15);
                chkBox.TabIndex = index;
                chkBox.Checked = ((e.Value & value) != 0);
                chkBox.Enabled = false;
                
                TextBox textbox = new TextBox();
                textbox.Location = new Point(35, (18 * index) + 10);
                textbox.Size = new System.Drawing.Size(350, 15);
                textbox.Text = e.Name + " ( 0x" + StringUtil.GetFormattedHexString(e.Value) + ")";
                textbox.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 8);
                textbox.ReadOnly = true;
                textbox.BorderStyle = BorderStyle.None;
                AddToolTipToTextbox(textbox, e.Description);

                panel.Controls.Add(chkBox);
                panel.Controls.Add(textbox);

                index++;
            }
        }

        private void AddToolTipToTextbox(TextBox textbox, string toolTipText)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 0;
            tt.ShowAlways = true;
            tt.SetToolTip(textbox, toolTipText);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
