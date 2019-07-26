using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Entity
{
    /// <summary>
    /// 任务信息
    /// </summary>
    public class JobInfo
    {
        /// <summary>
        /// ID（主键）
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public RequestType RequestType { get; set; }
        /// <summary>
        /// 超时时长，单位毫秒
        /// </summary>
        public int OutTime { get; set; }
        /// <summary>
        /// 触发条件
        /// </summary>
        public string Trigger { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 任务状态(0:无效,1:有效)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }
    }

    /// <summary>
    /// 请求类型
    /// </summary>
    public enum RequestType
    {
        Get,
        Post,
    }
}
