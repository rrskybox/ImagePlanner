﻿//Windows Visual Basic Forms Application: Image Planner
//
//Standalone application to generate a calendar of imaging times for (an object
//
// ------------------------------------------------------------------------
//
// Author: R.McAlister (2016-18)
// Version 1.0 - C# port from Version 2.0 VB
//  
// Note:  Some of the algorithms in this application derive directly from concepts and programs
//   found in Astronomy on the Personal Computer 4th Edition, Oliver Montenbruck and Thomas Pfleger,
//   Springer, 2000.  Much appreciated.
//
// ------------------------------------------------------------------------
//This form encapsulates the Target Shooting user interface
//Three button controls:  Create, Print, Close -- should be self-explanatory
//Target name input, calendar year input, minimum targt altitude input
//One datagrid for (calendar
//
//V3.0:     Major: Added WazzUp form
//V3.1:     1. Fixed Target Name to look up new target with a key press
//V3.1.1:   1. FIxed an error where Add Target was not finding the Active XML file, because there was none.
//          2. Fixed an error where Preview would screw up because there was an empty My Equipment.txt file.
//          3. Fixed Minimum entries so they did not sort if no Prospect List had been generated yet.
//V3.1.2:   !. Added the Tracking popup.
//          2. Cleaned up some test code here and there
//V3.1.3:   1. Changed selected date color to salmon
//          2. Restructured classes to create "AstroMath" and "AstroChart" namespaces
//              with "Planar", "Spherical", "Transform", "Formatter", "Sidereal" and DailyPosition classes
//          3. Disabled the calendar column sorting
//V3.2:     1. Modified target plan searches and methods to use "Default" instead of "Active"
//          2. Modified target plan file to "Humason" rather than "NightHawk".
//V3.3:     1. Fixed anomolies with positive longitudes and negative latitudes.  Also see AstroMath library


using AstroMath;
using Humason;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TheSky64Lib;

namespace ImagePlanner
{
    public partial class FormImagePlanner : Form
    {
        public DateTime SelectedDate;
        public TimeSpan SelectedTimeOfDay;

        //public double MinimumAltitude = 0;

        public DailyPosition[] sundata;
        public DailyPosition[] tgtdata;
        public DailyPosition[] moondata;
        public int TargetIndex;
        public DateTime TargetUTCDate;
        public bool SelectionEnabled;
        public static bool ProspectProtected = false;  //set to false if prospect pop up is not active, true otherwise -- used for selection
        public static bool ExoPlanetProtected = false;  //set to false if exoplanet pop up is not active, true otherwise -- used for selection
        public string moonDataDescription;

        public FormPreview previewForm = null;
        public Point previewFormLocation = new Point(0, 0);
        public FormTargetAltitude pathForm = null;
        public Point pathFormLocation = new Point(0, 0);
        public FormDetails detailForm = null;
        public Point detailFormLocation = new Point(0, 0);
        public FormTargetTrack trackForm = null;
        public Point trackFormLocation = new Point(0, 0);
        public FormProspect prospectForm = null;
        public Point prospectFormLocation = new Point(0, 0);
        public FormExoPlanet exoPlanetForm = null;
        public Point exoPlanetFormLocation = new Point(0, 0);
        public FormTargetList tgtListForm = null;
        public Point tgtListFormLocation = new Point(0, 0);

        private bool isInInit = false;
        private bool isListingTargets = false;

        public bool enteringTargetState;  //true if writing target in, false if target has been entered

        public static TargetChangeEvent QPUpdate = new TargetChangeEvent();

        public FormImagePlanner()
        {
            isInInit = true;

            InitializeComponent();

            SelectionEnabled = false;
            ButtonGreen(DetailsButton);
            ButtonGreen(AltitudeButton);
            ButtonGreen(PreviewButton);
            ButtonGreen(AddTargetPlanButton);
            ButtonGreen(DeleteTargetPlanButton);
            ButtonGreen(PrintButton);
            ButtonGreen(DoneButton);
            ButtonGreen(InfoButton);
            ButtonGreen(ProspectButton);
            ButtonGreen(AssessButton);
            ButtonGreen(TrackButton);
            ButtonGreen(ExoPlanetButton);
            ButtonGreen(ExportListButton);
            ButtonGreen(CurrentTargetListButton);
            ButtonGreen(TargetGroupButton);


            InitializeCurrentTarget();

            this.FontHeight = 1;
            MonthCalendar.RowCount = 31;
            for (int i = 0; i <= 30; i++)
                MonthCalendar.Rows[i].HeaderCell.Value = (i + 1).ToString();

            //Compute current dates based on TSX star chart julian date
            //  this allows star charts to be in different locations and time zones
            //  as set up by user
            sky6StarChart tsxsc = new sky6StarChart();

            //Get the star chart julian date and convert to current date/time
            DateTime tsxDate = TimeManagement.LocalTSXDateTime;  //preinitialize datetime from tsx
            DateTime dateTSX = TimeManagement.JulianDateNow();
            CurrentYearPick.Value = TimeManagement.StandardTimeFromDST(dateTSX).Year;
            SelectedTimeOfDay = (TimeSpan)TimeManagement.CurrentTSXTimeOfDay;
            GenerateCalendar();
            TimeManagement.CurrentTSXTimeOfDay = SelectedTimeOfDay;
            Show();
            System.Windows.Forms.Application.DoEvents();
            SelectionEnabled = true;
            //Figure out the year and load it into the year box
            string thisyear = dateTSX.ToString("yyyy");
            CurrentYearPick.Value = Convert.ToInt16(thisyear);
            //Pick the current date as the selected cell
            SelectedDate = dateTSX;
            int jCol = TimeManagement.StandardTimeFromDST(dateTSX).Month - 1;
            int iRow = TimeManagement.StandardTimeFromDST(dateTSX).Day - 1;
            MonthCalendar.Rows[iRow].Cells[jCol].Selected = true;
            //Set minimum altitude field
            MinAltitudeBox.Value = (decimal)Properties.Settings.Default.MinimumAltitude;
            //Fill in Humason target plans
            XFiles xf = new XFiles();
            if (xf != null)
            {
                List<string> tgtList = xf.GetTargetFiles();
                foreach (string tgt in tgtList)
                    if (!(tgt.Contains("Default")))
                        DropDownTargetList.Items.Add(tgt);
            }
            //Set selected item to current Humason target, if any
            string? currentTarget = xf.CurrentHumasonTarget;
            if (currentTarget != null)
            {
                for (int i = 0; i < DropDownTargetList.Items.Count; i++)
                    if (DropDownTargetList.Items[i].ToString().Contains(currentTarget))
                        DropDownTargetList.SelectedIndex = i;
            }
            else if (DropDownTargetList.Items.Count > 0)
                DropDownTargetList.SelectedItem = DropDownTargetList.Items[0];

            QPUpdate.TargetChangeEventHandler += WazzupEvent_Handler;
            isInInit = false;
            return;
        }

