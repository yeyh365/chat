using Chat.Application.Dto;
using Chat.Application.Services;
using Chat.Application.Services.Impl;
using Chat.Core.Model;
using Chat.Domain.Entities;
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
    public class ChatHub : Hub
    {
        #region Init
        private readonly IChatService _ChatService;
        /// <summary>
        /// DictionaryController
        /// </summary>
        /// <param name="DictionaryService"></param>
        public ChatHub(IChatService ChatService)
        {
            this._ChatService = ChatService;
        }
        // GET: api/<ChatController>
        #endregion
        /// <summary>
        /// 已登录的用户信息
        /// </summary>
        public static List<OnLineUser> OnlineUser { get; set; } = new List<OnLineUser>();
        public static List<string> UserIdAll { get; set; } =new List<string>();

        /// <summary>
        /// 模拟存放在数据库里的用户信息
        /// </summary>
        //   private static readonly List<UserModel> _dbuser = new List<UserModel> {
        //  new UserModel{
        //    UserName = "test1", Password = "111", GroupName = "Group1"
        //  },
        //  new UserModel{
        //    UserName = "test2", Password = "111", GroupName = "Group2"
        //  },
        //  new UserModel{
        //    UserName = "test3", Password = "111", GroupName = "Group2"
        //  },
        //  new UserModel{
        //    UserName = "test4", Password = "111", GroupName = "Group1"
        //  },
        //  new UserModel{
        //    UserName = "test5", Password = "111", GroupName = "Group3"
        //  },
        //};
       

        /// <summary>
        /// 登录验证
        /// </summary>
        public async Task Login(string username, string password,string Photo)
        {
           var _dbuser = _ChatService.GetOnlineUserList();
           string connid = Context.ConnectionId;
            ResultModel result = new ResultModel
            {
                Status = 0,
                Message = "登录成功！"
            };
            if (!OnlineUser.Exists(u => u.ID == connid))
            {
                var model = _dbuser.Find(u => u.UserName == username);
                if (model != null)
                {
                    model.ClientID = connid;
                    model.Remark = "All";
                    model.Deleted = false;
                    model.Photo = Photo;
                    var a = _ChatService.UpdateOnlineUserList(model);
                    //OnlineUser.Add(model);
                    //给当前的连接分组
                    await Groups.AddToGroupAsync(connid, model.GroupName);
                    await Groups.AddToGroupAsync(connid, model.Remark);
                }
                else
                {
                    OnlineUser onlineUser = new OnlineUser();
                    onlineUser.ClientID = connid;
                    onlineUser.Remark = "All";
                    onlineUser.Photo = Photo;
                    onlineUser.UserName = username;
                    var a = _ChatService.InsertOnlineUserList(onlineUser);
                    //OnlineUser.Add(model);
                    //给当前的连接分组
                    await Groups.AddToGroupAsync(connid, onlineUser.GroupName);
                    await Groups.AddToGroupAsync(connid, onlineUser.Remark);
                    result.Message = "新昵称登录";
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
            var _dbuser = _ChatService.GetOnlineUserList();
            var model = _dbuser.Find(u => u.ClientID == Context.ConnectionId);
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
            string connid = Context.ConnectionId;
            ResultModel result = new ResultModel();
            var _dbuser = _ChatService.GetOnlineUserList();
            var model = _dbuser.Find(u => u.ClientID == Context.ConnectionId);
            
            if (model == null)
            {
                result.Status = 1;
                result.Message = "请先登录！";
                await Clients.Client(connid).SendAsync("SendMessageResponse", result);
            }
            else
            {
                if (!String.IsNullOrEmpty(message)) {
                    ChatMesItem chatMesItem = new ChatMesItem();
                    chatMesItem.Sender = model.UserName;
                    chatMesItem.Content = message;
                    chatMesItem.Photo = model.Photo;
                    chatMesItem.Deleted = false;
                    chatMesItem.Timestamp = DateTime.Now;
                    var mes = _ChatService.InsertMesList(chatMesItem);
                }
                result.Data = _ChatService.GetChatMesList();
                result.Status = 0;
                result.Message = $"“{user}”发送的消息：{message}";
                await Clients.Group(model.GroupName).SendAsync("SendMessageResponse", result);
            }

        }
        /// <summary>
        /// 自动重连时更新ClintId
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task reconnectUpdateId(string NewclientId, string OldclientId)
        {
            string connid = Context.ConnectionId;
            ResultModel result = new ResultModel();
            var _dbuser = _ChatService.GetOnlineUserList();
            var model = _dbuser.Find(u => u.ClientID == OldclientId);
            model.ClientID= NewclientId;
            if (model == null)
            {
                result.Status = 1;
                result.Message = "请先登录！";
                await Clients.Client(connid).SendAsync("SendMessageResponse", result);
            }
            else
            {
                var a = _ChatService.UpdateOnlineUserList(model);
            }

        }
        public override Task OnConnectedAsync()
        {
            var token = Context.GetHttpContext().Request.Query["access_token"];
            var a = "aaaaaaaa";
            UserIdAll.Add(Context.ConnectionId);
            if (token== a)
            {
                // 连接验证通过，执行逻辑
            }
            else
            {
                // 连接验证失败，可以中止连接
                Context.Abort();
            }
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 当连接断开时的处理
        /// </summary>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connid = Context.ConnectionId;
            //UserIdAll.Remove(Context.ConnectionId);
            var model = _ChatService.GetOnlineUserList().Find(u => u.ClientID == connid);

            if (model != null)
            {
                //_ChatService.DeleteOnlineUserList(model);
                ResultModel result = new ResultModel()
                {
                    Status = 0,
                    OnlineUser = OnlineUser.FindAll(u => u.GroupName == model.GroupName)
                };
                result.Data = model; 
                Clients.Group(model.GroupName).SendAsync("GetUsersResponse", result);
            }
            return base.OnDisconnectedAsync(exception);
        }
        //public override Task OnConnectedAsync()
        //{
        //    // 客户端成功重新连接
        //    // 执行一些操作，例如更新连接状态或通知其他客户端
        //    return base.OnReconnected();
        //}
    }
}

