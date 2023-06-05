using AutoMapper;
using Chat.Application.Dto;
using Chat.Core.Enums;
using Chat.Core.Model;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Impl
{
    /// <summary>
    /// DictionaryService
    /// </summary>
    public class DictionaryService : ApplicationService, IDictionaryService
    {
        #region init
        private readonly IRepository<Dictionary> _DictionaryRepository;
        /// <summary>
        /// DictionaryService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="repositoryProvider"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContext"></param>
        public DictionaryService(
            IDBContext context,
            IRepositoryProvider repositoryProvider, IMapper mapper, IHttpContextAccessor httpContext) : base(context, repositoryProvider, mapper, httpContext)
        {
            this._DictionaryRepository = this._repositoryProvider.Create<Dictionary>(this._context);
        }
        #endregion
        #region Public
        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //public async Task<PagableData<DictionaryDto>> GetDictionaryList(DictionarySearch Search, CancellationToken cancellationToken)
        //{
        //    var userId = GetUserInfoByType("UserAD");
        //    var Query = this._DictionaryRepository.GetAll();
        //    if (!string.IsNullOrEmpty(Search.Name))
        //    {
        //        Query = Query.Where(t => t.Name.Contains(Search.Name));
        //    }
        //    var Total = await Query.CountAsync(cancellationToken);
        //    if (Search.Page > 0 && Search.Limit > 0)
        //    {
        //        Query = Query.OrderByDescending(x => x.Id).Skip((Search.Page - 1) * Search.Limit).Take(Search.Limit);
        //    }
        //    List<Dictionary> QueryList = Query.ToList();
        //    var ResultList = _mapper.Map<List<Dictionary>, List<DictionaryDto>>(QueryList);
        //    ResultList.ForEach(x =>
        //    {

        //    });
        //    if (QueryList.Count() <= 0)
        //    {
        //        Total = 0;
        //    }
        //    return new PagableData<DictionaryDto>
        //    {
        //        Data = ResultList,
        //        Total = Total
        //    };
        //}

        /// <summary>
        /// 获取数据字典详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetDictionaryInfo(int Id, CancellationToken cancellationToken)
        {
            var result = new ResultModel();
            Dictionary DataModel = await _DictionaryRepository.Get(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
            if (DataModel == null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "数据字典不存在";
                return result;
            }
            DictionaryDto DataInfo = _mapper.Map<Dictionary, DictionaryDto>(DataModel);
            result.Data = DataInfo;
            return result;
        }

        /// <summary>
        /// 创建数据字典
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> CreateDictionary(DictionaryDto Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            var DataModel = _mapper.Map<Dictionary>(Dto);
            Dictionary CheckModel = this._DictionaryRepository.Get(x => x.Name == DataModel.Name).FirstOrDefault();
            if (CheckModel != null)
            {
                result.Code = ResultCode.NotFound;
                result.Message = "数据字典已存在";
                return result;
            }
            using (var trans = this._context.BeginTrainsaction())
            {
                _DictionaryRepository.Insert(DataModel);
                await this._context.SaveChangesAsync(cancellationToken);
                trans.Commit();
                result.Message = "创建成功！";
                result.Data = DataModel;
            }
            return result;
        }

        /// <summary>
        /// 更新数据字典
        /// </summary>
        /// <param name="Dto">数据字典信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> UpdateDictionary(DictionaryDto Dto, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            try
            {

                var DataModel = _mapper.Map<Dictionary>(Dto);
                Dictionary CheckModel = this._DictionaryRepository.Get(x => x.Name == DataModel.Name && x.Id != DataModel.Id).FirstOrDefault();
                if (CheckModel != null)
                {
                    result.Code = ResultCode.NotFound;
                    result.Message = "数据字典已存在";
                    return result;
                }
                using (var trans = this._context.BeginTrainsaction())
                {
                    _DictionaryRepository.Update(DataModel);
                    await this._context.SaveChangesAsync(cancellationToken);
                    trans.Commit();
                    result.Message = "修改成功！";
                    result.Data = DataModel;
                }
            }
            catch (Exception ee)
            {

                var ss = 1;
            };

            return result;
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> DeleteDictionary(int Id, CancellationToken cancellationToken)
        {
            ResultModel result = new ResultModel();
            _DictionaryRepository.Delete(m => m.Id == Id);
            await _context.SaveChangesAsync(cancellationToken);
            result.Message = "删除成功！";
            return result;
        }
        #endregion
        #region Extend
        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetDictionaryByType(string type)
        {
            var result = new ResultModel();
            List<string> TypeList = type.Split(';').ToList();
            var DataList = _DictionaryRepository.Get(x => TypeList.Contains(x.Name)).ToList();
            var ResultList = _mapper.Map<List<Dictionary>, List<DictionaryDto>>(DataList);
            result.Data = ResultList;
            return result;
        }
        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultModel> GetDictionaryList(string type, CancellationToken cancellationToken)
        {
            var result = new ResultModel();
            //List<string> TypeList = type.Split(';').ToList();
            ////权限控制
            //var DataList = _DictionaryRepository.Get(x => TypeList.Contains(x.Name)).ToList();
            //if (type == "Province;City;County")
            //{
            //    List<int> Ids = GetDictionaryIds(DataList);
            //    DataList = DataList.Where(s => Ids.Contains(s.Id)).ToList();
            //}

            //var ResultList = _mapper.Map<List<Dictionary>, List<DictionaryDto>>(DataList);
            //result.Data = ResultList;
            return result;
        }
        #endregion
    }
}
