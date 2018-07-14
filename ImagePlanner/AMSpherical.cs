using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AstroMath
{
    public partial class Polar3D
    {
        public class Polar3
        {
            private float rho;      //radius
            private float theta;    //Latitude
            private float phi;      //Longitude

            public Polar3()
            { }

            public Polar3(float rhoVal, float thetaVal, float phiVal)
            {
                this.rho = rhoVal;
                this.theta = thetaVal;
                this.phi = phiVal;
            }

            public Polar3(Point3 cpt)
            {
                //Converts cartesian to spherical coordinates
                //REF: https://rbrundritt.wordpress.com/2008/10/14/conversion-between-spherical-and-cartesian-coordinates-systems/
                //function convertCartesianToSpherical(cartesian)
                //    var r = sqrt(x^2+y^2+z^2)
                //    var theta = arcos(z/r)
                //    var phi = atan(y/x);

                this.rho = (float)Math.Sqrt((cpt.X * cpt.X) + (cpt.Y * cpt.Y) + (cpt.Z * cpt.Z));

                this.theta = (float)Math.Acos((((float)cpt.Z)) / rho);
                this.phi = (float)Math.Atan2(((float)(cpt.Y)), (float)cpt.X);
            }

            public float Rho
            {
                get { return this.rho; }
                set { this.rho = value; }
            }
            public float Theta
            {
                get { return this.theta; }
                set { this.theta = value; }
            }
            public float Phi
            {
                get { return this.phi; }
                set { this.phi = value; }
            }

            public Polar3 RotateX(float rotationR)
            {
                Point3 cpt = new Point3(this);
                float r = (float)Math.Sqrt((cpt.Y * cpt.Y) + (cpt.Z * cpt.Z));
                float ang = (float)Math.Atan2(cpt.Z, cpt.Y);
                cpt.Y = (float)(r * Math.Cos(ang + rotationR));
                cpt.Z = (float)(r * Math.Sin(ang + rotationR));
                Polar3 spt = new Polar3(cpt);
                return spt;
            }
        }

        public class Point3
        {
            private float px;
            private float py;
            private float pz;

            public Point3()
            { }

            public Point3(float ex, float ey, float ez, float ew)
            {
                px = ex;
                py = ey;
                pz = ez;
            }

            public Point3(Polar3 sph)
            {
                //Converts spherical to cartesian coordinates
                //REF: https://rbrundritt.wordpress.com/2008/10/14/conversion-between-spherical-and-cartesian-coordinates-systems/
                //function convertSphericalToCartesian(lat,long)
                //    var x = r * sin theta * cos phi
                //    var y = r * sin theta * sin phi
                //    var z = r * cos theta;

                this.px = (float)(sph.Rho * Math.Sin(sph.Theta) * Math.Cos(sph.Phi));
                this.py = (float)(sph.Rho * Math.Sin(sph.Theta) * Math.Sin(sph.Phi));
                this.pz = (float)(sph.Rho * Math.Cos(sph.Theta));
            }

            public float X
            {
                get { return this.px; }
                set { this.px = value; }
            }
            public float Y
            {
                get { return this.py; }
                set { this.py = value; }
            }
            public float Z
            {
                get { return this.pz; }
                set { this.pz = value; }
            }

            public Point3 RotateX(float thetaRot)
            {
                //Rotate X,Y,Z coordinates around origin by spherical angles
                Polar3 spt = new Polar3(this.X, this.Y, this.Z);
                spt.RotateX(thetaRot);
                Point3 cpt = new Point3(spt);
                return cpt;
            }

        }

        #region Common Methods

        public static Point[] ProjectXY(Polar3[] spts)
        {
            //Projects set of spherical coordinates into an XY plane
            //  where rho is the radius or 1/2 X and 1/2 Y dimension
            //  where theta is the Altitude
            //  where phi is the Azimuth
            //
            //  Convert spherical points to cartesian points, then scrape off the z axis
            //
            //  Going to do this the hard way to get rid of points that should be invisible
            //First, convert all the spherical points to cartesian
            Point[] xypts = new Point[spts.Length];
            int visCount = 0;
            for (int i = 0; i < spts.Length; i++)
            {
                Point3 xyz = new Point3(spts[i]);
                if (xyz.Z >= 0)
                {
                    xypts[visCount] = new Point((int)xyz.X, (int)xyz.Y);
                    visCount++;
                }
            }
            //Create a new set of points of length visCount
            Point[] visXYpoints = new Point[visCount];
            //Load it up with the positive Z points
            for (int i = 0; i < visXYpoints.Length; i++)
            {
                visXYpoints[i] = xypts[i];
            }
            return visXYpoints;
        }

        #endregion
    }
}