        #region Button Handlers

        private void CreateButton_Click(Object sender, EventArgs e)  // Handles CreateButton.Click
        {
            ButtonRed(AssessButton);
            //Check contents of target box.  If empty then load whatever is in the Find window in TSX
            if (TargetNameBox.Text == "")
                AcquireTSXTargetName();
            enteringTargetState = false;
            RegenerateForms(RefreshEvent.RefreshType.Target);
            DataGridViewSelectedCellCollection selcel = MonthCalendar.SelectedCells;
            int iRow = selcel[0].RowIndex;
            int iCol = selcel[0].ColumnIndex;
            MonthCalendar.Rows[iRow].Cells[iCol].Selected = false;
            MonthCalendar.Rows[iRow].Cells[iCol].Selected = true;
            ButtonGreen(AssessButton);
            return;
        }

        private void TrackButton_Click(object sender, EventArgs e)
        {
            //Opens a target track form -- and force it if (one is not already open
            OpenTrack();
            return;
        }

        private void DetailsButton_Click(Object sender, EventArgs e)  // Handles DetailsButton.Click
        {
            //Opens a target details form -- and force it if (one is not already open
            OpenDetail();
            return;
        }

        private void AltitudeButton_Click(Object sender, EventArgs e)  // Handles AltitudeButton.Click
        {
            //Opens a target details form -- and force it if (one is not already open
            //if (ButtonIsGreen(AltitudeButton))
            //{
            //ButtonRed(AltitudeButton);
            DataGridViewSelectedCellCollection selcells = MonthCalendar.SelectedCells;
            DataGridViewCell cellpick = selcells[0];
            moonDataDescription = MonthCalendar.Rows[cellpick.RowIndex].Cells[cellpick.ColumnIndex].ToolTipText;
            OpenPath();
            return;
        }

        private void PreviewButton_Click(Object sender, EventArgs e)  // Handles PreviewButton.Click
        {
            //Opens a target details form -- and force it if (one is not already open
            OpenPreview();
            return;
        }

        private void ProspectButton_Click(object sender, EventArgs e)
        {
            //Opens prospect form
            OpenProspect();
            return;
        }

        private void ExoPlanetButton_Click(object sender, EventArgs e)
        {
            //Opens exoplanent form
            if (ExoPlanetProtected)
            {
                ExoPlanetProtected = false;
                exoPlanetForm.Close();
            }
            OpenExoPlanet();
            return;
        }

        private void AddTargetPlanButton_Click(Object sender, EventArgs e)  // Handles AddTargetPlanButton.Click
        {
            ButtonRed(AddTargetPlanButton);
            //Save a new Humason configuration file with this target name, RA and Dec and about nothing else

            string tgtName = TargetNameBox.Text;

            sky6StarChart tsxs = new sky6StarChart();
            sky6ObjectInformation tsxo = new sky6ObjectInformation();
            //if (the object is not found, just return 
            try
            {
                tsxs.Find(tgtName);
            }
            catch
            {
                ButtonGreen(AddTargetPlanButton);
                return;
            }
            int cnt = tsxo.Count;
            tsxo.Index = 0;
            tsxo.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
            double dRA = tsxo.ObjInfoPropOut;
            tsxo.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
            double dDec = tsxo.ObjInfoPropOut;
            XFiles xfn = new XFiles(tgtName);

            if (xfn != null)
            {
                xfn.ReplaceItem(XFiles.sbTargetNameName, tgtName);
                xfn.ReplaceItem(XFiles.sbTargetAdjustCheckedName, false);
                xfn.ReplaceItem(XFiles.sbTargetRAName, dRA);
                xfn.ReplaceItem(XFiles.sbTargetDecName, dDec);
                xfn.SavePlan(tgtName);
            }
            //clear current target list box and reload
            DropDownTargetList.Items.Clear();
            //Fill in Humason target plans
            XFiles xf = new XFiles();
            List<string> tgtList = xf.GetTargetFiles();
            foreach (string tgt in tgtList)
            {
                if (!(tgt.Contains("Default")))
                {
                    DropDownTargetList.Items.Add(tgt);
                }
            }
            foreach (string tgt in DropDownTargetList.Items)
            {
                if (tgt == tgtName)
                    DropDownTargetList.SelectedItem = tgt;
            }
            RegenerateForms(RefreshEvent.RefreshType.TargetList);
            ButtonGreen(AddTargetPlanButton);
            return;
        }

        private void DeleteTargetPlanButton_Click(Object sender, EventArgs e)  // Handles DeleteTargetPlanButton.Click
        {
            ButtonRed(DeleteTargetPlanButton);
            //Delete the Humason configuration file with this target name
            XFiles xfn = new XFiles();
            string tgtName = DropDownTargetList.SelectedItem.ToString();
            xfn.DeletePlan(tgtName);
            //clear current target list box and reload
            DropDownTargetList.Items.Clear(); ;
            //Fill in Humason target plans
            XFiles xf = new XFiles();
            List<string> tgtList = xf.GetTargetFiles();
            foreach (string tgt in tgtList)
            {
                if (!(tgt.Contains("Default")))
                {
                    DropDownTargetList.Items.Add(tgt);
                }
            }
            if (DropDownTargetList.Items.Count > 0)
            {
                DropDownTargetList.SelectedItem = DropDownTargetList.Items[0];
            }
            RegenerateForms(RefreshEvent.RefreshType.TargetList);
            ButtonGreen(DeleteTargetPlanButton);
            return;
        }

