using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PEFileTreeView
{
    public class PEFileNode : TreeNode
    {
        public PEFileNode(IPEFile peFile)
        {
            this.Tag = peFile;
            this.Text = peFile.FileName;
        }

        public IPEFile PEFileObject { get { return (IPEFile)this.Tag; } }
    }
}
