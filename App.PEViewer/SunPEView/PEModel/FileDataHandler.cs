using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunPEView.PEModel
{
    class FileDataHandler : IFileAccess
    {
        /************************************************************
         * Variables
         ************************************************************/
        private byte[] fileRawData;

        private uint internalFilePos;

        private Stack<uint> addressStack;

        /************************************************************
         * Properties
         ************************************************************/
        public long Length
        {
            get { return fileRawData.Length; }
        }

        public uint Position
        {
            get { return internalFilePos; }
            set { internalFilePos = value; }
        }

        public void PushAddress()
        {
            addressStack.Push(internalFilePos);
        }

        public void PopAddress()
        {
            internalFilePos = addressStack.Pop();
        }

        /************************************************************
         * Methods
         ************************************************************/

        public FileDataHandler(byte[] rawData)
        {
            if (rawData == null)
            {
                throw new ArgumentNullException();
            }

            fileRawData = rawData;
            addressStack = new Stack<uint>();
        }

        public FileDataHandler(BinaryReader br, long fileLength)
        {
            if (br == null)
            {
                throw new ArgumentNullException();
            }

            if (fileLength > uint.MaxValue)
            {
                throw new FileDataAccessException("Filesize not yet supported.");
            }

            fileRawData = br.ReadBytes((Int32)fileLength);
            addressStack = new Stack<uint>();
        }

        public FileDataHandler(FileDataHandler fileDataToCopy)
        {
            fileRawData = fileDataToCopy.CopyAllBytes();
            addressStack = new Stack<uint>();
        }

        public bool IsAddressValid()
        {
            return IsAddressValid(internalFilePos);
        }

        public bool IsAddressValid(uint pos)
        {
            return (pos >= 0 && pos < Length);
        }

        public bool IsAddressRangeValid(uint pos, uint length)
        {
            return (pos >= 0 && pos < Length - length);
        }

        public byte ReadByte(uint pos)
        {
            if (pos < 0 || pos >= Length)
            {
                throw new FileDataAccessException();
            }

            return (byte)fileRawData[pos];
        }

        public byte ReadByte()
        {
            return ReadByte(internalFilePos++);
        }

        public byte this[uint index]
        {
            get
            {
                return ReadByte(index);
            }
        }

        public ushort ReadUInt16(uint pos)
        {
            if (pos < 0 || pos >= Length - 1)
            {
                throw new FileDataAccessException();
            }

            return (ushort)(fileRawData[pos] | fileRawData[pos+1] << 8);
        }

        public ushort ReadUInt16()
        {
            ushort result = ReadUInt16(internalFilePos);
            internalFilePos += 2;
            return result;
        }

        public short ReadInt16(uint pos)
        {
            if (pos < 0 || pos >= Length - 1)
            {
                throw new FileDataAccessException();
            }

            return (short)(fileRawData[pos] | fileRawData[pos + 1] << 8);
        }

        public short ReadInt16()
        {
            short result = ReadInt16(internalFilePos);
            internalFilePos += 2;
            return result;
        }

        public uint ReadUInt32(uint pos)
        {
            if (pos < 0 || pos >= Length - 3)
            {
                throw new FileDataAccessException("Offset " + pos + " is out of file range.");
            }

            return (uint)(fileRawData[pos] | fileRawData[pos + 1] << 8 |
                            fileRawData[pos + 2] << 16 | fileRawData[pos + 3] << 24);
        }

        public uint ReadUInt32()
        {
            uint result = ReadUInt32(internalFilePos);
            internalFilePos += 4;
            return result;
        }

        public int ReadInt32(uint pos)
        {
            if (pos < 0 || pos >= Length - 3)
            {
                throw new FileDataAccessException();
            }

            return (int)(fileRawData[pos] | fileRawData[pos + 1] << 8 |
                            fileRawData[pos + 2] << 16 | fileRawData[pos + 3] << 24);
        }

        public int ReadInt32()
        {
            int result = ReadInt32(internalFilePos);
            internalFilePos += 4;
            return result;
        }

        public ulong ReadUInt64(uint pos)
        {
            if (pos < 0 || pos >= Length - 7)
            {
                throw new FileDataAccessException();
            }

            uint u1 = (uint)(fileRawData[pos] | fileRawData[pos + 1] << 8 |
                            fileRawData[pos + 2] << 16 | fileRawData[pos + 3] << 24);
            uint u2 = (uint)(fileRawData[pos + 4] | fileRawData[pos + 5] << 8 |
                            fileRawData[pos + 6] << 16 | fileRawData[pos + 7] << 24);
            return (ulong)(u1 | (ulong)u2 << 32);
        }

        public ulong ReadUInt64()
        {
            ulong result = ReadUInt64(internalFilePos);
            internalFilePos += 8;
            return result;
        }

        public long ReadInt64(uint pos)
        {
            if (pos < 0 || pos >= Length - 7)
            {
                throw new FileDataAccessException();
            }

            uint u1 = (uint)(fileRawData[pos] | fileRawData[pos + 1] << 8 |
                            fileRawData[pos + 2] << 16 | fileRawData[pos + 3] << 24);
            uint u2 = (uint)(fileRawData[pos + 4] | fileRawData[pos + 5] << 8 |
                            fileRawData[pos + 6] << 16 | fileRawData[pos + 7] << 24);
            return (long)(u1 | (ulong)u2 << 32);
        }

        public long ReadInt64()
        {
            long result = ReadInt64(internalFilePos);
            internalFilePos += 8;
            return result;
        }


        public string ReadAscii(uint pos, uint maxlength)
        {
            int length = (int)maxlength;
            for (int i = 0; i < maxlength; i++)
            {
                if (fileRawData[pos + i] == 0)
                {
                    length = i;
                    break;
                }
            }

            StringBuilder sb = new StringBuilder(length + 1);
            for (int i = 0; i < length; i++)
            {
                sb.Append((char)fileRawData[pos + i]);
            }
            return sb.ToString();
        }

        public string ReadAscii(uint maxlength)
        {
            string retStr = ReadAscii(internalFilePos, maxlength);
            internalFilePos += maxlength;
            return retStr;
        }

        public string ReadUnicode(uint pos, uint numCharactersToRead)
        {
            if ((pos < 0) || (pos > Length - numCharactersToRead * 2))
            {
                throw new FileDataAccessException();
            }

            byte[] byteArray = new byte[numCharactersToRead * 2];
            for (int i = 0; i < numCharactersToRead*2; i++)
            {
                byteArray[i] = fileRawData[pos + i];
            }
            
            return System.Text.Encoding.Unicode.GetString(byteArray);
        }

        public string ReadUnicode(uint numCharactersToRead)
        {
            string retStr = ReadUnicode(internalFilePos, numCharactersToRead);
            internalFilePos += (numCharactersToRead * 2);
            return retStr;
        }

        public string ReadUnicodeMaxLength(uint pos, uint maxlength)
        {
            if ((pos < 0) || (pos > Length - maxlength * 2))
            {
                throw new FileDataAccessException();
            }

            byte[] maxbyteArray = new byte[maxlength * 2];
            int actualLength;
            for (actualLength = 0; actualLength < (maxlength * 2) - 1; actualLength+=2)
            {
                if (fileRawData[pos + actualLength] == 0 && fileRawData[pos + actualLength + 1] == 0)
                {
                    break;
                }
                else
                {
                    maxbyteArray[actualLength] = fileRawData[pos + actualLength];
                    maxbyteArray[actualLength+1] = fileRawData[pos + actualLength+1];
                }
            }

            byte[] newArray = new byte[actualLength];
            Array.Copy(maxbyteArray, newArray, actualLength);
            return System.Text.Encoding.Unicode.GetString(newArray);
        }

        public string ReadUnicodeMaxLength(uint maxlength)
        {
            string retStr = ReadUnicodeMaxLength(internalFilePos, maxlength);
            internalFilePos += (uint)((retStr.Length * 2) + 2 /* terminating 0 */);
            return retStr;
        }

        public byte[] ReadBytes(uint pos, uint length)
        {
            if (pos < 0 || pos >= Length - length)
            {
                throw new FileDataAccessException();
            }

            byte[] res = new byte[length];
            Array.Copy(fileRawData, pos, res, 0, length);
            return res;
        }

        public byte[] CopyAllBytes()
        {
            byte[] newBuf = new byte[Length];
            fileRawData.CopyTo(newBuf, 0);
            return newBuf;
        }

        public void IncFilePosTo32BitBoundary()
        {
            while (internalFilePos % 4 != 0)
                internalFilePos++;
        }

        public class FileDataAccessException : IOException
        {
            public FileDataAccessException() : base() { }
            public FileDataAccessException(string message) : base(message) { }
        }
    }
}
