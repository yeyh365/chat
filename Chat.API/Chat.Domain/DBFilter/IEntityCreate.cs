using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DBFilter
{
    public interface IEntityCreate
    {
        /// <summary>
        /// 创建者的ID
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? Created { get; set; }
    }
}