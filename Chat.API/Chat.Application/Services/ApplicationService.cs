using AutoMapper;
using Chat.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    /// <summary>
    /// ApplicationService
    /// </summary>
    public abstract class ApplicationService
    {
        /// <summary>
        /// _context
        /// </summary>
        protected readonly IDBContext _context;
        /// <summary>
        /// _repositoryProvider
        /// </summary>
        protected readonly IRepositoryProvider _repositoryProvider;
        /// <summary>
        /// _mapper
        /// </summary>
        protected readonly IMapper _mapper;
        /// <summary>
        /// _httpContext
        /// </summary>
        protected readonly IHttpContextAccessor _httpContext;
        /// <summary>
        /// _httpContext
        /// </summary>
        protected readonly IConfiguration _Configuration;
        /// <summary>
        /// _httpContext
        /// </summary>
        protected readonly IMemoryCache _Cache;

        public Dictionary<string, string> dicColumn = new Dictionary<string, string>();

        /// <summary>
        /// ApplicationService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public ApplicationService(
            IDBContext context,
            IRepositoryProvider repositoryProvider,
            IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        /// <summary>
        /// ApplicationService(重载_Configuration)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        /// <param name="Configuration"></param>
        public ApplicationService(
            IDBContext context,
            IRepositoryProvider repositoryProvider,
            IMapper mapper, IHttpContextAccessor httpContext, IConfiguration Configuration)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _httpContext = httpContext;
            _Configuration = Configuration;
        }
        public ApplicationService(
    IDBContext context,
    IRepositoryProvider repositoryProvider,
    IMapper mapper, IHttpContextAccessor httpContext, IConfiguration Configuration, IMemoryCache Cache)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _httpContext = httpContext;
            _Configuration = Configuration;
            _Cache = Cache;
        }

        /// <summary>
        /// ApplicationService(重载_Configuration)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        /// <param name="Configuration"></param>
        public ApplicationService(IDBContext context,
            IRepositoryProvider repositoryProvider, IConfiguration Configuration, IMemoryCache Cache, IMapper mapper)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
            _Configuration = Configuration;
            _Cache = Cache;
            _mapper = mapper;
        }
        public ApplicationService(IHttpClientFactory clientFactory, IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Type">Type</param>
        /// <returns></returns>
        public string GetUserInfoByType(string Type)
        {
            try
            {
                var info = _httpContext.HttpContext.User.Claims.Where(x => x.Type == Type).FirstOrDefault();
                if (info != null)
                {
                    return _httpContext.HttpContext.User.Claims.Where(x => x.Type == Type).FirstOrDefault().Value;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ee)
            {
                return "";
            }
        }
    }
}
