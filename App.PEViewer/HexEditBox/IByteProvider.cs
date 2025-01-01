using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexEditBox
{
    public interface IByteProvider
    {
        byte[] GetBytes();

        //byte[] GetBytesCopy(int offset, int copyLength);

        long Length { get; }
    }
}
