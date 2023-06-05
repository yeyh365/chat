using Chat.Application.Dto;
using Chat.Application.Services;
using Chat.Application.Services.Impl;
using Chat.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _UserService;
        private readonly ITokenService _tokenService;
        private IHttpContextAccessor _httpContext;

        public AuthController(IUserService userService, ITokenService tokenService, IHttpContextAccessor httpContext)
        {

            this._UserService = userService;
            this._tokenService = tokenService;
            _httpContext = httpContext;
        }
        ///// <summary>
        ///// 用户登陆
        ///// </summary>
        ///// <param name="User"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        [HttpGet, Route("Login")]
        public ResultModel Login([FromQuery]UserItem Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            string IP = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();
            if (string.IsNullOrEmpty(Dto.Name))
            {
                return new ResultModel { Code = Core.Enums.ResultCode.FailValidate, Message = "账号密码无效" };
            }
            //string PassWord = MD5Helper.Md5Method(Dto.PassWord);
            //Dto.PassWord = PassWord;
            var user = this._UserService.Login(Dto, IP);
            if (user == null)
            {
                result.Code = Core.Enums.ResultCode.NotFound;
                result.Message = "未获取到用户信息,请联系管理员.";
                return result;
            }
            var Token = _tokenService.GetToken();
            user.Token = Token;
            result.Code = Core.Enums.ResultCode.Success;
            result.Data = user;
            return result;
        }
    }
}
