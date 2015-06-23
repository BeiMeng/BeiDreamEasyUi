using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeiDream.Common
{
    /// <summary>
    /// 数据传输对象
    /// </summary>
    public interface IDto
    {
        /// <summary>
        /// 标识
        /// </summary>
        string Id { get; set; }
    }
}
