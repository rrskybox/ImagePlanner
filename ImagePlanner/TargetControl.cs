using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstroMath;

namespace ImagePlanner
{
    public partial class TargetControl
    {
        //public methods for managing the daily position functions
        //SunCycle generates a year of sunrise and sunset events
        //TargetCycle generates a year of target rise and set events (within sunrise-sunset)
        //MoonCycle generates a year of moon rise and set events (within target rise-set)
        //MoonPhase adds phase to target events
        //Moonclear adds moonless percentage to target events

        public static DailyPosition[] SunCycle(int dYear, Celestial.LatLon obsLocation)
        {
            //Calculates a year//s worth of sunrises and sunsets
            const double twilight = -18; //(degrees)

            DailyPosition[] sunspots = new DailyPosition[367];
            Celestial.RADec sunpos;

            DateTime ndate = new DateTime(dYear, 1, 1, 0, 0, 0);     //create datetime object for jan 1, dYear
            DateTime udate = ndate.ToUniversalTime();                //convert to UTC
            DateTime sdate = udate.AddDays(-1);                 //back up one day to make sure Jan 1 is covered

            for (int dayidx = 0; dayidx < sunspots.Length; dayidx++)
            {
                DateTime tdate = sdate.AddDays(dayidx);
                sunpos = DailyPosition.SunRADec(Celestial.DateToJ2kC(tdate));
                sunspots[dayidx] = new DailyPosition(tdate,
                                 tdate.AddDays(1),
                                 sunpos,
                                 obsLocation,
                                 twilight);
            }
            return sunspots;
        }

        public static DailyPosition[] TargetCycle(Celestial.RADec tgtRADec, DailyPosition[] sunspots, Celestial.LatLon obsLocation, double minalt)
        {
            //Calculate an array of dailypositions for dark periods of the given target (as an RA/Dec object)
            DailyPosition[] tgtspots = new DailyPosition[sunspots.Length];
            for (int dayidx = 0; dayidx < sunspots.Length - 1; dayidx++)
            {
                tgtspots[dayidx] = new DailyPosition(sunspots[dayidx].Setting,
                                  sunspots[dayidx + 1].Rising,
                                  tgtRADec,
                                  obsLocation,
                                  minalt);
            }
            tgtspots[tgtspots.Length - 1] = new DailyPosition();
            tgtspots[sunspots.Length - 1] = tgtspots[sunspots.Length - 2];
            return tgtspots;
        }

        public static DailyPosition[] MoonCycle(DailyPosition[] tgtspots, Celestial.LatLon obsLocation)
        {
            //Calculate an array of dailypositions of intervals when the moon is above the horizon
            DailyPosition[] moonedspots = new DailyPosition[tgtspots.Length];
            for (int dayidx = 0; dayidx < tgtspots.Length; dayidx++)
            {
                moonedspots[dayidx] = new DailyPosition(tgtspots[dayidx].Rising,
                                                   tgtspots[dayidx].Setting,
                                                   DailyPosition.MoonRaDec(Celestial.DateToJ2kC(tgtspots[dayidx].Rising)),
                                                   obsLocation,
                                                   0);
            }
            return moonedspots;
        }

        public static DailyPosition[] MoonPhase(DailyPosition[] tgtdata)
        {
            //Computes moon phase for each date in tgtdata
            Celestial.RADec sunradec;
            Celestial.RADec moonradec;
            foreach (DailyPosition dp in tgtdata)
            {
                sunradec = DailyPosition.SunRADec(Celestial.DateToJ2kC(dp.UTCdate));
                moonradec = DailyPosition.MoonRaDec(Celestial.DateToJ2kC(dp.UTCdate));
                dp.SetMoonPhase(sunradec.RA, moonradec.RA);
            }
            return tgtdata;
        }

