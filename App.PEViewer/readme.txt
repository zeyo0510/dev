                ____________________________________________________
               /    ______                 __    __                /|
       	      /    / ____/                / /   /_/               / |
             /    / /_____  _____________/ /________________     / /
            /    / __  / / / / __  /  __/ __  / / __  / ___/    / /
           /     ___/ / /_/ / / / /__  / / / / / / / / ___/    / /
          /    /_____/_____/_/ /_/____/_/ /_/_/_/ /_/____/    / /
         /                                                   / /
        /     Web : www.sunshine2k.de                       / /    		
       /            www.bastian-molkenthin.de              / /
      /       Mail: webmaster@sunshine2k.de               / /
     /___________________________________________________/ /
     \____________________________________________________/


SunPEView:

Version 0.7.1.0 - 2015-03-27
----------------------------

SunPEView is a little PE file viewer to investigate PE32/PE64 files. 
It further supports a hexeditor view, a file location calculator and a hex-dec-bin converter. 

NEW: The complete source code for latest version 0.7.1.0 is freely available from my website!


Features:
---------
- Shows detailed information of each PE field, including file offset and field size as well as a description
- Encoding of flags / enumeration members
- Included hex viewer - click a field to go to its hex offset
- Integrated file location calculator (Virtual Address <-> Relative virtual offset <-> File Offset)
- Integrated hexadecimal <-> decimal <-> binary converter

- DOS / COFF / PE Header
- Section Table
- Import Table
- Export Table
- Debug Directory
- Resource Directory
- Base Relocations Directory
- Exception Directory
- TLS Directory
- Dotnet Directory


History:
--------

Version 0.7.1.0:
----------------
- Dotnet Directory support
- Improved bit flag characteristics window
- Log window (first version)
- Minor fixes


Version 0.7.0.0:
----------------
- Standard Resource Support: 
  - Bitmaps 
  - Cursors & Cursors Groups
  - Config Files
  - Version Info
  - Icons & Icon Groups
  - String Tables
  - HTML files
- Standard Resource Support Basic (only hex window):
  - Dialogs  
  - Menus
  - Accelerators
  - RCData
  - Font, Fonts Directory
  - Message Tables
- support of Base Relocations, Exception, TLS DataDirectory
- DEP support of PE image
- Reload button
- FLC: show section name
- Error Messages
- Bugs:
 - fixed a scrolling bug in the hex view
 - fixed a bug in the import table loading


Version 0.6.0.0:
----------------
- first published release (alpha)


Sunshine, March 2015


