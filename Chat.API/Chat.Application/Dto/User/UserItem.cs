using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Dto
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserItem
    {
        /// <summary>
        /// OpenId
        /// </summary>
        public string? OpenId { get; set; } = string.Empty;

        /// <summary>
        /// unionid
        /// </summary>
        public string? UnionId { get; set; } = string.Empty;

        /// <summary>
        /// 最后一次设备
        /// </summary>
        public string? DeviceNo { get; set; } = string.Empty;

        /// <summary>
        /// 用户账号
        /// </summary>
        public string? Account { get; set; } = string.Empty;

        /// <summary>
        /// 中文姓名
        /// </summary>
        public string? Name { get; set; } = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        public string? Password { get; set; } = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        public string? Phone { get; set; } = string.Empty;
        /// <summary>
        /// 发送者
        /// </summary>
        public string? Photo { get; set; } = string.Empty;

        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; } = string.Empty;
    }
}
