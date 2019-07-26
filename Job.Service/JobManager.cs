using Job.Bussiness;
using Job.Entity;
using Job.Utility;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Service
{
    public static class JobManager
    {
        public async static Task DoJob()
        {
            List<JobInfo> jobList = await JobLogic.GetAllJobList();
            LogHelper.WriteLog("jobList:" + JsonConvert.SerializeObject(jobList));
            if (jobList != null && jobList.Count > 0)
            {
                //1.通过工厂获取一个调度器的实例
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();

                foreach (JobInfo jobInfo in jobList)
                {
                    //2.创建任务对象
                    JobDataMap dataMap = new JobDataMap();
                    dataMap.Add("JobInfo", jobInfo);
                    IJobDetail job = JobBuilder.Create<JobExcute>()
                        .SetJobData(dataMap)
                        .WithIdentity(jobInfo.Name, jobInfo.GroupName)
                        .WithDescription(jobInfo.Description)
                        .Build();

                    //3.创建触发器
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity(jobInfo.Name, jobInfo.GroupName)
                        .WithDescription(jobInfo.Description)
                        .StartNow()
                        .WithCronSchedule(jobInfo.Trigger)
                        .Build();

                    await scheduler.ScheduleJob(job, trigger);
                }
            }
        }

    }
}
