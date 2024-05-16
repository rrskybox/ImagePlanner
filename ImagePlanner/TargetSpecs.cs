using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSky64Lib;


namespace ImagePlanner

{
    class TargetSpecs
    {
        //Class to look up the information about an object using TSX
        //
        public string TargetName { get; set; }
        public string Name2 { get; set; }
        public string TargetType { get; set; }
        public string MajorAxis { get; set; }
        public double MajorAxisF { get; set; }
        public string MinorAxis { get; set; }
        public double MinorAxisF { get; set; }
        public string Altitude { get; set; }
        public double AltitudeF { get; set; }
        public TimeSpan RiseTime { get; set; }
        public TimeSpan SetTime { get; set; }
        public TimeSpan TransitTime { get; set; }
        public double PhasePercent { get; set; }
        public TimeSpan TwilightEODTime { get; set; }
        public TimeSpan TwilightSODTime { get; set; }
        public string Constellation { get; set; }

        public TargetSpecs(string targetName)
        {
            //Use TSX find to load information about the given target
            //Open TSX starchart object
            TargetName = targetName;
            sky6StarChart tsxsc = new sky6StarChart();
            try { tsxsc.Find(targetName); }
            catch (Exception ex) { return; }
            sky6ObjectInformation tsxoi = new sky6ObjectInformation();
            int objIndex = 0;
            //
            int oCount = tsxoi.Count;
            tsxoi.Index = objIndex;
            int pExists;

            tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RISE_TIME);
            RiseTime = TimeSpan.FromHours(tsxoi.ObjInfoPropOut);

            tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_SET_TIME);
            SetTime = TimeSpan.FromHours(tsxoi.ObjInfoPropOut);

            tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_TRANSIT_TIME);
            TransitTime = TimeSpan.FromHours(tsxoi.ObjInfoPropOut);

            tsxoi.PropertyApplies(Sk6ObjectInformationProperty.sk6ObjInfoProp_PHASE_PERC);
            pExists = tsxoi.ObjInfoPropOut;
            if (pExists != 0)
            {
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_PHASE_PERC);
                PhasePercent = tsxoi.ObjInfoPropOut;
            }

            tsxoi.PropertyApplies(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_START);
            pExists = tsxoi.ObjInfoPropOut;
            if (pExists != 0)
            {
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_START);
                TwilightSODTime = TimeSpan.FromHours(tsxoi.ObjInfoPropOut);
            }

            tsxoi.PropertyApplies(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_END);
            pExists = tsxoi.ObjInfoPropOut;
            if (pExists != 0)
            {
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_END);
                TwilightEODTime = TimeSpan.FromHours(tsxoi.ObjInfoPropOut);
            }

            //TSX always returns 0 (false) on PropertyApplies for ALL_INFO, so ignor it
            tsxoi.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_ALL_INFO);
            string sAllInfo = tsxoi.ObjInfoPropOut;
            sAllInfo = sAllInfo.Replace("/", "-");
            string[] sInfoDB = sAllInfo.Split('\n');

            foreach (string s in sInfoDB)
            {
                if (s.Length > 0)
                {
                    //Separage property name from data
                    string[] dProp = s.Split(':');
                    string dKey = dProp[0].Trim();
                    string dData = dProp[1].Trim();
                    //Store data in appropriate variable
                    switch (dKey)
                    {
                        case "Constellation":
                            {
                                Constellation = dData;
                                break;
                            }
                        case "Name 2":
                            {
                                Name2 = dData;
                                break;
                            }
                        case "Object Type":
                            {
                                TargetType = dData;
                                break;
                            }
                        case "Minor Axis":
                            {
                                MinorAxisF = double.Parse(dData);
                                break;
                            }
                        case "Altitude":
                            {
                                AltitudeF = Utility.SexyParse(dData);
                                break;
                            }
                        case "Major Axis":
                            {
                                MajorAxisF = double.Parse(dData);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }
    }
}

