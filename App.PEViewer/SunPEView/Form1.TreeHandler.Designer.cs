using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunPEView.ContentVisualizer;
using SunPEView.PEModel;
using SunPEView.PEModel.PEFormat;
using SunPEView.PEModel.PEFormat.Resources;
using SunPEView.PEModel.PEFormat.DebugDirectory;
using System.Windows.Forms;
using PEFileTreeView;
using SunPEView.PEModel.PEFormat.Resources.StandardId;

namespace SunPEView
{
    partial class Form1
    {
        private static readonly string TREEKEY_DOS_HEADER = "Dos Header";
        private static readonly string TREEKEY_PE_HEADER = "PE Header";
        private static readonly string TREEKEY_PE_FILEHEADER = "PE File Header";
        private static readonly string TREEKEY_PE_OPTIONALHEADER = "PE Optional Header";
        private static readonly string TREEKEY_PE_DATADIRECTORIES = "PE Data Directories";
        private static readonly string TREEKEY_PE_SECTIONS = "PE Sections";
        private static readonly string TREEKEY_PE_IMPORTS = "PE Import Table";
        private static readonly string TREEKEY_PE_EXPORTS = "PE Export Table";
        private static readonly string TREEKEY_PE_EXPORT_FUNCS = "Exported Functions";
        private static readonly string TREEKEY_PE_RESOURCES = "PE Resource Table";
        private static readonly string TREEKEY_PE_DEBUG = "PE Debug Table";
        private static readonly string TREEKEY_PE_RELOCATION = "PE Relocation Table";
        private static readonly string TREEKEY_PE_EXCEPTION = "PE Exception Table";
        private static readonly string TREEKEY_PE_TLS = "PE TLS Table";
        private static readonly string TREEKEY_PE_DOTNET = "PE .NET Table";

        /// <summary>
        /// Init treeview. Must be called before inserting any elements.
        /// </summary>
        public void InitTreeHandler()
        {
            // add events of treeview
            peFileTreeView1.SelectedPEFileChanged += new PEFileTreeView.PEFileTreeViewControl.SelectedPEFileChangedDelegate(peFileTreeView1_SelectedPEFileChanged);
            peFileTreeView1.SelectedSectionChanged += new PEFileTreeView.PEFileTreeViewControl.SelectedSectionChangedDelegate(peFileTreeView1_SelectedSectionChanged);
        }

        /// <summary>
        /// Add file to treeview.
        /// </summary>
        public void AddFile()
        {
            peFileTreeView1.AddPEFile(peFile);
        }