        private void CurrentTargetListButton_Click(object sender, EventArgs e)
        {
            ButtonRed(CurrentTargetListButton);
            CurrentTargetListButton.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            Show();
            OpenTargetList();
            CurrentTargetListButton.Enabled = true;
            ButtonGreen(CurrentTargetListButton);
            Show();
            System.Windows.Forms.Application.DoEvents();
        }

        private void PrintButton_Click(Object sender, EventArgs e)  // Handles PrintButton.Click
        {
            ButtonRed(PrintButton);
            CaptureScreen();
            printCalendar.DefaultPageSettings.Landscape = true;
            printCalendar.Print();
            ButtonGreen(PrintButton);
            return;
        }

        private void DoneButton_Click(Object sender, EventArgs e)  // Handles DoneButton.Click
        {
            //Terminate application -- will leave TSX running
            Close();
            return;
        }

        private void InfoButton_Click(Object sender, EventArgs e)  // Handles InfoButton.Click
        {
            string hContent;
            try
            {
                hContent = "TSXToolkit: Image Planner";
                hContent += "\r\nV " + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                hContent += "\r\nRick McAlister, 2018";
            }
            catch
            {
                hContent = "Image Forecast in Debug";
            }
            MessageBox.Show(hContent, "Info", MessageBoxButtons.OK);
            return;
        }

        private void ExportListButton_Click(object sender, EventArgs e)
        {
            //Export target list to a text file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Target List Save File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                StreamWriter sw = File.CreateText(fileName);
                foreach (string tgt in DropDownTargetList.Items)
                {
                    sw.WriteLine(tgt);
                }
                sw.Close();
            }
        }

        private void TargetGroupButton_Click(object sender, EventArgs e)
        {
            //Loads a saved set of targets into Humason/Image Planner
            //after saving the current set, if any, to a current or new folder
            //inside of the Humason folder
            ButtonRed(TargetGroupButton);
            bool overWrite = true;
            string DocumentsDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string HumasonDirectoryPath = DocumentsDirectoryPath + "\\" + XFiles.HumasonFolderName;
            TargetSubfolderDialog.SelectedPath = HumasonDirectoryPath;
            DialogResult dResult = MessageBox.Show("Do you want to save the current target definitions?", "Swap Target Sets", MessageBoxButtons.YesNoCancel);
            switch (dResult)
            {
                case DialogResult.Cancel:
                    ButtonGreen(TargetGroupButton);
                    return;
                    break;
                case DialogResult.Yes:
                    TargetSubfolderDialog.Description = "Choose an existing or Create a new Group Folder";
                    DialogResult dr = TargetSubfolderDialog.ShowDialog();
                    string storeDir = TargetSubfolderDialog.SelectedPath;
                    List<string> currentTargetPaths = Directory.GetFiles(HumasonDirectoryPath).Where(x => x.Contains("TargetPlan")).ToList();
                    List<string> storeTargetPaths = Directory.GetFiles(storeDir).Where(x => x.Contains("TargetPlan")).ToList();
                    //Clear destination directory and move Humason target files there
                    foreach (string f in storeTargetPaths)
                        File.Delete(f);
                    foreach (string f in currentTargetPaths)
                        File.Move(f, storeDir + "\\" + Path.GetFileName(f));
                    //Get directory from which to load targets, then load them into the Humason directory
                    TargetSubfolderDialog.Description = "Choose an existing Group Folder from which to load";
                    dr = TargetSubfolderDialog.ShowDialog();
                    storeDir = TargetSubfolderDialog.SelectedPath;
                    List<string> newTargetPaths = Directory.GetFiles(storeDir).Where(x => x.Contains("TargetPlan")).ToList();
                    foreach (string f in newTargetPaths)
                        File.Copy(f, HumasonDirectoryPath + "\\" + Path.GetFileName(f), overWrite);
                    break;
                case DialogResult.No:
                    //Get directory from which to load targets, then load them into the Humason directory
                    TargetSubfolderDialog.Description = "Choose an existing Group Folder from which to load";
                    dr = TargetSubfolderDialog.ShowDialog();
                    storeDir = TargetSubfolderDialog.SelectedPath;
                    currentTargetPaths = Directory.GetFiles(HumasonDirectoryPath).Where(x => x.Contains("TargetPlan")).ToList();
                    foreach (string f in currentTargetPaths)
                        File.Delete(f);
                    newTargetPaths = Directory.GetFiles(storeDir).Where(x => x.Contains("TargetPlan")).ToList();
                    foreach (string f in newTargetPaths)
                        File.Copy(f, HumasonDirectoryPath + "\\" + Path.GetFileName(f), overWrite);
                    break;
            }
            //clear current target list box and reload
            DropDownTargetList.Items.Clear();
            //Fill in Humason target plans
            XFiles xf = new XFiles();
            List<string> tgtList = xf.GetTargetFiles();
            foreach (string tgt in tgtList)
            {
                if (!(tgt.Contains("Default")))
                {
                    DropDownTargetList.Items.Add(tgt);
                }
            }

            foreach (string tgt in DropDownTargetList.Items)
            {
                if (tgt == xf.CurrentHumasonTarget)
                    DropDownTargetList.SelectedItem = tgt;
            }
            RegenerateForms(RefreshEvent.RefreshType.TargetList);
            ButtonGreen(TargetGroupButton);
        }

        #endregion

        #region Input Handlers

        private void CurrentYearPick_ValueChanged(object sender, EventArgs e)
        {
            if (!SelectionEnabled)
                return;
            //if the current selected date is feb 29 (column 2, row 29) and the set year is not a leap year,
            //  then move the selected day up one row before entering the month-calendar selection changed method
            DateTime newDate;
            if (SelectedDate.Month == 2 && SelectedDate.Day == 29 && !DateTime.IsLeapYear((int)CurrentYearPick.Value))
                MonthCalendar.CurrentCell = MonthCalendar[MonthCalendar.CurrentCell.ColumnIndex, MonthCalendar.CurrentCell.RowIndex - 1];
            //TSX star chart can only be set using Julian Dates, so the best thing to do
            //  is use a differential in local days (from prior to new) and add that to the TSX julian day setting
            MonthCalendar_SelectionChanged(sender, e);
        }

