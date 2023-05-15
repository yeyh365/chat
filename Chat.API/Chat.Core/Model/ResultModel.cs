using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Model
{
    public class ResultModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<UserModel> OnlineUser { get; set; }
    }
}
