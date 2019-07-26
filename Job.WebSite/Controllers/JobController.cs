﻿using Job.Bussiness;
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
        /// 查询任务列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetList(Condition data)
        {
            //List<JobInfo> list = new List<JobInfo>();
            var list = await JobLogic.GetList(data);
            return Json(new { rows = list, total = list.Count });
        }
    }
}