using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSky64Lib;
using AstroMath;

namespace ImagePlanner
{
    public partial class Utilities
    {

        public static DateTime LocalToUTCTime(DateTime localTime)
        {
            //Converts TSX location time to UTC
            //return (localTime - OffsetUTC());
            return localTime.ToUniversalTime();
        }

        public static DateTime UTCToLocalTime(DateTime utcTime)
        {
            //Converts UTC to TSX location time
            //return (utcTime + OffsetUTC());
            return utcTime.ToLocalTime();
        }

        public static TimeSpan OffsetUTC()
        {
            //Determines difference in timespan between TSX location time and UTC time based on TSX terrestrial location settings
            sky6StarChart tsxsc = new sky6StarChart();
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Time_Zone);
            TimeSpan tz = TimeSpan.FromHours(tsxsc.DocPropOut);
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_DaylightSavingTimeIndex);
            double st = (double)tsxsc.DocPropOut;
            if (st == 0)
                return tz;
            else
                return (TimeSpan.FromHours(st - 24));
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
            DateTime timeUTC = timeLocal.ToUniversalTime();
            double maxAlt = Transform.RadiansToDegrees(position.Altitude((position.HourAngle(timeUTC, location)), location));
            return maxAlt;
        }

        public static double ComputeMaxAltitude(DateTime duskLocal, DateTime dawnLocal, double RAHours, double DecDegrees, double latitudeDegrees, double longitudeDegrees)
        // Returns maximum altitude for object at RA/Dec between times dusk and dawn
        {
            //Note: MaxAltitude expects the location longitude in positive East.  TSX is otherwise, so longitude has to be inverted.
            Celestial.LatLon location = new Celestial.LatLon(Transform.DegreesToRadians(latitudeDegrees), Transform.DegreesToRadians(-longitudeDegrees));
            Celestial.RADec position = new Celestial.RADec(Transform.HoursToRadians(RAHours), Transform.DegreesToRadians(DecDegrees));
            DateTime duskUTC = duskLocal.ToUniversalTime();
            DateTime dawnUTC = dawnLocal.ToUniversalTime();
            double maxAlt = Transform.RadiansToDegrees(AstroMath.DailyPosition.MaxAltitude(duskUTC, dawnUTC, position, location));
            return maxAlt;
        }

        public static double ComputeMinAltitude(DateTime duskLocal, DateTime dawnLocal, double RAHours, double DecDegrees, double latitudeDegrees, double longitudeDegrees)
        // Returns maximum altitude for object at RA/Dec between times dusk and dawn
        {
            //Note: MaxAltitude expects the location longitude in positive East.  TSX is otherwise, so longitude has to be inverted.
            Celestial.LatLon location = new Celestial.LatLon(Transform.DegreesToRadians(latitudeDegrees), Transform.DegreesToRadians(-longitudeDegrees));
            Celestial.RADec position = new Celestial.RADec(Transform.HoursToRadians(RAHours), Transform.DegreesToRadians(DecDegrees));
            DateTime startUTC = duskLocal.ToUniversalTime();
            DateTime endUTC = dawnLocal.ToUniversalTime();
            double maxAlt = Transform.RadiansToDegrees(AstroMath.DailyPosition.MinAltitude(startUTC, endUTC, position, location));
            return maxAlt;
        }


    }
}
