using Chat.Application.Dto;
using Chat.Core.Model;
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
    public interface IDictionaryService
    {
        #region Public
        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //Task<PagableData<DictionaryDto>> GetDictionaryList(DictionarySearch Search, CancellationToken cancellationToken);

        /// <summary>
        /// 获取数据字典详情
        /// </summary>
        /// <param name="Id">学员ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetDictionaryInfo(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// 创建数据字典
        /// </summary>
        /// <param name="Dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> CreateDictionary(DictionaryDto Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 更新数据字典
        /// </summary>
        /// <param name="Dto">数据字典信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateDictionary(DictionaryDto Dto, CancellationToken cancellationToken);

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="Id">数据字典id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteDictionary(int Id, CancellationToken cancellationToken);
        #endregion
        #region Extend
        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetDictionaryByType(string Type);

        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultModel> GetDictionaryList(string Type, CancellationToken cancellationToken);
        #endregion
    }
}
