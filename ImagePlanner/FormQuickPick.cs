// --------------------------------------------------------------------------------
// TSX Helper miniap for rapidly sorting and selecting targets by using an Observing List
// database query and object sortin  Can be used for using to CCDAP.
//
//   On occasion, I like to expedite the selection of a target and get it going.
//   The TSX "What's Up" feature can quickly produce a listing of available targets,
//   but working through object types, sizes, position, meridian flip times, to pick
//   a likely target can be cumbersome.  CCDNav is more user-friendly, but still requires
//   iteration to select targets.  CCDNav does have an additional ability to sort out a guider target
//   star, which is very useful, but only at the end of the selection process. 
//
//   for (instance, supposing I want to quickly find 1) a spiral galaxy, with 2) a reasonably large major axis,
//   with 3) at least 3 hours of visibility, and a 4) south azimuth.  In CCD Navigator,
//   this is mostly possible, but requires a certain amount of iteration because the target
//   lists are generated with single sorting parameters  With each "sorting" the user has to look
//   through the list of potential targets for what might work.  Same with TSX Observing List.  Either 
//   the user must sort and resort the output targets, or iteratively reenter selection criteria, then
//   look through the list.
//
//   This Quick Pick automation allows a user to look through a large observing list based on criteria and ranges
//   to narrow down to a choice or set of choices that can then be decided.  The criteria are:
//       1) object type,
//       2) range of object azimuth (normalized to compass direction), 
//       3) range of object size (based on major axis,
//       4) range of object availability (remaining time for imaging) times, 
//       5) range of object visibility (based on a 30 degree horizon)
//
//   Note that this tool is not a "planning" tool in the sense that CCDNAv, Astroplanner, etc are.  This tool
//   is more of an "aim and fire" tool, intended for a rapid selection of an individual target, presumably
//   immediately before initiating the shot and going to bed.
//
//   This miniapp takes advantage of the TSX Observing List feature, and sorts the target
//   database according to selection parameters. 
////
// Description:	//Operation as follows:
//   Install a database query file, if not already installed, for all NCG and M objects over 30 degrees altitude
//   Use it to produce an observing list in TSX 
//   Narrow the list down by selected object type
//   Optionally:
//       Narrow the list down based on minimum size (slider)
//       Narrow the list down based on minimum remaining time above horizon or dawn (slider)
//       Narrow the list down based on minimum current altitude (slider)
//       Narrow the list down based current azimuth (compass gauge)
//   Perform a Find on selected object -- display in TSX.
//   Reset and resort as needed
//
//// Environment:  Windows 7,8,10 executable, 32 and 64 bit.
//
// Requirements:  TSX Pro (Build 9334 or greater)
//
// Usage:        //Quick Pick is installed and runs as an miniap out of the TSXToolbox group.
//
// Author:		(REM) Rick McAlister, rrskybox@yahoo.com
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 06-08-16	    REM	1.0.0	Initial implementation
// 06-18-16      REM 1.1.0   Release implementation
// ---------------------------------------------------------------------------------
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
//using Microsoft.VisualBasic.PowerPacks;
using TheSkyXLib;

namespace ImagePlanner

{
    public partial class FormQuickPick : Form
    {
        public const double QPDefaultMinimumSize = 1;  //ArcMin
        public const double QPDefaultMinimumAltitude = 0;  //Degrees
        public const double QPDefaultMinimumDuration = 0.5;  //Hours

        private ObjectList olist;

        public DateTime PickDate;

        public string TypePicked;
        public double SizeMax;
        public double SizeMin;
        public double DurationMax;
        public double DurationMin;
        public double AltitudeMax;
        public double AltitudeMin;

        const int MainCircleDrawLocationX = 485 - 75;
        const int MainCircleDrawLocationY = 222 - 75;
        const int MainCircleSize = 150;
        const int MainCircleBorder = 20;
        const int PointCircleSize = 15;
        const int CircleRadius = 75;
        const int XCircleCenter = MainCircleDrawLocationX + CircleRadius;
        const int YCircleCenter = MainCircleDrawLocationY + CircleRadius;
        const int TipSize = 10;

        public FormQuickPick()
        {
            InitializeComponent();
            PickDate = DateTime.Now;
            SelectGalaxyButton.Checked = true;
            Closebutton.MouseClick += new System.Windows.Forms.MouseEventHandler(Closebutton_Click);
            return;
        }

        public FormQuickPick(DateTime pickDate)
        {
            InitializeComponent();
            PickDate = pickDate;
            SelectGalaxyButton.Checked = true;
            Closebutton.MouseClick += new System.Windows.Forms.MouseEventHandler(Closebutton_Click);
            return;
        }

