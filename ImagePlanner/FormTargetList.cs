using Humason;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagePlanner
{
    public partial class FormTargetList : Form
    {
        private string currentTarget;
        private DateTime currentDate;

        public static RefreshEvent RefreshUpdateEvent = new RefreshEvent();

        public FormTargetList(string selectTarget)
        {
            InitializeComponent();
            currentTarget = selectTarget;
            RefreshUpdateEvent.RefreshEventHandler += RefreshTargetListHandler;
        }

        public void WriteTargetList()
        {
            Color bgd = TargetDataGrid.BackgroundColor;
            TargetDataGrid.BackgroundColor = Color.LightSalmon;
            int selTargetIndex = 0;
            //Load the Target Plan datagridview
            TargetDataGrid.Rows.Clear();
            int ridx = 0;

            this.Text = "Current Target List as of " + TimeManagement.CurrentTSXDate.ToShortDateString();

            //Fill in Humason target plans
            XFiles xf = new XFiles();
            if (xf != null)
            {
                List<string> tgtList = xf.GetTargetFiles();
                foreach (string tgt in tgtList)
                    if (!(tgt.Contains("Default")))
                    {
                        string tgtName = tgt.Split('.')[0];
                        TargetSpecs tt = new TargetSpecs(tgtName);
                        TargetDataGrid.Rows.Add();
                        int colIndx = 0;
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = tt.TargetName;
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = tt.Name2;
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = tt.TargetType;
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = (int)tt.MajorAxisF;    //dd         
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = (int)tt.MinorAxisF;    //dd
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = tt.RiseTime.ToString(@"hh\:mm");
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = tt.TransitTime.ToString(@"hh\:mm");
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = tt.SetTime.ToString(@"hh\:mm");
                        TargetDataGrid.Rows[ridx].Cells[colIndx++].Value = (int)tt.AltitudeF;     //dd           
                        if (tt.TargetName == currentTarget)
                            selTargetIndex = ridx;
                        ridx++;
                    }
            }
            TargetDataGrid.Update();
            TargetDataGrid.ClearSelection();
            TargetDataGrid.Rows[selTargetIndex].Selected = true;
            TargetDataGrid.BackgroundColor = bgd;
        }

        private void TargetDataGrid_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //Selection of a cell -- update image planner with new target via event
            //Causes the image planner form to be update with the current target name selected
            int selectedRowIndex;
            if (TargetDataGrid.SelectedRows.Count > 0 && e.RowIndex > 0)
            {
                selectedRowIndex = TargetDataGrid.SelectedRows[0].Index;
                int tgtNameColumn = 0;
                string targetName = TargetDataGrid.Rows[selectedRowIndex].Cells[tgtNameColumn].Value.ToString();
                TargetChangeEvent qpEvent = FormImagePlanner.QPUpdate;
                qpEvent.TargetChangeUpdate(targetName);
            }

        }

        #region Event Subscription

        public void RefreshTargetListHandler(object sender, RefreshEvent.RefreshEventArgs e)
        {
            //if (e.NewDate.ToShortDateString() != TimeManagement.CurrentTSXDate.ToShortDateString())
            //int cols = TargetDataGrid.Columns.Count;
            if (e.RefreshType != RefreshEvent.RefreshType.Target)
                WriteTargetList();
        }

        #endregion
    }
}
