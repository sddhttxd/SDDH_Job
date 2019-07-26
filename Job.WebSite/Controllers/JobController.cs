using Job.Bussiness;
using Job.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Job.Data;
using System.Threading.Tasks;

namespace Job.WebSite.Controllers
{
    public class JobController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetList(Condition data)
        {
            var list = await JobLogic.GetList(data);
            return Json(new { rows = list, total = list.Count });
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task<bool> AddJob(JobInfo job)
        {
            return await JobLogic.AddJob(job);
        }

        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task<bool> EditJob(JobInfo job)
        {
            return await JobLogic.EditJob(job);
        }

        public async Task<bool> SwitchJob()
        {
            //return await JobLogic.SwitchJob();
            return true;
        }

    }
}