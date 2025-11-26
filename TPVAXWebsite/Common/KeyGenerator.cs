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
            if (string.IsNullOrEmpty(CCCD) || CCCD.Length < 12)
                return "KH" + _random.Next(10000000, 99999999).ToString();
            
            string cccdSuffix = CCCD.Substring(CCCD.Length - 6);
            return "KH" + cccdSuffix;
        }
        
        public static string GenMaLK(string CCCD)
        {
            if (string.IsNullOrEmpty(CCCD) || CCCD.Length < 12)
                return "LK" + _random.Next(10000000, 99999999).ToString();
            
            string cccdSuffix = CCCD.Substring(CCCD.Length - 6);
            string timestamp = DateTime.Now.ToString("HHmmss");
            return "LK" + cccdSuffix;
        }
        
        public static string GenMaHSTC(string CCCD)
        {
            if (string.IsNullOrEmpty(CCCD) || CCCD.Length < 12)
                return "HS" + _random.Next(10000000, 99999999).ToString();
            
            string cccdSuffix = CCCD.Substring(CCCD.Length - 6);
            return "HS" + cccdSuffix;
        }
        public static string GenMaTK()
        {
            return "TK" + _random.Next(10000000, 99999999).ToString();
        }
    }
}