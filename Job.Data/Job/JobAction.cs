using Job.Entity;
using Job.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Job.Data
{
    public class JobAction
    {
        /// <summary>
        /// SDDH数据库链接
        /// </summary>
        private readonly static string connectionString = ConfigHelper.ConnectionString;

        /// <summary>
        /// 获取全部有效任务
        /// </summary>
        /// <returns></returns>
        public async static Task<List<JobInfo>> GetAllJobList()
        {
            List<JobInfo> list = new List<JobInfo>();
            string sql = "select * from dbo.JobInfo where status =1";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var templist = await conn.QueryAsync<JobInfo>(sql);
                list = templist.ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取指定任务
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async static Task<List<JobInfo>> GetList(Condition condition)
        {
            List<JobInfo> list = new List<JobInfo>();
            string sql = "select * from dbo.JobInfo where 1=1 ";
            DynamicParameters parameters = new DynamicParameters();
            if (condition != null)
            {
                #region 根据查询条件组合sql
                if (!string.IsNullOrEmpty(condition.Name))
                {
                    sql += "and Name like '%'+@Name+'%'";
                    parameters.Add("Name", condition.Name);
                }
                if (!string.IsNullOrEmpty(condition.Creator))
                {
                    sql += "and Creator like '%'+@Creator+'%'";
                    parameters.Add("Creator", condition.Creator);
                }
                if (condition.Status.HasValue)
                {
                    sql += "and Status like '%'+@Status+'%'";
                    parameters.Add("Status", condition.Status.Value);
                }
                if (condition.CreateTimeStart.HasValue)
                {
                    sql += "and CreateTime > @CreateTimeStart ";
                    parameters.Add("CreateTimeStart", condition.CreateTimeStart);
                }
                if (condition.CreateTimeEnd.HasValue)
                {
                    sql += "and CreateTime > @CreateTimeEnd ";
                    parameters.Add("CreateTimeEnd", condition.CreateTimeEnd);
                }
                #endregion
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var templist = await conn.QueryAsync<JobInfo>(sql, parameters);
                list = templist.ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据Id查询任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<JobInfo> GetJobById(int id)
        {
            JobInfo jobInfo = null;
            string sql = "select * from dbo.JobInfo where Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                jobInfo = await conn.QueryFirstOrDefaultAsync<JobInfo>(sql);
            }
            return jobInfo;
        }

        /// <summary>
        /// 添加新任务
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async static Task<int> AddJob(JobInfo job)
        {
            string sql = "insert into dbo.JobInfo values(@Name, @Group, @RequestUrl, @RequestType, @OutTime, @Trigger, @Description, @Status, @Creator, GETDATE(), @Modifer, GETDATE())";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Name", job.Name);
            parameters.Add("Group", job.GroupName);
            parameters.Add("RequestUrl", job.RequestUrl);
            parameters.Add("RequestType", job.RequestType);
            parameters.Add("OutTime", job.OutTime);
            parameters.Add("Trigger", job.Trigger);
            parameters.Add("Description", job.Description);
            parameters.Add("Status", job.Status);
            parameters.Add("Creator", job.Creator);
            parameters.Add("Modifer", job.Modifier);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                return await conn.ExecuteAsync(sql, parameters);
            }
        }

        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async static Task<int> EditJob(JobInfo job)
        {
            JobInfo jobInfo = await GetJobById(job.Id);
            string sql = "update dbo.JobInfo set ModifiedTime=GETDATE()";
            DynamicParameters parameters = new DynamicParameters();
            #region 按修改字段组合更新语句 
            if (job.Name != jobInfo.Name)
            {
                sql += ",set Name=@Name";
                parameters.Add("Name", job.Name);
            }
            if (job.GroupName != jobInfo.GroupName)
            {
                sql += ",set GroupName=@GroupName";
                parameters.Add("GroupName", job.GroupName);
            }
            if (job.RequestUrl != jobInfo.RequestUrl)
            {
                sql += ",set RequestUrl=@RequestUrl";
                parameters.Add("RequestUrl", job.RequestUrl);
            }
            if (job.RequestType != jobInfo.RequestType)
            {
                sql += ",set RequestType=@RequestType";
                parameters.Add("RequestType", job.RequestType);
            }
            if (job.OutTime != jobInfo.OutTime)
            {
                sql += ",set OutTime=@OutTime";
                parameters.Add("OutTime", job.OutTime);
            }
            if (job.Trigger != jobInfo.Trigger)
            {
                sql += ",set Trigger=@Trigger";
                parameters.Add("Trigger", job.Trigger);
            }
            if (job.Description != jobInfo.Description)
            {
                sql += ",set Description=@Description";
                parameters.Add("Description", job.Description);
            }
            if (job.Status != jobInfo.Status)
            {
                sql += ",set Status=@Status";
                parameters.Add("Status", job.Status);
            }
            if (job.Modifier != jobInfo.Modifier)
            {
                sql += ",set Modifier=@Modifier";
                parameters.Add("Modifier", job.Modifier);
            }
            sql += " where Id=@Id";
            parameters.Add("Id", job.Id);
            #endregion
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                return await conn.ExecuteAsync(sql, parameters);
            }
        }

        /// <summary>
        /// 批量开关任务
        /// </summary>
        /// <param name="jobIds">任务编号</param>
        /// <param name="status">状态（0：关闭，1：开启）</param>
        /// <param name="userName">操作人</param>
        /// <returns></returns>
        public async static Task<int> SwitchJob(List<int> jobIds, int status, string userName)
        {
            string ids = string.Join(",", jobIds);
            if (!string.IsNullOrEmpty(ids))
            {
                string sql = "exec('update dbo.JobInfo set [Status]=@Status, Modifer=@Modifer, ModifiedTime=GETDATE() where Id in ('+@Ids+')')";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Status", status);
                parameters.Add("Modifer", userName);
                parameters.Add("Ids", ids);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    return await conn.ExecuteAsync(sql, parameters);
                }
            }
            else
            {
                return 0;
            }
        }

    }
}
