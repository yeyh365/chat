using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Dto
{
    /// <summary>
    /// 查询通用条件
    /// </summary>
    public class SearchCommon
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int Limit { get; set; }
    }
}
