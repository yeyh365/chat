using AutoMapper;
using Chat.Application.Dto;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StackExchange.Redis.Role;

namespace Chat.Application.Services.Impl
{
    public class UserService : ApplicationService, IUserService
    {
        #region init
        private readonly IRepository<User> _UserService;
        private readonly IRepository<Dictionary> _DictionaryRepository;

        /// <summary>
        /// AdministratorService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public UserService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._UserService = this._repositoryProvider.Create<User>(this._context);
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
        }
        #endregion
        public UserDto Login(UserItem Dto,string IP)
        {
            UserDto result = new UserDto();
            User user = new User();
            var randomName = this._DictionaryRepository.Get(s => s.Name == "RandomName" && s.Value== Dto.Name).FirstOrDefault();
            //var chatMes = _mapper.Map<ChatMes>(chatMesItem);

            if (randomName!= null)
            {
                user.Id = 0;
                user.Deleted = false;
                user.UnionId = IP;
                user.Name = Dto.Name;
                user.Photo = randomName.Remark;
                //var aaaa = _UserService.Get(u => u.Name == Dto.Name).FirstOrDefault();
                var aaaa = _UserService.Insert(user);
                this._context.SaveChanges();
                var DataModel = _mapper.Map<UserDto>(user);
                result = DataModel;
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
