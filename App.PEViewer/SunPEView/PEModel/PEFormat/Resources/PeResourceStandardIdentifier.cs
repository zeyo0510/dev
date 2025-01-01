using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Resources
{
    /// <summary>
    /// Hold all standard resource IDs.
    /// See WinUser.h in SDK.
    /// </summary>
    class PeResourceStandardIdentifier
    {
        public enum EStandardIdentifier
        {
            RESIDENTIFIER_CURSORS       = 1,
            RESIDENTIFIER_BITMAPS       = 2,
            RESIDENTIFIER_ICONS         = 3,
            RESIDENTIFIER_MENUS         = 4,
            RESIDENTIFIER_DIALOGS       = 5,
            RESIDENTIFIER_STRINGTABLES  = 6,
            RESIDENTIFIER_FONTDIRECORY  = 7,
            RESIDENTIFIER_FONTS         = 8,
            RESIDENTIFIER_ACCELERATORS  = 9,
            RESIDENTIFIER_RCDATA        = 10, 
            RESIDENTIFIER_MESSAGETABLES = 11,
            RESIDENTIFIER_CURSORGROUPS  = 12,
            RESIDENTIFIER_ICONGROUPS    = 14,
            RESIDENTIFIER_VERSIONINFO   = 16,
            RESIDENTIFIER_HTMLPAGES     = 23,
            RESIDENTIFIER_CONFIGFILES   = 24
        }

        public static readonly Dictionary<uint, string> Identifier
            = new Dictionary<uint, string>
        {
            { 1, "CURSORS" },
            { 2, "BITMAPS" },
            { 3, "ICONS" },
            { 4, "MENUS" },
            { 5, "DIALOGS" },
            { 6, "STRING TABLES" },
            { 7, "FONT DIRECORY" },
            { 8, "FONTS" },
            { 9, "ACCELERATORS" },
            { 10, "RCDATA" },
            { 11, "MESSAGE TABLES" },
            { 12, "CURSOR GROUPS" },
            { 14, "ICON GROUPS" },
            { 16, "VERSION INFO" },
            { 23, "HTML PAGES" },
            { 24, "CONFIGURATION FILES" }
        };

        public static bool IsStandardID(uint id)
        {
            return Identifier.ContainsKey(id);
        }
    }
}
