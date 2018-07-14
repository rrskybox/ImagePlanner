using System;

namespace AstroMath
{
    public partial class Celestial
    {
        ////Module Sidereal:  Functions for astronomical calculations
        #region Description
        //Format Conventions::
        //  date:                   datetime instance
        //  time:                   timespan instance
        //  days:                   double
        //  hours:                  double [0 < 24]
        //  centuries:              double 
        //  aradians:               double [0 < 2*PI]
        //  mradians:               double [-PI < +PI]
        //  degrees:                double [0 < 360.0]
        //
        //  RA:                     mradians
        //  Dec:                    mradians
        //  Alt:                    mradians
        //  Azm:                    mradians
        //  Hourangle:              hours
        //  Latitude:               mradians
        //  Longitude:              aradians
        //  Julian Date:            days
        //  Modified Julian Date:   days
        //  J2000 Date:             years    
        //  GMT:                    hours
        //  LST:                    hours
        //  
        //  LatLon                  object of terestrial location.              Properties: Latitude, Longitude
        //  AltAz                   object of horizontal celestial location.    Properties: Alt,      Az
        //  RADec                   object of equitorial celestial location.    Properties  RA,       Dec

        //Math and Trig Conversion Methods: 
        //
        //Planar.Frac(double)                   -> double:      returns the fractional remainder of a number
        //SinD(degrees)                  -> double:      returns the sine of an angle in degrees
        //CosD(degrees)                  -> double:      returns the cosine of an angle in degrees
        //RadiansToDegrees(mradians)     -> degrees:     Converts radians to degrees
        //DegreesToRadians(degrees)      -> aradians:    Converts degrees to radians
        //HoursToRadians(hours)          -> aradians:    Converts hours to radians
        //Transform.RadiansToHours(mradians)       -> hours:       Converts radians to hours
        //HoursToDegrees(hours)          -> hours:       Converts hours to degrees
        //Transform.DegreesToHours(degrees)        -> degrees:     Converts degrees to hours

        //Date and Time Conversion Methods
        //
        //DateToJulian(date)             -> days         Converts from Julian Day to UTC date
        //DateToMJD(date)                -> days         Converts from UTC date to Modified Julian Day
        //DateToJ2kD(date)               -> days         Converts from UTC date to J2000 Day
        //DateToJ2kC(date)               -> centuries    Converts from UTC date to J2000 Century
        //JulianToDate(days)             -> date         Converts from Julian Day to UTC date
        //JulianToJ2kD(days)             -> date         Converts from Julian Day to J2000 Day
        //JulianToMJD(days)              -> date         Converts from Julian Day to Modified Julian Day
        //J2kDtoDate(days)               -> date         Converts from J2000 day to UTC date
        //J2kDToJulian(days)             -> days         Converts from J2000 day to Julian Day
        //J2kCToJulian(days)             -> days         Converts from J2000 century to Julian Day
        //MJDtoDate(days)                -> days         Converts from Modified Julian Days to UTC date
        //MJDToJulian(days)              -> days         Converts from Modified Julian Days to Julian Days 
        //TimeToString(hours)            -> string:      Constructs a string formated as "HH:mm" from hours
        //
        //Astronomical Time Conversion Methods
        //
        //DateToGST(date)                -> hours:       returns Greenich Sidereal Time as a function of UTC
        //GSTToLST(hours, LatLon)        -> hours:       returns Local Sidereal Time as a function of Greenich Sidereal Time at longitude 
        //J2KDToLMST(days, lon)          -> hours:       returns Local Sidereal Time as a function of J2000 days at longitude
        //
        //Astronomical Coordinate Conversion Methods
        //
        //HourAngleToRA(hours)           -> aradians:    returns Right Ascension as a function of hour angle at UTC and longitude
        //RAToHourAngle(radians,date,lon)-> hours:       returns Hour Angle as a function of Right Ascension at UTC and longitude
        //ComputeHA(radians,lat,altitude)-> hours:       returns Hour Angle as a function of Declination at altitude and latitude
        //
        //New LatLon(latitude,longitude) -> LatLon:      Creates LatLon object from Latitude (radians), Longitude (radians)
        //New RADec(RA,Dec)              -> RADec:       Creates RADec object from RA (radians), Dec (radians)
        //RADec.MakeAltAz(HA,location)   -> AltAz        Creates new AltAz object from Hour Angle (radians) and location (LatLon)
        //RADec.Altitude(HA,location)    -> aradians     returns altitude (radians) for RADec as a function of Hour Angle (radians) and location (LatLon)
        //RADec.Azimuth                  -> mradians     returns azimuth (mradians) for RADec as a function of Hour Angle (radians) and location (LatLon)
        //RADec.HourAngle                -> mradians     returns Hour Angle (mradians) for RADec as a function of UTC date and location (LatLon)
        //New AltAz(altitude, azimuth)   -> AltAz:       Creates AltAz object from Altitude (radians), Azimuth (radians)
        //AltAz.MakeRADec                -> RADec:       Creates new RADec object as a function of a Apoint object at a specific hourangle
        //AltAz.RightAscension(HA, loc)  -> mradians     returns Right Ascension for AltAz as a function of Hour Angle and location
        //AltAz.Declination(HA, loc)     -> aradians     returns Declination for AltAz as a function of Hour Angle and location

