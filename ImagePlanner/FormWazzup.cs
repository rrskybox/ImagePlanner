using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using AstroMath;

namespace ImagePlanner
{
    public partial class FormWazzup : Form
    {
        public const double QPDefaultMinimumSize = 1;  //ArcMin
        public const double QPDefaultMinimumAltitude = 0;  //Degrees
        public const double QPDefaultMinimumDuration = 0.5;  //Hours

        private ObjectList ProspectList;

        private DateTime DuskDateLocal;
        private DateTime DawnDateLocal;

        private string TypePicked;
        private double SizeMax;
        private double SizeMin;
        private double DurationMax;
        private double DurationMin;
        private double AltitudeMax;
        private double AltitudeMin;

        public FormWazzup(DateTime duskDateUTC, DateTime dawnDateUTC)
        {
            InitializeComponent();
            DuskDateLocal = duskDateUTC.ToLocalTime();
            DawnDateLocal = dawnDateUTC.ToLocalTime();
            this.Text = "WazzUp for " + DuskDateLocal.ToString("MMM dd, yyyy");
            //WazzupLaunch(ObjectList.SearchType.Nebula);
            //SelectNebulaButton.Checked = true;
            //Minimize TSX to improve speed
            ManageTSX.MinimizeTSX();
            return;
        }

        private void WazzupLaunch(DBQFileManagement.SearchType searchDB)
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
            ProspectList = new ObjectList(searchDB, DuskDateLocal, DawnDateLocal);
            fnote.Close();

            ////for (each entry in the object list, if the TypeName hasnt already been added to the
            ////NGCTypesList then add it  
            this.CatalogedTypesList.Items.Clear();
            //Clear the object listing
            ProspectGrid.Rows.Clear();
            for (int oi = 0; oi < ProspectList.Count; oi++)
            {
                if (!(CatalogedTypesList.Items.Contains(ProspectList.TypeName(oi))))
                { CatalogedTypesList.Items.Add(ProspectList.TypeName(oi)); }
            }
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

            //Get the cataloged object type that was selected
            TypePicked = (string)CatalogedTypesList.SelectedItem;
            //Clear the object listing
            ProspectGrid.Rows.Clear();
            //Reset the Max and Min for the Type picked
            ResetMaxMin(TypePicked);
            //Set the numeric minimums displayed to the new minimum values
            SizeNumeric.Value = (decimal)SizeMin;
            AltitudeNumeric.Value = (decimal)AltitudeMin;
            DurationNumeric.Value = (decimal)DurationMin;
            //Sort the prospect list by object size
            List<DBQObject> sortList = ProspectList.SizeSort();
            //list the sorted objects
            ListSelectedObjects(sortList, TypePicked);
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
            SizeMax = 0;
            SizeMin = 1000;
            DurationMax = 0;
            DurationMin = 24;
            AltitudeMax = 0;
            AltitudeMin = 90;
            return;
        }

        private void ResetMaxMin(string objectType)
        {
            //Determine the minimum and maximum values in the current list of objects for the given objectType 
            PresetMaxMin();
            for (int oi = 0; oi < ProspectList.Count; oi++)
            {
                if (ProspectList.TypeName(oi) == objectType)
                {
                    double targetsize = ProspectList.TgtSize(oi);
                    if (targetsize >= SizeMax)
                    { SizeMax = targetsize; }
                    if (targetsize <= SizeMin)
                    { SizeMin = targetsize; }

                    double targetduration = ProspectList.TgtDuration(oi);
                    if (targetduration >= DurationMax)
                    { DurationMax = targetduration; }
                    if (targetduration <= DurationMin)
                    { DurationMin = targetduration; }


                    double targetaltitude = ProspectList.TgtMaxAltitude(oi);
                    if (targetaltitude >= AltitudeMax)
                    { AltitudeMax = targetaltitude; }
                    if (targetaltitude <= AltitudeMin)
                    { AltitudeMin = targetaltitude; }
                }
            }
            //If any minimums are less than zero, then reset to zero
            if (AltitudeMin < 0)
            { AltitudeMin = 0; }

            SizeNumeric.Maximum = (decimal)SizeMax;
            SizeTBMax.Text = SizeMax.ToString("0");
            SizeNumeric.Minimum = (decimal)SizeMin;
            SizeTBMin.Text = SizeMin.ToString("0");

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

        private void ListSelectedObjects(List<DBQObject> sortList, string typePicked)
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

                if ((typeName == typePicked)
                &&
                (tgtSize >= SizeMin)
                &&
                (tgtDuration >= DurationMin)
                &&
                (tgtMaxAltitude >= AltitudeMin))
                {
                    //TargetObjectList.Items.Add(tgt.Name);
                    ProspectGrid.Rows.Add();
                    ProspectGrid.Rows[pidx].Cells[0].Value = tgt.Name;
                    ProspectGrid.Rows[pidx].Cells[1].Value = (decimal)tgt.Size;
                    ProspectGrid.Rows[pidx].Cells[2].Value = (decimal)tgt.Duration;
                    ProspectGrid.Rows[pidx].Cells[3].Value = (decimal)tgt.MaxAltitude;
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

        private void SelectGalaxyButton_CheckedChanged(object sender, EventArgs e)
        {
            //Loads and runs new observing list for galaxies
            if (SelectGalaxyButton.Checked)
            {
                Color saveColor = SelectGalaxyButton.ForeColor;
                SelectGalaxyButton.ForeColor = Color.Red;
                WazzupLaunch(DBQFileManagement.SearchType.Galaxy);
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
                WazzupLaunch(DBQFileManagement.SearchType.Cluster);
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
                WazzupLaunch(DBQFileManagement.SearchType.Nebula);
                SelectNebulaButton.ForeColor = saveColor;
            }
            return;
        }

        private void SizeNumeric_ValueChanged(object sender, EventArgs e)
        {
            //This routine will reset the NGC list to only those items with a size greater than the size indicator
            SizeMin = (double)SizeNumeric.Value;
            if (ProspectList != null)
            {
                List<DBQObject> sortList = ProspectList.SizeSort();
                ListSelectedObjects(sortList, TypePicked);
            }
            return;
        }

        private void AltitudeNumeric_ValueChanged(object sender, EventArgs e)
        {
            AltitudeMin = (double)AltitudeNumeric.Value;
            if (ProspectList != null)
            {
                List<DBQObject> sortList = ProspectList.AltitudeSort();
                ListSelectedObjects(sortList, TypePicked);
            }
            return;
        }

        private void DurationNumeric_ValueChanged(object sender, EventArgs e)
        {
            DurationMin = (double)DurationNumeric.Value;
            if (ProspectList != null)
            {
                List<DBQObject> sortList = ProspectList.DurationSort();
                ListSelectedObjects(sortList, TypePicked);
            }
            return;

        }

        //private void HelpTips()
        //{
        //    //Reads in help tips text and presents it as a message box

        //    //Collect the file contents to be written
        //    Assembly dassembly = Assembly.GetExecutingAssembly();
        //    Stream dstream = dassembly.GetManifestResourceStream("Wazzup.Help and Tips.txt");
        //    StreamReader tsreader = new StreamReader(dstream);
        //    string via = tsreader.ReadToEnd();
        //    System.Windows.Forms.MessageBox.Show(via);
        //    return;
        //}
    }
}