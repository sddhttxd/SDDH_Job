﻿using Job.Data;
using Job.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Bussiness
{
    public static class JobLogic
    {
        /// <summary>
        /// 获取全部有效任务
        /// </summary>
        /// <returns></returns>
        public async static Task<List<JobInfo>> GetAllJobList()
        {
            return await JobAction.GetAllJobList();
        }

        /// <summary>
        /// 获取指定任务
        /// </summary>
        /// <returns></returns>
        public async static Task<List<JobInfo>> GetList(Condition condition)
        {
            return await JobAction.GetList(condition);
        }

        /// <summary>
        /// 根据Id查询任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<JobInfo> GetJobById(int id)
        {
            return await JobAction.GetJobById(id);
        }

        /// <summary>
        /// 添加新任务
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async static Task<int> AddJob(JobInfo job)
        {
            return await JobAction.AddJob(job);
        }

        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async static Task<int> EditJob(JobInfo job)
        {
            return await JobAction.EditJob(job);
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
            return await JobAction.SwitchJob(jobIds, status, userName);
        }


    }
}