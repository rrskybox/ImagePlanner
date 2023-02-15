using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Humason
{
    public class XFiles
    {
        // Configuration Class
        //
        // ------------------------------------------------------------------------
        // Module Name: Configuration 
        // Purpose: Store && retrieve configuration data
        // Developer: Rick McAlister
        // Creation Date:  6/6/2017
        // Major Modifications:
        // Copyright: Rick McAlister, 2017
        // 
        // Description: TBD
        // 
        // ------------------------------------------------------------------------

        //Private data

        string HumasonFolderName = "Humason";
        string HumasonTargetPlanFilename = "TargetPlan.xml";
        string HumasonTargetPlanXName = "HumasonTargetPlan";
        string HumasonDefaultTargetPlanFilename = "TargetPlanDefault.xml";
        string HumasonTargetPlanSearchPattern = "*.TargetPlan.xml";
        string HumasonSessionControlFilename = "SessionControl.xml";

        string nhDir;

        //Configuration Element Names

        public static string sbTargetNameName = "TargetName";
        public static string sbTargetAdjustCheckedName = "TargetAdjustChecked";
        public static string sbTargetRAName = "TargetRA";
        public static string sbTargetDecName = "TargetDec";
        public static string sbTargetPAName = "TargetPA";

        public Xccess Xmlf;
        public Xccess Xses;

        public XFiles()
        {
            nhDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + HumasonFolderName;
            if (!Directory.Exists(nhDir))  //no directory, so create it
                Directory.CreateDirectory(nhDir);
            else
                //Open session control xml file
                Xses = new Xccess(nhDir + "\\" + HumasonSessionControlFilename);
            return;
        }

        public XFiles(string targetName)
        {
            //Checks for an existing project plan as named targetName.  If so, return as an Xccess object.
            //if not, then create a new default project plan from the TargetPlanDefault xml file in the Humason directory
            //  and return it as an Xcess object.
            nhDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + HumasonFolderName;
            string nhTargetFilePath = nhDir + "\\" + targetName + "." + HumasonTargetPlanFilename;
            string nhDefaultFilePath = nhDir + "\\" + HumasonDefaultTargetPlanFilename;
            string nhSessionControlPath = nhDir + "\\" + HumasonSessionControlFilename;
            //convert target name to file name, notably change "/" to "_"
            string targetFileName = targetName.Replace("/", "_");
            if ((!(Directory.Exists(nhDir + "\\" + HumasonFolderName))))
            {
                Directory.CreateDirectory(nhDir);
            }
            if ((!(File.Exists(nhTargetFilePath))))
            {
                if ((!(File.Exists(nhDefaultFilePath)))) //No target xml file and no default xml file so just create null target file
                {
                    XElement cDefaultX = new XElement(HumasonTargetPlanXName);
                    cDefaultX.Save(nhDir + "\\" + targetFileName + "." + HumasonTargetPlanFilename);
                }
                else //No target xml file but there is a default target file so use it to create a new target file.
                {
                    XElement hnTgtX = XElement.Load(nhDefaultFilePath);
                    hnTgtX.Save(nhDir + "\\" + targetFileName + "." + HumasonTargetPlanFilename);
                }
            }
            //Create new Xccess object from whatever was found
            Xmlf = new Xccess(nhDir + "\\" + targetFileName + "." + HumasonTargetPlanFilename);
            return;
        }

        public bool SavePlan(string targetName)
        {
            //This command will save a copy of the current configuration.xml file
            //under the name targetname.configuration.xml
            //convert target name to file name, notably change "/" to "_"
            string targetFileName = targetName.Replace("/", "_");
            XElement hnCfgX = Xmlf.GetXccessFileX();
            hnCfgX.Save(nhDir + "\\" + targetFileName + "." + HumasonTargetPlanFilename);
            return true;
        }

        public void DeletePlan(string targetName)
        {
            //Removes the configuration file with the filename targetname
            //convert target name to file name, notably change "/" to "_"
            string targetFileName = targetName.Replace("/", "_");
            string cfgFolderName = nhDir;
            string cfgFilePath = cfgFolderName + "\\" + targetFileName + "." + HumasonTargetPlanFilename;
            System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("Are you sure you want to remove " + targetName + "?",
                "Confirm Deletion",
                System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                File.Delete(cfgFilePath);
            }
            return;
        }

        public void ReplacePlan(string targetName)
        {
            //Removes the configuration file with the filename targetname
            //convert target name to file name, notably change "/" to "_"
            string targetFileName = targetName.Replace("/", "_");
            string cfgFolderName = nhDir;
            string tgtFilePath = cfgFolderName + "\\" + targetFileName + "." + HumasonTargetPlanFilename;
            string cfgFilePath = cfgFolderName + "\\" + HumasonTargetPlanFilename;
            File.Copy(tgtFilePath, cfgFilePath, true);
            return;
        }

        public List<string> GetTargetFiles()
        {
            //return;s list of configuration filenames for targets
            //Get a list of files from the Humason directory

            List<string> targetNames = new List<string>();
            string[] tgtProspectPaths = Directory.GetFiles(nhDir, HumasonTargetPlanSearchPattern);
            foreach (string sFile in tgtProspectPaths)
            {
                string[] fname = Path.GetFileNameWithoutExtension(sFile).Split('.');
                if (fname.Length == 2)
                {
                    string fixName = fname[0].Replace("_", "/");
                    targetNames.Add(fixName);
                }
                if ((fname.Length == 4))
                {
                    targetNames.Add(fname[0] + "." + fname[1] + "." + fname[2]);
                }
            }
            return targetNames;
        }

        public string GetCurrentHumasonTarget()
        {
            //return target name currently loaded in Humason, if any
            if (Xses != null)
                return Xses.GetItem("CurrentTargetName");
            else
                return null;
        }

        public string GetItem(string itemName)
        {
            return Xmlf.GetItem(itemName);
        }

        public void SetItem(string itemName, string item)
        {
            Xmlf.SetItem(itemName, item);
        }



        public bool InitialItem(string ItemName, bool Item)
        {
            //if ( the xfile doesn//t have the member, then the original item is returned,
            // otherwise the element in the xfile Is returned
            return Xmlf.InitialItem(ItemName, Item);
        }

        public double InitialItem(string ItemName, double Item)
        {
            //if ( the xfile doesn//t have the member, then the original item is returned,
            // otherwise the element in the xfile Is returned
            return Xmlf.InitialItem(ItemName, Item);
        }

        public int InitialItem(string ItemName, int Item)
        {
            //if ( the xfile doesn//t have the member, then the original item is returned,
            // otherwise the element in the xfile Is returned
            return Xmlf.InitialItem(ItemName, Item);
        }

        public string InitialItem(string ItemName, string Item)
        {
            return Xmlf.InitialItem(ItemName, Item);
        }

        public DateTime InitialItem(string ItemName, DateTime Item)
        //if ( the xfile doesn//t have the member, then the original item is returned,
        // otherwise the element in the xfile is returned
        {
            return Xmlf.InitialItem(ItemName, Item);
        }


        public void ReplaceItem(string ItemName, bool Item)
        //The item is placed in the xfile -- boolean
        {
            Xmlf.ReplaceItem(ItemName, Item);
            return;
        }

        public void ReplaceItem(string ItemName, double Item)
        //The item is placed in the xfile -- double
        {
            Xmlf.ReplaceItem(ItemName, Item);
            return;
        }

        public void ReplaceItem(string ItemName, int Item)
        //The item is placed in the xfile -- integer
        {
            Xmlf.ReplaceItem(ItemName, Item);
            return;
        }

        public void ReplaceItem(string ItemName, DateTime Item)
        //The item is placed in the xfile -- string
        {
            Xmlf.ReplaceItem(ItemName, Item);
            return;
        }

        public void ReplaceItem(string ItemName, string Item)
        {
            //The item is placed in the xfile -- string
            Xmlf.ReplaceItem(ItemName, Item);
            return;
        }

        public bool CheckItem(string ItemName)
        {
            //Checks to see if any element named ItemName exists in the configuration file
            //The item is placed in the xfile -- string
            return Xmlf.CheckItem(ItemName);
        }

    }
}
