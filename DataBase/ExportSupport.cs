using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AdminTool.DataBase
{
    public class ExportSupport
    {
        public static DataTable GetExport(DataTable dt)
        {
            DataTable dt1 = new DataTable();
            try
            {
                String name = "Report_File";
              
            }
            catch (Exception ex)
            {
                ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return dt1;
        }

        internal static DataTable GetExport()
        {
            throw new NotImplementedException();
        }
    }
}