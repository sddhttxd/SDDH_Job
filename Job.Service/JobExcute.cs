using Job.Data;
using Job.Entity;
using Job.Utility;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Service
{
    public class JobExcute : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            JobInfo jobInfo = (JobInfo)context.JobDetail.JobDataMap.Get("JobInfo");
            return DemoExecute(jobInfo);
            //return DoExecute(jobInfo);
        }

        /// <summary>
        /// HttpExecute
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        private static Task DoExecute(JobInfo jobInfo)
        {
            if (jobInfo != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    switch (jobInfo.RequestType)
                    {
                        case "Get":
                            HttpHelper.HttpGet(jobInfo.RequestUrl, jobInfo.OutTime);
                            break;
                        case "Post":
                            HttpHelper.HttpPost(jobInfo.RequestUrl, "", jobInfo.OutTime);
                            break;
                    }
                    //Execute log
                });
            }
            else
            {
                //Execute log
                return null;
            }
        }

        /// <summary>
        /// DemoJob
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        private static Task DemoExecute(JobInfo jobInfo)
        {
            string batchno = Guid.NewGuid().ToString("N");
            if (jobInfo != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    LogHelper.WriteLog("Job Name：" + jobInfo.Name);
                    LogAction.WriteLog(batchno, "JobExcute", "Execute", jobInfo.Name);
                });
            }
            else
            {
                return Task.Factory.StartNew(() =>
                {
                    LogHelper.WriteLog("Job Name：null");
                    LogAction.WriteLog(batchno, "JobExcute", "Execute", "jobInfo is null");
                });
            }
        }

    }
}
