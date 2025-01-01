using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HexEditBox;
using SunPEView.PEModel;

namespace SunPEView.ContentVisualizer.Windows
{
    /// <summary>
    /// A window that contains a hexbox showing the given bytes
    /// </summary>
    public partial class GenericByteArrayWindow : Form
    {
        private IFileAccess iFileAccess;
        private long offset;
        private uint length;

        public GenericByteArrayWindow()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        public GenericByteArrayWindow(IFileAccess iFileAccess, uint offset, uint length) : this()
        {
            this.iFileAccess = iFileAccess;
            this.offset = offset;
            this.length = length;

            hexEditBox1.ByteProvider = 
                GenericByteArrayWindowByteProviderFactory.create(iFileAccess, offset, length);
        }

        public GenericByteArrayWindow(byte[] byteBuffer)
            : this()
        {
            hexEditBox1.ByteProvider =
                GenericByteArrayWindowByteProviderFactory.create(byteBuffer);
        }


        /// <summary>
        /// Factory class to build a GenericByteArrayWindowByteProvider instance
        /// </summary>
        private static class GenericByteArrayWindowByteProviderFactory 
        {
            public static GenericByteArrayWindowByteProvider create(IFileAccess iFileAccess, uint offset, uint length)
            {
                byte[] dataBytes = iFileAccess.ReadBytes(offset, length);
                return new GenericByteArrayWindowByteProvider(dataBytes, length);
            }

            public static GenericByteArrayWindowByteProvider create(byte[] byteBuffer)
            {
                return new GenericByteArrayWindowByteProvider(byteBuffer, byteBuffer.Length);
            }
        }

        /// <summary>
        /// Holds the data bytes to show. An instance can be used to be passed as ByteProvider to an hexbox.
        /// </summary>
        private class GenericByteArrayWindowByteProvider : IByteProvider
        {
            private byte[] dataBytes;
            private long length;

            public GenericByteArrayWindowByteProvider(byte[] dataBytes, long length) 
            {
                this.dataBytes = dataBytes;
                this.length = length;
            }

            public byte[] GetBytes()
            {
                return dataBytes;
            }

            public long Length
            {
                get { return length; }
            }
        }

        private void GenericByteArrayWindow_ResizeBegin(object sender, EventArgs e)
        {
            hexEditBox1.SuspendLayout();
        }

        private void GenericByteArrayWindow_ResizeEnd(object sender, EventArgs e)
        {
            //hexEditBox1.ResumeLayout();
        }

        private void GenericByteArrayWindow_SizeChanged(object sender, EventArgs e)
        {
            hexEditBox1.ResumeLayout();
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