        /// <summary>
        /// Add for each existing PE resource/element the corresponding treeview element.
        /// </summary>
        public void AddFileSections()
        {
            if (!peFile.isDosHeaderValid()) return;

            peFileTreeView1.AddFileSection(peFile, TREEKEY_DOS_HEADER);

            if (!peFile.isPeHeaderValid()) return;
            peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_HEADER);
            peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_FILEHEADER);
            peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_OPTIONALHEADER);
            peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_DATADIRECTORIES);

            // sections
            if (peFile.PeSectionHeaderTable == null) return;
            peFileTreeView1.SuspendLayout();
            peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_SECTIONS);
            foreach (PeSectionHeader section in peFile.PeSectionHeaderTable)
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_SECTIONS, section.ToString());
            }
            peFileTreeView1.ResumeLayout();

            // Import Directory
            if (peFile.ExistsImportDirectory())
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_IMPORTS);
                foreach (PeImportDescriptor importDesc in peFile.PeImportDirectoryTable)
                {
                    peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_IMPORTS, importDesc.getDllName());
                }
            }

            // Export Directory
            if (peFile.ExistsExportDirectory())
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_EXPORTS);
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_EXPORTS, TREEKEY_PE_EXPORT_FUNCS);
            }
            
            // Relocation Directory
            if (peFile.ExistsRelocationDirectory())
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_RELOCATION);

                foreach (PeRelocationEntry relocEntry in peFile.PeRelocationTable)
                {
                    peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_RELOCATION, relocEntry.ToString());
                }
            }

            // Resource Directory
            if (peFile.ExistsResourceDirectory())
            {
                // add main directory
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_RESOURCES);

                // add all entries
                foreach (PeResourceDirectoryEntry dirEntry in peFile.PeResourceDirectoryTable.GetResourceDirectoryEntries())
                {
                    peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_RESOURCES, dirEntry.ToString());
                    if (!dirEntry.IsLeaf())
                    {
                        AddResourceDirBranch(dirEntry.ToString(), dirEntry.ChildDirectory, 0);
                    }
                }
            }

            // Debug Directory
            if (peFile.ExistsDebugDirectory())
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_DEBUG);
                foreach (PeDebugDirectory debugDir in peFile.PeDebugDirectoryTable)
                {
                    peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_DEBUG, debugDir.GetName());
                }
            }

            // Exception Directory
            if (peFile.ExistsExceptionDirectory())
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_EXCEPTION);
            }

            // TLS Directory
            if (peFile.ExistsTLSDirectory())
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_TLS);
            }

            if (peFile.ExistsDotNetDirectory())
            {
                peFileTreeView1.AddFileSection(peFile, TREEKEY_PE_DOTNET);
            }
        }

        /// <summary>
        /// Helper function to add recursively the whole resource section of the PE file to the treeview.
        /// </summary>
        /// <param name="parentNodeName"></param>
        /// <param name="dirResTable"></param>
        /// <param name="level"></param>
        void AddResourceDirBranch(string parentNodeName, PeResourceDirectoryTable dirResTable, int level)
        {
            //Console.Out.WriteLine("1: " + GetSpaces(level) + parentNodeName + " - " + dirResTable.ToString());
            peFileTreeView1.AddFileSection(peFile, parentNodeName, dirResTable.ToString());
            
            foreach (PeResourceDirectoryEntry entry in dirResTable.GetResourceDirectoryEntries())
            {
                //Console.Out.WriteLine("2: " + GetSpaces(level) + entry.ParentDirectory.ToString() + " - " + entry.ToString());
                peFileTreeView1.AddFileSection(peFile, entry.ParentDirectory.ToString(), entry.ToString());
                
                if (!entry.IsLeaf())
                {
                    AddResourceDirBranch(entry.ToString(), entry.ChildDirectory, level + 1);
                }
                else
                {
                    // leaf node, so add data entry
                    //Console.Out.WriteLine("3: " + GetSpaces(level) + entry.ToString() + " - " + entry.DataEntry.ToString());
                   peFileTreeView1.AddFileSection(peFile, entry.ToString(), entry.DataEntry.ToString());
                }
            }
        }

        
        /// <summary>
        /// Called when user selects a node of the treeview.
        /// Handle the selection and load the necessary PE objects and show them in the datagridview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void peFileTreeView1_SelectedSectionChanged(object sender, PEFileTreeView.PEFileTreeViewControl.SectionSelectionChangeEvents e)
        {
            PEFile peFile = (PEFile)e.PESectionNode.PEFileObject;

            e.Node.Expand();

            if (e.PESectionNode.SectionName.Equals(TREEKEY_DOS_HEADER))
            {
                getContentVisualizer().ShowDosHeader(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_HEADER))
            {
                getContentVisualizer().ShowPeHeader(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_FILEHEADER))
            {
                getContentVisualizer().ShowPeFileHeader(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_OPTIONALHEADER))
            {
                getContentVisualizer().ShowPeOptionalHeader(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_DATADIRECTORIES))
            {
                getContentVisualizer().ShowDataDirectoryTable(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_SECTIONS))
            {
                getContentVisualizer().ShowOverallSections(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_IMPORTS))
            {
                getContentVisualizer().ShowImportTable(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_EXPORTS))
            {
                getContentVisualizer().ShowExportTable(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_EXPORT_FUNCS))
            {
                getContentVisualizer().ShowExportedFunctionsTable(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_RESOURCES))
            {
                getContentVisualizer().ShowResourceTable(peFile, peFile.PeResourceDirectoryTable); // TODO only 1 param,etr
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_DEBUG))
            {
                getContentVisualizer().ShowDebugDirectoryTable(peFile, peFile.PeDebugDirectoryTable); // TODO only 1 param,etr
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_RELOCATION))
            {
                getContentVisualizer().ShowRelocationTable(peFile, peFile.PeRelocationTable); // TODO only 1 param,etr
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_EXCEPTION))
            {
                getContentVisualizer().ShowExceptionTable(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_TLS))
            {
                getContentVisualizer().ShowTLSTable(peFile);
            }
            else if (e.PESectionNode.SectionName.Equals(TREEKEY_PE_DOTNET))
            {
                getContentVisualizer().ShowDotNetTable(peFile);
            }

            // base relocation entries
            if (peFile.ExistsRelocationDirectory())
            {
                foreach (PeRelocationEntry relocEntry in peFile.PeRelocationTable)
                {
                    if (e.PESectionNode.SectionName.Equals(relocEntry.ToString()))
                    {
                        getContentVisualizer().ShowRelocationEntry(peFile, relocEntry);
                    }
                }
            }

            // check if unique section was selected
            if (peFile.PeSectionHeaderTable != null)
            {
                foreach (PeSectionHeader section in peFile.PeSectionHeaderTable)
                {
                    if (e.PESectionNode.SectionName.Equals(section.ToString()))
                    {
                        getContentVisualizer().ShowSection(peFile, section);
                    }
                }
            }

            if (peFile.ExistsImportDirectory())
            {
                // check if unique import was selected
                foreach (PeImportDescriptor impDesc in peFile.PeImportDirectoryTable)
                {
                    if (e.PESectionNode.SectionName.Equals(impDesc.getDllName()))
                    {
                        getContentVisualizer().ShowImport(peFile, impDesc);
                    }
                }
            }

            // Debug Directory
            if (peFile.ExistsDebugDirectory())
            {
                foreach (PeDebugDirectory debugDir in peFile.PeDebugDirectoryTable)
                {
                    if (e.PESectionNode.SectionName.Equals(debugDir.GetName()))
                    {
                        getContentVisualizer().ShowDebugDirectory(peFile, debugDir);
                    }
                }
            }

            if (peFile.ExistsResourceDirectory())
            {
                // resource entry
                foreach (PeResourceDirectoryEntry dirEntry in peFile.PeResourceDirectoryTable.GetResourceDirectoryEntries())
                {
                    if (e.PESectionNode.SectionName.Equals(dirEntry.ToString()))
                    {
                        getContentVisualizer().ShowResourceEntry(peFile, dirEntry);
                        e.Node.Expand();
                    }
                    else
                    {
                        if (!dirEntry.IsLeaf())
                        {
                            CheckResourceBranch(dirEntry.ChildDirectory, e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Helper function to recursively check if a resource element of the treeview was clicked.
        /// If this is the case, load the corresponding resource elements and update the GUI.
        /// </summary>
        /// <param name="resDirTable"></param>
        /// <param name="e"></param>
        private void CheckResourceBranch(PeResourceDirectoryTable resDirTable, PEFileTreeViewControl.SectionSelectionChangeEvents e)
        {
            if (e.PESectionNode.SectionName.Equals(resDirTable.ToString()))
            {
                getContentVisualizer().ShowResourceTable(peFile, resDirTable);
                return;
            }
            else
            {
                foreach (PeResourceDirectoryEntry resDirEntry in resDirTable.GetResourceDirectoryEntries())
                {
                    if (e.PESectionNode.SectionName.Equals(resDirEntry.ToString()))
                    {
                        getContentVisualizer().ShowResourceEntry(peFile, resDirEntry);
                        return;
                    }
                    else
                    {
                        if (resDirEntry.IsLeaf())
                        {
                            if (e.PESectionNode.SectionName.Equals(resDirEntry.DataEntry.ToString()))
                            {
                                // found it, load the resource data entry to grid view
                                getContentVisualizer().ShowResourceDataEntry(peFile, resDirEntry.DataEntry);

                                // standard resource data entries dynamically add items to the treeview
                                PeResourceDataEntry resData = resDirEntry.DataEntry;
                                if (resData.IsTopLevelOfTypeID() && resData.IsStandardID() &&
                                    e.PESectionNode.Nodes.Count == 0)
                                {
                                   AddStdResItems(resData, e);
                                }
                                
                                return;
                            }

                            // leaf is not the one it is searched for   
                            // however, a dynamic added standard resource node might have been selected
                            if (resDirEntry.DataEntry.IsTopLevelOfTypeID() && 
                                resDirEntry.DataEntry.IsStandardID() &&
                                // note that we check the parent(!) data entry for child nodes
                                e.PESectionNode.Parent.Nodes.Count > 0)
                            {
                                CheckExtStdResItems(resDirEntry.DataEntry, e);
                            }

                        }
                        else
                        {
                            // no leaf, however resource data entry might contain special standard resources nodes
                            //continue checking the root nodes
                            CheckResourceBranch(resDirEntry.ChildDirectory, e);
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// Check if to dynamically add new nodes of resource data entries of standard resources.
        /// Note: resData MUST be a standard resource data entry.
        /// </summary>
        /// <param name="resData">resource data entry of standard resource</param>
        /// <param name="e">SectionSelectionChangeEvents which triggered the selection of the resource data entry</param>
        private void AddStdResItems(PeResourceDataEntry resData, PEFileTreeViewControl.SectionSelectionChangeEvents e)
        {
            /* ICON GROUPS */
            if (resData.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONGROUPS)
            {
                PeIconGroupResource resGroupIcon = (PeIconGroupResource)resData.GetStandardResource();
                if (resGroupIcon != null)
                {
                    getContentVisualizer().AddStdResIconGroupElements(peFileTreeView1, peFile, e.PESectionNode, resGroupIcon);
                }
            }
            /* CURSOR GROUPS */
            else if (resData.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORGROUPS)
            {
                PeCursorGroupResource resGroupCursor = (PeCursorGroupResource)resData.GetStandardResource();
                if (resGroupCursor != null)
                {
                    getContentVisualizer().AddStdResCursorGroupElements(peFileTreeView1, peFile, e.PESectionNode, resGroupCursor);
                }
            }
        }

        /// <summary>
        /// Check if a dynamic added standard resource item node was clicked. Requires special handling.
        /// </summary>
        /// <param name="resData"></param>
        /// <param name="e"></param>
        private void CheckExtStdResItems(PeResourceDataEntry resData, PEFileTreeViewControl.SectionSelectionChangeEvents e)
        {
            if (resData.IsTopLevelOfTypeID() &&  /* data entry is ID type */
                resData.IsStandardID())       /* it's also a standard ID */
            {
                /* ICON GROUPS */
                if (resData.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_ICONGROUPS)
                {
                    PeIconGroupResource resGroupIcon = (PeIconGroupResource)resData.GetStandardResource();
                    if (resGroupIcon != null)
                    {
                        foreach (PeIconGroupDirEntry iconDir in resGroupIcon)
                        {
                            if (e.PESectionNode.SectionName.Equals(iconDir.ToString()))
                            {
                                getContentVisualizer().ShowStdResIconGroupDirEntry(peFile, iconDir);
                            }
                        }
                    }
                }
                /* CURSOR GROUPS */
                else if (resData.GetTopLevelEntryID() == (int)PeResourceStandardIdentifier.EStandardIdentifier.RESIDENTIFIER_CURSORGROUPS)
                {
                    PeCursorGroupResource resGroupCursor = (PeCursorGroupResource)resData.GetStandardResource();
                    if (resGroupCursor != null)
                    {
                        foreach (PeCursorGroupDirEntry cursorDir in resGroupCursor)
                        {
                            if (e.PESectionNode.SectionName.Equals(cursorDir.ToString()))
                            {
                                getContentVisualizer().ShowStdResGroupGroupDirEntry(peFile, cursorDir);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when the pefile (root node of treeview) is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void peFileTreeView1_SelectedPEFileChanged(object sender, PEFileTreeView.PEFileTreeViewControl.PEFileSelectionChangeEvents e)
        {
            Console.Out.WriteLine("Selected: " + e.PEFileNode.PEFileObject.FileName);
            PEFile peFile = (PEFile)e.PEFileNode.PEFileObject;
            GeneralFileInfoFiller gfif = new GeneralFileInfoFiller(dataGridView1);
            gfif.FillContent(peFile);
        }
    }
}
