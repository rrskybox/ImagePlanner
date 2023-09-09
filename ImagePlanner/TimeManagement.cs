using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSky64Lib;
using AstroMath;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;

namespace ImagePlanner
{
    public partial class TimeManagement
    {

        private static TimeSpan? offsetHours = null;
        private static string? locationTSX = null;
        private static TimeSpan? localTimeZone = null;
        private static int dstIndex;

        public static DateTime LocalToUTCTime(DateTime localTime)
        {
            //Converts TSX location time to UTC
            return CorrectToDST(localTime) - (TimeSpan)OffsetUTC();
            //return localTime.ToUniversalTime();
        }

        public static DateTime UTCToLocalTime(DateTime utcTime)
        {
            //Converts UTC to TSX location time
            return CorrectToDST(utcTime + (TimeSpan)OffsetUTC());
            //return utcTime.ToLocalTime();
        }

        public static TimeSpan? OffsetUTC()
        {
            //Determines difference in timespan between TSX location time and UTC time based on TSX terrestrial location settings
            //Uses local nullable variable offsetHours to avoid querying TSX for every conversion -- that is very, very slow
            if (localTimeZone == null)
            {
                sky6StarChart tsxsc = new sky6StarChart();
                tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Time_Zone);
                localTimeZone = TimeSpan.FromHours(tsxsc.DocPropOut);
                tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_DaylightSavingTimeIndex);
                dstIndex = (int)tsxsc.DocPropOut;
            }
            return localTimeZone;
        }

        public static TimeSpan DSTCorrection(DateTime dt)
        {
            //Returns the number of hours to add to the input time dt to account for DST
            //
            //Daylight Saving Time (DST) in most of the United States starts on the second Sunday in March
            //and ends on the first Sunday in November. 
            //;Daylight Saving Options:
            // 0  DST_NOT_OBSERVED = Not observed
            // 1  DST_AUSTRALIA_NSW = Australia - NSW
            // 2  DST_AUSTRALIA_S = Australia - South
            // 3  DST_BRAZIL = Brazil
            // 4  DST_CHILE = Chile
            // 5  DST_CHINA = China
            // 6  DST_CUBA = Cuba
            // 7  DST_EGYPT = Egypt
            // 8  DST_EUROPE = Europe
            // 9  DST_FALKLAND = Falkland
            // 10  DST_GREENLAND = Greenland
            // 11  DST_IRAN = Iran
            // 12  DST_JORDAN = Jordan
            // 13  DST_KYRGYZSTAN = Kyrgyzstan
            // 14  DST_MOLDOVA = Moldova
            // 15  DST_NAMIBIA = Namibia
            // 16  DST_NEW_ZEALAND = New Zealand
            // 17  DST_N_AMERICA = U.S.and Canada
            // 18  DST_PARAGUAY = Paraguay
            // 19  DST_SYRIA = Syria
            // 20  DST_TASMANIA = Tasmania
            // 21  DST_UN_KINGDOM = United Kingdom
            // 22  DST_ISRAEL = Israel
            // 23  DST_MONGOLIA = Mongolia
            // 24  DST_PALESTINE = Palestine
            // 25  DST_FIJI = Fiji
            // 26  DST_TONGA = Tonga
            // 27  DST_MEXICO = Mexico

            switch (dstIndex)
            {
                case 0: //DST Not Observed
                    return TimeSpan.FromHours(0);
                case 8:  //Europe
                    return EuropeDSTAdjust(dt);
                case 17:  //US, Canada
                    return NorthAmericasDSTAdjust(dt);
                case 21:  //United Kingdom
                    return EuropeDSTAdjust(dt);
                default:  //All other regions
                    return EuropeDSTAdjust(dt);
            }
        }

        public static DateTime CorrectToDST(DateTime dt)
        {
            //Returns the number of hours to add to the input time dt to account for DST
            return dt += DSTCorrection(dt);
        }

        public static DateTime CorrectFromDST(DateTime dt)
        {
            //Returns the number of hours to add to the input time dt to account for DST
            return dt -= DSTCorrection(dt);
        }

        private static TimeSpan NorthAmericasDSTAdjust(DateTime dt)
        {
            //return adjusted date time for DST in us and canada
            DateTime secondMarchSunday;
            DateTime firstNovSunday;

            DateTime firstMarchDate = new DateTime(dt.Year, 3, 1);
            DayOfWeek firstMarchDay = firstMarchDate.DayOfWeek;
            if (firstMarchDay != DayOfWeek.Sunday)
                secondMarchSunday = firstMarchDate + TimeSpan.FromDays(14 - (int)firstMarchDay);
            else
                secondMarchSunday = firstMarchDate + TimeSpan.FromDays(7);
            DateTime firstNovDate = new DateTime(dt.Year, 11, 1);
            DayOfWeek firstNovDay = firstNovDate.DayOfWeek;
            if (firstNovDay != DayOfWeek.Sunday)
                firstNovSunday = firstNovDate + TimeSpan.FromDays(7 - (int)firstNovDay);
            else
                firstNovSunday = firstNovDate;
            if (dt >= secondMarchSunday && dt < firstNovDate)
                return TimeSpan.FromHours(1);
            else
                return TimeSpan.FromHours(0);

        }

        private static TimeSpan EuropeDSTAdjust(DateTime dt)
        {
            //return adjusted date time for Europe DST
            //The Daylight Saving Time (DST) period in Europe runs
            //from 01:00 UTC (Coordinated Universal Time) on the
            //last Sunday of March to 01:00 UTC on the last Sunday of October
            //every year.

            DateTime firstNovDate = new DateTime(dt.Year, 11, 1, 1, 0, 0);
            DateTime firstAprilDate = new DateTime(dt.Year, 04, 1, 1, 0, 0);
            DateTime lastMarchSunday;
            DateTime lastOctoberSunday;

            DayOfWeek firstAprilDay = firstAprilDate.DayOfWeek;
            if (firstAprilDay == DayOfWeek.Sunday)
                lastMarchSunday = firstAprilDate - TimeSpan.FromDays(7);
            else
                lastMarchSunday = firstAprilDate - TimeSpan.FromDays(14 + (int)firstAprilDay);
            DayOfWeek firstNovDay = firstNovDate.DayOfWeek;
            if (firstNovDay == DayOfWeek.Sunday)
                lastOctoberSunday = firstNovDate - TimeSpan.FromDays(7);
            else
                lastOctoberSunday = firstNovDate - TimeSpan.FromDays(14 + (int)firstAprilDay);
            if (dt >= lastMarchSunday && dt < firstNovDate)
                return TimeSpan.FromHours(1);
            else
                return TimeSpan.FromHours(0);
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
            return CorrectFromDST(Celestial.JulianToDate(jDAte));
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
            if (localDate.Hour < 12)
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
