using System;
using System.Xml.Linq;


namespace ImagePlanner
{
    public class FOVX
    {
        const int headerLength = 11;
        const int elementLength = 8;

        public string ActiveFieldXName = "Active";
        public string ReferenceFrameFieldXName = "ReferenceFrame";
        public string Description1FieldXName = "Description1";
        public string PositionAngleFieldXName = "PositionAngle";
        public string OffsetXFieldXName = "OffsetX";
        public string OffsetYFieldXName = "OffsetY";
        public string ScaleFieldXName = "Scale";
        public string EnabledFieldXName = "Enabled";
        public string Description2FieldXName = "Description2";
        public string UnitsFieldXName = "Units";

        public string ShapeFieldXName = "Shape";
        public string ElementDescriptionFieldXName = "ElementDescription";
        public string SizeXFieldXName = "SizeX";
        public string SizeYFieldXName = "SizeY";
        public string PixelsXFieldXName = "PixelsX";
        public string PixelsYFieldXName = "PixelsY";
        public string CenterOffsetXFieldXName = "CenterOffsetX";
        public string CenterOffsetYFieldXName = "CenterOffsetY";
        public string LinkedFieldXName = "Linked";
        public string ReversedFieldXName = "Reversed";

        public string FOVElementNumberXName = "FOVElementNumber";

        public string FOVIndicatorXName = "FOVIndicator";
        public string FOVElementXName = "FOVElement";

        public string fovdir;
        public string fovfile;
        public string fovXfile;
        public XElement xFovList;

        public FOVX()
        {

            fovdir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Software Bisque\\TheSkyX Professional Edition\\Field of View Indicators";
            fovfile = fovdir + "\\My Equipment.txt";
            fovXfile = fovdir + "\\My Equipment.xml";
            System.IO.TextReader fovDataFile = System.IO.File.OpenText(fovfile);
            //create xml object
            xFovList = new XElement("FieldOfViewIndicators");

            string fovline = fovDataFile.ReadLine();
            //skip past all field definition lines for now, maybe forever
            while (fovline != null)
            {
                if ((fovline.Contains("[F]")) && (fovline[0] != ';'))
                {
                    XElement xfovI = new XElement(FOVIndicatorXName);
                    string[] splitline;
                    int fElementCount;

                    splitline = fovline.Split('|');
                    fElementCount = (splitline.Length - headerLength) / elementLength;
                    xfovI.Add(new XElement(ActiveFieldXName, splitline[1]));
                    xfovI.Add(new XElement(ReferenceFrameFieldXName, splitline[2]));
                    xfovI.Add(new XElement(Description1FieldXName, splitline[3]));
                    xfovI.Add(new XElement(PositionAngleFieldXName, splitline[4]));
                    xfovI.Add(new XElement(OffsetXFieldXName, splitline[5]));
                    xfovI.Add(new XElement(OffsetYFieldXName, splitline[6]));
                    xfovI.Add(new XElement(ScaleFieldXName, splitline[7]));
                    xfovI.Add(new XElement(EnabledFieldXName, splitline[8]));
                    xfovI.Add(new XElement(Description2FieldXName, splitline[9]));
                    xfovI.Add(new XElement(UnitsFieldXName, splitline[10]));

                    for (int elm = 0; elm < fElementCount; elm++)
                    {
                        int splitIndx = elementLength * elm + headerLength;
                        XElement xelm = new XElement(FOVElementXName);
                        xelm.Add(new XElement(ShapeFieldXName, splitline[splitIndx + 0]));
                        xelm.Add(new XElement(ElementDescriptionFieldXName, splitline[splitIndx + 1]));
                        xelm.Add(new XElement(SizeXFieldXName, splitline[splitIndx + 2]));
                        xelm.Add(new XElement(SizeYFieldXName, splitline[splitIndx + 3]));
                        xelm.Add(new XElement(PixelsXFieldXName, splitline[splitIndx + 4]));
                        xelm.Add(new XElement(PixelsYFieldXName, splitline[splitIndx + 5]));
                        xelm.Add(new XElement(CenterOffsetXFieldXName, splitline[splitIndx + 6]));
                        xelm.Add(new XElement(CenterOffsetYFieldXName, splitline[splitIndx + 7]));
                        //xelm.Add(new XElement(Field_19, splitline(splitIndx + 8)));
                        //xelm.Add(new XElement(Field_20, splitline(splitIndx + 9)));
                        xelm.Add(new XElement(FOVElementNumberXName, elm.ToString()));
                        xfovI.Add(xelm);
                    }
                    xFovList.Add(xfovI);
                }
                fovline = fovDataFile.ReadLine();
            }
            xFovList.Save(fovXfile);
            fovDataFile.Close();
            return;
        }

        public string GetActiveFOVHead(string headingEntryName)
        {
            //Get content of an element identified by elementName, of first active fov
            foreach (XElement fovEntry in xFovList.Elements(FOVIndicatorXName))
            {
                if (fovEntry.Element(ActiveFieldXName).Value == "1")
                {
                    XElement hdrElement = fovEntry.Element(headingEntryName);
                    return (hdrElement.Value);
                }
            }
            return null;
        }

        public string GetActiveFOVElementEntry(int fovIndicatorElementNumber, string fovIndicatorElementComponent)
        {
            //Get content of an specific element, of first active fov
            foreach (XElement xfovEntry in xFovList.Elements(FOVIndicatorXName))
            {
                if (Convert.ToInt16(xfovEntry.Element(ActiveFieldXName).Value) == 1)
                {
                    foreach (XElement xfovelm in xfovEntry.Elements(FOVElementXName))
                    {
                        int testx = Convert.ToInt16(xfovelm.Element(FOVElementNumberXName).Value);
                        if (Convert.ToInt16(xfovelm.Element(FOVElementNumberXName).Value) == fovIndicatorElementNumber)
                        {
                            return (xfovelm.Element(fovIndicatorElementComponent).Value);
                        }
                    }
                }
            }
            return (null);
        }

    }
}