        //    //================================================================
        //    // Manifest constants
        //    //----------------------------------------------------------------

        #endregion

        #region Mathematical Constants

        public const double FIRST_GREGORIAN_YEAR = 1583.0;
        public const double JULIAN_BIAS = 2200000.0;
        public const double SIDEREAL_A = 0.0657098;

        public const double PI = Math.PI;
        public const double TWOPI = Math.PI * 2.0;
        public const double EPOCH2000 = 2451545.0;
        public const double EPOCHMJD = 2400000.5;

        public const double COSEPS = 0.91748;
        public const double SINEPS = 0.39778;
        public const double ARC = 206264.8062;
        #endregion


        #region Date and Time Functions

        //DATETOJULIAN(Date/time as datetime) -> Julian days as double
        //   Compute the Julian Days (double) for a date 
        public static double DateToJulian(DateTime datein)
        {
            //   JD = 367K - <(7*(K+<(M+9)/12>))/4> + <(275M)/9> + I + 1721013.5 + UT/24 - 0.5sign(100K+M-190002.5) + 0.5
            double uth = datein.Hour + (datein.Minute / 60.0) + (datein.Second / 3600.0);
            double jd = (367.0 * datein.Year) - (int)(7.0 * (datein.Year + (int)((datein.Month + 9.0) / 12.0)) / 4.0) +
            (int)((275 * datein.Month) / 9) +
            datein.Day + 1721013.5 + (uth / 24.0) - (0.5 * Math.Sign(100.0 * datein.Year + datein.Month - 190002.5)) + 0.5;
            return jd;
        }

        //DATETOMJD(Date/time as datetime) -> Modified Julian Days as double:
        public static double DateToMJD(DateTime thisdate)
        {
            //   Compute the Modified Julian Date from current Date/time
            double a;
            int b;

            int tyear = thisdate.Year;
            int tmonth = thisdate.Month;
            int tday = thisdate.Day;
            int thour = (int)((thisdate.Hour + thisdate.Minute / 60.0) + (thisdate.Second / 3600.0));


            a = 10000 * tyear + 100 * tmonth + tday;
            if (tmonth < 2)
            {
                tmonth = tmonth + 12;
                tyear = tyear - 1;
            }
            if (a <= 15821004.1)
            {
                b = -2 + (int)((tyear + 4716) / 4.0) - 1179;
            }
            else
            {
                b = (int)(tyear / 400.0) - (int)(tyear / 100.0) + (int)(tyear / 4.0);
            }
            a = 365 * tyear - 679004;
            double mjd = a + b + (int)(30.6001 * (tmonth + 1)) + tday + thour / 24.0;
            //int djulian = (a + b + (int)(30.6001 * (tmonth + 1)) + tday + thour / 24) + EPOCHMJD
            return mjd;
        }

        //DATETOJ2kD(Date/Time as datetime) > J2000 days as double:
        public static double DateToJ2kD(DateTime thisdate)
        {
            //   Convert a Date To J2000 days (Julian days from 1/1/2000, 0:0:0)
            //Compute the Julian days in the current epoch (2000)
            //   Convert date to julian,) { julian to J2000
            return JulianToJ2kD(DateToJulian(thisdate));
        }

        //DATETOJ2kC(Date/Time as datetime) > J2000 centuries as double:
        public static double DateToJ2kC(DateTime thisdate)
        {
            //   Convert a Date To J2000 centuries (Julian days from 1/1/2000, 0:0:0)
            //Compute the Julian days in the current epoch (2000)
            //   Convert date to julian,) { julian to J2000
            //int d = DateToJulian(thisdate) - epoch2000)
            //return JulianToJ2KC(DateToJulian(thisdate))
            ////return((DateToJulian(thisdate) - EPOCH2000) / 36525)
            return ((DateToJ2kD(thisdate)) / 36525.0);
        }

