using Job.Data;
using Job.Entity;
using Job.Utility;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Job.Service
{
    public partial class JobService : ServiceBase
    {
        private readonly static string logBatch = Guid.NewGuid().ToString("N");
        public JobService()
        {
            InitializeComponent();
            // 全局捕获异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                LogHelper.WriteLog("系统异常：" + JsonConvert.SerializeObject(ex));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("系统异常：" + JsonConvert.SerializeObject(ex));
            }
        }
        protected override async void OnStart(string[] args)
        {
            LogHelper.WriteLog("Service Start!");
            await LogAction.WriteLogAsync(logBatch, "JobService", "OnStart", "Service Start");
            await JobManager.DoJob();
        }

        protected override async void OnStop()
        {
            LogHelper.WriteLog("Service Stop!");
            await LogAction.WriteLogAsync(logBatch, "JobService", "OnStart", "Service Stop");
        }
    }
}
