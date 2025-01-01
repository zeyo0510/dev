using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SunPEView.PEModel.PEFormat.Enums;

namespace SunPEView.ContentVisualizer
{
    /// <summary>
    /// Class to insert general information of the PE file into the gridview.
    /// </summary>
    class GeneralFileInfoFiller : AbstractFiller, IContentFiller
    {
        public GeneralFileInfoFiller(ContentDataGridView dataGridView) : base(dataGridView) { }

        public void FillContent(SunPEView.PEModel.PEFile peFile)
        {
            dataGridView.ColumnCount = 2;
            dataGridView.Columns[0].Name = "File Property";
            dataGridView.Columns[1].Name = "Value";
            dataGridView.Columns[1].Width = 300;

            dataGridView.Rows.Add("Filename", peFile.FileName);
            dataGridView.Rows.Add("Full path", peFile.FilePath);
            dataGridView.Rows.Add("Size (bytes)", peFile.FileSize);

            if (!peFile.isPeHeaderValid())
            {
                dataGridView.Rows.Add("PE Type", "No PE file.");
            }
            else
            {
                if (peFile.isPe64())
                {
                    dataGridView.Rows.Add("PE Type", "64Bit Executable");
                }
                else
                {
                    dataGridView.Rows.Add("PE Type", "32Bit Executable");
                }
            }

            dataGridView.Rows.Add("Created", String.Format("{0:G}", peFile.FileInfo.CreationTime));
            dataGridView.Rows.Add("Last Access", String.Format("{0:G}", peFile.FileInfo.LastAccessTime));
            dataGridView.Rows.Add("Last Written", String.Format("{0:G}", peFile.FileInfo.LastWriteTime));

            FileAttributes attr = peFile.FileInfo.Attributes;
            dataGridView.Rows.Add("Readonly", ConvertBoolToString((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly));
            dataGridView.Rows.Add("Hidden", ConvertBoolToString((attr & FileAttributes.Hidden) == FileAttributes.Hidden));

            dataGridView.Rows.Add("MD5", peFile.GetMD5Hash());
            dataGridView.Rows.Add("SHA-1", peFile.GetSHA1Hash());

            if ((peFile.PeOptionalHeader.DllCharacteristics.Value & (ushort)EDllCharacteristics.DLLCHARACTERISTICS_NX_COMPAT.Value) != 0)
            {
                dataGridView.Rows.Add("DEP support", "YES");
            }
            else
            {
                dataGridView.Rows.Add("DEP support", "NO");
            }
        }

        private string ConvertBoolToString(bool val)
        {
            return (val ? "TRUE" : "FALSE");
        }
        
    }
}
