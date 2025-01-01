using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEFileTreeView
{
    public interface IPEFile
    {
        string FileName { get; }
        string FilePath { get; }
    }
}
