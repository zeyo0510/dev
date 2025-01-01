using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace SunPEView.ContentVisualizer
{
    /// <summary>
    /// Abstract class that handles common behavior of the datagrid component.
    /// </summary>
    abstract class AbstractFiller
    {
        protected ContentDataGridView dataGridView;

        private static HashSet<DataGridViewCellEventHandler> eventhandlers = new HashSet<DataGridViewCellEventHandler>();

        public AbstractFiller(ContentDataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
            dataGridView.Rows.Clear();
            RemoveAllCellClickHandler();
        }

        private void RemoveAllCellClickHandler()
        {
            foreach (DataGridViewCellEventHandler handler in eventhandlers)
            {
                dataGridView.CellClick -= handler;
            }
            eventhandlers.Clear();
        }

        public void AddCellClickHandler(DataGridViewCellEventHandler newHandler)
        {
            if (!eventhandlers.Contains(newHandler))
            {
                eventhandlers.Add(newHandler);
                dataGridView.CellClick += newHandler;
            }
        }

    }
}