        private void MonthCalendar_SelectionChanged(Object sender, EventArgs e)  // Handles MonthCalendar.SelectionChanged
        {
            if (!SelectionEnabled)
            {
                MonthCalendar.ClearSelection();
            }
            //Write to title line
            WriteTitle(TargetNameBox.Text, CurrentYearPick.Value.ToString());
            //if multiple cells selected, then just return (but this shouldn't happen
            DataGridViewSelectedCellCollection selcells = MonthCalendar.SelectedCells;
            if (selcells.Count == 0) return;
            //calculate selected date from year and cell position for day and month
            DataGridViewCell cellpick = selcells[0];
            try
            {
                SelectedDate = new DateTime((int)CurrentYearPick.Value, cellpick.ColumnIndex + 1, cellpick.RowIndex + 1);
            }
            catch
            {
                return;
            }
            //Set TSX to this newly selected date/year
            TimeManagement.LocalTSXDateTime = SelectedDate + SelectedTimeOfDay;
            //Find dailyposition index for (this date
            for (int idx = 0; idx < tgtdata.Length; idx++)
            {
                DateTime selectDay = TimeManagement.UTCToLocalTime(tgtdata[idx].UTCdate);
                if (SelectedDate.Year == selectDay.Year &&
                SelectedDate.Month == selectDay.Month &&
                SelectedDate.Day == selectDay.Day)
                {
                    TargetUTCDate = tgtdata[idx].UTCdate;
                    TargetIndex = idx;
                    break;
                }
            }
            int iRow = cellpick.ColumnIndex;
            moonDataDescription = MonthCalendar.Rows[cellpick.RowIndex].Cells[cellpick.ColumnIndex].ToolTipText;
            RegenerateForms(RefreshEvent.RefreshType.Date);
            return;
        }

        private void TargetNameBox_TextChanged(Object sender, KeyPressEventArgs e)  // Handles TargetNameBox.KeyPress
        {
            if (e.KeyChar == '\r')
            {
                enteringTargetState = false;
                //Set selected item to current Humason target, if any
                XFiles xf = new XFiles();
                xf.CurrentHumasonTarget = TargetNameBox.Text;
                RegenerateForms(RefreshEvent.RefreshType.Target);
            }
        }

        private void TargetNameBox_TextChanged(object sender, EventArgs e)
        {
            enteringTargetState = false;
            //Set selected item to current Humason target, if any
            XFiles xf = new XFiles();
            xf.CurrentHumasonTarget = TargetNameBox.Text;
            //RegenerateForms(RefreshEvent.RefreshType.Target);
        }



        private void DropDownTargetList_SelectedIndexChanged(Object sender, EventArgs e)  // Handles ImagePlannerTargetList.SelectedIndexChanged
        {
            //Loads the new target from the nh target list
            string tName = DropDownTargetList.SelectedItem.ToString();
            TargetNameBox.Text = tName;
            RegenerateForms(RefreshEvent.RefreshType.Target);
            return;
        }

        private void MinAltitudeBox_ValueChanged(Object sender, EventArgs e) //Handles MinAltitudeBox.KeyPress
        {
            Properties.Settings.Default.MinimumAltitude = (double)MinAltitudeBox.Value;
            Properties.Settings.Default.Save();
            RegenerateForms(RefreshEvent.RefreshType.Target);
            return;
        }

        #endregion

        #region Cell Methods

        public void WriteCell(int iRow, int jCol, string cText)
        {
            MonthCalendar.Rows[iRow].Cells[jCol].Value = cText;
            return;
        }

        public void PaintCell(int iRow, int jCol, Color background, Color foreground)
        {
            MonthCalendar.Rows[iRow].Cells[jCol].Style.BackColor = background;
            MonthCalendar.Rows[iRow].Cells[jCol].Style.ForeColor = foreground;
            return;
        }

        public void ClearLeapDay()
        {
            MonthCalendar.Rows[28].Cells[1].Value = "";
            MonthCalendar.Rows[28].Cells[1].Style.BackColor = Color.WhiteSmoke;
            return;
        }

        //Write Form Title
        public void WriteTitle(string tgtName, string tYear)
        {
            //Write title line in header of form
            // Acquire the version information and put it in the form header
            try { this.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(); }
            catch { this.Text = " in Debug"; } //probably in debug, no version info available
            this.Text = "Image Planner " + this.Text + ":  ";
            this.Text += tYear + " Conditions Forecast for " + tgtName + " at " + TimeManagement.LocateTSX() +
                "     ( @ Imaging Start Time > Imaging Duration (in hours) with Moon Phase " +
                "and constrained by Astronomical Twilight)";
            //update tooltip for (target name
            FillInTargetDetails(tgtName);
            return;
        }

        //Write detail data for (individual cells as tool tips for (cursor hover
        public void WriteToolTip(int iRow, int jCol, DailyPosition dpt)
        {
            string tiptext = "Start Imaging: " + TimeManagement.UTCToLocalTime(dpt.Rising).ToString("t") + "\r\n" +
                "End Imaging: " + TimeManagement.UTCToLocalTime(dpt.Setting).ToString("t") + "\r\n" +
                "Moon Up: " + ((1 - dpt.MoonFree) * 100).ToString("0") + "%" + "\r\n" +
                "Moon Phase: " + (dpt.MoonPhase * 100).ToString("0") + "%" + "\r\n";
            MonthCalendar.Rows[iRow].Cells[jCol].ToolTipText = tiptext;
            return;
        }

