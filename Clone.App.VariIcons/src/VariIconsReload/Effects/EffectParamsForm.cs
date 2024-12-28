using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VariIconsReload.Effects
{
	public partial class EffectParamsForm : Form
	{
		public EffectParamsForm(Control[] configs)
		{
			InitializeComponent();
			if (configs != null)
				_configsPanel.Controls.AddRange(configs);
		}
	}
}
