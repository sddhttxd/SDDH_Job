using Job.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;

namespace Job.Data
{
    public class LogAction
    {
        /// <summary>
        /// DB日志
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="tmpClass"></param>
        /// <param name="method"></param>
        /// <param name="content"></param>
        /// <param name="creator"></param>
        public static void WriteLog(string batch, string tmpClass, string method, string content, string creator = "system")
        {
            using (SqlConnection conn = new SqlConnection(ConfigHelper.ConnectionString))
            {
                string sql = "insert into dbo.DebugLog values(@Batch,@TmpClass,@Method,@TmpContent,@Creator,@CreateTime)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Batch", batch);
                parameters.Add("TmpClass", tmpClass);
                parameters.Add("Method", method);
                parameters.Add("TmpContent", content);
                parameters.Add("Creator", creator);
                parameters.Add("CreateTime", DateTime.Now);
                conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// DB日志异步
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="_class"></param>
        /// <param name="method"></param>
        /// <param name="content"></param>
        /// <param name="creator"></param>
        public async static Task WriteLogAsync(string batch, string tmpClass, string method, string content, string creator = "system")
        {
            using (SqlConnection conn = new SqlConnection(ConfigHelper.ConnectionString))
            {
                string sql = "insert into dbo.DebugLog values(@Batch,@Class,@Method,@Content,@Creator,@CreateTime)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Batch", batch);
                parameters.Add("Class", tmpClass);
                parameters.Add("Method", method);
                parameters.Add("Content", content);
                parameters.Add("Creator", creator);
                parameters.Add("CreateTime", DateTime.Now);
                await conn.ExecuteAsync(sql, parameters);
            }
        }
    }
}
