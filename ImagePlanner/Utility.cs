using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
