using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel
{
    public interface IFileAccess
    {
        uint Position { get; set; }
        long Length { get; }

        bool IsAddressValid();
        bool IsAddressValid(uint pos);
        bool IsAddressRangeValid(uint pos, uint length);

        byte ReadByte(uint pos);
        ushort ReadUInt16(uint pos);
        short ReadInt16(uint pos);
        uint ReadUInt32(uint pos);
        int ReadInt32(uint pos);
        ulong ReadUInt64(uint pos);
        long ReadInt64(uint pos);
        /// <summary>
        /// Read an ascii string from file. 
        /// Length of the string is the minimum of the first occuring zero-terminator or the value of parameter maxLength.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="maxlength"></param>
        /// <returns></returns>
        string ReadAscii(uint pos, uint maxlength);

        /// <summary>
        /// Read an unisode string from file
        /// </summary>
        /// <param name="pos">byte position in file to start reading</param>
        /// <param name="numCharactersToRead">number of unicode characters to read</param>
        /// <returns></returns>
        string ReadUnicode(uint pos, uint numCharactersToRead);
        string ReadUnicode(uint numCharactersToRead);
        string ReadUnicodeMaxLength(uint pos, uint maxlength);
        string ReadUnicodeMaxLength(uint maxlength);

        byte ReadByte();
        ushort ReadUInt16();
        short ReadInt16();
        uint ReadUInt32();
        int ReadInt32();
        ulong ReadUInt64();
        long ReadInt64();
        string ReadAscii(uint maxlength);

        byte[] ReadBytes(uint pos, uint length);

        //byte this[uint pos] { get; }

        // save and restore current address
        void PushAddress();
        void PopAddress();

        // alignment helper function
        void IncFilePosTo32BitBoundary();
    }
}
