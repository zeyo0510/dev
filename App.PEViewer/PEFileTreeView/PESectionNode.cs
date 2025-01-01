using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEFileTreeView
{
    public class PESectionNode : PEFileNode
    {
        private string sectionName;

        public PESectionNode(IPEFile peFile, string sectionName)
            : base(peFile)
        {
            this.sectionName = sectionName;
            this.Text = sectionName;
        }

        public string SectionName
        {
            get { return sectionName; }
        }
    }
}
