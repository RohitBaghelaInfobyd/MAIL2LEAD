using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AdminTool.Model
{
    public class HelperClass
    {
        public static string ApplicationErrorMsg = "Some thing went Wrong !! Please try Again.";
        public static string Format(string sData)
        {
            string sTemp = sData;
            sTemp = sTemp.Replace("\r\n", string.Empty);
            sTemp = sTemp.Replace("\t", string.Empty);
            sTemp = sTemp.Replace("<p>", string.Empty);
            sTemp = sTemp.Replace("</p>", string.Empty);
            sTemp = sTemp.Replace("<b>", string.Empty);
            sTemp = sTemp.Replace("</b>", string.Empty);
            sTemp = sTemp.Replace("<br>", string.Empty);
            sTemp = sTemp.Replace("<BR>", string.Empty);
            sTemp = sTemp.Replace(">", string.Empty);
            sTemp = sTemp.Replace("<", string.Empty);
            sTemp = sTemp.Replace("/", string.Empty);
            sTemp = sTemp.Replace("'", string.Empty);
            sTemp = sTemp.Replace("*", string.Empty);
            sTemp = sTemp.Replace("3D", string.Empty);
            sTemp = sTemp.Replace("=", string.Empty);
            sTemp = sTemp.Replace(";", string.Empty);
            return sTemp.Trim();
        }

        public static string FormatPhoneNo(string PhoneNo)
        {
            string sTemp = PhoneNo;
            sTemp = sTemp.Replace("(", string.Empty);
            sTemp = sTemp.Replace(")", string.Empty);
            sTemp = sTemp.Replace(" ", string.Empty);
            sTemp = sTemp.Replace("-", string.Empty);
            return sTemp.Trim();
        }
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}