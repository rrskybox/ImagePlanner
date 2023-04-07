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
            //Sort the prospect list by object size
            List<DBQObject> exoList = ExoPlanetList.NameSort();
            //list the sorted objects
            ListSelectedObjects(exoList);
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

        private void Closebutton_Click(Object sender, EventArgs e) // Handles Closebutton.Click
        {
            //Handles Close button: close window, end Quick Pick
            Close();
            return;
        }

        private void ListSelectedObjects(List<DBQObject> sortedExoList)
        {
            // builds a new one for all objects that
            ProspectGrid.Rows.Clear();

            int pidx = 0;
            foreach (DBQObject tgt in sortedExoList)
            {
                string typeName = tgt.TypeName;
                double tgtSize = tgt.Size;
                double tgtDuration = tgt.Duration;

                //TimeSpan tdiff = Utilities.OffsetUTC();
                TimeSpan tgtTransitFirstHalf = TimeSpan.FromHours(tgt.XpDurationHrs / 2);

                DateTime transitPlMidUTC = Utilities.NextTransitUTC(Utilities.LocalToUTCTime(DuskDateLocal), tgt.XpPl_TransMidJD, tgt.XpPl_TransPer);
                DateTime transitPlStartLocal = Utilities.UTCToLocalTime(transitPlMidUTC - tgtTransitFirstHalf);
                DateTime transitPlEndLocal = Utilities.UTCToLocalTime(transitPlMidUTC + tgtTransitFirstHalf);
                bool startOnSessionDate = Utilities.IsInSessionRange(SessionDate, transitPlStartLocal);
                bool endOnSessionDate = Utilities.IsInSessionRange(SessionDate, transitPlEndLocal);
                bool transitionStartOK = Utilities.IsBetweenDuskAndDawn(DuskDateLocal, DawnDateLocal, transitPlStartLocal);
                bool transitionEndOK = Utilities.IsBetweenDuskAndDawn(DuskDateLocal, DawnDateLocal, transitPlEndLocal);
                double tgtMinAltitude = Utilities.ComputeMinAltitude(transitPlStartLocal, transitPlEndLocal, tgt.RA, tgt.Dec, tgt.Lat, tgt.Long);
                //
                if ((tgtMinAltitude >= MinimumPlanningAltitude) &&
                    (tgt.XpDurationHrs > 0) &&
                    startOnSessionDate && endOnSessionDate && transitionStartOK && transitionEndOK)
                {
                    int i = 0;
                    ProspectGrid.Rows.Add();
                    ProspectGrid.Rows[pidx].Cells[i++].Value = tgt.Name;
                    ProspectGrid.Rows[pidx].Cells[i++].Value = transitPlStartLocal.ToString("HH:mm");
                    ProspectGrid.Rows[pidx].Cells[i++].Value = Utilities.ComputeAltitude(transitPlStartLocal, tgt.RA, tgt.Dec, tgt.Lat, tgt.Long).ToString("0");
                    ProspectGrid.Rows[pidx].Cells[i++].Value = transitPlEndLocal.ToString("HH:mm");
                    ProspectGrid.Rows[pidx].Cells[i++].Value = Utilities.ComputeAltitude(transitPlEndLocal, tgt.RA, tgt.Dec, tgt.Lat, tgt.Long).ToString("0");
                    ProspectGrid.Rows[pidx].Cells[i++].Value = tgt.XpDurationHrs.ToString("0.0");
                    ProspectGrid.Rows[pidx].Cells[i++].Value = tgt.XpDepth.ToString("0.00");
                    ProspectGrid.Rows[pidx].Cells[i++].Value = tgt.XpVmag.ToString("0.0");
                    ProspectGrid.Rows[pidx].Cells[i++].Value = tgtMinAltitude.ToString("0");
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

    }
}