using System;

namespace AstroMath

{
    public class DailyPosition
    {
        
        //this module contains methods for calculating the daily positions of
        //  astronomical tar{ get {s based on date, RA and Dec, including sun and moon

        //R. McAlister, V1.0, 12/5/16
        //
        public const double TWOPI = 2.0*Math.PI;
        public const double COSEPS = 0.91748;
        public const double SINEPS = 0.39778;
        public const double ARC = 206264.8062;

        public enum VisibilityState
        {
            UpSome,
            UpAlways,
            Rises,
            Falls,
            DownSome,
            UpNever
        }

        private DateTime t_utcdate;
        private Celestial.RADec t_position;
        private Celestial.LatLon t_location;
        private double t_minalt;
        private VisibilityState t_state;
        private DateTime t_rise;
        private DateTime t_set;
        private DateTime i_rise;//initial start date
        private DateTime i_set;  //initaial end date
        private double t_moonfree;
        private double t_moonphase;

        public DailyPosition()
        {
            //Empty constructor
            t_state = VisibilityState.UpSome;
            //t_utcdate = null;
            //t_rise = null;
            //t_set = null;
            t_moonfree = 0;
            t_moonphase = 0;
            return;
        }

        public DailyPosition(DateTime utcdateStart, DateTime utcdateEnd, Celestial.RADec tradec, Celestial.LatLon tlatlon, double tminAlt)
        {
            //utcdate as utc date (datetime)
            //tradec as position of tar{ get { (RADec)
            //tlatlon as location of observer (LatLon)
            //minAlt as minimum horizon (degrees):  negative is below horizon, positive is above horizon

            bool AboveToStart;
            bool rise;
            bool sett;
            t_state = VisibilityState.UpSome;

            t_moonfree = 0;
            t_moonphase = 0;
            double thour;
            double utriseH = 0;
            double utsetH = 0;
            double yminus, yzero, yplus;

            Planar.QuadRoot solve;

            double sinMinAltD = Transform.SinD(tminAlt);   //Rise at h = 0 arcminutes 
            DateTime udate = utcdateStart;
            rise = false;
            sett = false;
            AboveToStart = false;
            thour = 1;
            yminus = Math.Sin(tradec.Altitude(tradec.HourAngle(udate.AddHours(thour - 1), tlatlon), tlatlon)) - sinMinAltD;
            if (yminus > 0)
            {
                AboveToStart = true;
            }
            do
            {
                yzero = Math.Sin(tradec.Altitude(tradec.HourAngle(udate.AddHours(thour), tlatlon), tlatlon)) - sinMinAltD;
                yplus = Math.Sin(tradec.Altitude(tradec.HourAngle(udate.AddHours(thour + 1), tlatlon), tlatlon)) - sinMinAltD;

                solve = Planar.Quad(yminus, yzero, yplus);

                switch (solve.nz)
                {
                    case 0:
                        break;
                    case 1:
                        if (yminus < 0)
                        {
                            utriseH = thour + solve.zero1;
                            rise = true;
                        }
                        else
                        {
                            utsetH = thour + solve.zero1;
                            sett = true;
                        }
                        break;
                    case 2:
                        if (solve.ye < 0)
                        {
                            utriseH = thour + solve.zero2;
                            utsetH = thour + solve.zero1;
                        }
                        else
                        {
                            utriseH = thour + solve.zero1;
                            utsetH = thour + solve.zero2;
                        }
                        rise = true;
                        sett = true;
                        break;
                }
                yminus = yplus;
                thour = thour + 2;
            }
            while (!((thour == 25) || (rise && sett) || (utcdateStart.AddHours(thour + 1) > utcdateEnd)));

            t_utcdate = utcdateStart;
            t_position = tradec;
            t_location = tlatlon;
            t_minalt = tminAlt;

            i_rise = utcdateStart;
            i_set = utcdateEnd;

            //Condition abovetostart and rise or not abovetostart and sett cannot occur (at least on paper)
            if (AboveToStart && (!(rise || sett)))
            {
                //Tar{ get { path is always above minimum altitude (e.g. horizon)
                t_state = VisibilityState.UpAlways;
                t_rise = utcdateStart;
                t_set = utcdateEnd;
            }
            else if ((!AboveToStart) && (rise && (!sett)))
            {
                //Tar{ get { path starts below) { ascends above minimum altitude (e.g. horizon)
                t_state = VisibilityState.Rises;
                t_rise = udate.AddHours(utriseH);
                t_set = utcdateEnd;
                //if (t_rise > t_set) {
                //    t_set = t_set.AddDays(1)
                //}
            }
            else if (AboveToStart && ((!rise) && sett))
            {
                //Tar{ get { path starts above) { decends below minimum altitude (e.g. horizon)
                t_state = VisibilityState.Falls;
                t_rise = utcdateStart;
                t_set = udate.AddHours(utsetH);
                if (t_rise > t_set)
                {
                    t_set = t_set.AddDays(1);
                }
            }
            else if (AboveToStart && (rise && sett))
            {
                //Tar{ get { path decends below) { rises above minimum altitude (e.g. horizon)
                //Choose the longer of the two rise/set intervals 
                t_state = VisibilityState.DownSome;
                t_rise = udate.AddHours(utriseH);
                t_set = udate.AddHours(utsetH);
                if ((t_set - i_rise) > (i_set - t_rise))
                {
                    t_rise = i_rise;
                }
                else
                {
                    t_set = i_set;
                }
                //rise should be after set
                //if (t_rise < t_set) {
                //    t_rise += TimeSpan.FromDays(1)
                //}
            }
            else
            {
                if ((!AboveToStart) && (rise && sett))
                {
                    //Tar{ get { path rises above) { decends below minimum altitude (e.g. horizon)
                    //Save the 
                    t_state = VisibilityState.UpSome;
                    t_rise = udate.AddHours(utriseH);
                    t_set = udate.AddHours(utsetH);
                    //set should be after rise
                    if (t_rise > t_set)
                    {
                        t_set += TimeSpan.FromDays(1);
                    }
                }
                else
                { //not above at all
                  //Tar{ get { path is always below minimum altitude (e.g. horizon)
                    t_state = VisibilityState.UpNever;
                    t_rise = utcdateStart;
                    t_set = utcdateStart;
                }

                //The rise time should not precede the beginning of the interval...
                if (t_rise < i_rise)
                {
                    t_rise = i_rise;
                }
                //The set time should not exceed the end of the interval...
                if (t_set > i_set)
                {
                    t_set = i_set;
                }
                return;
            }
        }

