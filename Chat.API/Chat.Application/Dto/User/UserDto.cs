using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Dto
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto : User
    {
        /// <summary>
        /// JWT Token
        /// </summary>
        public string? Token { get; set; }
    }
}