        public void WriteMoonTip(DailyPosition[] moondata)
        {
            string tiptext = null;
            int jCol;
            int iRow;
            foreach (DailyPosition dp in moondata)
            {
                if (TimeManagement.UTCToLocalTime(dp.UTCdate).Year == CurrentYearPick.Value)
                {
                    switch (dp.Visibility)
                    {
                        case (DailyPosition.VisibilityState.UpSome):
                            tiptext = "Moonrise at " + TimeManagement.UTCToLocalTime(dp.Rising).ToString("t") + "\r\n" +
                        "Moonset at " + TimeManagement.UTCToLocalTime(dp.Setting).ToString("t") + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.UpAlways):
                            tiptext = "Moonrise before imaging" + "\r\n" +
                                "Moonset after imaging";
                            break;
                        case (DailyPosition.VisibilityState.Rises):
                            tiptext = "Moonrise at " + TimeManagement.UTCToLocalTime(dp.Rising).ToString("t") + "\r\n" +
                        "Moonset after imaging" + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.Falls):
                            tiptext = "Moonrise before imaging" + "\r\n" +
                        "Moonset at " + TimeManagement.UTCToLocalTime(dp.Setting).ToString("t") + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.DownSome):
                            tiptext = "Moonset at " + TimeManagement.UTCToLocalTime(dp.Rising).ToString("t") + "\r\n" +
                        "Moon rises at " + TimeManagement.UTCToLocalTime(dp.Setting).ToString("t") + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.UpNever):
                            tiptext = "No Moon during imaging" + "\r\n";
                            break;
                    }
                    jCol = TimeManagement.UTCToLocalTime(dp.Rising).Month - 1;
                    iRow = TimeManagement.UTCToLocalTime(dp.Rising).Day - 1;
                    MonthCalendar.Rows[iRow].Cells[jCol].ToolTipText += tiptext;
                }
            }
            return;
        }


        #endregion

        #region Calendar Methods
        //Calendar generation methods

        private void GenerateCalendar()
        {
            sky6StarChart tdoc = new sky6StarChart();
            sky6Utils tute = new sky6Utils();
            sky6ObjectInformation tobj = new sky6ObjectInformation();

            double traH;      //target RA in hours
            double tdecD;     //target Dec in degrees
            double tlatD;     //Observer Latitude in degrees
            double tlongD;     //Observer Longitude in degrees

            SelectedTimeOfDay = (TimeSpan)TimeManagement.CurrentTSXTimeOfDay;

            try
            {
                tdoc.Find(TargetNameBox.Text);
            }
            catch (Exception ex)
            {
                //! found
                System.Windows.Forms.MessageBox.Show("Target not Found: " + TargetNameBox.Text + " " + ex.Message);
                TargetNameBox.Text = "";
                return;
            }
            //Reset the target name to whatever TSX found
            tobj.Index = 0;
            //tobj.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
            //TargetNameBox.Text = tobj.ObjInfoPropOut;
            enteringTargetState = false;
            //TargetNameBox.Text = TargetNameBox.Text.Replace(" ", "");

            int vj = tobj.Count;
            tobj.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
            traH = tobj.ObjInfoPropOut;
            tobj.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
            tdecD = tobj.ObjInfoPropOut;
            tdoc.DocumentProperty(TheSky64Lib.Sk6DocumentProperty.sk6DocProp_Latitude);
            tlatD = tdoc.DocPropOut;
            tdoc.DocumentProperty(TheSky64Lib.Sk6DocumentProperty.sk6DocProp_Longitude);
            tlongD = tdoc.DocPropOut;
            //tdoc.DocumentProperty(TheSky64Lib.Sk6DocumentProperty.sk6DocProp_Time_Zone);

            Celestial.RADec tgtRADec = new Celestial.RADec(Transform.HoursToRadians(traH), Transform.DegreesToRadians(tdecD));
            Celestial.LatLon obsLocation = new Celestial.LatLon(Transform.DegreesToRadians(tlatD), Transform.DegreesToRadians(-tlongD));

            sundata = TargetControl.SunCycle((int)CurrentYearPick.Value, obsLocation);
            tgtdata = TargetControl.TargetCycle(tgtRADec, sundata, obsLocation, (double)MinAltitudeBox.Value);
            tgtdata = TargetControl.MoonPhase(tgtdata);

            moondata = TargetControl.MoonCycle(tgtdata, obsLocation);
            //Update the target positions with the moonfree properties
            tgtdata = TargetControl.MoonClear(tgtdata, moondata);

            WriteTitle(TargetNameBox.Text, CurrentYearPick.Value.ToString());
            SpawnCalendar(tgtdata);
            if ((CurrentYearPick.Value % 4) != 0)
            {
                ClearLeapDay();
            }
            WriteMoonTip(moondata);

            TimeManagement.CurrentTSXTimeOfDay = SelectedTimeOfDay;

            Show();
            System.Windows.Forms.Application.DoEvents();
            return;
        }

        public void SpawnCalendar(DailyPosition[] tgtdata)
        {
            int iRow;
            int jCol;
            string imgTime;
            string imgStart;
            string pIcon;
            string cellText = "";

            Color MedYellow = Color.FromArgb(255, 255, 255, 102);
            Color PaleYellow = Color.FromArgb(255, 255, 255, 153);
            Color PaleYellowGreen = Color.FromArgb(255, 204, 255, 85);
            Color MedYellowGreen = Color.FromArgb(255, 153, 255, 153);
            Color MedBlueGreen = Color.FromArgb(255, 51, 153, 153);
            Color DeepBlueGreen = Color.FromArgb(255, 51, 0, 253);
            Color VeryLightPink = Color.FromArgb(255, 255, 204, 255);

            TimeManagement.CurrentTSXTimeOfDay = SelectedTimeOfDay;

            foreach (DailyPosition dp in tgtdata)
            {
                if (dp.UTCdate.Year == CurrentYearPick.Value)
                {
                    //jCol = dp.Rising.Month - 1;
                    //iRow = dp.Rising.Day - 1;
                    DateTime cellDay = TimeManagement.StandardTimeToDST(dp.UTCdate);
                    DateTime sessionDay = TimeManagement.DateToSessionDate(cellDay);
                    jCol = sessionDay.Month - 1;
                    iRow = sessionDay.Day - 1;
                    if (dp.MoonFree == 0)
                    {
                        PaintCell(iRow, jCol, MedYellow, Color.Black);
                    }
                    else if (dp.MoonFree < 0.25)
                    {
                        PaintCell(iRow, jCol, PaleYellow, Color.Black);
                    }
                    else if (dp.MoonFree < 0.5)
                    {
                        PaintCell(iRow, jCol, PaleYellowGreen, Color.Black);
                    }
                    else if (dp.MoonFree < 0.75)
                    {
                        PaintCell(iRow, jCol, MedYellowGreen, Color.Black);
                    }
                    else if (dp.MoonFree < 1)
                    {
                        PaintCell(iRow, jCol, MedBlueGreen, Color.White);
                    }
                    else
                    {
                        PaintCell(iRow, jCol, DeepBlueGreen, Color.White);
                    }
                    if (dp.MoonPhase <= 0.05)
                    {
                        pIcon = "    O    ";
                    }
                    else if (dp.MoonPhase < 0.25)
                    {
                        pIcon = "   (O)   ";
                    }
                    else if (dp.MoonPhase < 0.5)
                    {
                        pIcon = "  ((O))  ";
                    }
                    else if (dp.MoonPhase < 0.75)
                    {
                        pIcon = " (((O))) ";
                    }
                    else
                    {
                        pIcon = "((((O))))";
                    }

                    switch (dp.Visibility)
                    {
                        case (DailyPosition.VisibilityState.UpNever):
                            cellText = "Too Low  " + pIcon;
                            PaintCell(iRow, jCol, VeryLightPink, Color.Black);
                            break;
                        case (DailyPosition.VisibilityState.UpAlways):
                            imgStart = TimeManagement.UTCToLocalTime(dp.Rising).ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.UpSome):
                            imgStart = TimeManagement.UTCToLocalTime(dp.Rising).ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.Rises):
                            imgStart = TimeManagement.UTCToLocalTime(dp.Rising).ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.Falls):
                            imgStart = TimeManagement.UTCToLocalTime(dp.Rising).ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.DownSome):
                            //Note that imaging time is split with this state, and the longest interval has been preselected
                            imgStart = TimeManagement.UTCToLocalTime(dp.Rising).ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "*" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                    }

                    WriteCell(iRow, jCol, cellText);
                    WriteToolTip(iRow, jCol, dp);
                }
            }
            TimeManagement.CurrentTSXTimeOfDay = SelectedTimeOfDay;
            return;
        }

        #endregion

        #region Print Methods
        //Print functions
        //private System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();

        Bitmap memoryImage;

        private Graphics CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            // memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
            return memoryGraphics;
        }

        #endregion

        #region PopUp Forms

        private void OpenPreview()
        {
            //Opens the target preview form and window
            if (isInInit)
                return;
            //First, close the old one, if (any
            if (this.previewForm != null)
            {
                this.previewFormLocation = this.previewForm.Location;
                this.previewForm.Close();
            }

            this.previewForm = new FormPreview(this.TargetNameBox.Text);
            //Locate the start position of this form at the lower right hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int pvSizeW = this.previewForm.Size.Width;
            int pvSizeH = this.previewForm.Size.Height;
            Point pvLoc = new Point((twPosX + twSizeW - pvSizeW), (twposY + twSizeH - pvSizeH));
            if (this.previewFormLocation != new Point(0, 0))
            {
                this.previewForm.Location = this.previewFormLocation;
            }
            else
            {
                this.previewForm.Location = pvLoc;
            }
            this.previewForm.StartPosition = FormStartPosition.Manual;
            //set this form as the owner
            previewForm.Owner = this;
            //show the form
            this.previewForm.Show();
            return;
        }

        private void OpenPath()
        {
            //Opens the target preview form and window
            if (isInInit)
                return;
            //First, close the old one, if (any
            if (this.pathForm != null)
            {
                this.pathFormLocation = this.pathForm.Location;
                this.pathForm.Close();
            }

            this.pathForm = new FormTargetAltitude(tgtdata[TargetIndex], moondata[TargetIndex], this.TargetNameBox.Text, moonDataDescription);

            //Locate the start position of this form at the lower left hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int pvSizeW = this.pathForm.Size.Width;
            int pvSizeH = this.pathForm.Size.Height;
            Point pvLoc = new Point(twPosX, (twposY + twSizeH - pvSizeH));
            if (this.pathFormLocation != new Point(0, 0))
            {
                this.pathForm.Location = this.pathFormLocation;
            }
            else
            {
                this.pathForm.Location = pvLoc;
            }
            this.pathForm.StartPosition = FormStartPosition.Manual;
            //set this form as the owner
            pathForm.Owner = this;
            //show the form
            this.pathForm.Show();
            return;
        }

        private void OpenDetail()
        {
            //Opens the detail information for (the current target
            //if (forced is false, { the form is not opened if (not already open

            if (isInInit)
                return;
            //First, close the old one, if (any
            if (this.detailForm != null)
            {
                this.detailFormLocation = this.detailForm.Location;
                this.detailForm.Close();
            }

            //Open the details form
            this.detailForm = new FormDetails(this.TargetNameBox.Text);
            //Locate the start position of this form at the lower right hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int dfSizeW = this.detailForm.Size.Width;
            int dfSizeH = this.detailForm.Size.Height;
            Point dfLoc = new Point((twPosX + twSizeW - dfSizeW), (twposY + (dfSizeH / 2)));
            if (this.detailFormLocation != new Point(0, 0))
            {
                this.detailForm.Location = this.detailFormLocation;
            }
            else
            {
                this.detailForm.Location = dfLoc;
            }
            this.detailForm.StartPosition = FormStartPosition.Manual;
            //set this form as the owner
            detailForm.Owner = this;
            this.detailForm.Show();
            return;
        }

        private void OpenProspect()
        {
            //Opens the target preview form and window
            if (isInInit)
                return;
            //Don't open a new pop up if the old one isn't ready to be closed
            if (ProspectProtected)
                return;

            //First, close the old one, if (any
            if (this.prospectForm != null)
            {
                this.prospectFormLocation = this.prospectForm.Location;
                this.prospectForm.Close();
            }

            DateTime dawnDateUTC = sundata[TargetIndex + 1].Rising;
            DateTime duskDateUTC = sundata[TargetIndex].Setting;

            this.prospectForm = new FormProspect(duskDateUTC, dawnDateUTC);
            //set this form as the owner
            prospectForm.Owner = this;
            //Locate the start position of this form at the lower left hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int pvSizeW = this.prospectForm.Size.Width;
            int pvSizeH = this.prospectForm.Size.Height;
            Point pvLoc = new Point(twPosX + ((twSizeW / 2) - (pvSizeW / 2)), (twposY + twSizeH - pvSizeH));
            if (this.prospectFormLocation != new Point(0, 0))
            {
                this.prospectForm.Location = this.prospectFormLocation;
            }
            else
            {
                this.prospectForm.Location = pvLoc;
            }
            this.prospectForm.StartPosition = FormStartPosition.Manual;
            //show the form
            this.prospectForm.Show();
            return;
        }

        private void OpenExoPlanet()
        {
            //Opens the exoplanet form and window
            if (isInInit)
                return;
            //Don't open a new pop up if the old one isn't ready to be closed
            if (ExoPlanetProtected)
                return;

            //First, close the old one, if (any
            if (this.exoPlanetForm != null)
            {
                this.exoPlanetFormLocation = this.exoPlanetForm.Location;
                this.exoPlanetForm.Close();
            }

            //Protect the exoplanet popup from being inadvertantly closed
            ExoPlanetProtected = true;

            DateTime dawnDateUTC = sundata[TargetIndex + 1].Rising;
            DateTime duskDateUTC = sundata[TargetIndex].Setting;

            this.exoPlanetForm = new FormExoPlanet(duskDateUTC, dawnDateUTC, (double)MinAltitudeBox.Value);
            //set this form as the owner
            exoPlanetForm.Owner = this;
            //Locate the start position of this form at the lower left hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int pvSizeW = this.exoPlanetForm.Size.Width;
            int pvSizeH = this.exoPlanetForm.Size.Height;
            Point pvLoc = new Point(twPosX + ((twSizeW / 2) - (pvSizeW / 2)), (twposY + twSizeH - pvSizeH));
            if (this.exoPlanetFormLocation != new Point(0, 0))
            {
                this.exoPlanetForm.Location = this.exoPlanetFormLocation;
            }
            else
            {
                this.exoPlanetForm.Location = pvLoc;
            }
            this.exoPlanetForm.StartPosition = FormStartPosition.Manual;
            //show the form
            this.exoPlanetForm.Show();
            return;
        }

        private void OpenTrack()
        {
            if (isInInit)
                return;
            //First, close the old one, if (any
            if (this.trackForm != null)
            {
                this.trackFormLocation = this.trackForm.Location;
                this.trackForm.Close();
            }

            this.trackForm = new FormTargetTrack(tgtdata[TargetIndex], moondata[TargetIndex], this.TargetNameBox.Text);
            //set this form as the owner
            trackForm.Owner = this;
            //Locate the start position of this form at the lower left hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int pvSizeW = this.trackForm.Size.Width;
            int pvSizeH = this.trackForm.Size.Height;
            //Point pvLoc = new Point(twPosX + ((twSizeW / 2) - (pvSizeW / 2)), (twposY + twSizeH - pvSizeH));
            Point pvLoc = new Point(twPosX, twposY + 100);
            if (this.trackFormLocation != new Point(0, 0))
            {
                this.trackForm.Location = this.trackFormLocation;
            }
            else
            {
                this.trackForm.Location = pvLoc;
            }
            this.trackForm.StartPosition = FormStartPosition.Manual;
            //show the form
            this.trackForm.Show();
            trackForm.ShowTrack();
        }

        private void OpenTargetList()
        {
            if (isInInit)
                return;
            //First, close the old one, if (any
            if (this.tgtListForm != null)
            {
                tgtListFormLocation = this.tgtListForm.Location;
                this.tgtListForm.Close();
                this.tgtListForm.Dispose();
            }
            tgtListForm = new FormTargetList(TargetNameBox.Text);
            tgtListForm.Owner = this;
            //Locate the start position of this form at the lower left hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int pvSizeW = this.tgtListForm.Size.Width;
            int pvSizeH = this.tgtListForm.Size.Height;
            Point pvLoc = new Point(twPosX, twposY + 100);
            if (this.tgtListFormLocation != new Point(0, 0))
            {
                this.tgtListForm.Location = this.tgtListFormLocation;
            }
            else
            {
                tgtListForm.Location = pvLoc;
            }
            this.tgtListForm.StartPosition = FormStartPosition.Manual;
            this.tgtListForm.Show();
            tgtListForm.WriteTargetList();
        }

        private void RegenerateForms(RefreshEvent.RefreshType rType)
        {
            //Common method for (rebuilding all the forms with new target, etc
            WriteTitle(TargetNameBox.Text, CurrentYearPick.Value.ToString());
            GenerateCalendar();
            if (detailForm != null && IsFormOpen(detailForm.Name))
                OpenDetail();
            if (previewForm != null && IsFormOpen(previewForm.Name))
                OpenPreview();
            if (pathForm != null && IsFormOpen(pathForm.Name))
                OpenPath();
            if (trackForm != null && IsFormOpen(trackForm.Name))
                OpenTrack();
            if (prospectForm != null && IsFormOpen(prospectForm.Name) && rType == RefreshEvent.RefreshType.Date)
                OpenProspect();
            if (tgtListForm != null && IsFormOpen(tgtListForm.Name))
                if (rType != RefreshEvent.RefreshType.Target)
                    OpenTargetList();
                else
                    DateChangeNotification(rType, SelectedDate);
            return;
        }

        #endregion

        #region Miscellaneous Helper Methods

        public void InitializeCurrentTarget()
        {
            //Set the target box text to first non null targat name from
            // 1) name in humason configuration file
            // 2) name in TSX find
            // 23 first in list of target objects in humason folder
            // 4) Messier M1
            //in that order
            string? tgtName;
            XFiles xf = new XFiles();
            tgtName = xf.CurrentHumasonTarget;
            if (tgtName == null)
                tgtName = AcquireTSXTargetName();
            if (tgtName == null)
                tgtName = xf.GetTargetFiles().Where(x => !x.Contains("Default")).FirstOrDefault();
            if (tgtName == null)
                tgtName = "M1";
            TargetNameBox.Text = tgtName;
        }

        private string IncrementCatalogNumber(string targetName, int increment)
        {
            //assumes an input string of a catelog object:  ccccnnnn where cccc are characters and nnnn are numbers
            //this function parses the string and return s a new string whre the number as been incremented
            char[] s = new char[] { ' ' };
            string[] parts = targetName.Split(s, StringSplitOptions.RemoveEmptyEntries);
            int nextDigit = (Convert.ToInt32(parts[1]) + increment);
            if (nextDigit > 1) return parts[0] + " " + nextDigit.ToString("0");
            else return parts[0] + " 1";
        }

        private void FillInTargetDetails(string tName)
        {
            //Retrieves details from TSX about the target object
            sky6StarChart tsxs = new sky6StarChart();
            sky6ObjectInformation tsxo = new sky6ObjectInformation();
            try
            {
                tsxs.Find(tName);
            }
            catch
            {
                return;
            }

            char[] illegalChars = { ' ', '^', '~', '#', '\'' };
            char[] trimChars = { ' ', '_', '^' };

            tsxo.Index = 0;
            tsxo.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_ALL_INFO);
            string sAllInfo = tsxo.ObjInfoPropOut;
            sAllInfo = sAllInfo.Replace("/", "-");
            string[] sInfoDB = sAllInfo.Split('\n');
            XElement infoX = new XElement("All_Properties");
            foreach (string ipair in sInfoDB)
            {
                string[] infoPair = ipair.Split(':');
                infoPair[0] = infoPair[0].Replace(" ", "_");
                string[] firstSpace = infoPair[0].Split('(');
                if (firstSpace[0] != "")
                {
                    if (!Utility.HasSpecialCharacters(firstSpace[0], illegalChars))
                    {
                        string xName = firstSpace[0].Trim(trimChars);
                        string xData = infoPair[1].Trim(' ');
                        infoX.Add(new XElement(xName, xData));
                    }
                }
            }

            //Get rid of multiple constellations.  Got to do it twice for (some reason
            foreach (XElement xmv in infoX.Elements("Constellation"))
            {
                if (xmv.Value.Length < 4)
                {
                    xmv.Remove();
                }
            }
            foreach (XElement xmv in infoX.Elements("Constellation"))
            {
                if (xmv.Value.Length < 4)
                {
                    xmv.Remove();
                }
            }
            //Get rid of the first RA (that//s the current, not J2000)
            XElement xra = infoX.Element("RA");
            xra.Remove();
            XElement xdec = infoX.Element("Dec");
            xdec.Remove();
            //read out interesting data
            string details = "";
            details += "Object:        " + EntryCheck(infoX, "Object_Name");
            details += "Catalog Id:    " + EntryCheck(infoX, "Catalog_Identifier");
            details += "Object Type:   " + EntryCheck(infoX, "Object_Type");
            details += "Constellation: " + EntryCheck(infoX, "Constellation");
            details += "Magnitude:     " + EntryCheck(infoX, "Magnitude");
            details += "Major Axis:    " + EntryCheck(infoX, "Major_Axis");
            details += "Minor Axis:    " + EntryCheck(infoX, "Minor_Axis");
            details += "Axis PA:       " + EntryCheck(infoX, "Axis_Position_Angle");
            details += "RA (J2000):    " + EntryCheck(infoX, "RA");
            details += "Dec (J2000):   " + EntryCheck(infoX, "Dec");

            TargetDetailsTip.SetToolTip(TargetNameBox, details);
            return;
        }

        private string EntryCheck(XElement xem, string name)
        {
            //reads and checks xelement entry { return s either the contents or N/A, with "\r\n" appended
            if (xem.Element(name) == null)
            {
                return "\t" + "N/A" + "\r\n";
            }
            else
            {
                return "\t" + xem.Element(name).Value + "\r\n";
            }
        }

        private string? AcquireTSXTargetName()
        {
            sky6ObjectInformation tsxo = new sky6ObjectInformation();
            int cnt = tsxo.Count;
            tsxo.Index = 0;
            tsxo.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
            string tName = tsxo.ObjInfoPropOut;
            TargetNameBox.Text = tName;
            if (tName == "")
                return null;
            else
                return tName;
        }

        #endregion

        #region Event Subscriptions

        private void WazzupEvent_Handler(object sender, TargetChangeEvent.TargetChangeEventArgs e)
        {
            ProspectProtected = true;
            this.TargetNameBox.Text = e.TargetName;
            enteringTargetState = false;
            //TargetNameBox.Text = TargetNameBox.Text.Replace(" ", "");
            RegenerateForms(RefreshEvent.RefreshType.Target);
            //DataGridViewSelectedCellCollection selcel = MonthCalendar.SelectedCells;
            //int iRow = selcel[0].RowIndex;
            //int iCol = selcel[0].ColumnIndex;
            //MonthCalendar.Rows[iRow].Cells[iCol].Selected = false;
            //MonthCalendar.Rows[iRow].Cells[iCol].Selected = true;
            //this.Show();
            System.Windows.Forms.Application.DoEvents();
            ProspectProtected = false;
            return;
        }

        private void PrintCalendar_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0, 1000, 750);

        }

        #endregion

        #region Event Publishing

        private void DateChangeNotification(RefreshEvent.RefreshType rType, DateTime newDate)
        {
            //Generates event for subscribing forms to update their displays with new date data
            RefreshEvent ndEvent = FormTargetList.RefreshUpdateEvent;
            ndEvent.RefreshUpdate(rType, newDate);
        }


        #endregion

        #region Button Color

        public void ButtonRed(System.Windows.Forms.Button formObj)
        {
            //Turns button Red
            formObj.BackColor = Color.LightSalmon;
            formObj.ForeColor = Color.Black;
            return;
        }

        public void ButtonGreen(System.Windows.Forms.Button formObj)
        {
            //Turns button Green;
            formObj.BackColor = Color.LightSeaGreen;
            formObj.ForeColor = Color.White;
            return;
        }

        public bool ButtonIsGreen(System.Windows.Forms.Button formObj)
        {
            //return s true is button is green, false otherwise
            if (formObj.BackColor == Color.LightSeaGreen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsFormOpen(string fName)
        {
            FormCollection fc = System.Windows.Forms.Application.OpenForms;
            foreach (Form f in fc)
                if (f.Name == fName)
                    return true;
            return false;
        }




        #endregion

    }
}
