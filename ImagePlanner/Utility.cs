using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

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

        public static void KillScriptError()
        {
            //Determine if a script error was thrown in the Star-Sky.com access
            //  if so, then kill that process.
            const string scriptErrorProcessName = "Script Error";
            //Get list of current process names

            Process[] PWIFind = Process.GetProcessesByName("Script Error");
            Thread.Sleep(1000);
            if (PWIFind.Length > 0) 
                PWIFind[0].Kill();
            return;
        }

    }
}
