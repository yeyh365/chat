using Chat.Application.Services;
using Chat.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ControllerBase
    {
        #region init
        private readonly IDictionaryService _DictionaryService;
        /// <summary>
        /// DictionaryController
        /// </summary>
        /// <param name="DictionaryService"></param>
        public DictionaryController(IDictionaryService DictionaryService)
        {
            this._DictionaryService = DictionaryService;
        }
        #endregion
        #region Public
        #endregion
        #region Extend
        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDictionaryByType")]
        [AllowAnonymous]
        public async Task<ResultModel> GetDictionaryByType(string Type )
        {
            return await _DictionaryService.GetDictionaryByType(Type);
        }
        /// <summary>
        /// 获取数据字典详情(Type,支持多类型传输)
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("GetDictionaryList")]
        [Authorize(Roles = "SuperAdmin,ProvinceAdmin,CityAdmin,CountyAdmin,CountyUser")]
        public async Task<ResultModel> GetDictionaryList(string Type, CancellationToken cancellationToken)
        {
            return await _DictionaryService.GetDictionaryList(Type, cancellationToken);
        }
        #endregion
    }
}