        public static DailyPosition[] MoonClear(DailyPosition[] tgtdata, DailyPosition[] moondata)
        {
            //Computes the relative length of time that the moon is up while the target is up, normalized 0-1
            //Places the result in the tgtdata.moonfree (as double) property
            double totaltime;
            double onlydarktime;
            double firstdarktime;
            double seconddarktime;

            for (int idx = 0; idx < tgtdata.Length; idx++)
            {
                switch (moondata[idx].Visibility)
                {
                    case DailyPosition.VisibilityState.UpSome:
                        onlydarktime = Math.Abs(((moondata[idx].Rising - moondata[idx].IntervalStartDate) +
                                                 (moondata[idx].IntervalEndDate - moondata[idx].Setting)).TotalHours);
                        totaltime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].IntervalStartDate).TotalHours);
                        tgtdata[idx].MoonFree = (onlydarktime / totaltime);
                        break;
                    case DailyPosition.VisibilityState.UpAlways:
                        tgtdata[idx].MoonFree = 0;
                        break;
                    case DailyPosition.VisibilityState.Rises:
                        onlydarktime = Math.Abs((moondata[idx].Rising - tgtdata[idx].Rising).TotalHours);
                        totaltime = Math.Abs((tgtdata[idx].Setting - tgtdata[idx].Rising).TotalHours);
                        tgtdata[idx].MoonFree = (onlydarktime / totaltime);
                        break;
                    case DailyPosition.VisibilityState.Falls:
                        onlydarktime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].Setting).TotalHours);
                        totaltime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].IntervalStartDate).TotalHours);
                        tgtdata[idx].MoonFree = (onlydarktime / totaltime);
                        break;
                    case DailyPosition.VisibilityState.DownSome:
                        firstdarktime = Math.Abs((moondata[idx].Rising - moondata[idx].IntervalStartDate).TotalHours);
                        seconddarktime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].Setting).TotalHours);
                        totaltime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].IntervalStartDate).TotalHours);
                        tgtdata[idx].MoonFree = ((firstdarktime + seconddarktime) / totaltime);
                        break;
                    case DailyPosition.VisibilityState.UpNever:
                        tgtdata[idx].MoonFree = 1;
                        break;
                }
            }
            return tgtdata;
        }

        public static bool IsMoonUp(DailyPosition[] tgtdata, DailyPosition[] moondata, DateTime rightNow)
        {
            //returns true if the moon is above the horizon at the rightNow datetime

            int thisDate = rightNow.DayOfYear - 1;
            bool isUp = Celestial.TimeInBetween(moondata[thisDate].Rising, moondata[thisDate].Setting, rightNow);
            return isUp;

            //For idx = 0 To (tgtdata.Length - 1)
            //    Select case moondata[idx].Visibility
            //        case DailyPosition.VisibilityState.UpSome
            //            onlydarktime = Math.Abs(((moondata[idx].Rising - moondata[idx].IntervalStartDate) +
            //                                     (moondata[idx].IntervalEndDate - moondata[idx].Setting)).TotalHours)
            //            totaltime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].IntervalStartDate).TotalHours)
            //            tgtdata[idx].MoonFree = (onlydarktime / totaltime)
            //        case DailyPosition.VisibilityState.UpAlways
            //            tgtdata[idx].MoonFree = 0
            //        case DailyPosition.VisibilityState.Rises
            //            onlydarktime = Math.Abs((moondata[idx].Rising - tgtdata[idx].Rising).TotalHours)
            //            totaltime = Math.Abs((tgtdata[idx].Setting - tgtdata[idx].Rising).TotalHours)
            //            tgtdata[idx].MoonFree = (onlydarktime / totaltime)
            //        case DailyPosition.VisibilityState.Falls
            //            onlydarktime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].Setting).TotalHours)
            //            totaltime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].IntervalStartDate).TotalHours)
            //            tgtdata[idx].MoonFree = (onlydarktime / totaltime)
            //        case DailyPosition.VisibilityState.DownSome
            //            firstdarktime = Math.Abs((moondata[idx].Rising - moondata[idx].IntervalStartDate).TotalHours)
            //            seconddarktime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].Setting).TotalHours)
            //            totaltime = Math.Abs((moondata[idx].IntervalEndDate - moondata[idx].IntervalStartDate).TotalHours)
            //            tgtdata[idx].MoonFree = ((firstdarktime + seconddarktime) / totaltime)
            //        case DailyPosition.VisibilityState.UpNever
            //            tgtdata[idx].MoonFree = 1
            //    End Select
            //}

        }
    }
}
