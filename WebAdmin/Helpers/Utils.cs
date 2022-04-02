using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;

namespace WebAdmin.Helpers
{
    public static class Util
    {
        public const string NumberSepr = ",";

        private static string insertSepr(string d0)
        {
            var d = "" + d0; // convert to string
            var i = 0;
            var d2 = "";
            var ic = 0;
            var ofs = d.Length - 1;
            var decimalpoint = d.IndexOf('.');
            if (decimalpoint >= 0) ofs = decimalpoint - 1;
            for (i = ofs; i >= 0; i--)
            {
                if (d[i].ToString() != NumberSepr)
                {
                    if (ic++ % 3 == 0 && i != ofs && d[i] != '-') d2 = NumberSepr + d2;
                    d2 = d[i] + d2;
                }
            }

            if (decimalpoint >= 0)
            {
                for (i = decimalpoint; i < d.Length; i++)
                    d2 += d[i];
            }
            return d2;
        }
        public static string RemNumSepr(string num)
        {
            return Regex.Replace(num.Replace(NumberSepr, ""), "[^0-9]", "");
        }
        public static string FormatNumber(this string num, int iRound = 0)
        {
            double a = 0;
            try
            {
                a = double.Parse(num);
                a = Math.Round(a, iRound);
            }
            catch
            {
                a = 0;
            }
            //if (a == 0) return "";
            return insertSepr(a.ToString());
            /*
            if (!IsHRMNumber(num)) num = "0";
            if (num == "0") return "";
            if (IsRound)
                return string.Format("{0:n0}", Double.Parse(num));
            else
                return string.Format("{0:n}", Double.Parse(num));
                */
        }
        public static string UrlHostUpload(HttpRequest a)
        {
            return a.Scheme + @"://" + a.Host + @"/Upload/";
        }
        public static string UrlHost(HttpRequest a)
        {
            return a.Scheme + @"://" + a.Host + @"/";
        }
        public static string GetUrlFile(this string filePath, HttpRequest a, string subFolder = "", string originDirectory = "")
        {
            var i = filePath.IndexOf(@"\Upload\");
            if (i > -1)
            {
                var dirOrigin = filePath.Substring(0, i + @"\Upload\".Length);
                filePath = filePath.Replace(dirOrigin, UrlHostUpload(a)).Replace(@"\", "/");
            }
            else
            {
                if (!String.IsNullOrEmpty(originDirectory) && !String.IsNullOrEmpty(subFolder))
                {
                    i = filePath.IndexOf(originDirectory);
                    if (i > -1)
                    {
                        //originDirectory = originDirectory.UrlEncode();
                        //filePath = filePath.UrlEncode();
                        filePath = filePath.Replace(originDirectory, UrlHost(a) + subFolder).Replace(@"\", "/");
                    }
                }
            }
            return filePath;
        }
    }
}