        private void QuickPickLaunch(ObjectList.SearchType searchDB)
        //Upon launch, load and create the required database query, run it and set the object
        //  type list and minimum/maximum values
        {
            olist = new ObjectList(searchDB, PickDate);
            this.Text = "QuickPick for " + PickDate.ToString("MMM dd, yyyy");
            //Reset sort max/min
            ResetNonAzCriteria();
            //azReset();

            //Set starting SizeMin and SizeMax for this list of objects
            int gfvt = SetFirstValidTarget(QPDefaultMinimumSize, QPDefaultMinimumAltitude, QPDefaultMinimumDuration);
            if (gfvt >= 0) { SetTrackBars(gfvt); }

            //for (each entry in the object list, if the TypeName hasnt already been added to the
            //NGCTypesList then add it  

            this.CatalogedTypesList.Items.Clear();
            for (int oi = 0; oi < olist.Count; oi++)
            {
                bool cflg = false;
                SetTrackBars(oi);
                for (int typelistindex = 0; typelistindex < CatalogedTypesList.Items.Count; typelistindex++)
                {
                    if (CatalogedTypesList.Items[typelistindex].ToString() == olist.TypeName(oi))
                    { cflg = true; }
                }
                if (!cflg)
                { CatalogedTypesList.Items.Add(olist.TypeName(oi)); }
            }
            ResetThumbPositions();
            return; ;
        }

         private void TargetObjectList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //Upon selection of an object, tell the ImagePlanner update everything.

            //sky6StarChart tsxsc = new sky6StarChart();
            //int oi = olist.TgtFind(TargetObjectList.SelectedItem.ToString());
            //tsxsc.Declination = olist.TgtDec(oi);
            //tsxsc.RightAscension = olist.TgtRA(oi);
            //tsxsc.Find(TargetObjectList.SelectedItem.ToString());
            //tsxsc = null;

            //Causes the image planner form to be update with the current target name selected
            UpdateImagePlanner(TargetObjectList.SelectedItem.ToString());
            return;
        }

