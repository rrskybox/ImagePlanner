using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
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

        string nhDir;

        //Configuration Element Names

        public static string sbTargetNameName = "TargetName";
        public static string sbTargetAdjustCheckedName = "TargetAdjustChecked";
        public static string sbTargetRAName = "TargetRA";
        public static string sbTargetDecName = "TargetDec";
        public static string sbTargetPAName = "TargetPA";

        public Xccess Xmlf;

        public XFiles()
        {
            nhDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + HumasonFolderName;
            if ((!(Directory.Exists(nhDir + "\\" + HumasonFolderName))))  //no directory, so create it
            {
                Directory.CreateDirectory(nhDir);
            }
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
                    //fixName = fixName.Replace("")
                    targetNames.Add(fixName);
                }
                if ((fname.Length == 4))
                {
                    targetNames.Add(fname[0] + "." + fname[1] + "." + fname[2]);
                }
            }
            return targetNames;
        }

        public string GetItem(string itemName)
        {
            return Xmlf.GetItem(itemName);
            //string hnCfgFilePath = nhDir + "\\" + HumasonTargetPlanFilename;
            //XElement hnCfgX = XElement.Load(hnCfgFilePath);
            //IEnumerable<XElement> itemX = hnCfgX.Elements(itemName);
            //if (itemX.Count() == 0)
            //{
            //    hnCfgX.Add(new XElement(itemName, null));
            //    return null;
            //}
            //else { return (itemX.ElementAt(0).Value); }
        }

        //public string GetItem(string itemSection, string itemName)
        //{
        //    return Xmlf.GetItem(itemSection, itemName);
        //    ////Retrieves entry from two levels deep
        //    //string hnCfgFilePath = nhDir + "\\" + HumasonTargetPlanFilename;
        //    //XElement hnCfgX = XElement.Load(hnCfgFilePath);
        //    //XElement sectionX = hnCfgX.Element(itemSection);
        //    ////Check section, if doesn//t exist, then return nothing
        //    //if (!sectionX.HasElements)
        //    //{
        //    //    return null;
        //    //}
        //    //else
        //    //{
        //    //    //Otherwise, look through the section for the itemname
        //    //    //if found, then return the entry, if Not, make a nothing entry
        //    //    IEnumerable<XElement> itemX = sectionX.Elements(itemName);
        //    //    if (itemX.Count() == 0)
        //    //    {
        //    //        return null;
        //    //    }
        //    //    else
        //    //    {
        //    //        return itemX.ElementAt(0).Value;
        //    //    }
        //    //}
        //}

        public void SetItem(string itemName, string item)
        {
            Xmlf.SetItem(itemName, item);
            //string hnCfgFilePath = nhDir + "\\" + HumasonTargetPlanFilename;
            //XElement hnCfgX = XElement.Load(hnCfgFilePath);
            //IEnumerable<XElement> sscfgXel = hnCfgX.Elements(itemName);
            //if ((sscfgXel.Count() == 0))
            //{
            //    hnCfgX.Add(new XElement(itemName, item));
            //}
            //else
            //{
            //    sscfgXel.ElementAt(0).ReplaceWith(new XElement(itemName, item));
            //}
            //hnCfgX.Save(hnCfgFilePath);
            //return;
        }

        //public void SetItem(string sectionName, string itemName, string item)
        //{
        //    //Set entry for level two-deep element
        //    //if ( the item Is in the file, but the entry Is "nothing" then just delete the entry

        //    string hnCfgFilePath = nhDir + "\\" + HumasonTargetPlanFilename;
        //    XElement hnCfgX = XElement.Load(hnCfgFilePath);
        //    IEnumerable<XElement> sectionX = hnCfgX.Elements(sectionName);
        //    if ((sectionX.Count() == 0))
        //    {
        //        XElement itemX = new XElement(itemName, item);
        //        hnCfgX.Add(new XElement(sectionName, itemX));
        //    }
        //    else
        //    {
        //        //check section for entry with itemName, if none then add, otherwise replace
        //        XElement itemX = sectionX.Element(itemName);
        //            if ((!itemX.HasAttributes))
        //        {
        //            if ((item <> ""))
        //            {
        //                sectionX.Add(new XElement(itemName, item))
        //                         }
        //            else
        //            {
        //                if ((item <> ""))
        //                {
        //                    itemX.ReplaceWith(new XElement(itemName, item))
        //                    }
        //                else
        //                {
        //                    itemX.Remove()
        //                    }
        //            }
        //        }
        //    }
        //    hnCfgX.Save(hnCfgFilePath)
        //            return;

        //}

        public bool InitialItem(string ItemName, bool Item)
        {
            //if ( the xfile doesn//t have the member, then the original item is returned,
            // otherwise the element in the xfile Is returned
            return Xmlf.InitialItem(ItemName, Item);
            //string itemStr = GetItem(ItemName)
            //    if ((itemStr = ""))
            //            {
            //                SetItem(ItemName, Convert.ToString(Item))
            //        return; Item
            //} else {
            //        return; (Convert.ToBoolean(GetItem(ItemName)))
            //    }

        }

        public double InitialItem(string ItemName, double Item)
        {
            //if ( the xfile doesn//t have the member, then the original item is returned,
            // otherwise the element in the xfile Is returned
            return Xmlf.InitialItem(ItemName, Item);
            //string itemStr = GetItem(ItemName)
            //if ((itemStr = ""))
            //        {
            //            SetItem(ItemName, Convert.ToString(Item))
            //    return Item;
            //     } else {
            //    return;(Convert.ToDouble(GetItem(ItemName)))
            //     }
        }

        public int InitialItem(string ItemName, int Item)
        {
            //if ( the xfile doesn//t have the member, then the original item is returned,
            // otherwise the element in the xfile Is returned
            return Xmlf.InitialItem(ItemName, Item);
            //string itemStr = GetItem(ItemName)
            //if ((itemStr = ""))
            //{
            //    SetItem(ItemName, Convert.ToString(Item))
            //return; Item
            // }
            //else
            //{
            //    return; (Convert.ToInt32(GetItem(ItemName)))
            //  }
        }

        public string InitialItem(string ItemName, string Item)
        {
            return Xmlf.InitialItem(ItemName, Item);
            //if ( the xfile doesn//t have the member, then the original item is returned,
            // otherwise the element in the xfile is returned

            //    string itemStr = GetItem(ItemName)
            //    if ((itemStr = ""))
            //    {
            //        SetItem(ItemName, " ")
            //    return; Item
            //}
            //    else
            //    {
            //        return; (Convert.ToString(GetItem(ItemName)))
            //        }
        }

        public DateTime InitialItem(string ItemName, DateTime Item)
        //if ( the xfile doesn//t have the member, then the original item is returned,
        // otherwise the element in the xfile is returned
        {
            return Xmlf.InitialItem(ItemName, Item);
            //    string itemStr = GetItem(ItemName)
            //if ((itemStr = ""))
            //        {
            //            SetItem(ItemName, Convert.ToString(Item))
            //    return; Item
            //} else {
            //    return;(DateTime.Parse(itemStr))
            //}
        }

        //        public InitialItem(string fSectionName, Filter fItem As Filter) As Boolean
        //        //For filters, this is a bit different.
        //        //if ( the filter is in the configuration file, then it is assumed active
        //        //  && only the filter index is updated.  if ( it is !in the configuration file,
        //        //  then no entry is made.  The return is either a true or false -- true indicates the filter
        //        //  is in the configuration file && thus, should be checked off.

        //string eFilterIndex = GetItem(fSectionName, fItem.Name)
        //        if ((eFilterIndex = ""))
        //                {
        //                    return; False
        //    } else {
        //            SetItem(fSectionName, fItem.Name, fItem.Index.ToString())
        //            return; true
        //        }

        //                }

        //        public InitialItem(string fSectionName, Flat fItem) As Boolean

        //        //For flats, this is a bit different
        //        //  if the side of pier is there, && the ra is the same, then move on
        //        //  otherwise, save the side of pier && pa

        //string eFlatPA = GetItem(fSectionName, fItem.SideOfPier)
        //        if ((eFlatPA = ""))
        //                { //no flat for this side
        //                    return; False
        //    } else {if((Convert.ToDouble(eFlatPA) <> fItem.RotationPA)) {
        //                        SetItem(fSectionName, fItem.SideOfPier, fItem.RotationPA.ToString())
        //            return; true
        //        }
        //                    return; False
        //    }

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

        //        public void ReplaceItem(string fSectionName, Filter fItem As Filter)
        //        //The item is placed in the xfile -- inside the filter sub-element
        //        //string eFilterNumber = GetItem(fSectionName, fItem.Name)
        //SetItem(fSectionName, fItem.Name, Convert.ToString(fItem.Index))
        //        return;
        //    }

        //    public void ReplaceItem(string fSectionName, Flat fItem As Flat)
        //        //The item is placed in the xfile -- inside the flat sub-element
        //        SetItem(fSectionName, fItem.SideOfPier, Convert.ToString(fItem.RotationPA))
        //        return;
        //    }

        public bool CheckItem(string ItemName)
        {
            //Checks to see if any element named ItemName exists in the configuration file
            //The item is placed in the xfile -- string
            return Xmlf.CheckItem(ItemName);
        }

        //public void NullItem(string fSectionName, Filter fItem As Filter)
        //        //The filter item is nulled in the xfile, although the entry is kept
        //        string eFilterNumber = GetItem(fSectionName, fItem.Name)
        //        SetItem(fSectionName, fItem.Name, Nothing)
        //    }

        //        public List<Filter> GetFilters()
        //        {
        //            //Read && return the set of entries stored in the configuration file
        //            // , in ths case, in the form of Filter objects

        //            string hnCfgFilePath = nhDir + "\\" + HumasonTargetPlanFilename
        //            XElement hnCfgX = XElement.Load(hnCfgFilePath)
        //            XElement sectionX = hnCfgX.Element(suFilterSetName)
        //            //Check section, if doesn//t exist, then return nothing
        //        if ((sectionX Is Nothing)) {
        //                return; Nothing
        //} else {
        //                //Otherwise, look through the section for the itemname
        //                //if found, then return the entry, if not, make a nothing entry
        //                Dim filterCount As Integer = sectionX.Elements().Count()
        //                 List<Filter> fSet = new List<Filter>();
        //                foreach (fx in sectionX.Elements()
        //                                fSet.Add(new Filter(fx.Name.ToString(), Convert.ToInt32(fx.Value)))
        //                 }
        //            return; fSet
        //            }
        //    }

        // <summary>
        // Read && return the set of entries stored in the configuration file
        // </summary>
        // <returns>List of type Flats in flats file</returns>


    }
}
