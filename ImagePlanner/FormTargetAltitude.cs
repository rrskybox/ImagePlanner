using AstroMath;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImagePlanner
{
    public partial class FormTargetAltitude : Form
    {
        public FormTargetAltitude(DailyPosition dp, DailyPosition mp, string targetName, string moonDataDescription)
        {
            //Windows Form to show path of target over one imaging session (one night) in graphical form
            InitializeComponent();
            string tName = targetName;
            DateTime localtoday = TimeManagement.UTCToLocalTime(dp.UTCdate);
            DateTime localtomorrow = TimeManagement.UTCToLocalTime(dp.UTCdate).AddDays(1);
            this.Text = tName + ": " + "Altitude on night of " + localtoday.ToString("MMM dd") + " / " + localtomorrow.ToString("MMM dd");

            if (dp.Visibility == DailyPosition.VisibilityState.UpNever)
            { //Cant get a rise out of it
                return;
            }
            else
            {
                //Graph altitude for both target and moon with the start and end set by the target duration
                GraphAltitude(dp.Rising, dp.Setting, dp.Position, dp.Location, "AltitudePath", Color.AliceBlue);
                GraphAltitude(dp.Rising, dp.Setting, mp.Position, dp.Location, "MoonPath", Color.Yellow);
            }
            string lineBreak = "\r\n" + "Moon Phases";
            this.MoonDataTextBox.Text = moonDataDescription.Replace("\r\n", "     ");
            this.MoonDataTextBox.Text = MoonDataTextBox.Text.Replace("Moon Phase", lineBreak);
        }

        private void GraphAltitude(DateTime gStart, DateTime gEnd, Celestial.RADec gRaDec, Celestial.LatLon gloc, string gName, Color gcolor)
        {
            //Graph the altitude change on 10 min intervals between gstart and gend for the position gRaDec for th observer at location gloc
            const int gPoints = 60;  //number of points to graph

            double gInterval = (gEnd - gStart).TotalHours / gPoints;
            DateTime gTime = gStart;
            while (gTime <= gEnd)
            {
                double haR = gRaDec.HourAngle(gTime, gloc);
                double haH = Transform.RadiansToHours(haR);
                double altR = gRaDec.Altitude(haR, gloc);
                double altitude = Transform.RadiansToDegrees(altR);
                //
                double gst = Celestial.DateUTCToGST(gTime);
                double lst = Celestial.GSTToLST(gst, gloc.Lon);
                //
                if (altitude > 0)
                {
                    DateTime localTime = TimeManagement.UTCToLocalTime(gTime);
                    AltitudeChart.Series[gName].Points.AddXY(localTime, altitude);
                }
                //If (TargetControl.IsMoonUp(ImageForecastForm.tgtdata, ImageForecastForm.moondata, gTime))) Then
                //    AltitudeChart.Series("AltitudePath").Points.
                gTime = gTime.AddHours(gInterval);
            }
            return;
        }
    }
}

