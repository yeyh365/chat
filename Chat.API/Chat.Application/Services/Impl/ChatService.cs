using AutoMapper;
using Chat.Application.Dto;
using Chat.Application.Dto.Chat;
using Chat.Core.Enums;
using Chat.Core.Model;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Chat.Application.Services.Impl
{
    public class ChatService: ApplicationService, IChatService
    {
        #region init
        private readonly IRepository<Dictionary> _DictionaryRepository;
        private readonly IRepository<ChatMes> _ChatMesRepository;
        private readonly IRepository<OnlineUser> _OnlineUserRepository;
        /// <summary>
        /// DictionaryService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public ChatService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
            this._ChatMesRepository = this._repositoryProvider.Create<ChatMes>(this._context);
            this._OnlineUserRepository = this._repositoryProvider.Create<OnlineUser>(this._context);

        }
        #endregion
        /// <summary>
        /// 已登录的用户信息
        /// </summary>
        public static List<OnLineUser> OnlineUser { get; set; } = new List<OnLineUser>();
        public static List<string> UserIdAll { get; set; } = new List<string>();

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
        /// 获取最新聊天记录
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ResultModel GetChatMesList()
        {
            var result = new ResultModel();
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderBy(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
            List<ChatMes> DataModel = _ChatMesRepository.GetAll().OrderByDescending(x => x.Id).Skip(0).Take(40).ToList();
            //DataModel = DataModel.OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "没有聊天记录";
                return result;
            }
            List<ChatMesDto> DataInfo = _mapper.Map<List<ChatMes>, List<ChatMesDto>>(DataModel);
            result.Data = DataInfo;
            return result;
        }
        /// <summary>
        /// 获取在线用户api接口
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ResultModel GetOnlineUserListApi()
        {
            var result = new ResultModel();
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderBy(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
            List<OnlineUser> DataModel = _OnlineUserRepository.GetAll().OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
            //DataModel = DataModel.OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "没有聊天记录";
                return result;
            }
            List<OnlineUserDto> DataInfo = _mapper.Map<List<OnlineUser>, List<OnlineUserDto>>(DataModel);
            result.Data = DataInfo;
            return result;
        }
        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public List<OnlineUser> GetOnlineUserList()
        {
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderBy(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
            List<OnlineUser> DataModel = _OnlineUserRepository.GetAll().OrderByDescending(x => x.Id).ToList();
            //DataModel = DataModel.OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
            return DataModel;
        }
        /// <summary>
        /// 获取插入在线用户列表
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public OnlineUser InsertOnlineUserList(OnlineUser onlineUser)
        {
            onlineUser.Id = 0;
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderBy(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
            OnlineUser result = _OnlineUserRepository.Insert(onlineUser);
            this._context.SaveChanges();
            //DataModel = DataModel.OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
            return result;
        }
        /// <summary>
        /// 退出后删除在线用户列表
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public void DeleteOnlineUserList(OnlineUser onlineUser)
        {
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderBy(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
             _OnlineUserRepository.Delete(onlineUser);
            this._context.SaveChanges();
            //DataModel = DataModel.OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
        }
        /// <summary>
        /// 获取插入在线用户列表
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public OnlineUser UpdateOnlineUserList(OnlineUser onlineUser)
        {
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderBy(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
            OnlineUser result = _OnlineUserRepository.Update(onlineUser);
            this._context.SaveChanges();
            //DataModel = DataModel.OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
            return result;
        }
        /// <summary>
        /// 获取插入在线用户列表
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ChatMes InsertMesList(ChatMesItem chatMesItem)
        {
            //if (Search.Page > 0 && Search.Limit > 0)
            //{
            //    Query = Query.OrderBy(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
            //}
            var chatMes = _mapper.Map<ChatMes>(chatMesItem);
            ChatMes result = _ChatMesRepository.Insert(chatMes);
            this._context.SaveChanges();
            //DataModel = DataModel.OrderByDescending(x => x.Id).Skip(0).Take(20).ToList();
            return result;
        }
    }

}
