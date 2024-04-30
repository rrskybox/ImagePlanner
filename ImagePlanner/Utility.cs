using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePlanner
{
    internal static class Utility
    {
        public static string CullChars(string str, char[] clearChars)
        {
            //trim diacritics if necessary
            str = str.Trim(clearChars);
            //clear the rest
            foreach (char c in clearChars)
            {
                string cStr = c.ToString();
                str.Replace(cStr, string.Empty);
            }
            return str;
        }
    }
}
