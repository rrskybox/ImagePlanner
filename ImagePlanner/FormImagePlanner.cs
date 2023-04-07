//Windows Visual Basic Forms Application: Image Planner
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
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Deployment.Application;
using TheSky64Lib;

namespace ImagePlanner
{
    public partial class FormImagePlanner : Form
    {
        public DateTime SelectedDate;
        public double MinimumAltitude = 0;

        public DailyPosition[] sundata;
        public DailyPosition[] tgtdata;
        public DailyPosition[] moondata;
        public int TargetIndex;
        public DateTime TargetUTCDate;
        public bool SelectionEnabled;
        public bool ProspectProtected = false;  //set to false if prospect pop up is not active, true otherwise -- used for selection
        public bool ExoPlanetProtected = false;  //set to false if exoplanet pop up is not active, true otherwise -- used for selection
        public string moonDataDescription;

        public FormPreview previewForm = null;
        public Point previewFormLocation = new Point(0, 0);
        public FormTargetPath pathForm = null;
        public Point pathFormLocation = new Point(0, 0);
        public FormDetails detailForm = null;
        public Point detailFormLocation = new Point(0, 0);
        public FormTargetTrack trackForm = null;
        public Point trackFormLocation = new Point(0, 0);
        public FormWazzup wazzupForm = null;
        public Point wazzupFormLocation = new Point(0, 0);
        public FormExoPlanet exoPlanetForm = null;
        public Point exoPlanetFormLocation = new Point(0, 0);


        public bool enteringTargetState;  //true if writing target in, false if target has been entered

        public static WazzupEvent QPUpdate = new WazzupEvent();

        public FormImagePlanner()
        {
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

            this.FontHeight = 1;
            MonthCalendar.RowCount = 31;
            for (int i = 0; i <= 30; i++)
            {
                MonthCalendar.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

            //Compute current dates based on TSX star chart julian date
            //  this allows star charts to be in different locations and time zones
            //  as set up by user
            sky6StarChart tsxsc = new sky6StarChart();
            //Get the star chart julian date and convert to current date/time
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow);
            DateTime dateTSXutc = AstroMath.Celestial.JulianToDate(tsxsc.DocPropOut);
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Time_Zone);
            double tzTSX = tsxsc.DocPropOut;
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_DaylightSavingTimeIndex);
            double tzIndexTSX = tsxsc.DocPropOut;
            DateTime dateTSX;
            if (tzIndexTSX == 0)
                dateTSX = Utilities.DateToSessionDate(dateTSXutc.AddHours(tzTSX));
            else
                dateTSX = Utilities.DateToSessionDate(dateTSXutc.AddHours(tzIndexTSX-24));
            CurrentYearPick.Value = dateTSX.Year;
            GenerateCalendar();
            Show();
            System.Windows.Forms.Application.DoEvents();
            SelectionEnabled = true;
            //Figure out the year and load it into the year box
            string thisyear = dateTSX.ToString("yyyy");
            CurrentYearPick.Value = Convert.ToInt16(thisyear);
            //Pick the current date as the selected cell
            SelectedDate = dateTSX;
            int jCol = dateTSX.Month - 1;
            int iRow = dateTSX.Day - 1;
            MonthCalendar.Rows[iRow].Cells[jCol].Selected = true;

            //Fill in Humason target plans
            XFiles xf = new XFiles();
            if (xf != null)
            {
                List<string> tgtList = xf.GetTargetFiles();
                foreach (string tgt in tgtList)
                {
                    if (!(tgt.Contains("Default")))
                    {
                        ImagePlannerTargetList.Items.Add(tgt);
                    }
                }
                if (ImagePlannerTargetList.Items.Count > 0)
                {
                    ImagePlannerTargetList.SelectedItem = ImagePlannerTargetList.Items[0];
                }
            }
            //Set selected item to current Humason target, if any
            string currentTarget = xf.GetCurrentHumasonTarget();
            if (currentTarget != "")
                for (int i = 0; i < ImagePlannerTargetList.Items.Count; i++)
                    if (ImagePlannerTargetList.Items[i].ToString().Contains(currentTarget))
                        ImagePlannerTargetList.SelectedIndex = i;

            QPUpdate.WazzupEventHandler += WazzupEvent_Handler;

