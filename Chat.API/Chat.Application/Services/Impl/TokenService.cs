using AutoMapper;
using Chat.Application.Dto;
using Chat.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Impl
{
    public class TokenService : ApplicationService, ITokenService
    {
        private readonly JwtSetting _jwtSetting;
        /// <summary>
        /// TokenService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        /// <param name="options"></param>
        public TokenService(IDBContext context, IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext, IOptions<JwtSetting> options) : base(context, repositoryProvider, mapper, httpContext)
        {
            _jwtSetting = options.Value;
        }
        /// <summary>
        /// 生成token
        /// </summary>
        /// <returns></returns>
        public string GetToken(UserDto User)
        {
            var claims = new Claim[]
           {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("hrcode", User.OpenId, ClaimTypes.Name),
                new Claim("Name", User.Name, ClaimValueTypes.String),
                new Claim("Account", User.Phone, ClaimValueTypes.String),
                new Claim("OpenId", User.OpenId, ClaimValueTypes.String),
                new Claim("Post", "User", ClaimValueTypes.String),
                new Claim("Province", "", ClaimValueTypes.String),
                new Claim("City", "", ClaimValueTypes.String),
                new Claim("County", "", ClaimValueTypes.String),
                new Claim("authority", User.Phone,ClaimValueTypes.String),
                new Claim(ClaimTypes.Role, "User",ClaimValueTypes.String)
           };

            //创建令牌
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                signingCredentials: _jwtSetting.Credentials,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds)
            );

            //生成token
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;

        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            //省市区权限

           // var Provinces = Admin.DicList.Where(s => s.Name == "Province").Select(s => s.Value).ToList() ?? new List<string>();
           // var Citys = Admin.DicList.Where(s => s.Name == "City").Select(s => s.Value).ToList() ?? new List<string>();
           // var Countys = Admin.DicList.Where(s => s.Name == "County").Select(s => s.Value).ToList() ?? new List<string>();

           // var claims = new Claim[]
           //{
           //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
           //     new Claim("hrcode", Admin.Account, ClaimTypes.Name),
           //     new Claim("Name", Admin.Name, ClaimValueTypes.String),
           //     new Claim("Account", Admin.Account, ClaimValueTypes.String),
           //     new Claim("OpenId", Admin.Account, ClaimValueTypes.String),
           //     new Claim("Post", Admin.Post, ClaimValueTypes.String),
           //     new Claim("authority", Admin.Account,ClaimValueTypes.String),
           //     new Claim("Provinces", string.Join(";",Provinces),ClaimValueTypes.String),
           //     new Claim("Citys", string.Join(";",Citys),ClaimValueTypes.String),
           //     new Claim("Countys", string.Join(";",Countys),ClaimValueTypes.String),
           //     new Claim("ChildRole", Admin.ChildRole,ClaimValueTypes.String),

           //     new Claim(ClaimTypes.Role, Admin.Post,ClaimValueTypes.String)
           //};

            //创建令牌
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                signingCredentials: _jwtSetting.Credentials,
                //claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds)
            );

            //生成token
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;

        }
        /// <summary>
        /// 校验token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        public bool VerifyToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;
            //对称秘钥
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSetting.SecurityKey));
            //校验token
            var validateParameter = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSetting.Issuer,
                ValidAudience = _jwtSetting.Audience,
                IssuerSigningKey = securityKey,
                ClockSkew = TimeSpan.Zero//校验过期时间必须加此属性
            };
            //不校验，直接解析token
            //jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token1);
            bool success = false;
            try
            {
                //校验并解析token,validatedToken是解密后的对象
                principal = new JwtSecurityTokenHandler().ValidateToken(token, validateParameter, out SecurityToken validatedToken);
                //获取payload中的数据 
                var jwtPayload = ((JwtSecurityToken)validatedToken).Payload.SerializeToJson();
                success = true;

            }
            catch (SecurityTokenExpiredException ex)
            {
                //表示过期
                success = false;
            }
            catch (SecurityTokenException ex)
            {
                //表示token错误
                success = false;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
    }
}
