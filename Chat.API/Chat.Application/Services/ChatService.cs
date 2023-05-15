using Chat.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    public class ChatService
    {
        public ChatService() { }
        /// <summary>
        /// 已登录的用户信息
        /// </summary>
        public static List<UserModel> OnlineUser { get; set; } = new List<UserModel>();
        public static List<string> UserIdAll { get; set; } = new List<string>();

        /// <summary>
        /// 模拟存放在数据库里的用户信息
        /// </summary>
        private static readonly List<UserModel> _dbuser = new List<UserModel> {
       new UserModel{
         UserName = "test1", Password = "111", GroupName = "Group1"
       },
       new UserModel{
         UserName = "test2", Password = "111", GroupName = "Group2"
       },
       new UserModel{
         UserName = "test3", Password = "111", GroupName = "Group2"
       },
       new UserModel{
         UserName = "test4", Password = "111", GroupName = "Group1"
       },
       new UserModel{
         UserName = "test5", Password = "111", GroupName = "Group3"
       },
     };
    }
}
