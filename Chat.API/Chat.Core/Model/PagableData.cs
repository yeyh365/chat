using Chat.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Model
{
    public class PagableData<T>
    {
        public PagableData()
        {
            Code = ResultCode.Success;
        }

        public IEnumerable<T> Data { get; set; }

        public int Total { get; set; }

        public ResultCode Code { get; set; }



    }
    public class PagableData
    {
        public PagableData()
        {
            Code = ResultCode.Success;
        }

        public dynamic Data { get; set; }

        public int Total { get; set; }

        public ResultCode Code { get; set; }



    }
}
