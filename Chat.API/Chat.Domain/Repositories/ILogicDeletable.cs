using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Repositories
{
    public interface ILogicDeletable
    {
        bool Deleted { get; set; }
    }
}
