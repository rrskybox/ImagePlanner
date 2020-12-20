using System;
using System.Collections.Generic;
using System.Linq;
using TheSkyXLib;
using AstroMath;

namespace ImagePlanner
{
    public class DBQObject
    {
        public string[] ObjectTypeName = {
                "STAR",
                "VARIABLE STAR",
                "SUSPECTED VARIABLE",
                "DOUBLE STAR",
                "GALAXY",
                "C TYPE GAlAXY",
                "ELLIPTICAL GALAXY",
                "LENTICULAR GALAXY",
                "SPIRAL GALAXY",
                "IRREGULAR GALAXY",
                "GALACTIC CLUSTER",
                "OPEN CLUSTER",
                "GLOBULAR CLUSTER",
                "NEBULA CLUSTER",
                "NEBULA",
                "BRIGHT NEBULA",
                "DARK NEBULA",
                "PLANETARY NEBULA",
                "ACTUAL STAR",
                "OTHER NGC",
                "MIXED DEEP SKY",
                "NST GSC",
                "QUASAR",
                "XRAY SOURCE",
                "RADIO SOURCE",
                "SUN",
                "MERCURY",
                "VENUS",
                "EARTH",
                "MARS",
                "JUPITER",
                "SATURN",
                "URANUS",
                "NEPTUNE",
                "PLUTO",
                "MOON",
                "COMET",
                "ASTEROID",
                "EXT COMET",
                "EXT ASTEROID",
                "SPACECRAFT"};

        public string Name;
        public int Type;
        public string TypeName;
        public double Size;
        public DateTime Rise;
        public DateTime Set;
        public double Altitude; //Not initialized
        public double Azm; //Not initialized
        public double Dec;
        public double RA;
        public double Lat;
        public double Long;
        public double Duration;
        public double MaxAltitude;
        public double JDate; //Not initialized

        public double DawnAlt;
        public double DuskAlt;

        public DBQObject(string noname, int notype, double nosize, DateTime norise, DateTime noset,
            double noDec, double noRA, double noLat, double noLong, double noduration, double nomaxalt)
        {
            Name = noname;
            Type = notype;
            TypeName = ObjectTypeName[notype];
            Size = nosize;
            Rise = norise;
            Set = noset;
            Dec = noDec;
            RA = noRA;
            Lat = noLat;
            Long = noLong;
            Duration = noduration;
            MaxAltitude = nomaxalt;
            return;
        }
    }

    public class ObjectList
    {
        private string oname;
        private int otype;
        private double osize;
        private DateTime orise;
        private DateTime oset;
        private double oDec;
        private double oRA;
        private double oLat;
        private double oLong;
        private double oduration;
        private double omaxaltitude;

        private List<DBQObject> dbqList = new List<DBQObject>();

