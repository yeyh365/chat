using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Dto
{
    /// <summary>
    /// 用户查询条件
    /// </summary>
    public class UserSearch : SearchCommon
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; } = "";
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = "";
    }
}
