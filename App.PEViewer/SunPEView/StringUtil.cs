using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView
{
    /// <summary>
    /// Helper class for string handling.
    /// </summary>
    class StringUtil
    {
        /// <summary>
        /// Get the 32bit hexadecimal string of given value.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetFormattedStringUint(uint val)
        {
            return String.Format("{0:X8}", val);
        }

        /// <summary>
        /// Get the 64bit hexadecimal string of given value.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetFormattedStringUlong(ulong val)
        {
            return String.Format("{0:X16}", val);
        }

        /// <summary>
        /// Get the hexadecimal string of given value.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetFormattedHexString(int val)
        {
            return String.Format("{0:X}", val);
        }

        /// <summary>
        /// Function to remove C-style hexprefix "0x" from given string. 
        /// </summary>
        /// <param name="s">string to remove prefix from</param>
        /// <returns>If the prefix exists, a string without the prefix is returbed. 
        /// Otherwise the unchanged string is returned.</returns>
        public static string GetHexStringWithoutPrefix(string s)
        {
            return (s.StartsWith("0x") ? s.Remove(0, 2) : s);
        }

        /// <summary>
        /// Convert given numerical value to hex string.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetFormattedHexString(long num)
        {
            return String.Format("{0:X8}", num);
        }


        /// <summary>
        /// Check if given string represents a valid binary number.
        /// That is if it just contains 0 or 1 and is less than 64 bits long.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidBinaryString(string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;

            if (s.Length >= 64)
                return false;
            foreach (char c in s)
            {
                if (!(c == '0' || c == '1'))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Convert given numerical value to hex byte string.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetFormattedByteString(byte num)
        {
            return String.Format("{0:X2}", num);
        }

        /// <summary>
        /// Convert given numerical value to single character string.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetFormattedCharString(char num)
        {
            return String.Format("{0:c}", num);
        }
    }
}
