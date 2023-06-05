using Chat.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Chat.Core.Model
{
    public class ResultModel
    {
        public ResultModel()
        {
            Code = ResultCode.Success;
        }
        public ResultModel(ResultCode code)
        {
            Code = code;
        }

        public ResultCode Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
        public int Status { get; set; }
        public List<OnLineUser> OnlineUser { get; set; }

    }
}
