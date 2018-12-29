//Class for accessing XML files in Humason

using System;
using System.Xml.Linq;
using System.IO;

namespace Humason
{
    public class Xccess
    {
        private string XMLFilePath = null;

        public Xccess(string nhXMLFilePath)
        {
            XMLFilePath = nhXMLFilePath;
            return;
        }

        public Xccess(string nhXMLFilePath, string nhContainerX)
        {
            if (!(File.Exists(nhXMLFilePath)))
            {
                XElement cDefaultX = new XElement(nhContainerX);
                cDefaultX.Save(nhXMLFilePath);
            }
            XMLFilePath = nhXMLFilePath;
            return;
        }

        public void AppendContentsToXFile(string nhXContentsPath)
        {
            //Normally called to Add a summary entry to the summary file
            //  where this class instance represents a summary file, and the

            //  ContentFile represents a class instance of a targetplan

            //Get the contents of this file
            XElement nhThisX = GetXccessFileX();
             //Get the contents of the file to be added in
            XElement nhThatX = XElement.Load(nhXContentsPath);
           //Build new summary element
            nhThisX.Add(nhThatX);
            SetXccessFileX(nhThisX);
            return;
        }

        public XElement GetXccessFileX()
        {
            //Gets the whole contents of an xml file for this class instance
            return XElement.Load(XMLFilePath);
        }

        public void SetXccessFileX(XElement fileXContents)
        {
            //Saves the whole contents to the xml file for this class instance
            fileXContents.Save(XMLFilePath);
            return;
        }

        public string GetItem(string itemName)
        {
            string spFilePath = XMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement itemX = spPlanX.Element(itemName);
            if (itemX == null)
            {
                spPlanX.Add(new XElement(itemName, null));
                return null;
            }
            else
            { return (itemX.Value); }
        }

        public void SetItem(string itemName, string item)
        {
            string spFilePath = XMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sscfgXel = spPlanX.Element(itemName);
            if (sscfgXel == null)
            { spPlanX.Add(new XElement(itemName, item)); }
            else
            { sscfgXel.ReplaceWith(new XElement(itemName, item)); }
            spPlanX.Save(spFilePath);
            return;
        }

        public bool InitialItem(string ItemName, bool Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned

            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            { return (Convert.ToBoolean(GetItem(ItemName))); }
        }

        public double InitialItem(string ItemName, double Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned

            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            { return (Convert.ToDouble(GetItem(ItemName))); }
        }

        public int InitialItem(string ItemName, int Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned
            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            { return (Convert.ToInt32(GetItem(ItemName))); }
        }

        public string InitialItem(string ItemName, string Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned

            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, " ");
                return Item;
            }
            else
            { return (Convert.ToString(GetItem(ItemName))); }
        }

        public DateTime InitialItem(string ItemName, DateTime Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned
            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            {
                return (DateTime.Parse(itemStr));
            }
        }

        public void ReplaceItem(string ItemName, bool Item)
        {
            //The item is placed in the xfile -- boolean
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, double Item)
        {
            //The item is placed in the xfile -- double
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, int Item)
        {
            //The item is placed in the xfile -- integer
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, DateTime Item)
        {
            //The item is placed in the xfile -- string
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, string Item)
        {
            //The item is placed in the xfile -- string
            SetItem(ItemName, Item);
            return;
        }

        public bool CheckItem(string ItemName)
        {
            //Checks to see if any element named ItemName exists in the configuration file
            //The item is placed in the xfile -- string
            string Item = GetItem(ItemName);
            if (Item == null)
            { return false; }
            else
            { return true; }
        }













    }
}
