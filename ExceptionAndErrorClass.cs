using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace AdminTool
{
    public class ExceptionAndErrorClass
    {
        public static string StoretheErrorLog(string MethodName, string ErrorText)
        {
            string status = string.Empty;
            string FilePath = ConfigurationManager.AppSettings["googleWebServiceErrortxt"] + "GLBWCFWebServiceError.txt";
            try
            {
                //if (!Directory.Exists(FilePath))
                //{
                //    Directory.CreateDirectory(FilePath);
                //}
                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath);
                }
                File.AppendAllText(FilePath, "***************** \n " + DateTime.Now + ", \n  Method Name :- " + MethodName + ", \n  Error :- " + ErrorText + " \n ***************** \n");
            }
            catch (Exception ex)
            {


            }
            return status;
        }
    }
}