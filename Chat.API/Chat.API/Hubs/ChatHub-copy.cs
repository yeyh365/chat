﻿using Chat.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat.API.Hubs
{
    public class ChatHubCopy : Hub
    {
        /// <summary>
        /// 已登录的用户信息
        /// </summary>
        public static List<OnLineUser> OnlineUser { get; set; } = new List<OnLineUser>();
        public static List<string> UserIdAll { get; set; } =new List<string>();

        /// <summary>
        /// 模拟存放在数据库里的用户信息
        /// </summary>
        private static readonly List<OnLineUser> _dbuser = new List<OnLineUser> {
       new OnLineUser{
         UserName = "test1", Password = "111", GroupName = "Group1"
       },
       new OnLineUser{
         UserName = "test2", Password = "111", GroupName = "Group2"
       },
       new OnLineUser{
         UserName = "test3", Password = "111", GroupName = "Group2"
       },
       new OnLineUser{
         UserName = "test4", Password = "111", GroupName = "Group1"
       },
       new OnLineUser{
         UserName = "test5", Password = "111", GroupName = "Group3"
       },
     };

        /// <summary>
        /// 登录验证
        /// </summary>
        public async Task Login(string username, string password)
        {
            string connid = Context.ConnectionId;
            ResultModel result = new ResultModel
            {
                Status = 0,
                Message = "登录成功！"
            };
            if (!OnlineUser.Exists(u => u.ID == connid))
            {
                var model = _dbuser.Find(u => u.UserName == username && u.Password == password);
                if (model != null)
                {
                    model.ID = connid;
                    OnlineUser.Add(model);
                    //给当前的连接分组
                    await Groups.AddToGroupAsync(connid, model.GroupName);
                }
                else
                {
                    result.Status = 1;
                    result.Message = "账号或密码错误！";
                }
            }
            //给当前连接返回消息
            await Clients.Client(connid).SendAsync("LoginResponse", result);
        }

        /// <summary>
        /// 获取所在组的在线用户
        /// </summary>
        public async Task GetUsers()
        {
            var model = OnlineUser.Find(u => u.ID == Context.ConnectionId);
            ResultModel result = new ResultModel();
            if (model == null)
            {
                result.Status = 1;
                result.Message = "请先登录！";
            }
            else
            {
                result.OnlineUser = OnlineUser.FindAll(u => u.GroupName == model.GroupName);
            }
            //给所在组返回消息
            await Clients.Group(model.GroupName).SendAsync("GetUsersResponse", result);
        }

        public async Task SendMessage(string user, string message)
        {
            ResultModel result = new ResultModel();
            var model = OnlineUser.Find(u => u.ID == Context.ConnectionId);
            if (model == null)
            {
                result.Status = 1;
                result.Message = "请先登录！";
            }
            else
            {
                result.Status = 0;
                result.Message = $"“{user}”发送的消息：{message}";
            }
            await Clients.Group(model.GroupName).SendAsync("SendMessageResponse", result);
        }

        public override Task OnConnectedAsync()
        {
            UserIdAll.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 当连接断开时的处理
        /// </summary>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connid = Context.ConnectionId;
            UserIdAll.Remove(Context.ConnectionId);
            var model = OnlineUser.Find(u => u.ID == connid);
            int count = OnlineUser.RemoveAll(u => u.ID == connid);
            if (model != null)
            {
                ResultModel result = new ResultModel()
                {
                    Status = 0,
                    OnlineUser = OnlineUser.FindAll(u => u.GroupName == model.GroupName)
                };
                Clients.Group(model.GroupName).SendAsync("GetUsersResponse", result);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}

