using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;


// IF I DOUBLE CLICKED AN STARTUP FOLDER ENTRY IT WILL BE EDITED

//------- Actually we need to refresh the view to reflect any changes made when
//------- 1) Enable, Disable, Edit, Delete or add an Entry
//------- Others\ ViewMain_KeyDown,
//------- And we need to immediately apply any changes the user has done when
//------- 1) the form load, 2)after the unload of the options dialog
//-------  But what changes need immediate apply
//-------  1) OnTop, 2) Toolbar Dock Postion 3)The default Entry of the double click

namespace StartupEdit
{
    public partial class FrmMainM : Form
    {
        public FrmMainM()
        {
            InitializeComponent();

            // assign the view
            OneFetch.TheGlobalView = ViewMain;
        }


        #region Our Declaration

        /// <summary>
        /// The Core Class Use It to get and fetch Entries from virtually anywhere
        /// </summary>
        FetchMan OneFetch = new FetchMan();

        /// <summary>
        /// An instance of the operation ( ClsAddOns ) which holds the analyze method
        /// </summary>
        private ClsAddOns MyAction = new ClsAddOns();

        /// <summary>
        /// an instance not intialized of the other form carries out opertions like edit and  add
        /// </summary>
        private FrmEditAddEntries MyEd;

        #endregion

        #region Form Code

        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            Check(MyAction.ReportOS());

            MyAction.FirstLaunch(); // Create necessary keys

            ApplyViewOptions(); // Apply ViewMain and TreeMain options

            ApplyFormOptions(true); // Apply All User's Customizations related to the form

            TreeMain.SelectedNode = TreeMain.Nodes[0]; // Focuse on the first node

            ContainerMathod("All Enabled Entries"); // core method
        }

        private void FrmMain_Closing(object sender, FormClosingEventArgs e)
        {
            if (MyAction.ReportOS() != "WinXp Without Admin") { SetOptions(); }
        }

        #endregion

        #region Toolbar Code

        private void TheBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            int Numbero = BarEdit.Rectangle.Height;

