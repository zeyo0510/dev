using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VariIconsSDK.UI
{
	public partial class SaveChangesForm : Form
	{
		public SaveChangesForm()
		{
			InitializeComponent();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ListBox.ObjectCollection Items
		{
			get { return listBox1.Items; }
		}
	}
}
