using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    class PeElem<T> : IPeElem
    {
        long offset;
        T value;
        string name;

        /// <summary>
        /// only valid if T is string. The size of an ASCII string in file has a max size
        /// where the actual could be shorter. This value stores the max size.
        /// </summary>
        int strMaxSize;

        /// <summary>
        /// only valid if T is a string
        /// </summary>
        public void SetStringSize(int size)
        { 
            strMaxSize = size;
        }

        public PeElem(long offset, T value, string name)
        {
            this.offset = offset;
            this.value = value;
            this.name = name;
        }

        public PeElem(long offset, T value, string name, int strSize)
        {
            this.offset = offset;
            this.value = value;
            this.name = name;
            this.strMaxSize = strSize;
        }

        public Object ObjectValue
        {
            get { return value; }
        }

        public T Value
        {
            get { return value; }
        }

        public long Offset
        {
            get { return offset; }
        }

        public string OffsetString
        {
            get { return String.Format("{0:X8}", offset); }
        }

        public string Name
        {
            get { return name; }
        }

        public string ValueString()
        {
            if (value is byte)
            {
                return String.Format("{0:X2}", value);
            }
            else if (value is ushort)
            {
                return String.Format("{0:X4}", value);
            }
            else if (value is uint)
            {
                return String.Format("{0:X8}", value);
            }
            else if (value is string)
            {
                return String.Format("{0}", value);
            }
            else if (value is ulong)
            {
                return String.Format("{0:X16}", value);
            }
            return base.ToString();
        }

        public int SizeInBytes()
        {
            if (value is byte)
                return 1;
            else if (value is ushort)
                return 2;
            else if (value is uint)
                return 4;
            else if (value is ulong)
                return 8;
            else if (value is string)
                return strMaxSize;
            else if (value is PeRegion)
                return (int)(value as PeRegion).Length;
            else return -1;
        }

        public string NumValAsAscii()
        {
            if (value is byte)
            {
                byte val = Convert.ToByte(value);
                return Char.ToString((char)val);
            }
            else if (value is ushort)
            {
                ushort val = Convert.ToUInt16(value);
                return Char.ToString((char)(val & 0x00FF)) + Char.ToString((char)(val >> 8));
            }
            else if (value is uint)
            {
                uint val = Convert.ToUInt32(value);
                return Char.ToString((char)(val & 0x000000FF)) +
                       Char.ToString((char)((val & 0x0000FF00) >> 8)) +
                       Char.ToString((char)((val & 0x00FF0000) >> 16)) +
                       Char.ToString((char)((val & 0xFF000000) >> 24));
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
