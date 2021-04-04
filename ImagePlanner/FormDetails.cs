using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheSky64Lib;
using System.Xml;
using System.Xml.Linq;

namespace ImagePlanner
{
    public partial class FormDetails : Form
    {
        public FormDetails(string targetName)
        {
            InitializeComponent();
            FillInTargetDetails(targetName);
            Show();
            return;
        }

        private void FillInTargetDetails(string tName)
        {
            //Retrieves details from TSX about the target object
            sky6StarChart tsxs = new sky6StarChart();
            sky6ObjectInformation tsxo = new sky6ObjectInformation();
            try
            {
                tsxs.Find(tName);
            }
            catch
            {
                return;
            }

            tsxo.Index = 0;
            tsxo.Property(TheSky64Lib.Sk6ObjectInformationProperty.sk6ObjInfoProp_ALL_INFO);
            string sAllInfo = tsxo.ObjInfoPropOut;
            sAllInfo = sAllInfo.Replace("/", "-");

            string[] sInfoDB = sAllInfo.Split('\n');
            XElement infoX = new XElement("All_Properties");
            foreach (string ipair in sInfoDB)
            {
                string[] infoPair = ipair.Split(':');
                infoPair[0] = infoPair[0].Replace(" ", "_");
                string[] firstSpace = infoPair[0].Split('(');
                if (firstSpace[0] != "")
                {
                    firstSpace[0] = firstSpace[0].Trim('_');
                    infoPair[1] = infoPair[1].Trim(' ');
                    infoX.Add(new XElement(firstSpace[0], infoPair[1]));
                }
            }
            //Get rid of multiple constellations.  Got to do it twice for some reason
            foreach (XElement xmv in infoX.Elements("Constellation"))
            {
                if (xmv.Value.Length < 4)
                {
                    xmv.Remove();
                }
            }
            foreach (XElement xmv in infoX.Elements("Constellation"))
            {
                if (xmv.Value.Length < 4)
                {
                    xmv.Remove();
                }
            }
            //Get rid of the first RA (that//s the current, not J2000)
            XElement xra = infoX.Element("RA");
            xra.Remove();
            XElement xdec = infoX.Element("Dec");
            xdec.Remove();
            //read out interesting data
            string details = "";
            details += "Object:        " + EntryCheck(infoX, "Object_Name");
            details += "Catalog Id:    " + EntryCheck(infoX, "Catalog_Identifier");
            details += "Object Type:   " + EntryCheck(infoX, "Object_Type");
            details += "Constellation: " + EntryCheck(infoX, "Constellation");
            details += "Magnitude:     " + EntryCheck(infoX, "Magnitude");
            details += "Major Axis:    " + EntryCheck(infoX, "Major_Axis");
            details += "Minor Axis:    " + EntryCheck(infoX, "Minor_Axis");
            details += "Axis PA:       " + EntryCheck(infoX, "Axis_Position_Angle");
            details += "RA (J2000):    " + EntryCheck(infoX, "RA");
            details += "Dec (J2000):   " + EntryCheck(infoX, "Dec");
            details += "\r\n";

            DetailTextBox.Text = details;

            return;
        }

        private string EntryCheck(XElement xem, string name)
        {
            //reads and checks xelement entry) { returns either the contents or N/A, with vbcrlf appended
            if (xem.Element(name) == null)
            {
                return "\t" + "N/A" + "\r\n";
            }
            else
            {
                return "\t" + xem.Element(name).Value + "\r\n";
            }
        }

    }
}
