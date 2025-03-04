﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    enum EMachine
    {
        /* values taken from WinNT.h */

        IMAGE_FILE_MACHINE_UNKNOWN    = 0,
        IMAGE_FILE_MACHINE_I386       = 0x014c,  // Intel 386.
        IMAGE_FILE_MACHINE_R3000      = 0x0162,  // MIPS little-endian, 0x160 big-endian
        IMAGE_FILE_MACHINE_R4000      = 0x0166,  // MIPS little-endian
        IMAGE_FILE_MACHINE_R10000     = 0x0168,  // MIPS little-endian
        IMAGE_FILE_MACHINE_WCEMIPSV2  = 0x0169,  // MIPS little-endian WCE v2
        IMAGE_FILE_MACHINE_ALPHA      = 0x0184,  // Alpha_AXP
        IMAGE_FILE_MACHINE_SH3        = 0x01a2,  // SH3 little-endian
        IMAGE_FILE_MACHINE_SH3DSP     = 0x01a3,
        IMAGE_FILE_MACHINE_SH3E       = 0x01a4,  // SH3E little-endian
        IMAGE_FILE_MACHINE_SH4        = 0x01a6,  // SH4 little-endian
        IMAGE_FILE_MACHINE_SH5        = 0x01a8,  // SH5
        IMAGE_FILE_MACHINE_ARM        = 0x01c0,  // ARM Little-Endian
        IMAGE_FILE_MACHINE_THUMB      = 0x01c2,
        IMAGE_FILE_MACHINE_AM33       = 0x01d3,
        IMAGE_FILE_MACHINE_POWERPC    = 0x01F0,  // IBM PowerPC Little-Endian
        IMAGE_FILE_MACHINE_POWERPCFP  = 0x01f1,
        IMAGE_FILE_MACHINE_IA64       = 0x0200,  // Intel 64
        IMAGE_FILE_MACHINE_MIPS16     = 0x0266,  // MIPS
        IMAGE_FILE_MACHINE_ALPHA64    = 0x0284,  // ALPHA64
        IMAGE_FILE_MACHINE_MIPSFPU    = 0x0366,  // MIPS
        IMAGE_FILE_MACHINE_MIPSFPU16  = 0x0466,  // MIPS
        IMAGE_FILE_MACHINE_AXP64      = IMAGE_FILE_MACHINE_ALPHA64,
        IMAGE_FILE_MACHINE_TRICORE    = 0x0520,  // Infineon
        IMAGE_FILE_MACHINE_CEF        = 0x0CEF,
        IMAGE_FILE_MACHINE_EBC        = 0x0EBC,  // EFI Byte Code
        IMAGE_FILE_MACHINE_AMD64      = 0x8664,  // AMD64 (K8)
        IMAGE_FILE_MACHINE_M32R       = 0x9041,  // M32R little-endian
        IMAGE_FILE_MACHINE_CEE        = 0xC0EE
    }
}
