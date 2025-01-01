using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat
{
    /// <summary>
    /// General interface each member of a PE file implements
    /// </summary>
    interface IPeElem
    {
        /// <summary>
        /// thw actual value as general object
        /// </summary>
        Object ObjectValue { get; }

        /// <summary>
        /// absolute file offset of this member
        /// </summary>
        long Offset { get; }

        /// <summary>
        /// name of the PE element
        /// </summary>
        string Name { get; }

        /// <summary>
        /// the actual value as formatted string
        /// </summary>
        /// <returns></returns>
        string ValueString();

        /// <summary>
        /// convert the numerical value to its ASCII representation, 
        /// e.g for value 0x54AD, "MZ" would be returned 
        /// </summary>
        /// <returns></returns>
        string NumValAsAscii();

        /// <summary>
        /// absolute file offset of this member as string
        /// </summary>
        string OffsetString{ get; }

        /// <summary>
        /// the number of bytes of which the elements consists of
        /// </summary>
        /// <returns></returns>
        int SizeInBytes();
    }
}
