using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace AstroMath
{
    public partial class Planar
    {
        public class QuadRoot
        {
            //Structure for returning quadratic root results
            public int nz;
            public double xe;
            public double ye;
            public double zero1;
            public double zero2;

            public QuadRoot()
            {
                nz = 0;  //Number of roots within the interval [-1,1]
                xe = 0;  //Extreme value of X in parabola solution
                ye = 0;  //Extreme value of Y in parabola solution
                zero1 = 0;  //First root within [-1,1] for NZ = 1,2
                zero2 = 0;  //Second root within [-1,1] for NZ = 2
            }
        }

        public static QuadRoot Quad(double yminus, double yzero, double yplus)
        {

            //Finds a parabola through three points 
            //   (-1,yminus), (0,yzero), (1,yplus)
            //   that do not lie on a straight line.

            double a, b, c, dis, dx;
            QuadRoot qr = new QuadRoot();

            qr.nz = 0;
            a = 0.5 * (yminus + yplus) - yzero;
            b = 0.5 * (yplus - yminus);
            c = yzero;
            qr.xe = -b / (2 * a);
            qr.ye = ((a * qr.xe + b) * qr.xe) + c;
            dis = Math.Pow(b, 2) - (4 * a * c);
            if (dis >= 0)
            {
                dx = 0.5 * Math.Sqrt(dis) / Math.Abs(a);
                qr.zero1 = qr.xe - dx;
                qr.zero2 = qr.xe + dx;
                if (Math.Abs(qr.zero1) <= 1)
                {
                    qr.nz = qr.nz + 1;
                }
                if (Math.Abs(qr.zero2) <= 1)
                {
                    qr.nz = qr.nz + 1;
                }
                if (qr.zero1 < -1)
                {
                    qr.zero1 = qr.zero2;
                }
            }
            return qr;
        }

        public static double Frac(double x)
        {
            //   returns fraction Of A less than 1 As positive value
            if (x < 0)
            { x = Math.Abs(x - (int)x); }
            else
            { x = x - (int)x; }
            if (x < 0)
            { return x + 1; }
            else
            { return x; }
        }

        public static Point ThirdPoint(Point C, double circleradius, double Alpha, double ht)
        {
            //Calculates the coordinations (point) for the third point of a isocolese triangle with
            // a height of ht and rotated to an angle (radians)
            double P = Math.Sqrt(Math.Pow(ht, 2) + Math.Pow((circleradius / 2), 2));
            double Beta = Math.Sin(ht / P);
            Point T = new Point((int)(C.X + P * Math.Cos(Alpha + Beta)), (int)(C.Y + P * Math.Sin(Alpha + Beta)));
            return T;
        }

        public static int TimeMachine(DateTime u, DateTime d, DateTime r1, DateTime s1, DateTime r2, DateTime s2, DateTime r3, DateTime s3)
        {
            //computes difference sets for tgt and sun intersections
            //  return a value for each type of intersection, return 0 if something doesn//t match up
            int stype = 0;
            if (u < r1)
            {
                if (d < r1)
                {
                    stype = 1;
                }
                else if (r1 <= d && d <= s1)
                {
                    stype = 2;
                }
                else if (s1 <= d && d <= r2)
                {
                    stype = 3;
                }
                else if (r2 <= d && d <= s2)
                {
                    stype = 4;
                }
                else if (s2 <= d && d <= r3)
                {
                    stype = 5;
                }
                else if (r3 <= d && d <= s3)
                {
                    stype = 6;
                }
                else if (d > s3)
                {
                    stype = 7;
                }
            }
            else if (r1 <= u && u <= s1)
            {
                if (u <= d && d <= s1)
                {
                    stype = 8;
                }
                else if (s1 <= d && d <= r2)
                {
                    stype = 9;
                }
                else if (r2 <= d && d <= s2)
                {
                    stype = 10;
                }
                else if (s2 <= d && d <= r3)
                {
                    stype = 11;
                }
                else if (r3 <= d && d <= s3)
                {
                    stype = 12;
                }
                else if (d > s3)
                {
                    stype = 13;
                }
            }
            else if (s1 <= u && u <= r2)
            {
                if (u <= d && d <= r2)
                {
                    stype = 14;
                }
                else if (r2 <= d && d <= s2)
                {
                    stype = 15;
                }
                else if (s2 <= d && d <= r3)
                {
                    stype = 16;
                }
                else if (r3 <= d && d <= s3)
                {
                    stype = 17;
                }
                else if (d > s3)
                {
                    stype = 18;
                }
            }
            else if (r2 <= u && u <= s2)
            {
                if (u <= d && d <= s2)
                {
                    stype = 19;
                }
                else if (s2 <= d && d <= r3)
                {
                    stype = 20;
                }
                else if (r3 <= d && d <= s3)
                {
                    stype = 21;
                }
                else if (d > s3)
                {
                    stype = 22;
                }
            }
            else if (s2 <= u && u <= r3)
            {
                if (u <= d && d <= r3)
                {
                    stype = 23;
                }
                else if (r3 <= d && d <= s3)
                {
                    stype = 24;
                }
                else if (d > s3)
                {
                    stype = 25;
                }
            }
            else if (r3 <= u && u <= s3)
            {
                if (u <= d && d <= s3)
                {
                    stype = 26;
                }
                else if (d > s3)
                {
                    stype = 27;
                }
            }
            else if (s3 < u)
            {
                stype = 28;
            }
            return stype;
        }

        public static int LongestPeriod(TimeSpan a, TimeSpan b, TimeSpan c)
        {
            //returns a value, 1, 2, or 3 according to which parameter is the longest timespan
            if (a >= b && a >= c)
            {
                return 1;
            }
            else if (b >= a && b >= c)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public static TimeSpan LongestInterval(TimeSpan i1, TimeSpan i2)
        {
            //return the longest of two timespans
            if (i1.TotalHours > i2.TotalHours)
            {
                return i1;
            }
            else
            {
                return i2;
            }
        }

        public static Point LocationOffset(Point Center, double Diameter)
        { //Determines the upper left corner offset of a circle drawing from it//s center, based on angle
            Center.X = (int)(Center.X - Diameter / 2);
            Center.Y = (int)(Center.Y - Diameter / 2);
            return Center;
        }

    }
}
