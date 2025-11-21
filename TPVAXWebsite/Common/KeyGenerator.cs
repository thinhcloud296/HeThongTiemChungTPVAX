using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPVAXWebsite.Common
{
    public static class KeyGenerator
    {
        private static readonly Random _random = new Random();

        public static string GenMaKH(string CCCD)
        {
            string cccdSuffix = CCCD.Length == 12 ? CCCD.Substring(6, 6) : string.Empty;
            return string.Equals(cccdSuffix, string.Empty) ? string.Empty : "KHHG" + cccdSuffix;
        }
        public static string GenMaLK(string CCCD)
        {
            string cccdSuffix = CCCD.Length == 12 ? CCCD.Substring(6, 6) : string.Empty;
            return string.Equals(cccdSuffix, string.Empty) ? string.Empty : "LKHS" + cccdSuffix;
        }
        public static string GenMaHSTC(string CCCD)
        {
            string cccdSuffix = CCCD.Length == 12 ? CCCD.Substring(6, 6) : string.Empty;
            return string.Equals(cccdSuffix, string.Empty) ? string.Empty : "HSTC" + cccdSuffix;
        }
        public static string GenMaTK()
        {
            return "TK" + _random.Next(10000000, 99999999).ToString();
        }
    }
}