using AstroChart;
using AstroMath;
using System;
using System.Drawing;
using System.Windows.Forms;
using TheSky64Lib;

namespace ImagePlanner
{
    public partial class FormTargetTrack : Form
    {
        const int skymapRadius = 100;

        private string targetName;
        private DateTime tgtDateUTC;
        private DateTime moonDateUTC;
        private double tgtUpH;
        private double tgtDownH;
        private Celestial.RADec tgtPosition;
        private Celestial.LatLon obsLocation;
        private Celestial.RADec MoonPosition;
        private double tgtDecD;
        private double tgtTransitH;
        private double obsLatD;
        private double moonRiseH;
        private double moonSetH;
        private double moonDecD;

        public FormTargetTrack(DailyPosition dp, DailyPosition mp, string targetNameS)
        {
            InitializeComponent();
            targetName = targetNameS;

            tgtDateUTC = dp.UTCdate;
            moonDateUTC = mp.iRise;
            tgtUpH = TimeManagement.UTCToLocalTime(dp.Rising).Hour + (TimeManagement.UTCToLocalTime(dp.Rising).Minute / 60.0);
            tgtDownH = TimeManagement.UTCToLocalTime(dp.Setting).Hour + (TimeManagement.UTCToLocalTime(dp.Setting).Minute / 60.0);
            tgtDecD = 90.0 - Transform.RadiansToDegrees(dp.Position.Dec);
            tgtPosition = dp.Position;
            MoonPosition = mp.Position;
            obsLocation = dp.Location;
            //Set lat 
            obsLatD = Transform.RadiansToDegrees(dp.Location.Lat);

            //Moon look up stuff
            Celestial.RADec moonRADec = DailyPosition.MoonRaDec(Celestial.DateToJ2kC(dp.UTCdate));
            moonDecD = 90 - Transform.RadiansToDegrees(mp.Position.Dec);
            //Get the rise/set times from TSX
            sky6StarChart tsxs = new sky6StarChart();
            sky6ObjectInformation tsxo = new sky6ObjectInformation();
            //Set the date/time to the local date for the target
            tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow, Celestial.DateToJulian(tgtDateUTC));

            //Get some target stuff that's hard to calculate
            tsxs.Find(targetName);
            ////wait a second
            //System.Threading.Thread.Sleep(500);
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_TRANSIT_TIME);
            tgtTransitH = tsxo.ObjInfoPropOut;

            //Test stuff
            //double testmoontransitH = MoonPosition.TransitTime(tgtDateUTC, obsLocation);

            //Get some moon stuff now
            tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow, Celestial.DateToJulian(moonDateUTC));
            tsxs.Find("Moon");
            ////wait a second
            //System.Threading.Thread.Sleep(500);
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RISE_TIME);
            moonRiseH = tsxo.ObjInfoPropOut;
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_SET_TIME);
            moonSetH = tsxo.ObjInfoPropOut;
            //put the target back in
            tsxs.Find(targetName);

            tsxs = null;
            tsxo = null;
            return;
        }

        public void ShowTrack()
        {
            // Create a Graphics object for the Control.
            Point center = new Point(125, 125);
            double declination = tgtDecD;
            double startHa = tgtUpH;
            double endHa = tgtDownH;

            Graphics g = TrackPanel.CreateGraphics();
            SkyView sv = new SkyView(g, center, (float)skymapRadius, (float)(90.0 - obsLatD));

            //Create pens for drawing 
            Pen whitePen = new Pen(Color.White, 1.0F);
            whitePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
            Pen redPen = new Pen(Color.Red, 4.0F);
            redPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
            Pen orangePen = new Pen(Color.Orange, 2.0F);
            orangePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
            Pen yellowPen = new Pen(Color.Yellow, 4.0F);
            yellowPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
            Pen greenPen = new Pen(Color.LightSeaGreen, 2.0F);
            greenPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;

            //Draw hour angle lines (longitude)
            for (int iHour = 0; iHour < 24; iHour++)
            {
                g.DrawCurve(whitePen, sv.HourLine(iHour));
            }

            //Draw declination lines (latitude) -- some lines could be empty because of xy projection
            for (int iLat = 0; iLat < 18; iLat++)
            {
                Point[] latLines = sv.DecLine(iLat * 10);
                int asz = latLines.Length;

                if (asz > 1)
                {
                    g.DrawLines(whitePen, latLines);
                }
            }
            //Draw equator
            Point[] equLine = sv.DecLine(90.0);
            g.DrawLines(greenPen, equLine);

            //Draw transit hour
            //double transitH = AstroMath.NormalizeHours(tgtPosition.TransitTime(tgtDateUTC, tgtLocation));
            g.DrawCurve(orangePen, sv.HourLine(tgtTransitH));

            //Draw target path
            Point[] trackPts = sv.TrackLine(startHa, endHa, declination);
            if (trackPts.Length > 3)
            {
                int tjumpIndx = ContinuityCheck(trackPts);
                if (tjumpIndx == 0)
                {
                    if (trackPts.Length > 3)
                    { g.DrawCurve(redPen, trackPts); }
                }
                else
                {
                    if (tjumpIndx > 3)
                    { g.DrawCurve(redPen, trackPts, 0, (tjumpIndx - 2), 1.0F); }
                    if ((trackPts.Length - tjumpIndx) > 3)
                    { g.DrawCurve(redPen, trackPts, (tjumpIndx + 1), (trackPts.Length - tjumpIndx - 2), 1.0F); }
                }
            }
            //Draw moon path
            Point[] moonPts = sv.TrackLine(moonRiseH, moonSetH, moonDecD);
            if (moonPts.Length > 3)
            {
                int mjumpIndx = ContinuityCheck(moonPts);
                if (mjumpIndx == 0)
                {
                    if (moonPts.Length > 3)
                    { g.DrawCurve(yellowPen, moonPts); }
                }
                else
                {
                    if (mjumpIndx > 3)
                    { g.DrawCurve(yellowPen, moonPts, 0, (mjumpIndx - 2), 1.0F); }
                    if ((moonPts.Length - mjumpIndx) > 3)
                    { g.DrawCurve(yellowPen, moonPts, (mjumpIndx + 1), (moonPts.Length - mjumpIndx - 2), 1.0F); }
                }
            }
            int thour = (int)tgtTransitH;
            int tmin = ((int)(tgtTransitH - thour)) * 60;
            string transitText = thour.ToString("00")  + tmin.ToString("00");
            this.Text = targetName + " Track <E-W> Transit @ " + transitText;
            return;

        }

        private int ContinuityCheck(Point[] checkPoints)
        //Runs through the list looking for a discontinuity -- i.e. big negative jump in X
        //  if so, the index of where the negative jump starts is returned, otherwise 0
        {
            Point lastX = checkPoints[0];
            for (int i = 0; i < checkPoints.Length; i++)
            {
                if (checkPoints[i].X < lastX.X)
                { return i; }
            }
            return 0;
        }
    }
}

