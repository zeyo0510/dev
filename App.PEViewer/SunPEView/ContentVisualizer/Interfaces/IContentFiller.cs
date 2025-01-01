using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.ContentVisualizer
{
    interface IContentFiller
    {
        void FillContent(SunPEView.PEModel.PEFile peFile);
    }
}