        //JULIANTODATE(JD days as datetime) -> Date/Time as datetime:
        public static DateTime JulianToDate(double jd)
        {
            //   Convert Julian Days To a civil date
            int b, d, f;
            double jd0, c, e;
            jd0 = (int)(jd + 0.5);
            if (jd0 < 2299161.0)
            {
                c = jd0 + 1524.0;
            }
            else
            {
                b = (int)((jd0 - 1867216.25) / 36524.25);
                c = jd0 + (b - (int)(b / 4.0)) + 1525.0;
            }
            d = (int)((c - 122.1) / 365.25);
            e = 365.0 * d + (int)(d / 4.0);
            f = (int)((c - e) / 30.6001);
            int day = (int)(c - e + 0.5) - (int)(30.6001 * f);
            int month = (int)(f - 1.0 - 12.0 * (int)(f / 14.0));
            int year = d - 4715 - (int)((7.0 + month) / 10.0);
            DateTime dateout = new DateTime(year, month, day);
            dateout = dateout.AddHours(24.0 * (jd + 0.5 - jd0));
            return dateout;
        }

        //JULIANTOJ2kD(Julian days as double) -> Time in Julian days since 2000
        public static double JulianToJ2kD(double jd)
        {
            //   Convert Julian Days To J2000 days
            return (jd - EPOCH2000);
        }

        //JULIANTOJ2KC(Julian days as double) -> Time in Julian centuries since 2000
        public static double JulianToJ2KC(double jd)
        {
            //   Convert Julian Days To J2000 centuries
            return ((jd - EPOCH2000) / 36525.0);
        }

        //JULIANTOMJD(Julian days as double) -> Modified Julian Days as double
        public static double JulianToMJD(double jd)
        {
            //   Convert Modified Julian Days to Julian days
            return (jd - EPOCHMJD);
        }

        //J2kDTODATE(J2000 days as double) -> Date/time as datetime:
        public static DateTime J2kDToDate(double j2k)
        {
            //   Convert J2000 Days To a civil date
            int b, d, f;
            double jd, jd0, c, e;


            jd = J2kDToJulian(j2k);
            jd0 = (int)(jd + 0.5);
            if (jd0 < 2299161.0)
            {
                c = jd0 + 1524.0;
            }
            else
            {
                b = (int)((jd0 - 1867216.25) / 36524.25);
                c = jd0 + (b - (int)(b / 4.0)) + 1525.0;
            }
            d = (int)((c - 122.1) / 365.25);
            e = 365.0 * d + (int)(d / 4);
            f = (int)((c - e) / 30.6001);
            int day = (int)(c - e + 0.5) - (int)(30.6001 * f);
            int month = (int)(f - 1.0 - 12.0 * (int)(f / 14.0));
            int year = d - 4715 - (int)((7.0 + month) / 10.0);
            DateTime jdate = new DateTime(year, month, day);
            jdate = jdate.AddHours(24.0 * (jd + 0.5 - jd0));
            return jdate;
        }

        //J2kDTOJULIAN(J2000 days as double) -> Julian days as double:
        public static double J2kDToJulian(double j2k)
        {
            //   Convert J2000 days To Julian Days
            return (EPOCH2000 + j2k);
        }

        //J2kCTODate(J2000 centuries as double) -> Julian days as datetime:
        public static DateTime J2kCToDate(double j2kc)
        {
            //   Convert J2000 years To Gregorian date
            return (JulianToDate(J2kCToJulian(j2kc)));
        }

        //J2kCTOJULIAN(J2000 centuries as double) -> Julian days as double:
        public static double J2kCToJulian(double j2kc)
        {
            //   Convert J2000 years To Julian Days
            return (EPOCH2000 + (j2kc * 36525.0));
        }

        //MJDTODATE(MJD days as datetime) -> Date/Time as datetime:
        public static DateTime MJDToDate(double mjd)
        {
            //   Convert Modified Julian Days To a civil date
            int b, d, f;
            double jd, jd0, c, e;
            jd = mjd + EPOCHMJD;
            jd0 = (int)(jd + 0.5);
            if (jd0 < 2299161.0)
            {
                c = jd0 + 1524.0;
            }
            else
            {
                b = (int)((jd0 - 1867216.25) / 36524.25);
                c = jd0 + (b - (int)(b / 4)) + 1525.0;
            }
            d = (int)((c - 122.1) / 365.25);
            e = 365.0 * d + (int)(d / 4.0);
            f = (int)((c - e) / 30.6001);
            int day = (int)(c - e + 0.5) - (int)(30.6001 * f);
            int month = f - 1 - 12 * (int)(f / 14.0);
            int year = d - 4715 - (int)((7 + month) / 10.0);
            DateTime dateout = new DateTime(year, month, day);
            dateout = dateout.AddHours(24.0 * (jd + 0.5 - jd0));
            return dateout;
        }

        //MJDTOJULIAN(MJD days as double) -> Julian Days as double
        public static double MJDToJulian(double mjd)
        {
            //   Convert Modified Julian Days to Julian days
            return (mjd + EPOCHMJD);
        }

        //MJDTOJ2kD(MJD days as double) -> J2000 days as double:
        public static double MJDToJ2kD(double mjd)
        {
            //   Convert MJD Days To J2000 Days
            return JulianToJ2kD(mjd + EPOCHMJD);
        }

