using Chat.Application.Services;
using Chat.Application.Services.Impl;
using Chat.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ChatController : ControllerBase
    {
        #region Init
        private readonly IChatService _ChatService;
        /// <summary>
        /// DictionaryController
        /// </summary>
        /// <param name="DictionaryService"></param>
        public ChatController(IChatService ChatService)
        {
            this._ChatService = ChatService;
        }
        // GET: api/<ChatController>
        #endregion
        #region Public
        /// <summary>
        /// 获取聊天记录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetChatMesList")]
        public ResultModel GetChatMesList()
        {
            //var input = "hello!你好啊";
            //var client = new RestClient("https://api.openai-proxy.com/v1/chat/completions");
            //var request = new RestRequest("", Method.Post);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", OPENAPI_TOKEN);
            //var messages = new[] { new { role = "user", content = input } };
            //var body = JsonConvert.SerializeObject(new { model = "gpt-3.5-turbo", messages = messages });
            //request.AddJsonBody(body);

            //var response = client.Execute(request);
            //Console.WriteLine(response.Content);
            return _ChatService.GetChatMesList();
        }
        /// <summary>
        /// 获取在线用户
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetOnlineUserList")]
        public ResultModel GetOnlineUserList()
        {
            //var input = "hello!你好啊";
            //var client = new RestClient("https://api.openai-proxy.com/v1/chat/completions");
            //var request = new RestRequest("", Method.Post);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", OPENAPI_TOKEN);
            //var messages = new[] { new { role = "user", content = input } };
            //var body = JsonConvert.SerializeObject(new { model = "gpt-3.5-turbo", messages = messages });
            //request.AddJsonBody(body);

            //var response = client.Execute(request);
            //Console.WriteLine(response.Content);
            return _ChatService.GetOnlineUserListApi();
        }
        #endregion
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ChatController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ChatController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
