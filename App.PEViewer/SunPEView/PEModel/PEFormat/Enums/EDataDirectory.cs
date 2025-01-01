using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.Enums
{
    enum EDataDirectory
    {
        IMAGE_DIRECTORY_ENTRY_EXPORT         =  0,   // Export Directory
        IMAGE_DIRECTORY_ENTRY_IMPORT         =  1,   // Import Directory
        IMAGE_DIRECTORY_ENTRY_RESOURCE       =  2,   // Resource Directory
        IMAGE_DIRECTORY_ENTRY_EXCEPTION      =  3,   // Exception Directory
        IMAGE_DIRECTORY_ENTRY_SECURITY       =  4,   // Security Directory
        IMAGE_DIRECTORY_ENTRY_BASERELOC      =  5,   // Base Relocation Table
        IMAGE_DIRECTORY_ENTRY_DEBUG          =  6,   // Debug Directory
        IMAGE_DIRECTORY_ENTRY_ARCHITECTURE   =  7,   // Architecture Specific Data
        IMAGE_DIRECTORY_ENTRY_GLOBALPTR      =  8,   // RVA of GP
        IMAGE_DIRECTORY_ENTRY_TLS            =  9,  // TLS Directory
        IMAGE_DIRECTORY_ENTRY_LOAD_CONFIG    = 10,   // Load Configuration Directory
        IMAGE_DIRECTORY_ENTRY_BOUND_IMPORT   = 11,   // Bound Import Directory in headers
        IMAGE_DIRECTORY_ENTRY_IAT            = 12,   // Import Address Table
        IMAGE_DIRECTORY_ENTRY_DELAY_IMPORT   = 13,   // Delay Load Import Descriptors
        IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR = 14   // COM Runtime descriptor
    }

    internal static class EDataDirectoryExtensions
    {
        private static readonly string[] names = { "Export Directory",
                                            "Import Directory",
                                            "Resource Directory",
                                            "Exception Directory",
                                            "Security Directory",
                                            "Relocation Directory",
                                            "Debug Directory",
                                            "Architecture Directory",
                                            "Global Pointer Directory",
                                            "TLS Directory",
                                            "Configuration Directory",
                                            "Bound Import Directory",
                                            "Import Address Directory",
                                            "Delay Import Directory",
                                            "COM / .NET Directory",
                                            "Reserved Directory "
                                          };

        internal static string GetDisplayName(this EDataDirectory dataDir)
        {
            return names[(int)dataDir];
        }

        private static readonly string[] descriptions = {
              "Contains the exports of the PE file",
              "Contains the imports of the PE file",
              "Contains the embedded resources",
              "Exception information",
              "Table of attribute certificates. The RVA is here actually a file offset.",
              "Base relocations table",
              "Debug information table",
              "Architecture-specific data",
              "RVA of the value to be stored in the global pointer register",
              "Thread local storage data",
              "Specific Windows NT OS family data",
              "Array of bound import descriptors, each of which describes a DLL this image was bound up with at the time of the image creation",
              "Import Address table",
              "Array of 32-byte ImgDelayDescr structures, each structure describing a delay-load import",
              " The CLR header structure",
              "Reserved"                            
              };

        internal static string GetDescription(this EDataDirectory dataDir)
        {
            return descriptions[(int)dataDir];
        }
    }
}
