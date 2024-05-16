using System;
using System.Linq;

namespace ImagePlanner
{
    internal static class Utility
    {
        public static string CullChars(string strIn, char[] clearChars)
        {
            //trim diacritics if necessary
            string str = strIn.Trim(clearChars);
            //clear the rest
            foreach (char c in clearChars)
            {
                string cStr = c.ToString();
                str.Replace(cStr, string.Empty);
            }
            return str;
        }

        public static bool HasSpecialCharacters(string str, char[] specialChars)
        {
            foreach (char c in specialChars)
            {
                if (str.Contains(c))
                    return true;
            }
            return false;
        }

        public static double SexyParse(string sData)
        {
            //converts a string that contains either a sexidecimal or decimal number, depending
            //detect number format
            string[] subS = sData.Split(' ');
            if (subS.Length == 3)
            {
                //trim last character of each substring (deg, min, sec)
                string degS = subS[0].Remove(subS[0].Length - 1, 1);
                string minS = subS[1].Remove(subS[1].Length - 1, 1);
                string secS = subS[2].Remove(subS[2].Length - 1, 1);
                //calculate decimal
                return Convert.ToDouble(degS) + Convert.ToDouble(minS) / 60.0 + Convert.ToDouble(secS) / 3600.0;
            }
            else if (sData != null)
                return Convert.ToDouble(sData);
            else
                return 0;
        }

    }
}