       //J2kDTOLMST(J2000 days as double, longitude (radians) as double) -> LMST hours as double:
        public static double J2kDToLMST(double j2k, double longR)
        {
            //   Convert J2000 Days at longitude to Local Mean Sidereal Time in hours
            return GSTToLST(DateUTCToGST(J2kDToDate(j2k)), longR);
        }

        //MJDTOLMST(MJD days as double, longitude (radians) as double) -> LMST hours as double:
        public static double MJDtoLMST(double mjd, double longR)
        {
            //   Convert Modified Julian Days at longitude to Local Mean Sidereal Time in hours
            double mjd0, t, ut, gmst;
            mjd0 = (int)(mjd);
            ut = (mjd - mjd0) * 24.0;
            t = (mjd0 - 51544.5) / 36525.0;
            gmst = 6.697374558 + 1.0027379093 * ut + (8640184.812866 + (0.093104 - 0.0000062 * t) * t) * t / 360.00;
            double lmst = 24.0 * Planar.Frac((gmst - Transform.RadiansToHours(longR)) / 24.0);
            return lmst;
        }

        //DATEUTCTOGST(Date/time as utc datetimu) -> GST (hours) as double:
        public static double DateUTCToGST(DateTime userdate)
        {
            //   Compute Greenich Sidereal Time in hours from user date & time
            // Greenwich Sidereal Time is translated from UTC from the number of days from J2000.
            double hours = Transform.DegreesToHours((280.6061837 + 360.98564736629 * DateToJ2kD(userdate)) % 360.0);
            return hours;
        }

        //LSTTOLOCALTIME(Date/time as utc datetime) -> local hours (hours) as double:
        public static double LSTToLocalTime(double lst, double longitudeD)
        {
            double JD0 = (int)(DateToJulian(DateTime.UtcNow)) + 0.5;
            double gt = 6.656306 + 0.0657098242 * (JD0 - 2445700.5) + 1.0027379093 * (DateTime.UtcNow.Hour + DateTime.UtcNow.Minute / 60.0);
            double gmst = Transform.NormalizeHours(lst - (longitudeD / 15.0));
            double ut = Planar.Frac(((gmst - 6.697374558) - (0.0657098242 * (JD0 - 2445700.5))) / 1.0027379093) * 24;
            if (ut < 0)
            { ut += 24; }
            TimeSpan ltz = DateTime.Now - DateTime.UtcNow;
            double lt = Transform.NormalizeHours(ut + ltz.TotalHours);
            return lt;
        }

        //GSTTOLST(GST hours as double, longitude (radians) as double) -> LST hours as double:
        public static double GSTToLST(double gst, double longitude)
        {
            //   Compute Local Sideral Time in hours from Greenich Sidereal Time in hours at a longitude in radians
            //Local Sidereal Time is Greenwich Sidereal Time decremented by 1 hour per 15 degrees of site longitude, independent of date
            //gsth is GST in hours
            //site is longitude in radians -- positive for east longitude
            double lst = (gst - Transform.RadiansToHours(longitude) + 24) % 24.0;
            return lst;
        }

        //LSTTOGST(LST as hours, longitude (radians) as double) -> GST hours as double:
        public static double LSTToGST(double lst, LatLon location)
        {
            //   Compute Local Sideral Time in hours from Greenich Sidereal Time in hours at a longitude in radians
            //Local Sidereal Time is Greenwich Sidereal Time decremented by 1 hour per 15 degrees of site longitude, independent of date
            //gsth is GST in hours
            //site is longitude in radians -- positive for east longitude
            double gst = (lst + Transform.RadiansToHours(location.Lon) + 24) % 24.0;
            return gst;
        }
        #endregion

        #region Time Comparison Methods

        //DAYPLUSHOURS(DateTime thisdate, double someHours) -> datetime (day + hours)
        public static DateTime DayPlusHours(DateTime thisdate, double someHours)
        {
            //  return a datetime for this date at given hour(s)
            DateTime dateday = new DateTime(thisdate.Year, thisdate.Month, thisdate.Day).AddHours(someHours);
            return dateday;
        }

