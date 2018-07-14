using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AstroMath
{
    class Formatters
    {

        public static string HourString(double dvalue)
        //Converts a double value (dvalue) to a string looking like an hour:minutes
        {
            int hr = (int)Math.Truncate(dvalue);
            int min = (int)Math.Truncate((dvalue - hr) * 60);
            return (hr.ToString() + ":" + min.ToString());
        }

         //public static bool AzRangeCheck(double LeftAz, double RightAz, double AzR)
        //{
        //    //returns true if Az is between Upper and Lower Az, which is tricky because of circular nature of angles
        //    if (LeftAz >= RightAz)
        //    {
        //        if ((RightAz <= AzR) && (AzR <= LeftAz))
        //        { return true; }
        //        else
        //        { return false; }
        //    }
        //    else //LeftAz <= RightAz
        //    {
        //        if ((LeftAz <= AzR) && (AzR <= RightAz))
        //        { return false; }
        //        else
        //        { return true; }
        //    }
        //}

        // //public static double NormalizeHours(TimeSpan hours)
        //{ 
        //                return (((hours.TotalHours) + 24.0) % 24.0);
        //}


        //public static double NormalizeAngle(double angle)
        //{
        //    int newangle = (int)(angle * (180 / Math.PI));
        //    if (angle < 0)
        //    { newangle = newangle + 360; }
        //    if (newangle > 360)
        //    {
        //        newangle = newangle % 360;
        //    }
        //    return (DegToRad(newangle));
        //}

        //public static double DegToRad(double Deg)
        //{
        //    return (Deg * Math.PI / 180);
        //}


        //public static double RadToDeg(double Rad)
        //{
        //    return (Rad * 180 / Math.PI);
        //}

        //public static double CanvasToAzimuth(double angle)
        //{
        //    return NormalizeAngle(angle + DegToRad(90));
        //}

        //public static double AzimuthToCanvas(double angle)
        //{
        //    return NormalizeAngle(angle + DegToRad(270));
        //}

    }
}
