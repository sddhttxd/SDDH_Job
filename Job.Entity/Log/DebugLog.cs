using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Entity
{
    /// <summary>
    /// 调试日志
    /// </summary>
    public class DebugLog
    {
        /// <summary>
        /// ID（主键）
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 批次（GUID）
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
