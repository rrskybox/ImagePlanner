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

        private double MinimumPlanningAltitude;

        public FormExoPlanet(DateTime duskDateUTC, DateTime dawnDateUTC, double minimumListedAltitude)
        {
            InitializeComponent();
            DuskDateLocal = duskDateUTC.ToLocalTime();
            DawnDateLocal = dawnDateUTC.ToLocalTime();
            SessionDate = Convert.ToDateTime(DuskDateLocal.ToString("yyyy MM dd"));
            this.Text = "ExoPlanets for " + DuskDateLocal.ToString("MMM dd, yyyy");
            MinimumPlanningAltitude = minimumListedAltitude;
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
            //Check for zero objects
            if (ExoPlanetList.Count == 0)
            {
                fnote.Close();
                MessageBox.Show("No exoplanet targets listed.  Check to see if the associated Confirmed or Candidate catalog is loaded as an SDB.  " +
                    "If not, use Transient Search to acquire current exoplanet catalog.");
                return;
            }
            //Clear the object listing
            ProspectGrid.Rows.Clear();
            //Reset the Max and Min for the Type picked
            ResetMaxMin();
            //Set the numeric minimums displayed to the new minimum values
            AltitudeNumeric.Value = (decimal)MinimumPlanningAltitude;
            DurationNumeric.Value = (decimal)DurationMin;
            //Sort the prospect list by object size
            List<DBQObject> exoList = ExoPlanetList.DurationSort();
            //list the sorted objects
            ListSelectedObjects(exoList, AltitudeMin, DurationMin);
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
            List<DBQObject> culledList = ExoPlanetList.DurationSort();
            //list the sorted objects
            ListSelectedObjects(culledList, AltitudeMin, DurationMin);
            return;
        }

        private void Closebutton_Click(Object sender, EventArgs e) // Handles Closebutton.Click
        {
            //Handles Close button: close window, end Quick Pick
            Close();
            return;
        }

        private void ResetMaxMin()
        {
            //Determine the minimum and maximum values in the current list of objects for the given objectType 
            //Zero out all the slider max and min
            DurationMax = 0;
            DurationMin = 24;
            AltitudeMax = 0;
            AltitudeMin = 90;
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
            AltitudeNumeric.Minimum = (decimal)MinimumPlanningAltitude;
            AltitudeTBMin.Text = AltitudeMin.ToString("0");
            return;
        }

        private void ListSelectedObjects(List<DBQObject> sortedExoList, double minAlt, double minDur)
        {
            // builds a new one for all objects that
            ProspectGrid.Rows.Clear();

            int pidx = 0;
            foreach (DBQObject tgt in sortedExoList)
            {
                string typeName = tgt.TypeName;
                double tgtSize = tgt.Size;
                double tgtDuration = tgt.Duration;
                double tgtMaxAltitude = tgt.MaxAltitude;
                //TimeSpan tdiff = Utilities.OffsetUTC();
                TimeSpan tgtTransitFirstHalf = TimeSpan.FromHours(tgt.XpDurationHrs / 2);

                DateTime transitPlMidUTC = Utilities.NextTransitUTC(Utilities.LocalToUTCTime(DuskDateLocal), tgt.XpPl_TransMidJD, tgt.XpPl_TransPer);
                DateTime transitPlStartLocal = Utilities.UTCToLocalTime(transitPlMidUTC - tgtTransitFirstHalf);
                DateTime transitPlEndLocal = Utilities.UTCToLocalTime(transitPlMidUTC + tgtTransitFirstHalf);
                bool startOnSessionDate = Utilities.IsInSessionRange(SessionDate, transitPlStartLocal);
                bool endOnSessionDate = Utilities.IsInSessionRange(SessionDate, transitPlEndLocal);
                bool transitionStartOK = Utilities.IsBetweenDuskAndDawn(DuskDateLocal, DawnDateLocal, transitPlStartLocal);
                bool transitionEndOK = Utilities.IsBetweenDuskAndDawn(DuskDateLocal, DawnDateLocal, transitPlEndLocal);
                //
                if ((tgtDuration >= minDur) &&
                    (tgtMaxAltitude >= minAlt) &&
                    (tgt.XpDurationHrs > 0) &&
                    startOnSessionDate && endOnSessionDate && transitionStartOK && transitionEndOK )
                {
                    ProspectGrid.Rows.Add();
                    ProspectGrid.Rows[pidx].Cells[0].Value = tgt.Name;
                    ProspectGrid.Rows[pidx].Cells[1].Value = transitPlStartLocal.ToString("HH:mm");
                    ProspectGrid.Rows[pidx].Cells[2].Value = transitPlEndLocal.ToString("HH:mm");
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
                List<DBQObject> exoList = ExoPlanetList.AltitudeSort();
                ListSelectedObjects(exoList, AltitudeMin, DurationMin);
            }
            return;
        }

        private void DurationNumeric_ValueChanged(object sender, EventArgs e)
        {
            DurationMin = (double)DurationNumeric.Value;
            if (ExoPlanetList != null)
            {
                List<DBQObject> exoList = ExoPlanetList.DurationSort();
                ListSelectedObjects(exoList, AltitudeMin, DurationMin);
            }
            return;

        }

    }
}