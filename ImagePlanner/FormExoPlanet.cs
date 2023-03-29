using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ImagePlanner
{
    /// <TSX_unnamed_Data_Fields></TSX_unnamed_Data_Fields>
    /// Data 0 - Planetary solution
    /// Data 1 - vMag
    /// Data 2 - Planetary Orbital Period
    /// Data 3 - System Distribution
    /// Data 4 - Star Spectrum type
    /// Data 5 - Planetary Transit duration
    /// Data 6 - Planetary Transit Depth (mag maybe)
    /// Data 7 - Planetary Transit Midpoint Initial Julian Date
    /// Data 8 - Planetary Next Transit Midpoint (calculated by Transient Search)
    /// Data 9 - Planetary Next Transit Midpoint earliest (calculated by Transient Search i.e. +/- error)
    /// Data 10 - Planetary Next Transit Midpoint latest (calculated by Transient Search i.e. +/- error)
    /// </TSX_unnamed_Data_Fields></TSX_unnamed_Data_Fields>

    public partial class FormExoPlanet : Form
    {
        public const double QPDefaultMinimumAltitude = 0;  //Degrees
        public const double QPDefaultMinimumDuration = 0.5;  //Hours

        private ObjectList ExoPlanetList;

        private DateTime DuskDateLocal;
        private DateTime DawnDateLocal;
        private DateTime SessionDate;

        private double DurationMax;
        private double DurationMin;
        private double AltitudeMax;
        private double AltitudeMin;

        public FormExoPlanet(DateTime duskDateUTC, DateTime dawnDateUTC, double minimumAltitude)
        {
            InitializeComponent();
            DuskDateLocal = duskDateUTC.ToLocalTime();
            DawnDateLocal = dawnDateUTC.ToLocalTime();
            SessionDate = Convert.ToDateTime(DawnDateLocal.ToString("yyyy MM dd"));
            this.Text = "ExoPlanets for " + DuskDateLocal.ToString("MMM dd, yyyy");
            AltitudeMin = minimumAltitude;
            ManageTSX.MinimizeTSX();
            return;
        }

        private void ExoPlanetLaunch(DBQFileManagement.SearchType searchDB)
        {
            //Update the window title with Wazzup and the current date being looked at
            //Post the searching notice window -- this is going to take awhile
            //Load and rund the database query for the given object type (serachtype)
            //Clear the searching notice -- the rest shouldn't take long

            //Scan the the object list for different types and put them in the object type list
            //(don't check max and min until a type has been picked)
            //
            FormSearchingNotice fnote = new FormSearchingNotice();
            fnote.Show();
            ExoPlanetList = new ObjectList(searchDB, DuskDateLocal, DawnDateLocal);
            //Clear the object listing
            ProspectGrid.Rows.Clear();
            //Reset the Max and Min for the Type picked
            ResetMaxMin();
            //Set the numeric minimums displayed to the new minimum values
            AltitudeNumeric.Value = (decimal)AltitudeMin;
            DurationNumeric.Value = (decimal)DurationMin;
            //Sort the prospect list by object size
            List<DBQObject> sortList = ExoPlanetList.SizeSort();
            //list the sorted objects

            ListSelectedObjects(sortList);

            fnote.Close();
            return;
        }

        private void ProspectGrid_CellDoubleClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            //Upon double click of a name, tell the ImagePlanner update everything.
            if (e.RowIndex < 0)
            { return; }
            string tName = (string)ProspectGrid.Rows[e.RowIndex].Cells[0].Value;
            UpdateImagePlanner(tName);
            return;
        }

        private void CatalogedTypesList_DoubleClickEvent(Object sender, EventArgs e)
        {
            //Upon selection of an object type, clear the object list and reset criteria,
            //  then relist all objects of that type.  Then sort, etc.

            //Clear the object listing
            ProspectGrid.Rows.Clear();
            //Reset the Max and Min for the Type picked
            ResetMaxMin();
            //Set the numeric minimums displayed to the new minimum values
            AltitudeNumeric.Value = (decimal)AltitudeMin;
            DurationNumeric.Value = (decimal)DurationMin;
            //Sort the prospect list by object size
            List<DBQObject> sortList = ExoPlanetList.SizeSort();
            //list the sorted objects
            ListSelectedObjects(sortList);
            return;
        }

        private void Closebutton_Click(Object sender, EventArgs e) // Handles Closebutton.Click
        {
            //Handles Close button: close window, end Quick Pick
            Close();
            return;
        }

        private void PresetMaxMin()
        {
            //Zero out all the slider max and min
            DurationMax = 0;
            DurationMin = 24;
            AltitudeMax = 0;
            AltitudeMin = 90;
            return;
        }

        private void ResetMaxMin()
        {
            //Determine the minimum and maximum values in the current list of objects for the given objectType 
            PresetMaxMin();
            for (int oi = 0; oi < ExoPlanetList.Count; oi++)
            {

                double targetduration = ExoPlanetList.TgtDuration(oi);
                if (targetduration >= DurationMax)
                { DurationMax = targetduration; }
                if (targetduration <= DurationMin)
                { DurationMin = targetduration; }

                double targetaltitude = ExoPlanetList.TgtMaxAltitude(oi);
                if (targetaltitude >= AltitudeMax)
                { AltitudeMax = targetaltitude; }
                if (targetaltitude <= AltitudeMin)
                { AltitudeMin = targetaltitude; }
            }
            //If any minimums are less than zero, then reset to zero
            if (AltitudeMin < 0)
            { AltitudeMin = 0; }

            DurationNumeric.Maximum = (decimal)DurationMax;
            DurationTBMax.Text = DurationMax.ToString("00.0");
            DurationNumeric.Minimum = (decimal)DurationMin;
            DurationTBMin.Text = DurationMin.ToString("00.0");

            AltitudeNumeric.Maximum = (decimal)AltitudeMax;
            AltitudeTBMax.Text = AltitudeMax.ToString("0");
            AltitudeNumeric.Minimum = (decimal)AltitudeMin;
            AltitudeTBMin.Text = AltitudeMin.ToString("0");
            return;
        }

        private void ListSelectedObjects(List<DBQObject> sortList)
        {
            // builds a new one for all objects that
            ProspectGrid.Rows.Clear();

            int pidx = 0;
            foreach (DBQObject tgt in sortList)
            {
                string typeName = tgt.TypeName;
                double tgtSize = tgt.Size;
                double tgtDuration = tgt.Duration;
                double tgtMaxAltitude = tgt.MaxAltitude;
                TimeSpan tdiff = Utilities.OffsetUTC();
                TimeSpan tgtTransitFirstHalf = TimeSpan.FromHours(tgt.XpDurationHrs / 2);

                //DateTime transitStart = (tgt.XpTransitMid + tdiff) - tgtTransitFirstHalf;
                //DateTime transitEnd = (tgt.XpTransitMid + tdiff) + tgtTransitFirstHalf;

                DateTime transitPlMid = Utilities.NextTransit(DuskDateLocal.ToUniversalTime(), tgt.XpPl_TransMid, tgt.XpPl_TransPer);
                DateTime transitPlStart = (transitPlMid + tdiff) - tgtTransitFirstHalf;
                DateTime transitPlEnd = (transitPlMid + tdiff) + tgtTransitFirstHalf;

                if ((tgtDuration >= DurationMin) &&
                    (tgtMaxAltitude >= AltitudeMin) &&
                    (tgt.XpDurationHrs > 0) &&
                    (Utilities.IsInDateRange(SessionDate, transitPlStart)))
                {
                    ProspectGrid.Rows.Add();
                    ProspectGrid.Rows[pidx].Cells[0].Value = tgt.Name;
                    ProspectGrid.Rows[pidx].Cells[1].Value = transitPlStart.ToString("HH:mm");
                    ProspectGrid.Rows[pidx].Cells[2].Value = transitPlEnd.ToString("HH:mm");
                    ProspectGrid.Rows[pidx].Cells[3].Value = tgt.XpDurationHrs.ToString();
                    ProspectGrid.Rows[pidx].Cells[4].Value = tgt.XpDepth.ToString();
                    ProspectGrid.Rows[pidx].Cells[5].Value = tgt.XpVmag.ToString();
                    pidx++;
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

        private void SelectConfirmedExoPlanetButton_CheckedChanged(object sender, EventArgs e)
        {
            //Loads and runs new observing list for galaxies
            if (ConfirmedButton.Checked)
            {
                Color saveColor = ConfirmedButton.ForeColor;
                ConfirmedButton.ForeColor = Color.Red;
                ExoPlanetLaunch(DBQFileManagement.SearchType.ConfirmedExoPlanet);
                ConfirmedButton.ForeColor = saveColor;
            }
            return;
        }

        private void SelectCandidateExoPlanetButton_CheckedChanged(object sender, EventArgs e)
        {
            //Loads and runs new observing list for star clusters
            if (CandidateButton.Checked)
            {
                Color saveColor = CandidateButton.ForeColor;
                CandidateButton.ForeColor = Color.Red;
                ExoPlanetLaunch(DBQFileManagement.SearchType.CandidateExoPlanet);
                CandidateButton.ForeColor = saveColor;
            }

            return;
        }

        private void AltitudeNumeric_ValueChanged(object sender, EventArgs e)
        {
            AltitudeMin = (double)AltitudeNumeric.Value;
            if (ExoPlanetList != null)
            {
                List<DBQObject> sortList = ExoPlanetList.AltitudeSort();
                ListSelectedObjects(sortList);
            }
            return;
        }

        private void DurationNumeric_ValueChanged(object sender, EventArgs e)
        {
            DurationMin = (double)DurationNumeric.Value;
            if (ExoPlanetList != null)
            {
                List<DBQObject> sortList = ExoPlanetList.DurationSort();
                ListSelectedObjects(sortList);
            }
            return;

        }

    }
}