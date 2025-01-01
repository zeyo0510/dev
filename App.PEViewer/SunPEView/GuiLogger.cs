using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.PEModel;
using System.Drawing;
using System.Windows.Forms;

namespace SunPEView
{
    class GuiLogger
    {
        private System.Windows.Forms.ListBox logBox;

        public GuiLogger(System.Windows.Forms.ListBox logBox)
        {
            this.logBox = logBox;
            this.logBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(logBox_DrawItem);

        }

        void logBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index != -1)
            {
                ListBoxLogEventItem listItem = (ListBoxLogEventItem)((ListBox)sender).Items[e.Index];

                e.Graphics.DrawString(listItem.ToString(), ((Control)sender).Font,
                    new SolidBrush(listItem.TextColor), e.Bounds);
            }
            if (e.State == DrawItemState.Focus)
            {
                e.DrawFocusRectangle();
            }
        }
        public void logger_logEvent(object sender, PELogger.LoggingEventArgs e)
        {
            //logBox.Items.Add(e.LogMessage);
            logBox.Items.Add(new ListBoxLogEventItem(e));
            // scroll to end
            int visibleItems = logBox.ClientSize.Height / logBox.ItemHeight;
            logBox.TopIndex = Math.Max(logBox.Items.Count - visibleItems + 1, 0);
        }

        public void logContextMenuClearLog_Click(object sender, EventArgs e)
        {
            logBox.Items.Clear();
        }
    }

    class ListBoxLogEventItem
    {
        private PELogger.LoggingEventArgs logEvent;
        private string text;

        public ListBoxLogEventItem(PELogger.LoggingEventArgs e)
        {
            this.logEvent = e;
            StringBuilder sb = new StringBuilder();
            switch (e.LogLevel)
            {
                case PELogger.LoggingLevel.INFO:
                    sb.Append("(INFO) ");
                    break;
                case PELogger.LoggingLevel.WARNING:
                    sb.Append("(WARNING) ");
                    break;
                case PELogger.LoggingLevel.ERROR:
                    sb.Append("(ERROR) ");
                    break;
            }
            sb.Append(DateTime.Now.ToString("HH:mm:ss: "));
            sb.Append(e.LogMessage);
            this.text = sb.ToString();
        }

        public override string ToString()
        {
            return text;
        }

        public Color TextColor
        {
            get
            {
                switch (logEvent.LogLevel)
                {
                    case PELogger.LoggingLevel.INFO:
                        return Color.DarkBlue;
                    case PELogger.LoggingLevel.WARNING:
                        return Color.Orange;
                    case PELogger.LoggingLevel.ERROR:
                        return Color.Red;
                    default:
                        return Color.Black;
                }
            }
        }
    }
}
