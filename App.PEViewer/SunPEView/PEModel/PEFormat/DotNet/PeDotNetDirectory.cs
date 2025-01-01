using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel.PEFormat.DotNet
{
    class PeDotNetDirectory : AbstractPeDescriptor
    {
        public PeElem<uint> Cb { get; private set; }
        public PeElem<ushort> MajorRuntimeVersion { get; private set; }
        public PeElem<ushort> MinorRuntimeVersion { get; private set; }
        public PeDataDirectory MetaData { get; private set; }
        public PeElem<uint> Flags{ get; private set; }
        public PeElem<uint> EntryPointToken { get; private set; }
        public PeDataDirectory Resources { get; private set; }
        public PeDataDirectory StrongNameSignature { get; private set; }
        public PeDataDirectory CodeManagerTable { get; private set; }
        public PeDataDirectory VTableFixups { get; private set; }
        public PeDataDirectory ExportAddressTableJumps { get; private set; }
        public PeDataDirectory ManagedNativeHeader { get; private set; }
              
        private PEFile peFile;
        private long dotnetDirOffset;        /* file offset of start of TLS table */

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="peFile"></param>
        /// <param name="relocDirOffset"></param>
        public PeDotNetDirectory(PEFile peFile, long dotnetDirOffset)
        {
            this.peFile = peFile;
            this.dotnetDirOffset = dotnetDirOffset;
        }

        /// <summary>
        /// Read the DotNet directory from file
        /// </summary>
        /// <param name="fDataReader"></param>
        private void Read(IFileAccess fDataReader)
        {
            fDataReader.PushAddress();
            fDataReader.Position = (uint)dotnetDirOffset;

            Cb = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "SizeOfStructure");
            MajorRuntimeVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MajorRuntimeVersion");
            MinorRuntimeVersion = new PeElem<ushort>(fDataReader.Position, fDataReader.ReadUInt16(), "MinorRuntimeVersion");
            MetaData = new PeDataDirectory(peFile);
            MetaData.Read(fDataReader, "MetaData");
            Flags = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "Flags");
            EntryPointToken = new PeElem<uint>(fDataReader.Position, fDataReader.ReadUInt32(), "EntryPointToken/RVA");
            Resources = new PeDataDirectory(peFile);
            Resources.Read(fDataReader, "Resources");
            
            StrongNameSignature = new PeDataDirectory(peFile);
            StrongNameSignature.Read(fDataReader, "StrongNameSignature");

            CodeManagerTable = new PeDataDirectory(peFile);
            CodeManagerTable.Read(fDataReader, "CodeManagerTable");

            VTableFixups = new PeDataDirectory(peFile);
            VTableFixups.Read(fDataReader, "VTableFixups");

            ExportAddressTableJumps = new PeDataDirectory(peFile);
            ExportAddressTableJumps.Read(fDataReader, "ExportAddressTableJumps");

            ManagedNativeHeader = new PeDataDirectory(peFile);
            ManagedNativeHeader.Read(fDataReader, "ManagedNativeHeader");

            fDataReader.PopAddress();
        }

        /// <summary>
        /// Get all elements
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IPeElem> GetEnumerator()
        {
            if (Cb == null)
            {   /* lazy loading */
                Read(peFile.FileData);
            }

            yield return Cb;
            yield return MajorRuntimeVersion;
            yield return MinorRuntimeVersion;
            yield return MetaData.VirtualAddress;
            yield return MetaData.Size;
            yield return Flags;
            yield return EntryPointToken;
            yield return Resources.VirtualAddress;
            yield return Resources.Size;
            yield return StrongNameSignature.VirtualAddress;
            yield return StrongNameSignature.Size;
            yield return CodeManagerTable.VirtualAddress;
            yield return CodeManagerTable.Size;
            yield return VTableFixups.VirtualAddress;
            yield return VTableFixups.Size;
            yield return ExportAddressTableJumps.VirtualAddress;
            yield return ExportAddressTableJumps.Size;
            yield return ManagedNativeHeader.VirtualAddress;
            yield return ManagedNativeHeader.Size;
        }

        protected override void AddDescriptions()
        {
            AddDescriptionEntry(Cb.Name, "Size of the header in bytes");
            AddDescriptionEntry(MajorRuntimeVersion.Name, "The minimum version of the runtime required to run this program");
            AddDescriptionEntry(MinorRuntimeVersion.Name, "The minor portion of the version");
            AddDescriptionEntry(MetaData.VirtualAddress.Name, "RVA of the physical metadata");
            AddDescriptionEntry(MetaData.Size.Name, "Size of the physical metadata");
            AddDescriptionEntry(Flags.Name, "Flags describing the runtime image");
            AddDescriptionEntry(EntryPointToken.Name, "Token for the MethodDef or File of the entry point for the image");

            AddDescriptionEntry(Resources.VirtualAddress.Name, "RVA of implementation-specific resources");
            AddDescriptionEntry(Resources.Size.Name, "Size of implementation-specific resources");
            AddDescriptionEntry(StrongNameSignature.VirtualAddress.Name, "RVA of the hash data for this PE file used by the CLI loader for binding and versioning");
            AddDescriptionEntry(StrongNameSignature.Size.Name, "Size of the hash data for this PE file used by the CLI loader for binding and versioning");
            AddDescriptionEntry(CodeManagerTable.VirtualAddress.Name, "RVA of CodeManagerTable, always 0 (?)");
            AddDescriptionEntry(CodeManagerTable.Size.Name, "Size of CodeManagerTable, always 0 (?)");
            AddDescriptionEntry(VTableFixups.VirtualAddress.Name, "RVA of an array of locations in the file that contain an array of function pointers");
            AddDescriptionEntry(VTableFixups.Size.Name, "Size of an array of locations in the file that contain an array of function pointers");
            AddDescriptionEntry(ExportAddressTableJumps.VirtualAddress.Name, "RVA of ExportAddressTableJumps, always 0 (?)");
            AddDescriptionEntry(ExportAddressTableJumps.Size.Name, "Size of ExportAddressTableJumps, always 0 (?)");
            AddDescriptionEntry(ManagedNativeHeader.VirtualAddress.Name, "RVA of ManagedNativeHeader, always 0 (?)");
            AddDescriptionEntry(ManagedNativeHeader.Size.Name, "Size of ManagedNativeHeader, always 0 (?)");
        }
    }
}
