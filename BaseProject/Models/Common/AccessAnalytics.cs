using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace BaseProject.Models.Common
{
    public class AccessAnalytics
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static AnalyticsViewModels ThongKe()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<AnalyticsViewModels>("sp_ThongKe", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}