            switch (TheBar.Buttons.IndexOf(e.Button))
            {
                case 0: // File
                    IsdalFile.Show(TheBar, new Point(0, Numbero));
                    break;
                case 2: // Edit
                    IsdalEdit.Show(TheBar, new Point(56, Numbero));
                    break;
                case 4: // View
                    IsdalView.Show(TheBar, new Point(114, Numbero));
                    break;
                case 6: // Tools
                    IsdalTools.Show(TheBar, new Point(177, Numbero));
                    break;
                case 8: // Help
                    IsdalHelp.Show(TheBar, new Point(242, Numbero));
                    break;
            }
        }


        #endregion

        #region TreeView Code

        private void TreeMain_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            ContainerMathod(e.Node.FullPath);
        }


        #endregion

        #region ListView Code

        private void ViewMain_DoubleClick(object sender, System.EventArgs e)
        {
            if (IsdalMain.MenuItems[2].DefaultItem == true)
            {
                IsdalMainEdit_Click(sender, e);
                return;
            }

            IsdalMainProp_Click(sender, e);
        }


        private void ViewMain_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            if (ViewMain.SelectedItems.Count == 0)
            {
                if (ViewMain.Sorting == SortOrder.Ascending)
                {
                    ViewMain.Sorting = SortOrder.Descending;
                }
                else
                {
                    ViewMain.Sorting = SortOrder.Ascending;
                }
            }

            this.ViewMain.ListViewItemSorter = new SortView(e.Column);
        }


        private void ViewMain_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            EnableDisable();
        }


        private void ViewMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keyBoardSafe(sender, e);

            if (ViewMain.SelectedItems.Count != 0)
            {
                keyBoardRisky(sender, e);
            }
        }


        #endregion

        #region Statusbar Code

        private void Main_SBar_PanelClick(object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    switch (e.StatusBarPanel.Text)
                    {
                        case "Program Options":
                            IsdalMainOptions_Click(sender, e);
                            break;

                        case "Finished Scanning":
                            IsdalMainRefresh_Click(sender, e);
                            break;
                    }
                }
            }
        }


        #endregion

        #region Context Menus Group

        #region IsdalMain

        private void IsdalMain_Popup(object sender, System.EventArgs e)
        {
            EnableDisable();
        }


        private void IsdalMainLarge_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("LargeIcon");
        }


        private void IsdalMainSmall_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("SmallIcon");
        }


        private void IsdalMainList_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("List");
        }


        private void IsdalMainDetails_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("Details");
        }


        //-----------------------------------------------------------------------------------------------------\\
        //--------------------------  Begining of Delete, Edit, Add, Enable, Disable --------------------------\\
        //-----------------------------------------------------------------------------------------------------\\

        private void IsdalMainRemove_Click(object sender, System.EventArgs e)
        {
            ClsEntriesManipulation MyClsEntriesManipulation = new ClsEntriesManipulation();

            if
                (
                !MyClsEntriesManipulation.RemoveTheEntry
                (
                ViewMain.FocusedItem.Text, // Entry Name
                MyAction.AnalyzeIt(ViewMain.FocusedItem.SubItems[1].Text),  // Entry Data 
                ViewMain.FocusedItem.SubItems[2].Text, // Entry Root (hkey)
                ViewMain.FocusedItem.SubItems[4].Text,  // EntryStatus
                ViewMain.FocusedItem.SubItems[5].Text  // EntryKey
                )
                )
            {
                MessageBox.Show
                ("File No longer Exists \n no system changes occured", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ContainerMathod(TreeMain.SelectedNode.FullPath);
        }

        private void IsdalMainEdit_Click(object sender, System.EventArgs e)
        {
            // CombHkey || 0 = LocalMachine 1 = CurrentUser

            int TheRootIndex = 0, TheTypeIndex = 0;

            if (ViewMain.FocusedItem.SubItems[2].Text == "HKEY_CURRENT_USER") { TheRootIndex = 1; }

            switch (ViewMain.FocusedItem.SubItems[3].Text)
            {
                case "Run": //Run
                    TheTypeIndex = 0;
                    break;

                case "RunOnce": // RunOnce
                    TheTypeIndex = 1;
                    break;

                case "RunOnceEx": // RunOnceEx
                    TheTypeIndex = 2;
                    break;

                case "RunServices": // RunServices
                    TheTypeIndex = 3;
                    break;

                case "RunServicesOnce": // RunServicesOnce
                    TheTypeIndex = 4;
                    break;
            }

            MyEd = new FrmEditAddEntries();

            MyEd.CombHkey.SelectedIndex = TheRootIndex;
            MyEd.CombSuffix.SelectedIndex = TheTypeIndex;
            //MyEd.CombSuffix.Enabled = false;

            MyEd.TxtCommandAddEdit.Text = ViewMain.FocusedItem.SubItems[1].Text;
            MyEd.TxtNameAddEdit.Text = ViewMain.FocusedItem.Text;

            MyEd.Text = " Edit " + ViewMain.FocusedItem.Text;

            MyEd.pictureBox1.Image = this.ViewMain.LargeImageList.Images[ViewMain.FocusedItem.ImageIndex];

            MyEd.Cmd_Test.DialogResult = DialogResult.Yes; // Send a Yes So We could know it is edit

            if (MyEd.ShowDialog(this) == DialogResult.Yes)// this to refresh the view when necessary
            {
                ContainerMathod(TreeMain.SelectedNode.FullPath);
            }
        }


        private void IsdalMainAdd_Click(object sender, System.EventArgs e)
        {
            MyEd = new FrmEditAddEntries();

            MyEd.CombHkey.SelectedIndex = 1;
            MyEd.CombSuffix.SelectedIndex = 0;

            MyEd.TxtCommandAddEdit.Text = null;
            MyEd.TxtNameAddEdit.Text = null;

            MyEd.Cmd_Test.DialogResult = DialogResult.OK; // Send an OK So We could know it is Add

            if (MyEd.ShowDialog(this) == DialogResult.OK)
            {
                // here we should not refer to the currently selected node WHY
                // cause the user might not add in the same spot
                //AND you can track where did he created the entry an focus on it
                ContainerMathod("All Enabled Entries");

                TreeMain.SelectedNode = TreeMain.Nodes[0];
            }
        }


        private void IsdalMainEnable_Click(object sender, System.EventArgs e)
        {
            ClsEntriesManipulation MyClsEntriesManipulation = new ClsEntriesManipulation();

            if (
                MyClsEntriesManipulation.EnableEntry
                (
                ViewMain.FocusedItem.Text, // Entry Name
                ViewMain.FocusedItem.SubItems[1].Text, // EntryData
                ViewMain.FocusedItem.SubItems[2].Text,/*Entry Root*/
                ViewMain.FocusedItem.SubItems[3].Text, /*Entry Type*/
                ViewMain.FocusedItem.SubItems[5].Text // Entry Path
                ))
            {
                ContainerMathod(TreeMain.SelectedNode.FullPath);
            }
        }


        private void IsdalMainDisable_Click(object sender, System.EventArgs e)
        {
            ClsEntriesManipulation MyClsEntriesManipulation = new ClsEntriesManipulation();

            if (
                MyClsEntriesManipulation.DisableEntry
                (
                ViewMain.FocusedItem.Text, // Entry Name
                ViewMain.FocusedItem.SubItems[1].Text, // Entry Data
                ViewMain.FocusedItem.SubItems[2].Text, // Entry Root hkey
                ViewMain.FocusedItem.SubItems[3].Text,  // Entry Type
                ViewMain.FocusedItem.SubItems[5].Text  // EntryKey 
                ))
            {
                ContainerMathod(TreeMain.SelectedNode.FullPath);
            }
        }

        //-----------------------------------------------------------------------------------------------------\\
        //----------------------------   End of Delete, Edit, Add, Enable, Disable ----------------------------\\
        //-----------------------------------------------------------------------------------------------------\\

        private void IsdalMainExecute_Click(object sender, System.EventArgs e)
        {
            try
            {
                ProcessStartInfo Exec = new ProcessStartInfo();

                Exec.FileName = MyAction.AnalyzeIt(ViewMain.FocusedItem.SubItems[1].Text);
                Exec.UseShellExecute = true;

                Process.Start(Exec);
            }
            catch (Win32Exception MyExp)
            {
                MessageBox.Show("Internal Error Occured While Trying To Execute" + "\n" +
                    MyAction.AnalyzeIt(ViewMain.FocusedItem.SubItems[1].Text) + "\n" +
                    MyExp.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void IsdalMainProp_Click(object sender, System.EventArgs e)
        {
            if (CheckForEntryPoint())
            {
                ClsAPICalls.CallPropDialog(this.Handle, ClsAPICalls.GetProperties.SHOP_FILEPATH,
                    MyAction.AnalyzeIt(ViewMain.FocusedItem.SubItems[1].Text), null);
                return;
            }
                MessageBox.Show
                    ("Hi Man \n Your Shell32.dll Is Out Of Date Version 5 or Higher Is Required");
        }


        private void IsdalMainExplore_Click(object sender, System.EventArgs e)
        {
            FileInfo MyFile = new FileInfo(MyAction.AnalyzeIt(ViewMain.FocusedItem.SubItems[1].Text));

            if (!MyFile.Exists)
            {
                MessageBox.Show
                ("File No longer Exists \n no system changes occured", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ProcessStartInfo Exp = new ProcessStartInfo();

            Exp.Verb = "Explore";
            Exp.FileName = MyFile.DirectoryName;
            Exp.UseShellExecute = true;
            Exp.WorkingDirectory = MyFile.DirectoryName;
            Exp.WindowStyle = ProcessWindowStyle.Maximized;

            Process.Start(Exp);
        }


        private void IsdalMainAbout_Click(object sender, System.EventArgs e)
        {
            FrmOptions About = new FrmOptions();

            About.TabOptions.TabPages.Remove(About.PageGeneral);
            About.Text = "About Ahmad Shaban";

            About.ShowDialog(this);
        }


        private void IsdalMainRefresh_Click(object sender, System.EventArgs e)
        {
            ContainerMathod(TreeMain.SelectedNode.FullPath);
        }


        private void IsdalMainOptions_Click(object sender, System.EventArgs e)
        {
            FrmOptions Options = new FrmOptions();

            Options.TabOptions.TabPages.Remove(Options.PageAbout);

            Options.Text = "Options";

            Options.ShowDialog(this);

            //We call it just for the of the immediate apply of the OnTop and the Doubleclicking the view Option 
            ApplyFormOptions(false);
        }


        private void IsdalMainClose_Click(object sender, System.EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();

            this.Close();
        }


        #endregion

        #region IsdalFile

        private void IsdalFile_Popup(object sender, System.EventArgs e)
        {
            EnableDisable();
        }


        private void IsdalFileAdd_Click(object sender, System.EventArgs e)
        {
            IsdalMainAdd_Click(sender, e);
        }


        private void IsdalFileExit_Click(object sender, System.EventArgs e)
        {
            IsdalMainClose_Click(sender, e);
        }


        private void IsdalFileOptions_Click(object sender, System.EventArgs e)
        {
            IsdalMainOptions_Click(sender, e);
        }


        #endregion

        #region IsdalEdit

        private void IsdalEdit_Popup(object sender, System.EventArgs e)
        {
            EnableDisable();
        }


        private void IsdalEditDisable_Click(object sender, System.EventArgs e)
        {
            IsdalMainDisable_Click(sender, e);
        }


        private void IsdalEditDelete_Click(object sender, System.EventArgs e)
        {
            IsdalMainRemove_Click(sender, e);
        }


        private void IsdalEditEnable_Click(object sender, System.EventArgs e)
        {
            IsdalMainEnable_Click(sender, e);
        }


        private void IsdalEditEdit_Click(object sender, System.EventArgs e)
        {
            IsdalMainEdit_Click(sender, e);
        }


        #endregion

        #region IsdalView

        private void IsdalView_Popup(object sender, System.EventArgs e)
        {
            EnableDisable();
        }


        private void IsdalViewLarg_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("LargeIcon");
        }


        private void IsdalViewSmall_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("SmallIcon");
        }


        private void IsdalViewList_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("List");
        }


        private void IsdalViewDitails_Click(object sender, System.EventArgs e)
        {
            SwitchViewTo("Details");
        }


        private void IsdalViewRefresh_Click(object sender, System.EventArgs e)
        {
            IsdalMainRefresh_Click(sender, e);
        }


        #endregion

        #region IsdalTools

        private void IsdalTools_Popup(object sender, System.EventArgs e)
        {
            EnableDisable();
        }


        private void IsdalToolsExec_Click(object sender, System.EventArgs e)
        {
            IsdalMainExecute_Click(sender, e);
        }


        private void IsdalToolsExplore_Click(object sender, System.EventArgs e)
        {
            IsdalMainExplore_Click(sender, e);
        }


        private void IsdalToolsProp_Click(object sender, System.EventArgs e)
        {
            IsdalMainProp_Click(sender, e);
        }

        private void IsdalToolsTurnOff_Click(object sender, System.EventArgs e)
        {
            FrmShutDown FrmIt = new FrmShutDown();
            FrmIt.ShowDialog(this);
        }


        private void IsdalToolsFileEditor_Click(object sender, System.EventArgs e)
        {
            FrmTextEditor Frma = new FrmTextEditor();

            Frma.ShowDialog(this);
        }


        #endregion

        #region IsdalHelp

        private void IsdalHelp_Popup(object sender, System.EventArgs e)
        {
            EnableDisable();
        }


        private void IsdalHelpAbout_Click(object sender, System.EventArgs e)
        {
            IsdalMainAbout_Click(sender, e);
        }


        #endregion

        #region IsdalTree Code

        private void IsdalTreeExpandAll_Click(object sender, System.EventArgs e)
        {
            TreeMain.ExpandAll();
        }


        private void IsdalTreeCollapseAll_Click(object sender, System.EventArgs e)
        {
            TreeMain.CollapseAll();
        }


        #endregion

        #endregion

        #region Additional Methods

        /// <summary>
        /// This Is The Method Which is like a container to a group of methods
        /// RefreshView, FillStatusBar, EnableDisable
        /// </summary>
        /// <param name="TheString">put here the name of the seleted node</param>
        void ContainerMathod(string TheString)
        {
            RefreshView(TheString);

            FillStatusBar();

            EnableDisable();
        }


        /// <summary>
        /// To fill the statusbar with data like how many disabled and how many enabled
        /// </summary>
        void FillStatusBar()
        {
            PanelEntriesFound.Text = "Total Entries Found \"" + ViewMain.Items.Count.ToString() + "\"";

            PanelOSVersion.Text = Environment.OSVersion.ToString() + " Profile Loaded";
        }


        /// <summary>
        /// to enable or disable Entries in the menus and toolbar according to information supplied
        /// </summary>
        private void EnableDisable()
        {
            if (ViewMain.SelectedItems.Count == 0) // means no item is selected
            {
                // Disable all the items first
                foreach (MenuItem IsdalEditItem in this.IsdalEdit.MenuItems)
                {
                    IsdalEditItem.Enabled = false;
                }

                foreach (MenuItem IsdalToolsItem in this.IsdalTools.MenuItems)
                {
                    IsdalToolsItem.Enabled = false;
                    if (IsdalToolsItem.Text == "T&weaks") { break; }
                }

                for (int IsdalMainItem = 2; IsdalMainItem <= 11; IsdalMainItem++)
                {
                    IsdalMain.MenuItems[IsdalMainItem].Enabled = false;
                }
            } // big if
            else // if there's an item selected enable according to the given info
            {
                foreach (MenuItem IsdalEditItem in this.IsdalEdit.MenuItems)
                {
                    IsdalEditItem.Enabled = true;
                }

                foreach (MenuItem IsdalToolsItem in this.IsdalTools.MenuItems)
                {
                    IsdalToolsItem.Enabled = true;
                }

                foreach (MenuItem IsdalMainItem in this.IsdalMain.MenuItems)
                {
                    IsdalMainItem.Enabled = true;
                }

                switch (ViewMain.FocusedItem.SubItems[4].Text)
                {

                    case "Enabled": //4
                        IsdalMain.MenuItems[9].Enabled = false; // Enable
                        IsdalEdit.MenuItems[3].Enabled = false; //Enable
                        break;


                    case "Disabled": //4
                        IsdalMain.MenuItems[2].Enabled = false; // Edit
                        IsdalMain.MenuItems[10].Enabled = false; // Disable

                        IsdalEdit.MenuItems[0].Enabled = false; //Disable
                        IsdalEdit.MenuItems[5].Enabled = false; //Edit
                        break;

                    case "Disabled By MSConfig.exe": //4
                        {
                            #region Temp action till i fully do it

                            if (MyAction.ReportOS() == "A None WinXp System")
                            {
                                IsdalMain.MenuItems[2].Enabled = false; // Edit
                                IsdalMain.MenuItems[10].Enabled = false; // Disable

                                IsdalEdit.MenuItems[0].Enabled = false; //Disable
                                IsdalEdit.MenuItems[5].Enabled = false; //Edit
                            }
                            else
                            {
                                foreach (MenuItem IsdalEditItem in this.IsdalEdit.MenuItems)
                                {
                                    IsdalEditItem.Enabled = false;
                                }

                                foreach (MenuItem IsdalToolsItem in this.IsdalTools.MenuItems)
                                {
                                    IsdalToolsItem.Enabled = false;
                                    if (IsdalToolsItem.Text == "T&weaks") { break; }
                                }

                                for (int IsdalMainItem = 2; IsdalMainItem <= 11; IsdalMainItem++)
                                {
                                    IsdalMain.MenuItems[IsdalMainItem].Enabled = false;

                                }
                            }

                            #endregion
                        }
                        break;
                } // switch

                if (ViewMain.FocusedItem.SubItems[2].Text == "Startup Folder") //2
                {
                    // No Edit
                    IsdalMain.MenuItems[2].Enabled = false; // Edit
                    IsdalEdit.MenuItems[5].Enabled = false; // Edit
                }
            } // Big else

            //-----------------------------------Enable Tweaks----------------------------------//
            if (MyAction.ReportOS() != "A None WinXp System")
            {
                IsdalTools.MenuItems[5].Enabled = true;
            }
        }


        /// <summary>
        /// We have three Contextmenu This Method Is To Handle The Check State Of The Three
        /// According To The Current View of the list view
        /// </summary>
        /// <param name="TheState"></param>
        private void SwitchViewTo(string TheState)
        {
            foreach (MenuItem IsdalViewItem in IsdalView.MenuItems)
            {
                IsdalViewItem.Checked = false;
            }

            foreach (MenuItem IsdalMainViewItem in IsdalMainView.MenuItems)
            {
                IsdalMainViewItem.Checked = false;
            }

            switch (TheState)
            {
                case "Details":

                    IsdalMainViewDetails.Checked = true;
                    IsdalViewDetails.Checked = true;

                    ViewMain.View = View.Details;
                    break;

                case "List":

                    IsdalMainViewList.Checked = true;
                    IsdalViewList.Checked = true;

                    ViewMain.View = View.List;
                    break;

                case "LargeIcon":

                    IsdalMainViewLarg.Checked = true;
                    IsdalViewLarg.Checked = true;

                    ViewMain.View = View.LargeIcon;
                    break;

                case "SmallIcon":

                    IsdalMainViewSmall.Checked = true;
                    IsdalViewSmall.Checked = true;

                    ViewMain.View = View.SmallIcon;
                    break;

                default:
                    IsdalMainViewDetails.Checked = true;
                    IsdalViewDetails.Checked = true;

                    ViewMain.View = View.Details;
                    break;
            }
        }


        /// <summary>
        /// To check the shell32.dll to for method "SHObjectProperties" shell32.dll version 5.0 or later required
        /// </summary>
        /// <returns>returns true if it is found otherwise false</returns>
        private bool CheckForEntryPoint()
        {
            return
                ExitTheWin.CheckEntryPoint("shell32.dll", "SHObjectProperties");

        }


        private void Check( string toswitchit)
        {
            string TheMsg = "\n An Access Denied error was returned while attempting to read" +
                        " from the registry. " +
                        "\n You may need to log on using an Administrator account to make the specified changes.";

            switch (toswitchit) // get the os version
            {
                case "WinXp Without Admin":     
                    MessageBox.Show(" Sorry Mr \"" + Environment.UserName + TheMsg, "Admin Not Detected",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Application.Exit();
                    this.Close();
                    return;

                case "WinXp With Admin":
                    // CAUTION //
                    // YOU MAY THINK THERE'S A DUPLICATION IN THE FOLLOWING TWO LINES
                    // NO THERE'S NO SUCH THING
                    // WHEN YOU REMOVE THE FIRST NODE THE TREE VIEW RESORT ITS INDEX. SO !!!!!! ???
                    TreeMain.Nodes[2].Nodes[1].Nodes[3].Remove(); // To remove the RunServices and
                    TreeMain.Nodes[2].Nodes[1].Nodes[3].Remove(); //  RunServicesOnce
                    break;

                case "OUTSIDER":
                    MessageBox.Show("Wooow What kinda of OS is this !? out", "OUTSIDER", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    Application.Exit();
                    this.Close();
                    return;

                default:
                    MessageBox.Show("Wooow What kinda of OS is this !?", "OUTSIDER", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    Application.Exit();
                    this.Close();
                    return;
            }
        }

        #region For Applying User Options

        /// <summary>
        /// Method Responsible for Applying the ListView Options we just call it in the form load once
        /// and we do not need it again
        /// </summary>
        private void ApplyViewOptions()
        {
            try
            {
                //Open the key according to the user name, so we let each user 
                //has his own settigs
                RegistryKey InnerReg = Registry.LocalMachine.OpenSubKey
                    (@"Software\AlQademoUn\StartEdit\" + Environment.UserName, true);

                // Check The "save The columns width" option
                if (bool.Parse(InnerReg.GetValue("SaveColWidth", "false").ToString()))
                {
                    for (int Cols = 0; Cols <= 5; Cols++) // Because we have only 5 columns
                    {
                        ViewMain.Columns[Cols].Width = int.Parse(
                            InnerReg.GetValue("Col" + Cols.ToString(), 150).ToString());
                    }
                }

                // to remeber the last view applied by the user details, list, 
                //small icons or larg icons
                if (bool.Parse(InnerReg.GetValue("RememberView", "false").ToString()))
                {
                    SwitchViewTo(InnerReg.GetValue("LastView", "Details").ToString());
                }

                // to Expand the treeview if the user wishes that
                if (bool.Parse(InnerReg.GetValue("ExpandOnStart", "false").ToString()))
                {
                    TreeMain.ExpandAll();
                    TreeMain.Nodes[0].EnsureVisible();
                }

                InnerReg.Close();
            }
            catch (System.Security.SecurityException MyExp)
            {
                MessageBox.Show
                    ("An error returned while trying to access the windows registry" +
                        "Error is " + MyExp.Message + "\n" +
                    "Startup Editor requires an administrartor privileges to run properly", "Access is denied");
                return;
            }
            catch (ArgumentNullException MyEx)
            {
                MessageBox.Show("Error applying settings" + "\n" + MyEx.Message);
            }
            catch (NullReferenceException MyEx)
            {
                MessageBox.Show("Error applying settings" + "\n" + MyEx.Message);
            }
            catch (Exception MyEx)
            {
                MessageBox.Show("Error applying settings" + "\n" + MyEx.Message);
            }
        }


        /// <summary>
        /// To Apply The Main Form Options On Form_Load Event
        /// </summary>
        /// <param name="ApplySizeAndLocation">That's Because When We Want To Apply It Only on Start</param>
        private void ApplyFormOptions(bool ApplySizeAndLocation)
        {
            //Open the key, we let each user has his own settigs
            RegistryKey TheGlobalReg = Registry.LocalMachine.OpenSubKey
                (@"Software\AlQademoUn\StartEdit\" + Environment.UserName, true);

            //IsdalMain Default Entry
            if (TheGlobalReg.GetValue("EditDefault", "false").ToString() == "true")
            {
                IsdalMain.MenuItems[2].DefaultItem = true; // edit
                IsdalMain.MenuItems[5].DefaultItem = false; // prpoerties
            }
            else
            { // needed for the apply in runtime
                IsdalMain.MenuItems[2].DefaultItem = false; //edit
                IsdalMain.MenuItems[5].DefaultItem = true; // properties
            }

            // TopMost Or Not
            this.TopMost = bool.Parse(TheGlobalReg.GetValue("OnTop", "false").ToString());

            //Check Docking Option
            if (bool.Parse(TheGlobalReg.GetValue("BarBottom", "false").ToString()))
            {
                TheBar.Dock = DockStyle.Bottom;
            }
            else // to apply the changes while form is open
            {
                TheBar.Dock = DockStyle.Top;
            }

            if (ApplySizeAndLocation)
            {

                //check "save size" option
                if (bool.Parse(TheGlobalReg.GetValue("SaveFrmSize", "false").ToString()))
                {
                    this.Size = new System.Drawing.Size(
                        int.Parse(TheGlobalReg.GetValue("MainWidth", 800).ToString()),
                        int.Parse(TheGlobalReg.GetValue("MainHeight", 400).ToString()));
                }

                // check "save location" Option
                if (bool.Parse(TheGlobalReg.GetValue("SaveFrmLocation", "false").ToString()))
                {
                    this.Location = new System.Drawing.Point(
                        int.Parse(TheGlobalReg.GetValue("MainX", 10).ToString()),
                        int.Parse(TheGlobalReg.GetValue("MainY", 10).ToString()));
                }
            }

            TheGlobalReg.Close();
        }


        #endregion

        #region Save the Settings on Form_Closing

        /// <summary>
        /// Method Used When the main form is closing to get the needed settings and save it
        /// in the registry so we could retrieve it when form load if the user set that
        /// </summary>
        private void SetOptions()
        {
            // Open the to save the setting in it
            RegistryKey TheSetOptionsReg = Registry.LocalMachine.OpenSubKey
                (@"Software\AlQademoUn\StartEdit\" + Environment.UserName, true);

            // First Save the listview last view applied by the user
            TheSetOptionsReg.SetValue("LastView", ViewMain.View);

            // Then save the width of each column
            for (int ColWidth = 0; ColWidth <= 5; ColWidth++)
            {
                TheSetOptionsReg.SetValue("Col" + ColWidth.ToString(), ViewMain.Columns[ColWidth].Width);
            }

            // Save Width, Heght, X, Y
            TheSetOptionsReg.SetValue("MainWidth", this.Width);
            TheSetOptionsReg.SetValue("MainHeight", this.Height);
            TheSetOptionsReg.SetValue("MainX", this.Location.X);
            TheSetOptionsReg.SetValue("MainY", this.Location.Y);

            // you should always close the key
            TheSetOptionsReg.Close();
        }


        #endregion

        #region CoreMethod

        /// <summary>
        /// WOW method to deal with the results
        /// </summary>
        /// <param name="ThePath">What do you want to get ??</param>
        private void RefreshView(string ThePath)
        {
            PanelScanning.Text = "Please Wait! Currently Scanning..........";
            PanelEntriesFound.Text = "Now Counting !!";

            ViewMain.Items.Clear();

            ViewMain.BeginUpdate();

            switch (ThePath)
            {
                case "Registry":
                    OneFetch.GetLocalMahine(false);
                    OneFetch.GetCurrentUser(false);
                    break;

                case "Registry\\Local Machine":
                    OneFetch.GetLocalMahine(false);
                    break;

                case "Registry\\Local Machine\\Run":
                    OneFetch.ReadRegistry("HKEY_LOCAL_MACHINE", null, "Run", false);
                    break;

                case "Registry\\Local Machine\\RunOnce":
                    OneFetch.ReadRegistry("HKEY_LOCAL_MACHINE", null, "RunOnce", false);
                    break;

                case "Registry\\Local Machine\\RunOnceEx":
                    OneFetch.ReadRegistry("HKEY_LOCAL_MACHINE", null, "RunOnceEx", false);
                    break;

                case "Registry\\Local Machine\\RunServices":
                    OneFetch.ReadRegistry("HKEY_LOCAL_MACHINE", null, "RunServices", false);
                    break;

                case "Registry\\Local Machine\\RunServicesOnce":
                    OneFetch.ReadRegistry("HKEY_LOCAL_MACHINE", null, "RunServicesOnce", false);
                    break;

                case "Registry\\Current User":
                    OneFetch.GetCurrentUser(false);
                    break;

                case "Registry\\Current User\\Run":
                    OneFetch.ReadRegistry("HKEY_CURRENT_USER", null, "Run", false);
                    break;

                case "Registry\\Current User\\RunOnce":
                    OneFetch.ReadRegistry("HKEY_CURRENT_USER", null, "RunOnce", false);
                    break;

                case "Startup Folder":
                    OneFetch.GetStartupFolder(false);
                    break;

                case "Startup Folder\\All Users":
                    OneFetch.ReadStartUpFolders("All Users Enabled");
                    break;

                case "Startup Folder\\Current User":
                    OneFetch.ReadStartUpFolders("User Name Enabled");
                    break;

                case "All Disabled Entries":
                    OneFetch.GetLocalMahine(true);
                    OneFetch.GetCurrentUser(true);
                    OneFetch.GetStartupFolder(true);
                    break;

                case "All Enabled Entries":
                    OneFetch.GetLocalMahine(false);
                    OneFetch.GetCurrentUser(false);
                    OneFetch.GetStartupFolder(false);
                    break;

                case "MSConfig":
                    OneFetch.GetMSConfigReg(MyAction.ReportOS());
                    OneFetch.GetMSConfigFolder(MyAction.ReportOS());
                    break;

                case "MSConfig\\startupreg":
                        OneFetch.GetMSConfigReg(MyAction.ReportOS());
                        break;

                case "MSConfig\\startupfolder":
                        OneFetch.GetMSConfigFolder(MyAction.ReportOS());
                    break;

                default:
                    ViewMain.Items.Clear();
                    break;
            }

            // we use it so we can remove the customization file "desktop.ini" from appearing
            // note that we list any file type in the startup folders not just the *.lnk (shortcuts)
            foreach (ListViewItem ViewMainItem in ViewMain.Items)
            {
                if (ViewMainItem.Text == "desktop.ini") { ViewMainItem.Remove(); }
            }

            ViewMain.EndUpdate();

            PanelScanning.Text = "Finished Scanning";
        }


        #endregion

        #region KeyBoardd

        /// <summary>
        /// An item MUST NOT be selected to call this mehos
        /// </summary>
        /// <param name="sender">an object raises</param>
        /// <param name="e">the event</param>
        void keyBoardSafe(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F2:
                    IsdalMainOptions_Click(sender, e);
                    break;

                case Keys.F3:
                    IsdalMainAdd_Click(sender, e);
                    break;

                case Keys.F4:
                    IsdalToolsFileEditor_Click(sender, e);
                    break;

                case Keys.F5:
                    ContainerMathod(TreeMain.SelectedNode.FullPath);
                    break;

                case Keys.F6:
                    IsdalToolsTurnOff_Click(sender, e);
                    break;
            }
        }


        /// <summary>
        /// An item MUST be selected to call this mehos
        /// </summary>
        /// <param name="sender">an object raises</param>
        /// <param name="e">the event</param>
        void keyBoardRisky(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    IsdalMainRemove_Click(sender, e);
                    break;
            }
        }


        #endregion




        #endregion

    }
}
