using System.Collections.Generic;

namespace SSO.Extensions
{
    public static class StringExtensions
    {
        public static string Replace(this string s, IList<string> p, IList<string> r)
        {
            string s1 = s;
            for(var i = 0; i < p.Count; i++)
            {
                s1 = s1.Replace(p[i], r[i]);
            }
            return s1;
        }
        public static string Mobile(this string m)
        {
            if (m.Length > 9)
            {
                if (m.Left(1) == "0")
                    m = "+84" + m.Right(m.Length - 1);
                else
                if (m.Left(2) == "84")
                    m = "+" + m;
                else if (m.Left(3) != "+84")
                    m = "+84" + m;

                if (m.Length == 12) return m;
            }
            return "";
        }

        public static string Left(this string s, int n)
        {
            if (s.Length > n)
            {
                s = s.Substring(0, n);
            }
            return s;
        }
        //public static string Right(this string s, int n, bool IsN)
        //{
        //    if (IsN)
        //    {
        //        s = s.Right(n);
        //    }
        //    else
        //    {
        //        s = s.Substring(n);
        //    }
        //    return s;
        //}
        public static string Right(this string s, int n)
        {
            if (s.Length > n)
            {
                s = s.Substring(s.Length - n, n);
            }
            return s;
        }
    }
}
