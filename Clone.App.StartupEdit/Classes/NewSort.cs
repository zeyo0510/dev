using System;
using System.Collections;
using System.Windows.Forms;

namespace StartupEdit
{

	class SortView : IComparer 
	{
		private int col;

		public SortView() 
		{
			col=0;
		}

		public SortView( int column ) 
		{
			col = column;
		}

		public int Compare ( object x, object y ) 
		{
			int Res = 
			
				String.Compare (
				 (	( ListViewItem ) x ).SubItems[col].Text, ( ( ListViewItem ) y ).SubItems[col].Text );

			return Res;
		}
	}

}
