using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SunPEView.PEModel.Exceptions;

namespace SunPEView.PEModel.PEFormat
{
    class DosHeader : AbstractPeDescriptor
    {
        /* DosHeader structure from WinNT.h */
        public static readonly ushort DOS_SIGNATURE = 0x5A4D;

        /// <summary>
        /// Magic number
        /// </summary>
        public PeElem<ushort> E_magic { get; private set;  }

        /// <summary>
        /// // Bytes on last page of file
        /// </summary>
        public PeElem<ushort> E_cblp { get; private set; }

        /// <summary>
        /// Pages in file
        /// </summary>
        public PeElem<ushort> E_cp { get; private set; }

        /// <summary>
        /// // Relocations
        /// </summary>
        public PeElem<ushort> E_crlc { get; private set; }

        /// <summary>
        /// Size of header in paragraphs
        /// </summary>
        public PeElem<ushort> E_cparhdr { get; private set; }

        /// <summary>
        /// Minimum extra paragraphs needed
        /// </summary>
        public PeElem<ushort> E_minalloc { get; private set; }

        /// <summary>
        /// Maximum extra paragraphs needed
        /// </summary>
        public PeElem<ushort> E_maxalloc { get; private set; }

        /// <summary>
        /// Initial (relative) SS value
        /// </summary>
        public PeElem<ushort> E_ss { get; private set; }

        /// <summary>
        /// // Initial SP value
        /// </summary>
        public PeElem<ushort> E_sp { get; private set; }

        /// <summary>
        /// Checksum
        /// </summary>
        public PeElem<ushort> E_csum { get; private set; }

        /// <summary>
        /// Initial IP value
        /// </summary>
        public PeElem<ushort> E_ip { get; private set; }

        /// <summary>
        /// Initial (relative) CS value
        /// </summary>
        public PeElem<ushort> E_cs { get; private set; }

        /// <summary>
        /// File address of relocation table
        /// </summary>
        public PeElem<ushort> E_lfarlc { get; private set; }

        /// <summary>
        /// Overlay number
        /// </summary>
        public PeElem<ushort> E_ovno { get; private set; }

        /// <summary>
        /// Reserved words
        /// </summary>
        public PeElem<ushort>[] E_res { get; private set; }

        /// <summary>
        /// OEM identifier (for e_oeminfo)
        /// </summary>
        public PeElem<ushort> E_oemid { get; private set; }

        /// <summary>
        /// OEM information; e_oemid specific
        /// </summary>
        public PeElem<ushort> E_oeminfo { get; private set; }

        /// <summary>
        /// Reserved words
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PeElem<ushort>[] E_res2 { get; private set; }

        /// <summary>
        /// Reserved words
        /// </summary>
        public PeElem<uint> E_lfanew { get; private set; }


        public void Read(IFileAccess fDataReader)
        {
            fDataReader.Position = 0;

            E_magic = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_magic");
            E_cblp = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_cblp");
            E_cp = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_cp");
            E_crlc = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_crlc");
            E_cparhdr = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_cparhdr");
            E_minalloc = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_minalloc");
            E_maxalloc = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_maxalloc");
            E_ss = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_ss");
            E_sp = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_sp");
            E_csum = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_csum");
            E_ip = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_ip");
            E_cs = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_cs");
            E_lfarlc = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_lfarlc");
            E_ovno = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_ovno");
            E_res = new PeElem<ushort>[4];
            for (int i = 0; i < 4; i++)
            {
                E_res[i] = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_res (" + i + ")");
            }
            E_oemid = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_oemid");
            E_oeminfo = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_oeminfo");
            E_res2 = new PeElem<ushort>[10];
            for (int i = 0; i < 10; i++)
            {
                E_res2[i] = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "e_res2 (" + i + ")");
            }
            E_lfanew = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "e_lfanew");
        }

        public IEnumerator<IPeElem> GetEnumerator()
        {
            yield return E_magic;
            yield return E_cblp;
            yield return E_cp;
            yield return E_crlc;
            yield return E_cparhdr;
            yield return E_minalloc;
            yield return E_maxalloc;
            yield return E_ss;
            yield return E_sp;
            yield return E_csum;
            yield return E_ip;
            yield return E_cs;
            yield return E_lfarlc;
            yield return E_ovno;
            yield return E_res[0];
            yield return E_res[1];
            yield return E_res[2];
            yield return E_res[3];
            yield return E_oemid;
            yield return E_oeminfo;
            yield return E_res2[0];
            yield return E_res2[1];
            yield return E_res2[2];
            yield return E_res2[3];
            yield return E_res2[4];
            yield return E_res2[5];
            yield return E_res2[6];
            yield return E_res2[7];
            yield return E_res2[8];
            yield return E_res2[9];
            yield return E_lfanew;
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(E_magic.Name, "Magic number of DOS header");
            AddDescriptionEntry(E_cblp.Name, "Bytes on last page of file");
            AddDescriptionEntry(E_cp.Name, "Pages in file");
            AddDescriptionEntry(E_crlc.Name, "Relocations");
            AddDescriptionEntry(E_cparhdr.Name, "Size of header in paragraphs");
            AddDescriptionEntry(E_minalloc.Name, "Minimum extra paragraphs needed");
            AddDescriptionEntry(E_maxalloc.Name, "Maximum extra paragraphs needed");
            AddDescriptionEntry(E_ss.Name, "Initial (relative) SS value");
            AddDescriptionEntry(E_sp.Name, "Initial SP value");
            AddDescriptionEntry(E_csum.Name, "Checksum");
            AddDescriptionEntry(E_ip.Name, "Initial IP value");
            AddDescriptionEntry(E_cs.Name, "Initial (relative) CS value");
            AddDescriptionEntry(E_lfarlc.Name, "File address of relocation tablee");
            AddDescriptionEntry(E_ovno.Name, "Overlay number");
            AddDescriptionEntry(E_res[0].Name, "Reserved words");
            AddDescriptionEntry(E_res[1].Name, "Reserved words");
            AddDescriptionEntry(E_res[2].Name, "Reserved words");
            AddDescriptionEntry(E_res[3].Name, "Reserved words");
            AddDescriptionEntry(E_oemid.Name, "OEM identifier (for e_oeminfo)");
            AddDescriptionEntry(E_oeminfo.Name, "OEM information; e_oemid specific");
            AddDescriptionEntry(E_res2[0].Name, "Reserved words");
            AddDescriptionEntry(E_res2[1].Name, "Reserved words");
            AddDescriptionEntry(E_res2[2].Name, "Reserved words");
            AddDescriptionEntry(E_res2[3].Name, "Reserved words");
            AddDescriptionEntry(E_res2[4].Name, "Reserved words");
            AddDescriptionEntry(E_res2[5].Name, "Reserved words");
            AddDescriptionEntry(E_res2[6].Name, "Reserved words");
            AddDescriptionEntry(E_res2[7].Name, "Reserved words");
            AddDescriptionEntry(E_res2[8].Name, "Reserved words");
            AddDescriptionEntry(E_res2[9].Name, "Reserved words");
            AddDescriptionEntry(E_lfanew.Name, "File address of new exe header");
        }
    }
}