        //TimeInBetween(earliestTime as datetime, latestTime as datetime, thisTime as datetime) as boolean
        public static bool TimeInBetween(DateTime earliestTime, DateTime latestTime, DateTime thisTime)
        {
            //Determines if (this time is later than the earliestTime but sooner than the latestTime, ignoring the date
            if ((earliestTime <= thisTime) && (thisTime <= latestTime))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //InteralOverlap(dusk, dawn, rise, set) returns hours over overlap
        public static double IntervalOverlap(DateTime iDusk, DateTime iDawn, DateTime iRise, DateTime iSet)
        {
            //Computes the hours between rise and set that overlap the hours between dusk and dawn where
            //  all datetimes are normalized to the same day.
            //
            //Break the day into two parts - Dusk to midnight and midnight to dawn

            double iHours = 0;

            DateTime Dusk = new DateTime(1, 1, 1, iDusk.Hour, iDusk.Minute, iDusk.Second);
            DateTime Dawn = new DateTime(1, 1, 1, iDawn.Hour, iDawn.Minute, iDawn.Second);
            DateTime Rise = new DateTime(1, 1, 1, iRise.Hour, iRise.Minute, iRise.Second);
            DateTime Set = new DateTime(1, 1, 1, iSet.Hour, iSet.Minute, iSet.Second);
            DateTime BeforeMidnight = new DateTime(1, 1, 1, 23, 59, 59);
            DateTime AfterMidnight = new DateTime(1, 1, 1, 0, 0, 0);
            //TSX returns 12 and 12 for objects that never set.  if so, bump the rise time by a minute just for this algorithm
            //variables used for debug
            double nightDuration = Transform.NormalizeHours(Dawn - Dusk);
            double objectUpDuration = Transform.NormalizeHours(Set - Rise);

            //Dusk period - before midnight
            if (Rise == Set)
            { iHours = nightDuration; }
            else
            {
                //Dusk Period
                if (Rise < Set) //Rise before Set
                {
                    if (Rise < Dusk) //Rise Before Dusk
                    {
                        if (Set < Dusk)
                        //Set before dusk //never up after dusk 
                        { iHours = 0; }
                        else
                        //Set after dusk, must be before or at midnight //dusk to set     
                        { iHours = (Set - Dusk).TotalHours; }
                    }
                    else
                    //Rise after Dusk, set must be before or at midnght//rise to set
                    { iHours = (Set - Rise).TotalHours; }
                }
                else
                // set before rise
                {
                    if (Set < Dusk) //Set before dusk
                    {
                        if (Rise < Dusk) //rise before dusk//dusk to midnight
                        { iHours = (BeforeMidnight - Dusk).TotalHours; }
                        else//rise after dusk up to midnight //rise to midnight 
                        { iHours = (BeforeMidnight - Rise).TotalHours; }
                    }
                    else//set after dusk, rise must be between set and midnight//dusk to set + rise to midnight
                    { iHours = (Set - Dusk).TotalHours + (BeforeMidnight - Rise).TotalHours; }
                }
                //Dawn period
                if (Rise < Set)//Rise before set
                {
                    if (Rise < Dawn)//rise before dawn
                    {
                        if (Set < Dawn)//set before dawn //rise to set
                        { iHours += (Set - Rise).TotalHours; }
                        else //Set after dawn//Rise to dawn
                        { iHours += (Dawn - Rise).TotalHours; }
                    }
                    else//rise after dawn//never up
                    { iHours += 0; }
                }
                else //Set before Rise
                {
                    if (Set < Dawn) //set before dawn
                    {
                        if (Rise < Dawn) //rise before dawn//midnight to set + rise to dawn
                        { iHours += ((Set - AfterMidnight).TotalHours + (Dawn - Rise).TotalHours); }
                        else //rise after dawn//midnight to set
                        { iHours += (Set - AfterMidnight).TotalHours; }
                    }
                    else //set after dawn//midnight to dawn
                    { iHours += (Dawn - AfterMidnight).TotalHours; }
                }
            }
            return iHours;
        }

        #endregion

        #region Astronomical Coordinates Conversion Methods

        //HOURANGLETORA(hourangle (radians) as double, UTC Date & Time as datetime, longitude (radians) as double) -> RA (radians) as double:
        public static double HourAngleToRA(double ha, DateTime ut, double longitude)
        {
            //  Compute hourangle at Universal Time and longitude
            double lst = GSTToLST(DateUTCToGST(ut), longitude);
            double ra = (lst - ha) % TWOPI;
            return ra;
        }

        #endregion

        #region Lat Long Class

        //CLASS:  SIDEREAL.LATLON
        //   Location object classes:  Terrestrial (LatLon), Horizontal (AltAz), Equitorial (RADec)

        public class LatLon
        {
            //Represents a terrestrial location in latitude, longitude
            //
            //  properties
            //    .lat   [ latitude in radians, in [-pi,+pi] ]
            //    .lon    [ longitude in radians, in [0,+2*pi] ]

            private double r_lat;
            private double r_lat_deg;
            private string r_lat_dir;
            private double r_lon;
            private double r_lon_deg;
            private string r_lon_dir;

            //Empty constructor (GM)
            public LatLon()
            {
                r_lat = 0;
                r_lat_deg = 0;
                r_lat_dir = "N";
                r_lon = 0;
                r_lon_deg = 0;
                r_lon_dir = "E";
                return;
            }

            //Parameterized constructor -- -pi <= lat <= +pi;  0 <= lon <= +pi
            public LatLon(double latitude, double longitude)
            {
                r_lat = latitude % Math.PI;
                if (r_lat < 0)
                {
                    r_lat_dir = "S";
                    r_lat_deg = -(360.0 * r_lat / TWOPI);
                }
                else
                {
                    r_lat_dir = "N";
                    r_lat_deg = (360.0 * r_lat / TWOPI);
                }
                r_lon = Math.Abs(longitude) % (TWOPI);
                if (longitude >= Math.PI)
                {
                    r_lon_dir = "E";
                    r_lon_deg = Transform.RadiansToDegrees(Math.PI - r_lon);
                }
                else
                {
                    r_lon_dir = "W";
                    r_lon_deg = Transform.RadiansToDegrees(r_lon);
                }
            }

            public double Lat
            {
                get
                { return r_lat; }
            }

            public double Lon
            {
                get
                { return r_lon; }
            }

            public string GetLatitudeString()
            { return (r_lat_deg.ToString() + " " + r_lat_dir); }

            public string GetLongitudeString()
            { return (r_lon_deg.ToString() + " " + r_lon_dir); }
        }
        #endregion

        #region RA Dec Class
        //CLASS:  SIDEREAL.RADEC
        public class RADec
        {
            //Represents a sky location in equitorial coords. (RA, Dec)
            //
            //    .RA   [ Right Ascension in radians, in [0,+2*pi] ]
            //    .Dec  [ Dec in radians, in [-pi,+pi ]
            ////
            private double dr_RA;
            private double dr_Dec;
            private double dh_RA;
            private double dd_Dec;

            //Empty constructor
            public RADec()
            {
                dr_RA = 0;
                dr_Dec = 0;
                dh_RA = 0;
                dd_Dec = 0;
                return;
            }

            //RA and Dec parameterized constructor
            public RADec(double RA, double Dec)
            {
                dr_RA = Math.Abs(RA) % TWOPI;
                dr_Dec = Dec % Math.PI;
                dh_RA = Transform.RadiansToHours(dr_RA);
                dd_Dec = Transform.RadiansToDegrees(dr_Dec);
                return;
            }

            public double RA
            {

                get
                { return (dr_RA); }
                set
                { dr_RA = Math.Abs(value) % TWOPI; }
            }

            public double Dec

            {
                get
                { return (dr_Dec); }
                set
                { dr_Dec = value % Math.PI; }
            }

            //RADec.MakeAltAz:  Create a new AltAz instance from an RADec instance
            public AltAz MakeAltAz(double haR, LatLon loc)
            {
                //Create a new AltAz instance from an RADec instance, i.e. convert from equitorial to horizontal.
                //  RADec.MakeAltAz -> AltAz
                //(ha is an object//s hour angle in radians) and
                //(latitude is the observer//s latitude in radians) ->
                //return position in the observer//s sky
                //      in horizon coordinates as an AltAz instance ]
                //   Compute altitude at declination given latitude and hourangle
                //  alt  =  altitude of object as seen from latLon at utc
                double alt = Altitude(haR, loc);
                double czm = Azimuth(haR, loc);
                if (Math.Sin(haR) < 0)
                { return new AltAz(alt, czm); }
                else
                { return new AltAz(alt, TWOPI - Math.Acos(czm)); }
            }

            public double Altitude(double haR, LatLon location)
            {
                //returns the altitude (radians) of the object at RADec for the given latitude and hour angle, in radians
                //(latitude is the observer//s latitude in radians) ->
                //   Compute altitude at declination given latitude and hourangle
                //  alt  =  altitude of object as seen from latLon at utc
                double alt = Math.Asin((Math.Sin(dr_Dec) * Math.Sin(location.Lat)) + (Math.Cos(dr_Dec) * Math.Cos(location.Lat) * Math.Cos(haR)));
                //   az  :=  azimuth of object as seen from latLon at utc 
                return alt;
            }
            //         
            public double Azimuth(double haR, LatLon loc)
            {
                //Computes Azimuth (radians) of the RADec object for the given latitude and hour angle
                //  RADec.MakeAltAz -> AltAz
                //(ha is an object//s hour angle in radians) and
                //(latitude is the observer//s latitude in radians) ->
                //return position in the observer//s sky
                //  alt  =  altitude of object as seen from latLon at utc
                double alt = Math.Asin(Math.Sin(dr_Dec) * Math.Sin(loc.Lat) + Math.Cos(dr_Dec) * Math.Cos(loc.Lat) * Math.Cos(haR));
                //   az  :=  azimuth of object as seen from latLon at utc 
                double czm = Math.Acos((Math.Sin(dr_Dec) - Math.Sin(alt) * Math.Sin(loc.Lat)) / (Math.Cos(alt) * Math.Cos(loc.Lat)));
                //if (sin(HA) is negative,) { Azm = Azm, otherwise Azm = 2pi - Azm    
                if (Math.Sin(haR) < 0)
                { return czm; }
                else
                { return TWOPI - czm; }
            }

            //    //Calculate the hour angle(radians) for the current time &location
            public double HourAngle(DateTime utcdate, LatLon location)
            { //in radians
              //int haR = Sidereal.HoursToRadians(Sidereal.RAToHourAngle(r_RA, ldate, loc.Lon))
              //   Compute hourangle from RA in radians at Universal date & time and east longitude
                double lstH = GSTToLST(DateUTCToGST(utcdate), location.Lon);
                double lstR = Transform.HoursToRadians(lstH);
                double haR = (lstR - dr_RA);
                if (haR < 0)
                { haR += TWOPI; }
                return haR;
            }

            //Calculate the transit time for current RADEC in UTC
            public double TransitTime(DateTime UTCDate, LatLon location)
            {
                //Compute the hour angle for the target at the current location
                //   subract the hour angle to the current time to get the transit time
                double dHA = Transform.RadiansToHours(HourAngle(UTCDate, location));
                DateTime localTransit = UTCDate.ToLocalTime() - TimeSpan.FromHours(dHA);
                double ttH = localTransit.Hour + (localTransit.Minute / 60.0);
                return ttH;
            }
        }
        #endregion

        #region Alt Az Class
        //CLASS:  SIDEREAL.ALTAZ
        public class AltAz
        {
            //Represents a sky location in horizon coords. (altitude/azimuth)
            //
            //  Exports/Invariants
            //    .Alt   [ altitude in radians, in [-pi,+pi] ]
            //    .Azm   [ azimuth in radians, in [0,2*pi] ]
            ////
            private double r_alt;
            private double r_azm;

            //Empty constructor
            public AltAz()
            {
                r_alt = 0;
                r_azm = 0;
            }

            //Parameterized constructor
            public AltAz(double alt, double az)
            {
                r_alt = alt % Math.PI;
                r_azm = Math.Abs(az) % (TWOPI);
            }

            public double Alt
            {
                get
                { return (r_alt); }
                set
                { r_alt = value % Math.PI; }
            }

            public double Azm
            {
                get
                { return (r_azm); }
                set
                { r_azm = Math.Abs(value) % (TWOPI); }
            }

            //Convert AltAz (from location at hourangle) to RaDec
            public RADec MakeRaDec(double haR, LatLon loc)
            {
                //Convert between horizontal and equatorial coordinates.
                double RA = RightAscension(haR, loc);
                double Dec = Declination(haR, loc);
                return new RADec(RA, Dec);
            }

            //RightAscension: (from location at hourangle) to RaDec
            public double RightAscension(double ha, LatLon loc)
            {
                //Compute RA of object in horizontal coordinates at location LatLon and hour angle haR (radians)
                double RA = Math.Cos(r_alt) * Math.Sin(loc.Lat) * Math.Cos(r_azm) + Math.Sin(r_alt) * Math.Cos(r_azm);
                double Dec = Math.Cos(r_alt) * Math.Sin(r_azm);
                double z = Math.Sin(r_alt) * Math.Sin(loc.Lat) * Math.Sin(loc.Lat) * Math.Cos(r_azm);
                return RA;
            }

            //Declination: (from location at hourangle) to RaDec
            public double Declination(double ha, LatLon loc)
            {
                //Convert between horizontal and equatorial coordinates.
                double RA = Math.Cos(r_alt) * Math.Sin(loc.Lat) * Math.Cos(r_azm) + Math.Sin(r_alt) * Math.Cos(r_azm);
                double Dec = Math.Cos(r_alt) * Math.Sin(r_azm);
                double z = Math.Sin(r_alt) * Math.Sin(loc.Lat) * Math.Sin(loc.Lat) * Math.Cos(r_azm);
                return Dec;
            }
        }
        #endregion

        #region Astronomical Methods

        public static RADec SunRADec(double jc2K)
        {
            //jc2k = julian centuries since year 2000
            double t = jc2K;
            double m = TWOPI * Planar.Frac(0.993133 + 99.997361 * t);
            double dl = 6893.0 * Math.Sin(m) + 72.0 * Math.Sin(2 * m);
            double l = TWOPI * Planar.Frac(0.7859453 + m / TWOPI + (6191.2 * t + dl) / 1296000.0);
            double sl = Math.Sin(l);
            double x = Math.Cos(l);
            double y = COSEPS * sl;
            double z = SINEPS * sl;
            double rho = Math.Sqrt(1.0 - z * z);
            double dec = (360.0 / TWOPI) * Math.Atan(z / rho);
            double ra = (48.0 / TWOPI) * Math.Atan(y / (x + rho));
            if (ra < 0)
            { ra = ra + 24.0; }
            RADec a = new RADec(Transform.HoursToRadians(ra), Transform.DegreesToRadians(dec));
            return a;
        }

        public static RADec MoonRaDec(double j2kC)
        {
            //Produces approximate RA,Dec position of moon 
            //j2kC in julian centuries since the year 2000
            double lo = Planar.Frac(0.606433 + 1336.855225 * j2kC);     //mean longitude moon (in rev)
            double l = TWOPI * Planar.Frac(0.374897 + 1325.55241 * j2kC);   //mean anomaly of the moon
            double ls = TWOPI * Planar.Frac(0.993133 + 99.997361 * j2kC);   //mean anomally of the sun
            double d = TWOPI * Planar.Frac(0.827361 + 1236.853086 * j2kC);    //diff longitude of Moon-Sun  
            double f = TWOPI * Planar.Frac(0.259086 + 1342.227825 * j2kC);    //mean argument of latitude

            double dl = 22640 * Math.Sin(l) - 4586.0 * Math.Sin(l - 2.0 * d) + 2370.0 * Math.Sin(2.0 * d) + 769.0 * Math.Sin(2.0 * l) -
                668.0 * Math.Sin(ls) - 412.0 * Math.Sin(2.0 * f) - 212.0 * Math.Sin(2.0 * l - 2.0 * d) - 206.0 * Math.Sin(l + ls - 2.0 * d) +
                192.0 * Math.Sin(l + 2.0 * d) - 165.0 * Math.Sin(ls - 2.0 * d) - 125.0 * Math.Sin(d) - 110.0 * Math.Sin(l + ls) +
                148.0 * Math.Sin(l - ls) - 55.0 * Math.Sin(2.0 * f - 2.0 * d);

            double s = f + (dl + 412.0 * Math.Sin(2.0 * f) + 541.0 * Math.Sin(ls)) / ARC;
            double h = f - 2 * d;
            double n = -526.0 * Math.Sin(h) + 44.0 * Math.Sin(l + h) - 31.0 * Math.Sin(-l + h) - 23.0 * Math.Sin(ls + h) +
                    11.0 * Math.Sin(-ls + h) - 25.0 * Math.Sin(-2.0 * l + f) + 21.0 * Math.Sin(-l + f);
            double l_moon = TWOPI * Planar.Frac(lo + dl / 1296000.0); //in rad
            double b_moon = (18520.0 * Math.Sin(s) + n) / ARC; //in rad
            double cb = Math.Cos(b_moon);
            double x = cb * Math.Cos(l_moon);
            double v = cb * Math.Sin(l_moon);
            double w = Math.Sin(b_moon);
            double y = COSEPS * v - SINEPS * w;
            double z = SINEPS * v + COSEPS * w;
            double rho = Math.Sqrt(1.0 - Math.Pow(z, 2));
            double dec = (360.0 / TWOPI) * Math.Atan(z / rho);
            double ra = (48.0 / TWOPI) * Math.Atan(y / (x + rho));
            if (ra < 0)
            { ra = ra + 24.0; }
            RADec a = new RADec(Transform.HoursToRadians(ra), Transform.DegreesToRadians(dec));
            return a;
        }

        public static double MaxAltitude(DateTime DuskUTC, DateTime DawnUTC, RADec position, LatLon location)
        {
            //Computes the maximum altitude that a target at position (viewed from location) achieves 
            //   between Dusk and Dawn
            //Returns duration in radians

            double tMaxAlt = 0;
            double tDuskHArad = position.HourAngle(DuskUTC, location);
            double tDawnHArad = position.HourAngle(DawnUTC, location);
            double tDuskAltrad = position.Altitude(tDuskHArad, location);
            double tDawnAltrad = position.Altitude(tDawnHArad, location);
            double darkHours = ((DawnUTC - DuskUTC).TotalHours); //in hours

            //Any hour angles that are more than nighttime (from meridian) will not peak.
            //or, better said, all hour angles that are <= nighttime will peak during the night.
            //if so, then the maximum altitude is at the hourangle 0.
            //if not, then the maximum altitude is the greater of the HA at dawn and the HA at dusk
            double tDuskHAHrs = Transform.RadiansToHours(tDuskHArad);
            double tDawnHAHrs = Transform.RadiansToHours(tDawnHArad);
            double tDuskAltHrs = Transform.RadiansToHours(tDuskAltrad);
            double tDawnAltHrs = Transform.RadiansToHours(tDawnAltrad);
            if (tDuskHAHrs >= (24 - darkHours))
            { tMaxAlt = position.Altitude(Transform.HoursToRadians(0.0), location); }
            else
            {
                if (tDuskAltHrs > tDawnAltHrs)
                { tMaxAlt = tDuskAltrad; }
                else
                { tMaxAlt = tDawnAltrad; }
            }
            return tMaxAlt;
        }
        #endregion
    }
}
