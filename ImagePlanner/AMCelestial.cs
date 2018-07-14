using System;
using System.Drawing;
using AstroMath;

namespace AstroChart
{
    public partial class SkyView
    {
        //Class of objects and methods for drawing celestial maps
        //  base class creates a filled circle that is used as the background
        //
        //All these methods create graphics on in cartesian coordinates, then
        //  map the points on to a spherical projection

        private Graphics gpho;
        private Rectangle skyMapRectangle;  //base rectangle for sky background
        private Point smCenter; //center of skymap in xy pixels
        private double smRadius; //radius of skymap in pixels
        private double smObsLatD; //observer's location in latitude
        private Point northPoint;  //location of top of map
        private Point southPoint;  //location of bottom of map
        private Point eastPoint; //location of left side of map
        private Point westPoint; //location of right side of map
        private Point northPole;   //location of north pole on sky map

        public SkyView(Graphics fcntl, Point centerPoint, float radius, float observersLatitude)

        {
            gpho = fcntl;
            smCenter = centerPoint;
            smRadius = radius;
            smObsLatD = observersLatitude;

            //Derive a upper left corner and size for a rectangle that defines
            //  a circle centered on centerpoint with a radius of radius
            //Builds the background image at centerPoint of radius 
            Point leftCorner = new Point((centerPoint.X - (int)radius), (centerPoint.Y - (int)radius));
            Size skyMapSize = new Size((int)(2 * radius), (int)(2 * radius));
            skyMapRectangle = new Rectangle(leftCorner, skyMapSize);
            //Generate a few reference points -- top and bottom (north and south points) and north pole
            northPoint = new Point((int)centerPoint.X, (int)(centerPoint.Y - radius));
            southPoint = new Point((int)centerPoint.X, (int)(centerPoint.Y + radius));
            westPoint = new Point((int)(centerPoint.X + radius), (int)centerPoint.Y);
            eastPoint = new Point((int)(centerPoint.X - radius), (int)centerPoint.Y);
            northPole = new Point((int)centerPoint.X, (int)(centerPoint.Y - (radius * Math.Cos(Transform.DegreesToRadians(observersLatitude)))));
            Rectangle east90 = new Rectangle(northPole, new Size(northPole.X - eastPoint.X, eastPoint.Y - northPole.Y));

            Brush blackBrush = new SolidBrush(Color.Navy);
            // Draw the map background.
            gpho.FillEllipse(blackBrush, skyMapRectangle);

            return;
        }

        public Point[] HourLine(double hourAngleH)
        {
            //Creates a set of points that define a line of longitude over the skyviewmap
            //Convert hour angle to spherical theta
            float hourAnglePhi = (float)Transform.HourAngleToPolarAngle(hourAngleH);
            float latR = (float)Transform.DegreesToRadians(this.smObsLatD);

            //Create set of 100 spherical points to use
            Polar3D.Polar3[] spts = new Polar3D.Polar3[100];
            //generate a line of constant rho and latitude that goes between startha and endha
            float deltaLatR = (float)((Math.PI) / spts.Length);
            float startLat = 0;
            for (int i = 0; i < spts.Length; i++)
            {
                float nextLatR = (float)((deltaLatR * i) + startLat);
                spts[i] = new Polar3D.Polar3((float)smRadius, nextLatR, hourAnglePhi);
                spts[i] = spts[i].RotateX(latR);
            }
            //Project this line onto the XY surface by spherical to cartesian conversion , then scrape off the z axis

            Point[] xyArc = Polar3D.ProjectXY(spts);
            for (int i = 0; i < xyArc.Length; i++)
            { xyArc[i].Offset(smCenter.X, smCenter.Y); }

            return xyArc;
        }

        public Point[] DecLine(double decAngleD)
        {
            //Creates a set of points that define a line of longitude over the skyviewmap
            //Convert hour angle to spherical theta
            //Declines will 
            float decAngleR = (float)Transform.DegreesToRadians(decAngleD);
            float latR = (float)Transform.DegreesToRadians(this.smObsLatD);

            //Create set of 100 spherical points to use
            Polar3D.Polar3[] spts = new Polar3D.Polar3[100];
            //generate a line of constant rho and latitude that goes between startha and endha
            //set the start of the great circle to -90 so all the X values are increasing for the DrawLine function.
            float deltaPhiR = (float)((2.0 * Math.PI) / spts.Length);
            float startPhi = -(float)(Math.PI / 2.0);

            for (int i = 0; i < spts.Length; i++)
            {
                float nextPhiR = (float)((deltaPhiR * i) + startPhi);
                spts[i] = new Polar3D.Polar3((float)smRadius, decAngleR, nextPhiR);
                spts[i] = spts[i].RotateX(latR);
            }
            //Project this line onto the XY surface by spherical to cartesian conversion , then scrape off the z axis
            Point[] xyArc = Polar3D.ProjectXY(spts);
            for (int i = 0; i < xyArc.Length; i++)
            { xyArc[i].Offset(smCenter.X, smCenter.Y); }
            return xyArc;
        }

        public Point[] TrackLine(double startHourAngleH, double endHourAngleH, double declinationD)
        {
            //Produces a line arc of integer X,Y points that are equidistant from the center

            //Start out with a set of spherical points which represent a great circle line on a sphere of
            //  of radius smradius
            //
            //Convert hour angle to spherical theta
            float startAnglePhi = (float)Transform.HourAngleToPolarAngle(startHourAngleH);
            float endAnglePhi = (float)Transform.HourAngleToPolarAngle(endHourAngleH);
            float decR = (float)Transform.DegreesToRadians(declinationD);
            float latR = (float)Transform.DegreesToRadians(this.smObsLatD);

            //Create set of 100 spherical points to use
            Polar3D.Polar3[] spts = new Polar3D.Polar3[100];
            //generate a line of constant rho and latitude that goes between startha and endha
            float deltaHr = (float)((Transform.NormalizeHours(endHourAngleH - startHourAngleH)) / spts.Length);
            for (int i = 0; i < spts.Length; i++)
            {
                double nextHaH = (float)((deltaHr * i) + startHourAngleH);
                float nextPhiR = (float)Transform.HourAngleToPolarAngle(nextHaH);
                spts[i] = new Polar3D.Polar3((float)smRadius, decR, nextPhiR);
                spts[i] = spts[i].RotateX(latR);
            }
            //Project this line onto the XY surface by spherical to cartesian conversion , then scrape off the z axis

            Point[] xyArc = Polar3D.ProjectXY(spts);
            for (int i = 0; i < xyArc.Length; i++)
            { xyArc[i].Offset(smCenter.X, smCenter.Y); }

            return xyArc;
        }

    }
}