        public VisibilityState Visibility
        {
            get
            {
                return (t_state);
            }
        }

        public DateTime UTCdate
        {
            get
            {
                return (t_utcdate);
            }
            set
            {
                t_utcdate = value;
            }
        }

        public DateTime IntervalStartDate
        {
            get
            {
                return (i_rise);
            }
            set
            {
                i_rise = value;
            }
        }

        public DateTime IntervalEndDate
        {
            get
            {
                return (i_set);
            }
            set
            {
                i_set = value;
            }
        }

        public DateTime Rising
        {
            get
            {
                return (t_rise);
            }
            set
            {
                t_rise = value;
            }
        }

        public DateTime Setting
        {
            get
            {
                return (t_set);
            }
            set
            {
                t_set = value;
            }
        }

        public double MoonFree
        {
            get
            {
                return (t_moonfree);
            }
            set
            {
                t_moonfree = value;
            }
        }

        public Celestial.RADec Position
        {
            get
            {
                return (t_position);
            }
            set
            {
                t_position = value;
            }
        }

        public Celestial.LatLon Location
        {
            get
            {
                return (t_location);
            }
            set
            {
                t_location = value;
            }
        }

        public double MinAlt
        {
            get
            {
                return (t_minalt);
            }
            set
            {
                t_minalt = value;
            }
        }

        public double MoonPhase
        {
            get
            {
                return (t_moonphase);
            }
            set
            {
                t_moonphase = value;
            }
        }

        public DateTime iRise
        {
            get
            {
                return (i_rise);
            }
            set
            {
                i_rise = value;
            }
        }

        public DateTime iSet
        {
            get
            {
                return (i_set);
            }
            set
            {
                i_set = value;
            }
        }

        //MoonPhase:  Compute approximate phase of moon as fraction based on sun ra (radians) and moon ra (radians)
        public void SetMoonPhase(double sunRA, double moonRA)
        {
            //moonazm in radians
            //sunazm in radians
            //The phase of the moon varies from 0 to 100% as sun - moon RA = +/-180 degrees (pi radians)
            //Note:  this is a bit of an approximation, except around the new and full moons
            //
            double dif = 1 - (Math.Abs(Math.PI - (Math.Abs(sunRA - moonRA)))) / Math.PI;
            t_moonphase = dif;
            return;
        }

        public bool IsUp(DateTime positionTime)
        {
            //Method returns true if (this object daily postion is above the horizon at the 
            //  positionTime or false if (it is not
            //if (()) {
            return true;
        }

    }
}