        private void CatalogedTypesList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //Upon selection of an object type, clear the object list and reset criteria,
            //  then relist all objects of that type.  Then sort, etc.
            TargetObjectList.Items.Clear();
            ResetNonAzCriteria();
            TypePicked = CatalogedTypesList.SelectedItem.ToString();
            for (int oi = 0; oi < olist.Count; oi++)
            {
                if (olist.TypeName(oi) == TypePicked)
                { SetTrackBars(oi); }
            }
            ResetThumbPositions();
            List<DBQObject> sortList = olist.SizeSort();
            SelectCheck(sortList, CatalogedTypesList.SelectedItem.ToString());
            return;
        }

        private void Closebutton_Click(Object sender, EventArgs e) // Handles Closebutton.Click
        {
            //Handles Close button: close window, end Quick Pick
            Close();
            return;
        }

        private void ResetNonAzCriteria()
        {
            //Zero out all the slider max and min
            TypePicked = null;
            SizeMax = 0;
            SizeMin = 1000;
            DurationMax = 0;
            DurationMin = 24;
            AltitudeMax = 0;
            AltitudeMin = 90;
            return;
        }

        private void ResetThumbPositions()
        {
            //Reset slider thumbs to minimum position
            SizeNumeric.Value = (int)Math.Ceiling(SizeMin);
            AltitudeNumeric.Value = (int)AltitudeMin;
            DurationNumeric.Value = (int)Math.Ceiling(DurationMin);
            return;
        }

        //This is where the beef is...

        private int SetFirstValidTarget(double minSize, double minAlt, double minDuration)
        {
            //Returns the index of the first target object that exceeds the minimums
            // or -1 if none is found
            int oidx = -1;
            for (int i = 0; i < this.olist.Count; i++)
            {
                //Get the target size(major axis) for the object
                double targetsize = olist.TgtSize(i);
                double targetduration = olist.TgtDuration(i);
                double targetaltitude = olist.TgtAltitude(i);
                if ((targetsize >= minSize) || (targetaltitude >= minAlt) || (targetduration >= minDuration))
                {
                    SizeMin = targetsize;
                    AltitudeMin = targetaltitude;
                    DurationMin = targetduration;
                    return i;
                }
            }
            return oidx;
        }


        private void SetTrackBars(int oindex)
        {
            //This subroutine resets the maximum and minimum values for the size, duration and altitude sliders
            //  without moving the current indicator value, for the object out of the object list based on oindex

            double targetsize;
            double targetduration;
            double targetaltitude;

            //Get the target size(major axis) for the object
            targetsize = olist.TgtSize(oindex);
            targetduration = olist.TgtDuration(oindex);
            targetaltitude = olist.TgtAltitude(oindex);

            if ((targetsize < QPDefaultMinimumSize) || (targetaltitude < QPDefaultMinimumAltitude) || (targetduration < QPDefaultMinimumDuration))
            {
                return;
            }

            //If it is larger than the current max, then up the current max and display
            if (targetsize >= SizeMax)
            {
                SizeMax = targetsize;
                SizeNumeric.Maximum = (int)SizeMax;
                SizeTBMax.Text = SizeMax.ToString("0.0");
            }
            //If it is smaller than the trackbar indicator, then lower the minimum and display
            if (targetsize <= SizeMin)
            {
                SizeMin = targetsize;
                SizeNumeric.Minimum = (int)SizeMin;
                SizeTBMin.Text = SizeMin.ToString("0.0");
            }

            //Get the target duration (time before setting or dawn

            //If it is larger than the current max, then up the current max and display
            if (targetduration >= DurationMax)
            {
                DurationMax = targetduration;
                DurationNumeric.Maximum = (int)DurationMax;
                DurationTBMax.Text = Utilities.HourString(DurationMax);
            }
            //If it is smaller than the trackbar indicator, then lower the minimum and display
            if (targetduration <= DurationMin)
            {
                DurationMin = targetduration;
                DurationNumeric.Minimum = (int)DurationMin;
                DurationTBMin.Text = Utilities.HourString(DurationMin);
            }
            //Get the target altitude
            //If it is larger than the current max, then up the current max and display
            if (targetaltitude >= AltitudeMax)
            {
                AltitudeMax = targetaltitude;
                AltitudeNumeric.Maximum = (int)AltitudeMax;
                AltitudeTBMax.Text = AltitudeMax.ToString("0.0");
            }
            //If it is smaller than the trackbar indicator, then lower the minimum and display
            if (targetaltitude <= AltitudeMin)
            {
                AltitudeMin = targetaltitude;
                AltitudeNumeric.Minimum = (int)AltitudeMin;
                AltitudeTBMin.Text = AltitudeMin.ToString("0.0");
                //AltitudeTrackBar.Value = (int)AltitudeMin;
            }
            return;
        }

        private void SelectCheck(List<DBQObject> sortList, string typePicked)
        {
            //clears the object list, then builds a new one for all objects that
            // meet the critera for output to sort list
            TargetObjectList.Items.Clear();
            //            for (int tid = 0; tid < olist.Count; tid++)
            foreach (DBQObject tgt in sortList)
            {
                string typeName = tgt.TypeName;
                double tgtSize = tgt.Size;
                double tgtDuration = tgt.Duration;
                double tgtAltitude = tgt.Alt;
                if ((typeName == typePicked)
                  &&
                  (tgtSize >= SizeMin)
                  &&
                  (tgtDuration >= DurationMin)
                  &&
                  (tgtAltitude >= AltitudeMin))
                {
                    TargetObjectList.Items.Add(tgt.Name);
                }
            }
            return;
        }

        private void UpdateImagePlanner(string targetName)
        {
            //Causes the image planner form to be update with the current target name selected
            WazzupEvent qpEvent = FormImagePlanner.QPUpdate;
            qpEvent.QPTargetUpdate(targetName);
            return;
        }

        private void SelectGalaxyButton_CheckedChanged(object sender, EventArgs e)
        {
            //Loads and runs new observing list for galaxies
            if (SelectGalaxyButton.Checked)
            {
                Color saveColor = SelectGalaxyButton.ForeColor;
                SelectGalaxyButton.ForeColor = Color.Red;
                QuickPickLaunch(ObjectList.SearchType.Galaxy);
                SelectGalaxyButton.ForeColor = saveColor;
            }
            return;
        }

        private void SelectClusterButton_CheckedChanged(object sender, EventArgs e)
        {
            //Loads and runs new observing list for star clusters
            if (SelectClusterButton.Checked)
            {
                Color saveColor = SelectClusterButton.ForeColor;
                SelectClusterButton.ForeColor = Color.Red;
                QuickPickLaunch(ObjectList.SearchType.Cluster);
                SelectClusterButton.ForeColor = saveColor;
            }

            return;
        }

        private void SelectNebulaButton_CheckedChanged(object sender, EventArgs e)
        {
            //Loads and runs new observing list for Nebulae
            if (SelectNebulaButton.Checked)
            {
                Color saveColor = SelectNebulaButton.ForeColor;
                SelectNebulaButton.ForeColor = Color.Red;
                QuickPickLaunch(ObjectList.SearchType.Nebula);
                SelectNebulaButton.ForeColor = saveColor;
            }
            return;
        }

        private void SizeNumeric_ValueChanged(object sender, EventArgs e)
        {
           //This routine will reset the NGC list to only those items with a size greater than the size indicator
            SizeMin = (double)SizeNumeric.Value;
            List<DBQObject> sortList = olist.SizeSort();
            SelectCheck(sortList, TypePicked);
            return;
        }

       private void AltitudeNumeric_ValueChanged(object sender, EventArgs e)
        {
            AltitudeMin = (double)AltitudeNumeric.Value;
            List<DBQObject> sortList = olist.AltitudeSort();
            SelectCheck(sortList, TypePicked);
            return;
                    }

        private void DurationNumeric_ValueChanged(object sender, EventArgs e)
        {
          DurationMin = (double)DurationNumeric.Value;
            List<DBQObject> sortList = olist.DurationSort();
            SelectCheck(sortList, TypePicked);
            return;

        }


        //private void HelpTips()
        //{
        //    //Reads in help tips text and presents it as a message box

        //    //Collect the file contents to be written
        //    Assembly dassembly = Assembly.GetExecutingAssembly();
        //    Stream dstream = dassembly.GetManifestResourceStream("QuickPick.Help and Tips.txt");
        //    StreamReader tsreader = new StreamReader(dstream);
        //    string via = tsreader.ReadToEnd();
        //    System.Windows.Forms.MessageBox.Show(via);
        //    return;
        //}
    }
}