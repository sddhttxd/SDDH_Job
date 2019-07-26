using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Utility
{
    public static class ConfigHelper
    {
        /// <summary>
        /// SDDH数据库链接
        /// </summary>
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["sddh_db"].ToString();

    }
}
