using Chat.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    /// <summary>
    /// ITokenService
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// GetToken
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        string GetToken(UserDto User);
        string GetToken();
        /// <summary>
        /// 校验token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        public bool VerifyToken(string token, out ClaimsPrincipal principal);

    }
}
