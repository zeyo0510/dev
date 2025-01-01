using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SunPEView.ContentVisualizer.Windows
{
    public partial class GenericEnumDisplayWindow : Form
    {
        public GenericEnumDisplayWindow(Enum e, string caption)
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            this.Text = caption;

            // init listview
            listView1.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Name", 300, HorizontalAlignment.Left);
            listView1.Columns.Add("Value", 120, HorizontalAlignment.Left);

            string[] enumNames = Enum.GetNames(e.GetType());
            //Font boldFont = new Font(listView1.Font, FontStyle.Italic);
            listView1.SuspendLayout();
            foreach(string enumName in enumNames)
            {
                ListViewItem item = listView1.Items.Add(enumName);
                //item.Font = boldFont;
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add(String.Format("0x{0:X4}", (int)Enum.Parse(e.GetType(), enumName)));
                item.SubItems[1].BackColor = Color.LightGray;
            }
            listView1.ResumeLayout();
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
