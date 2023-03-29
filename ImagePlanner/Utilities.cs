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

        public static TimeSpan OffsetUTC()
        {
            //Determines difference in timespan between TSX time and UTC time based on TSX terrestrial location settings
            sky6StarChart tsxsc = new sky6StarChart();
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Time_Zone);
            TimeSpan tz = TimeSpan.FromHours(tsxsc.DocPropOut);
            tsxsc.DocumentProperty(Sk6DocumentProperty.sk6DocProp_DaylightSavingTimeIndex);
            double st = (double)tsxsc.DocPropOut;
            if (st == 0)
                return tz;
            else
                return (TimeSpan.FromHours(24 - st));
        }

        public static DateTime JulianToUTC(double jDAte)
        {
            return Celestial.JulianToDate(jDAte);
        }

        public static Boolean IsInDateRange(DateTime sessionDate, DateTime comparisonDate)
        {
            if (comparisonDate > sessionDate - TimeSpan.FromHours(12) && comparisonDate < sessionDate + TimeSpan.FromHours(12))
                return 
                    true;
            else 
                return false;
        }

        public static DateTime NextTransit(DateTime rightNow, double julDate, double periodDays)
        {
            DateTime firstTransit = JulianToUTC(julDate);
            TimeSpan span = rightNow - firstTransit;
            int cycles = (int)(span.TotalDays / periodDays) + 1;
            TimeSpan spanToNext = TimeSpan.FromDays(cycles * periodDays);
            DateTime nextTransit = firstTransit + spanToNext;
            return nextTransit;
        }

    }
}
