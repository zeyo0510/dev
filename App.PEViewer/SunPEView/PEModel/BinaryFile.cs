using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunPEView.PEModel
{
    /// <summary>
    /// Class to access a binary file. Also used to provide HexEditBox the data.
    /// </summary>
    internal class BinaryFile : HexEditBox.IByteProvider
    {
        /************************************************************
         * Variables
         ************************************************************/
        private string filename;

        private FileDataHandler fdata;

        private byte[] originalByteBuffer;

        private FileInfo fi;


        /************************************************************
         * Properties
         ************************************************************/
        /// <summary>
        /// Name of file including extension
        /// </summary>
        public string FileName
        {
            get { return fi.Name; }
        }

        /// <summary>
        /// Name including complete filepath and extension of file
        /// </summary>
        public string FilePath
        {
            get { return fi.FullName; }
        }

        /// <summary>
        /// Length of file in bytes.
        /// </summary>
        public long FileSize
        {
            get
            {
                if (fi.Length != fdata.Length) { throw new InvalidOperationException(); }
                return fi.Length;
            }
        }

        public IFileAccess FileData
        {
            get { return fdata; }
        }

        public FileInfo FileInfo
        {
            get { return fi; }
        }

        /************************************************************
         * IByteProvider interface implementation
         ************************************************************/
        public byte[] GetBytes() { return originalByteBuffer; }
        public long Length { get { return FileSize; } }


        /************************************************************
         * Methods
         ************************************************************/

        public BinaryFile(string filename)
        {
            this.filename = filename;
        }

        public virtual void LoadFile()
        {
            FileStream fs;

            if (filename == null)
            {
                throw new ArgumentNullException();
            }

            if (!File.Exists(filename))
            {
                throw new ArgumentException(filename + " does not exist.");
            }

            // get file info object
            fi = new FileInfo(filename);

            // create file stream 
            fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);

            // get total byte length of the file
            long length = new FileInfo(filename).Length;

            // attach filestream to binary reader
            BinaryReader binReader = new BinaryReader(fs);

            // read entire file into buffer
            fdata = new FileDataHandler(binReader, length);
            originalByteBuffer = fdata.CopyAllBytes();

            // close file reader
            fs.Close();
            fs.Dispose();
            binReader.Close();
        }

        public string GetMD5Hash()
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            md5.ComputeHash(originalByteBuffer);
            return BitConverter.ToString(md5.Hash).Replace("-", "");
        }

        public string GetSHA1Hash()
        {
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            sha1.ComputeHash(originalByteBuffer);
            return BitConverter.ToString(sha1.Hash).Replace("-", "");
        }
    }
}
