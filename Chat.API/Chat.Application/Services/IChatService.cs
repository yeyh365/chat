using Chat.Application.Dto;
using Chat.Core.Model;
using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    /// <summary>
    /// IDictionaryService
    /// </summary>
    public interface IChatService
    {
        #region Public
        /// <summary>
        /// 获取最新聊天记录
        /// </summary>
        /// <param name=""></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ResultModel GetChatMesList();
        /// <summary>
        /// 获取在线用户
        /// </summary>
        /// <returns></returns>
        ResultModel GetOnlineUserListApi();
        List<OnlineUser> GetOnlineUserList();

        /// <summary>
        /// 保存在线用户
        /// </summary>
        /// <param name="onlineUser"></param>
        /// <returns></returns>
        OnlineUser InsertOnlineUserList(OnlineUser onlineUser);

        /// <summary>
        /// 删除一个在线用户
        /// </summary>
        /// <param name="onlineUser"></param>
        /// <returns></returns>
        void DeleteOnlineUserList(OnlineUser onlineUser);
        /// <summary>
        /// 更新在线用户新
        /// </summary>
        /// <param name="onlineUser"></param>
        /// <returns></returns>
        OnlineUser UpdateOnlineUserList(OnlineUser onlineUser);

        /// <summary>
        /// 保存在线用户
        /// </summary>
        /// <param name="onlineUser"></param>
        /// <returns></returns>
        ChatMes InsertMesList(ChatMesItem chatMesItem);

        #endregion
        #region Extend
        #endregion
    }
}
