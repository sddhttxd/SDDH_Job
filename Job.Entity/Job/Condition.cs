using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Entity
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 任务状态(0:无效,1:有效)
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间开始
        /// </summary>
        public DateTime? CreateTimeStart { get; set; }
        /// <summary>
        /// 创建时间截止
        /// </summary>
        public DateTime? CreateTimeEnd { get; set; }
    }
}
