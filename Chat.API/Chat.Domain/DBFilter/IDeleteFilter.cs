using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DBFilter
{
    public interface IDeleteFilter
    {
        bool Deleted { get; set; }
    }
}