            return;
        }

        #region Button Handlers

        private void CurrentYearPick_TextChanged(Object sender, KeyPressEventArgs e) //Handles CurrentYearPick.KeyPress
        {
            if (e.KeyChar == '\r')
            {
                WriteTitle(TargetNameBox.Text, CurrentYearPick.Value.ToString());
                RegenerateForms();
                return;
            }
            return;
        }

        private void MonthCalendar_SelectionChanged(Object sender, EventArgs e)  // Handles MonthCalendar.SelectionChanged
        {
            if (!SelectionEnabled)
            {
                MonthCalendar.ClearSelection();
            }
            DataGridViewSelectedCellCollection selcells = MonthCalendar.SelectedCells;
            if (selcells.Count == 0)
            { return; }

            DataGridViewCell cellpick = selcells[0];
            try
            {
                SelectedDate = new DateTime((int)CurrentYearPick.Value, cellpick.ColumnIndex + 1, cellpick.RowIndex + 1);
            }
            catch
            {
                return;
            }
            //Find dailyposition index for (this date
            for (int idx = 0; idx < tgtdata.Length; idx++)
            {
                DateTime selectDay = tgtdata[idx].UTCdate.ToLocalTime();
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
            //If the Path pop up is open then update it.
            OpenPath();
            //If the Track pop up is open then update i.
            OpenTrack();

            //If the prospect popup is open then just close it otherwise it's a whole 'nother wait to get it filled out.
            if (!ProspectProtected)
            {
                if (ButtonIsGreen(ProspectButton))
                {
                    return;
                }
                else
                {
                    wazzupForm.Close();
                    ButtonGreen(ProspectButton);
                }
            }
            return;
        }

        private void CreateButton_Click(Object sender, EventArgs e)  // Handles CreateButton.Click
        {
            ButtonRed(AssessButton);
            //TargetNameBox.Text = TargetNameBox.Text.Replace(" ", "");
            enteringTargetState = false;
            RegenerateForms();
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
            if (ButtonIsGreen(TrackButton))
            {
                ButtonRed(TrackButton);
                OpenTrack();
            }
            else
            {
                trackForm.Close();
                ButtonGreen(TrackButton);
            }
            return;
        }

        private void DetailsButton_Click(Object sender, EventArgs e)  // Handles DetailsButton.Click
        {
            //Opens a target details form -- and force it if (one is not already open
            if (ButtonIsGreen(DetailsButton))
            {
                ButtonRed(DetailsButton);
                OpenDetail();
            }
            else
            {
                detailForm.Close();
                ButtonGreen(DetailsButton);
            }
            return;
        }

        private void AltitudeButton_Click(Object sender, EventArgs e)  // Handles AltitudeButton.Click
        {
            //Opens a target details form -- and force it if (one is not already open
            if (ButtonIsGreen(AltitudeButton))
            {
                ButtonRed(AltitudeButton);
                DataGridViewSelectedCellCollection selcells = MonthCalendar.SelectedCells;
                DataGridViewCell cellpick = selcells[0];
                moonDataDescription = MonthCalendar.Rows[cellpick.RowIndex].Cells[cellpick.ColumnIndex].ToolTipText;
                OpenPath();
            }
            else
            {
                pathForm.Close();
                ButtonGreen(AltitudeButton);
            }
            return;
        }

        private void PreviewButton_Click(Object sender, EventArgs e)  // Handles PreviewButton.Click
        {
            //Opens a target details form -- and force it if (one is not already open
            if (ButtonIsGreen(PreviewButton))
            {
                ButtonRed(PreviewButton);
                OpenPreview();
            }
            else
            {
                previewForm.Close();
                ButtonGreen(PreviewButton);
            }
            return;
        }

        private void ProspectButton_Click(object sender, EventArgs e)
        {
            if (ButtonIsGreen(ProspectButton))
            {
                ButtonRed(ProspectButton);
                OpenProspect();
            }
            else
            {
                wazzupForm.Close();
                ButtonGreen(ProspectButton);
            }
            return;
        }

        private void ExoPlanetButton_Click(object sender, EventArgs e)
        {
            if (ButtonIsGreen(ExoPlanetButton))
            {
                ButtonRed(ExoPlanetButton);
                OpenExoPlanet();
            }
            else
            {
                exoPlanetForm.Close();
                ButtonGreen(ExoPlanetButton);
            }
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
            ImagePlannerTargetList.Items.Clear();
            //Fill in Humason target plans
            XFiles xf = new XFiles();
            List<string> tgtList = xf.GetTargetFiles();
            foreach (string tgt in tgtList)
            {
                if (!(tgt.Contains("Default")))
                {
                    ImagePlannerTargetList.Items.Add(tgt);
                }
            }
            foreach (string tgt in ImagePlannerTargetList.Items)
            {
                if (tgt == tgtName)
                {
                    ImagePlannerTargetList.SelectedItem = tgt;
                }
            }
            ButtonGreen(AddTargetPlanButton);
            return;
        }

        private void DeleteTargetPlanButton_Click(Object sender, EventArgs e)  // Handles DeleteTargetPlanButton.Click
        {
            ButtonRed(DeleteTargetPlanButton);
            //Delete the Humason configuration file with this target name
            XFiles xfn = new XFiles();
            string tgtName = ImagePlannerTargetList.SelectedItem.ToString();
            xfn.DeletePlan(tgtName);
            //clear current target list box and reload
            ImagePlannerTargetList.Items.Clear(); ;
            //Fill in Humason target plans
            XFiles xf = new XFiles();
            List<string> tgtList = xf.GetTargetFiles();
            foreach (string tgt in tgtList)
            {
                if (!(tgt.Contains("Default")))
                {
                    ImagePlannerTargetList.Items.Add(tgt);
                }
            }
            if (ImagePlannerTargetList.Items.Count > 0)
            {
                ImagePlannerTargetList.SelectedItem = ImagePlannerTargetList.Items[0];
            }
            ButtonGreen(DeleteTargetPlanButton);
            return;
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

        #endregion

        #region Input Handlers

        private void TargetNameBox_TextChanged(Object sender, KeyPressEventArgs e)  // Handles TargetNameBox.KeyPress
        {
            if (!enteringTargetState) //just started collecting characters
            {
                enteringTargetState = true;
                TargetNameBox.Text = null;
                if ((e.KeyChar == 'N') || (e.KeyChar == 'n'))
                {
                    e.KeyChar = ' ';
                    TargetNameBox.Text = "NGC";
                    TargetNameBox.Focus();
                    TargetNameBox.SelectionStart = TargetNameBox.Text.Length;
                }
                else if ((e.KeyChar == 'M') || (e.KeyChar == 'm'))
                {
                    e.KeyChar = ' ';
                    TargetNameBox.Text = "M";
                    TargetNameBox.Focus();
                    TargetNameBox.SelectionStart = TargetNameBox.Text.Length;
                }
                else if ((e.KeyChar == 'P') || (e.KeyChar == 'p'))
                {
                    e.KeyChar = ' ';
                    TargetNameBox.Text = "PGC";
                    TargetNameBox.Focus();
                    TargetNameBox.SelectionStart = TargetNameBox.Text.Length;
                }
                else if ((e.KeyChar == 'I') || (e.KeyChar == 'i'))
                {
                    e.KeyChar = ' ';
                    TargetNameBox.Text = "IC";
                    TargetNameBox.Focus();
                    TargetNameBox.SelectionStart = TargetNameBox.Text.Length;
                }
                else if ((e.KeyChar == 'H') || (e.KeyChar == 'h'))
                {
                    e.KeyChar = ' ';
                    TargetNameBox.Text = "HERSCHEL";
                    TargetNameBox.Focus();
                    TargetNameBox.SelectionStart = TargetNameBox.Text.Length;
                }
                else if ((e.KeyChar == 'C') || (e.KeyChar == 'c'))
                {
                    e.KeyChar = ' ';
                    TargetNameBox.Text = "CALDWELL";
                    TargetNameBox.Focus();
                    TargetNameBox.SelectionStart = TargetNameBox.Text.Length;
                }
            }
            if (e.KeyChar == '\r')
            {
                enteringTargetState = false;
                RegenerateForms();
                return;
            }
            return;
        }

        private void ImagePlannerTargetList_SelectedIndexChanged(Object sender, EventArgs e)  // Handles ImagePlannerTargetList.SelectedIndexChanged
        {
            //Loads the new target from the nh target list
            string tName = ImagePlannerTargetList.SelectedItem.ToString();
            TargetNameBox.Text = tName;
            RegenerateForms();
            return;
        }

        private void MinAltitudeBox_ValueChanged(Object sender, KeyPressEventArgs e) //Handles MinAltitudeBox.KeyPress
        {
            if (e.KeyChar == '\r')
            {
                MinimumAltitude = (double)MinAltitudeBox.Value;
                RegenerateForms();
                return;
            }
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
            this.Text += tYear + " Imaging Conditions Forecast for (" + tgtName +
                "       ( @ Imaging Start Time > Imaging Duration (in hours) with Moon Phase " +
                "and constrained by Astronomical Twilight)";
            //update tooltip for (target name
            FillInTargetDetails(tgtName);
            return;
        }

        //Write detail data for (individual cells as tool tips for (cursor hover
        public void WriteToolTip(int iRow, int jCol, DailyPosition dpt)
        {
            string tiptext = "Start Imaging: " + dpt.Rising.ToLocalTime().ToString("t") + "\r\n" +
                "End Imaging: " + dpt.Setting.ToLocalTime().ToString("t") + "\r\n" +
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
                if (dp.UTCdate.ToLocalTime().Year == CurrentYearPick.Value)
                {
                    switch (dp.Visibility)
                    {
                        case (DailyPosition.VisibilityState.UpSome):
                            tiptext = "Moonrise at " + dp.Rising.ToLocalTime().ToString("t") + "\r\n" +
                        "Moonset at " + dp.Setting.ToLocalTime().ToString("t") + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.UpAlways):
                            tiptext = "Moonrise before imaging" + "\r\n" +
                                "Moonset after imaging";
                            break;
                        case (DailyPosition.VisibilityState.Rises):
                            tiptext = "Moonrise at " + dp.Rising.ToLocalTime().ToString("t") + "\r\n" +
                        "Moonset after imaging" + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.Falls):
                            tiptext = "Moonrise before imaging" + "\r\n" +
                        "Moonset at " + dp.Setting.ToLocalTime().ToString("t") + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.DownSome):
                            tiptext = "Moonset at " + dp.Rising.ToLocalTime().ToString("t") + "\r\n" +
                        "Moon rises at " + dp.Setting.ToLocalTime().ToString("t") + "\r\n";
                            break;
                        case (DailyPosition.VisibilityState.UpNever):
                            tiptext = "No Moon during imaging" + "\r\n";
                            break;
                    }
                    jCol = dp.Rising.ToLocalTime().Month - 1;
                    iRow = dp.Rising.ToLocalTime().Day - 1;
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
            tdoc.DocumentProperty(TheSky64Lib.Sk6DocumentProperty.sk6DocProp_Time_Zone);

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

            foreach (DailyPosition dp in tgtdata)
            {
                if (dp.UTCdate.Year == CurrentYearPick.Value)
                {
                    jCol = dp.Rising.Month - 1;
                    iRow = dp.Rising.Day - 1;
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
                            imgStart = dp.Rising.ToLocalTime().ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.UpSome):
                            imgStart = dp.Rising.ToLocalTime().ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.Rises):
                            imgStart = dp.Rising.ToLocalTime().ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.Falls):
                            imgStart = dp.Rising.ToLocalTime().ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "@" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                        case (DailyPosition.VisibilityState.DownSome):
                            //Note that imaging time is split with this state, and the longest interval has been preselected
                            imgStart = dp.Rising.ToLocalTime().ToString("HH:mm");
                            imgTime = ((dp.Setting - dp.Rising).TotalHours).ToString("0.0");
                            cellText = "*" + imgStart + " >" + imgTime + "h " + pIcon;
                            break;
                    }

                    WriteCell(iRow, jCol, cellText);
                    WriteToolTip(iRow, jCol, dp);
                }
            }
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
            if (ButtonIsGreen(PreviewButton))
            {
                return;
            }
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
            if (ButtonIsGreen(AltitudeButton))
            {
                return;
            }
            //First, close the old one, if (any
            //First, close the old one, if (any
            if (this.pathForm != null)
            {
                this.pathFormLocation = this.pathForm.Location;
                this.pathForm.Close();
            }

            this.pathForm = new FormTargetPath(tgtdata[TargetIndex], moondata[TargetIndex], this.TargetNameBox.Text, moonDataDescription);

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

            if (ButtonIsGreen(DetailsButton))
            {
                return;
            }
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
            if (ButtonIsGreen(ProspectButton))
            {
                return;
            }
            //Don't open a new pop up if the old one isn't ready to be closed
            if (ProspectProtected)
            { return; }

            //First, close the old one, if (any
            if (this.wazzupForm != null)
            {
                this.wazzupFormLocation = this.wazzupForm.Location;
                this.wazzupForm.Close();
            }

            DateTime dawnDateUTC = sundata[TargetIndex + 1].Rising;
            DateTime duskDateUTC = sundata[TargetIndex].Setting;

            this.wazzupForm = new FormWazzup(duskDateUTC, dawnDateUTC);
            //set this form as the owner
            wazzupForm.Owner = this;
            //Locate the start position of this form at the lower left hand corner of the parent form
            int twPosX = this.Location.X;
            int twposY = this.Location.Y;
            int twSizeW = this.Size.Width;
            int twSizeH = this.Size.Height;
            int pvSizeW = this.wazzupForm.Size.Width;
            int pvSizeH = this.wazzupForm.Size.Height;
            Point pvLoc = new Point(twPosX + ((twSizeW / 2) - (pvSizeW / 2)), (twposY + twSizeH - pvSizeH));
            if (this.wazzupFormLocation != new Point(0, 0))
            {
                this.wazzupForm.Location = this.wazzupFormLocation;
            }
            else
            {
                this.wazzupForm.Location = pvLoc;
            }
            this.wazzupForm.StartPosition = FormStartPosition.Manual;
            //show the form
            this.wazzupForm.Show();
            return;
        }

        private void OpenExoPlanet()
        {
            //Opens the exoplanet form and window
            if (ButtonIsGreen(ExoPlanetButton))
            {
                return;
            }
            //Don't open a new pop up if the old one isn't ready to be closed
            if (ExoPlanetProtected)
            { return; }

            //First, close the old one, if (any
            if (this.exoPlanetForm != null)
            {
                this.exoPlanetFormLocation = this.exoPlanetForm.Location;
                this.exoPlanetForm.Close();
            }

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
            //Opens the target preview form and window
            if (ButtonIsGreen(TrackButton))
            {
                return;
            }
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
            return;
        }

        private void RegenerateForms()
        {
            //Common method for (rebuilding all the forms with new target, etc
            WriteTitle(TargetNameBox.Text, CurrentYearPick.Value.ToString());
            GenerateCalendar();
            OpenDetail();
            OpenPreview();
            OpenPath();
            OpenTrack();
            return;
        }

        #endregion

        #region Miscellaneous Helper Methods

        private string IncrementCatalogNumber(string targetName, int increment)
        {
            //assumes an input string of a catelog object:  ccccnnnn where cccc are characters and nnnn are numbers
            //this function parses the string and return s a new string whre the number as been incremented
            char[] s = new char[] { ' ' };
            string[] parts = targetName.Split(s, StringSplitOptions.RemoveEmptyEntries);
            int nextDigit = (Convert.ToInt32(parts[1]) + increment);
            if (nextDigit > 1) return parts[0] + " " + nextDigit.ToString("0");
            else return parts[0] + " 1";
            //string catNum = System.Text.RegularExpressions.Regex.Match(targetName, "\\d+").Value;
            //string catName = targetName.Replace(catNum, "");
            //int catNumInt = Convert.ToInt32(catNum);
            //catNumInt += increment;
            //if (catNumInt < 1)
            //{
            //    catNumInt = 1;
            //}
            //catNum = catNumInt.ToString();
            //string resultString = catName + catNum;
            //return resultString;
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
                    firstSpace[0] = firstSpace[0].Trim('_');
                    firstSpace[0] = System.Text.RegularExpressions.Regex.Replace(firstSpace[0], "//", "");
                    infoPair[1] = infoPair[1].Trim(' ');
                    infoX.Add(new XElement(firstSpace[0], infoPair[1]));
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

        #endregion

        #region Event Handlers

        private void WazzupEvent_Handler(object sender, WazzupEvent.WazzupEventArgs e)
        {
            ProspectProtected = true;
            this.TargetNameBox.Text = e.TargetName;
            enteringTargetState = false;
            //TargetNameBox.Text = TargetNameBox.Text.Replace(" ", "");
            RegenerateForms();
            DataGridViewSelectedCellCollection selcel = MonthCalendar.SelectedCells;
            int iRow = selcel[0].RowIndex;
            int iCol = selcel[0].ColumnIndex;
            MonthCalendar.Rows[iRow].Cells[iCol].Selected = false;
            MonthCalendar.Rows[iRow].Cells[iCol].Selected = true;
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



        #endregion

    }
}