        public ObjectList(DBQFileManagement.SearchType searchDB, DateTime duskDateLocal, DateTime dawnDateLocal)
        {
            //Determine if search database file exists, if not, create it
            if (!DBQFileManagement.DBQsInstalled())
            { DBQFileManagement.InstallDBQs(); }

            //Load the path to the selected search database query
            sky6DataWizard tsxdw = new sky6DataWizard();
            tsxdw.Path = DBQFileManagement.GetDBQPath(searchDB);
            //Set the search date for the dusk query
            sky6StarChart tsxs = new sky6StarChart();
            tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Latitude);
            oLat = tsxs.DocPropOut;
            tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Longitude);
            oLong = tsxs.DocPropOut;
            double jdate = Celestial.DateToJulian(duskDateLocal.ToUniversalTime());
            tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow, jdate);
            tsxdw.Open();
            //sky6ObjectInformation tsxoi = new sky6ObjectInformation();
            sky6ObjectInformation tsxoi = tsxdw.RunQuery;

            //Fill in data arrays (for speed purposes)
            int tgtcount = tsxoi.Count;
            for (int i = 0; i < tgtcount; i++)
            {
                tsxoi.Index = i;
                //tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_ALL_INFO);
                //var AllInfo = tsxoi.ObjInfoPropOut;

                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
                oname = (tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_OBJECTTYPE);
                otype = (tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_MAJ_AXIS_MINS);
                osize = (tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RISE_TIME);
                orise = (duskDateLocal - duskDateLocal.TimeOfDay).AddHours(tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_SET_TIME);
                oset = (duskDateLocal - duskDateLocal.TimeOfDay).AddHours(tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
                oDec = (tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
                oRA = (tsxoi.ObjInfoPropOut);
                //compute the duration
                oduration = Celestial.IntervalOverlap(duskDateLocal, dawnDateLocal, orise, oset);
                //compute the maximum altitude
                omaxaltitude = ComputeMaxAltitude(duskDateLocal, dawnDateLocal, oRA, oDec, oLat, oLong);
                // if the duration is greater than zero, then add it
                if (oduration > 0)
                {
                    dbqList.Add(new DBQObject(oname, otype, osize, oset, orise, oDec, oRA, oLat, oLong, oduration, omaxaltitude));
                }
            }
            //Note that all these entries should have at least some duration
            //Set the search date for the dawn query
            jdate = Celestial.DateToJulian(dawnDateLocal.ToUniversalTime());
            tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow, jdate);
            tsxdw.Open();
            tsxoi = tsxdw.RunQuery;

            //check each entry to see if it is already in the dusk list
            //  if so, just ignor, if not get the resf of the info and add it
            for (int i = 0; i < tsxoi.Count; i++)
            {
                tsxoi.Index = i;
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
                oname = (tsxoi.ObjInfoPropOut);
                bool newEntry = true;
                foreach (DBQObject tgt in dbqList)
                {
                    if (tgt.Name == oname)
                    {
                        newEntry = false;
                        break;
                    }
                }
                if (newEntry)
                {
                    tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_OBJECTTYPE);
                    otype = (tsxoi.ObjInfoPropOut);
                    tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_MAJ_AXIS_MINS);
                    osize = (tsxoi.ObjInfoPropOut);
                    tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RISE_TIME);
                    orise = (duskDateLocal - duskDateLocal.TimeOfDay).AddHours(tsxoi.ObjInfoPropOut);
                    tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_SET_TIME);
                    oset = (duskDateLocal - duskDateLocal.TimeOfDay).AddHours(tsxoi.ObjInfoPropOut);
                    tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
                    oDec = (tsxoi.ObjInfoPropOut);
                    tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
                    oRA = (tsxoi.ObjInfoPropOut);
                    //compute the duration
                    oduration = Celestial.IntervalOverlap(duskDateLocal, dawnDateLocal, orise, oset);
                    //compute the maximum altitude
                    omaxaltitude = ComputeMaxAltitude(duskDateLocal, dawnDateLocal, oRA, oDec, oLat, oLong);
                    // if the duration is greater than zero, then add it
                    if (oduration > 0)
                    {
                        dbqList.Add(new DBQObject(oname, otype, osize, oset, orise, oDec, oRA, oLat, oLong, oduration, omaxaltitude));
                    }
                }
            }

            //Now clear out all the entries that have no duration between
            //Reset tsx to computer clock
            tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_UseComputerClock, 1);
            tsxs = null;
            tsxoi = null;
            return;
        }

        public int Count
        {
            get { return (dbqList.Count); }
            set { return; }
        }

        public string TgtName(int entry)
        {
            return dbqList[entry].Name;
        }

        public int TypeId(int entry)
        {
            return dbqList[entry].Type;
        }

        public string TypeName(int entry)
        {
            return dbqList[entry].TypeName;
        }

        public double TgtSize(int entry)
        {
            return dbqList[entry].Size;
        }

        public double TgtDuration(int entry)
        {
            return dbqList[entry].Duration;
        }

        public double TgtMaxAltitude(int entry)
        {
            return (dbqList[entry].MaxAltitude);
        }

        public double TgtDawnAltitude(int entry)
        {
            return (dbqList[entry].DawnAlt);
        }

        public double TgtDuskAltitude(int entry)
        {
            return (dbqList[entry].DuskAlt);
        }

        public double TgtAzimuth(int entry)
        {
            return (dbqList[entry].Azm);
        }

        public double TgtRA(int entry)
        {
            return dbqList[entry].RA;
        }

        public double TgtDec(int entry)
        {
            return dbqList[entry].Dec;
        }

        public double TgtLat(int entry)
        {
            return dbqList[entry].Lat;
        }

        public double TgtLong(int entry)
        {
            return dbqList[entry].Long;
        }

        public double TgtJdate(int entry)
        {
            return dbqList[entry].JDate;
        }

        public int TgtFind(string tname)
        {
            for (int i = 0; i < dbqList.Count; i++)
            {
                if (tname == dbqList[i].Name)
                { return i; }
            }
            return 0;
        }

        public List<DBQObject> SizeSort()
        {
            //Sort list on size of object
            dbqList = dbqList.OrderBy(i => -i.Size).ToList();
            return dbqList;
        }
        public List<DBQObject> AltitudeSort()
        {
            //Sort list on size of object
            dbqList = dbqList.OrderBy(i => -i.Altitude).ToList();
            return dbqList;
        }
        public List<DBQObject> DurationSort()
        {
            //Sort list on size of object
            dbqList = dbqList.OrderBy(i => i.Set).ToList();
            return dbqList;
        }

        private double ComputeMaxAltitude(DateTime duskLocal, DateTime dawnLocal, double RAHours, double DecDegrees, double latitudeDegrees, double longitudeDegrees)
        // Returns maximum altitude for object at RA/Dec between times dusk and dawn
        {
            Celestial.LatLon location = new Celestial.LatLon(Transform.DegreesToRadians(latitudeDegrees), Transform.DegreesToRadians(longitudeDegrees));
            Celestial.RADec position = new Celestial.RADec(Transform.HoursToRadians(RAHours), Transform.DegreesToRadians(DecDegrees));
            double maxAlt = Transform.RadiansToDegrees(AstroMath.DailyPosition.MaxAltitude(duskLocal.ToUniversalTime(), dawnLocal.ToUniversalTime(), position, location));
            return maxAlt;
        }

        private double ComputeAltitude(DateTime timeLocal, double RAHours, double DecDegrees, double latitudeDegrees, double longitudeDegrees)
        // Returns maximum altitude for object at RA/Dec between times dusk and dawn
        {
            Celestial.LatLon location = new Celestial.LatLon(Transform.DegreesToRadians(latitudeDegrees), Transform.DegreesToRadians(longitudeDegrees));
            Celestial.RADec position = new Celestial.RADec(Transform.HoursToRadians(RAHours), Transform.DegreesToRadians(DecDegrees));
            double maxAlt = Transform.RadiansToDegrees(position.Altitude((position.HourAngle(timeLocal.ToUniversalTime(), location)), location));
            return maxAlt;
        }

    }
}

