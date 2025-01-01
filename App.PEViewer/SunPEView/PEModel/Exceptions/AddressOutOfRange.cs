using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.Exceptions
{
    public class AddressOutOfRange : Exception
    {
        public AddressOutOfRange(string str) : base(str)
        {
        }
    }
}
