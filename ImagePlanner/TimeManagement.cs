using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSky64Lib;
using AstroMath;
using System.Drawing;

namespace ImagePlanner
{
    public partial class TimeManagement
    {

        private static TimeSpan? offsetHours;
        private static string? locationTSX;

        public static DateTime LocalToUTCTime(DateTime localTime)
        {
            //Converts TSX location time to UTC
            return (localTime - OffsetUTC());
            //return localTime.ToUniversalTime();
        }

        public static DateTime UTCToLocalTime(DateTime utcTime)
        {
            //Converts UTC to TSX location time
            return (utcTime + OffsetUTC());
            //return utcTime.ToLocalTime();
        }

        public static TimeSpan OffsetUTC()
        {
            //Determines difference in timespan between TSX location time and UTC time based on TSX terrestrial location settings
            //Uses local nullable variable offsetHours to avoid querying TSX for every conversion -- that is very, very slow
            if (offsetHours == null)
            {
                DateTime julDateTime = JulianDateNow();
                DateTime utcNow = DateTime.UtcNow;
                offsetHours = TimeSpan.FromHours((julDateTime-utcNow).Hours);
            }
            return (TimeSpan)offsetHours;
        }

        public static DateTime JulianDateNow()
        {
            //Returns the current Julian Date of the Star Chart
            sky6Utils tsxu = new sky6Utils();
            tsxu.ComputeJulianDate();
            double julDate = tsxu.dOut0;
            tsxu.ConvertJulianDateToCalender(julDate);
            DateTime julDateTime = new DateTime((int)tsxu.dOut0, (int)tsxu.dOut1, (int)tsxu.dOut2, (int)tsxu.dOut3, (int)tsxu.dOut4, (int)tsxu.dOut5);
            return (julDateTime);
        }

        public static string LocateTSX()
        {
            //Determines the current configured location for the TSX instantiation
            sky6StarChart tsxsc = new sky6StarChart();
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_LocationDescription);
            string loc = tsxsc.DocPropOut;
            return loc;
        }

        public static DateTime JulianToUTC(double jDAte)
        {
            return Celestial.JulianToDate(jDAte);
        }

        public static bool IsBetweenDuskAndDawn(DateTime localDuskDate, DateTime localDawnDate, DateTime localDate)
        {
            if (localDate >= localDuskDate && localDate <= localDawnDate)
                return true;
            else
                return false;
        }

        //Note on Session Date
        //  A Session Date is the date starting at 12 noon and ending at 11:59AM the next day.
        public static DateTime DateToSessionDate(DateTime localDate)
        {
            DateTime sessionDate = localDate.Date;
            if (localDate < sessionDate - TimeSpan.FromHours(12))
                return sessionDate - TimeSpan.FromDays(1);
            else
                return sessionDate;
        }

        //Note on Session Date
        //  A Session Date is the date starting at 12 noon and ending at 11:59AM the next day.
        public static Boolean IsInSessionRange(DateTime sessionDate, DateTime comparisonDate)
        {
            //session Date is a DateTime for mm/dd/yyyy at 00:00
            //  scrub it just in case
            sessionDate = sessionDate.Date;
            if (comparisonDate >= sessionDate + TimeSpan.FromHours(12) && comparisonDate < sessionDate + TimeSpan.FromHours(36))
                return true;
            else
                return false;
        }

        public static DateTime NextTransitUTC(DateTime rightNowUTC, double firstTransitJD, double transitPeriodDays)
        {
            DateTime firstTransit = JulianToUTC(firstTransitJD);
            TimeSpan span = rightNowUTC - firstTransit;
            int cycles = (int)(span.TotalDays / transitPeriodDays) + 1;
            TimeSpan spanToNext = TimeSpan.FromDays(cycles * transitPeriodDays);
            DateTime nextTransit = firstTransit + spanToNext;
            return nextTransit;
        }

        public static double ComputeAltitude(DateTime timeLocal, double RAHours, double DecDegrees, double latitudeDegrees, double longitudeDegrees)
        // Returns maximum altitude for object at RA/Dec between times dusk and dawn
        {
            //Note: MaxAltitude expects the location longitude in positive East.  TSX is otherwise, so longitude has to be inverted.
            Celestial.LatLon location = new Celestial.LatLon(Transform.DegreesToRadians(latitudeDegrees), Transform.DegreesToRadians(-longitudeDegrees));
            Celestial.RADec position = new Celestial.RADec(Transform.HoursToRadians(RAHours), Transform.DegreesToRadians(DecDegrees));
            DateTime timeUTC = TimeManagement.LocalToUTCTime(timeLocal);
            double maxAlt = Transform.RadiansToDegrees(position.Altitude((position.HourAngle(timeUTC, location)), location));
            return maxAlt;
        }

        public static double ComputeMaxAltitude(DateTime startLocal, DateTime endLocal, double RAHours, double DecDegrees, double latitudeDegrees, double longitudeDegrees)
        // Returns maximum altitude for object at RA/Dec between times dusk and dawn
        {
            //Note: MaxAltitude expects the location longitude in positive East.  TSX is otherwise, so longitude has to be inverted.
            Celestial.LatLon location = new Celestial.LatLon(Transform.DegreesToRadians(latitudeDegrees), Transform.DegreesToRadians(-longitudeDegrees));
            Celestial.RADec position = new Celestial.RADec(Transform.HoursToRadians(RAHours), Transform.DegreesToRadians(DecDegrees));
            DateTime duskUTC = TimeManagement.LocalToUTCTime(startLocal);
            DateTime dawnUTC = TimeManagement.LocalToUTCTime(endLocal);
            double maxAlt = Transform.RadiansToDegrees(AstroMath.DailyPosition.MaxAltitude(duskUTC, dawnUTC, position, location));
            return maxAlt;
        }
        public static double ComputeMinAltitude(DateTime startLocal, DateTime endLocal, double RAHours, double DecDegrees, double latitudeDegrees, double longitudeDegrees)
        // Returns maximum altitude for object at RA/Dec between times dusk and dawn
        {
            //Note: MaxAltitude expects the location longitude in positive East.  TSX is otherwise, so longitude has to be inverted.
            Celestial.LatLon location = new Celestial.LatLon(Transform.DegreesToRadians(latitudeDegrees), Transform.DegreesToRadians(-longitudeDegrees));
            Celestial.RADec position = new Celestial.RADec(Transform.HoursToRadians(RAHours), Transform.DegreesToRadians(DecDegrees));
            DateTime startUTC = TimeManagement.LocalToUTCTime(startLocal);
            DateTime endUTC = TimeManagement.LocalToUTCTime(endLocal);
            double maxAlt = Transform.RadiansToDegrees(AstroMath.DailyPosition.MinAltitude(startUTC, endUTC, position, location));
            return maxAlt;
        }

    }
}
