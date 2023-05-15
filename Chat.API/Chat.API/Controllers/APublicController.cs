using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3;
using Newtonsoft.Json;
using RestSharp;
using Chat.EntityFrameworkCore.Redis;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APublicController : ControllerBase
    {
        const string OPENAPI_TOKEN = "Bearer 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";//输入自己的api-key
        /// <summary>
        /// Nginx代理后的openai请求
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet, Route("GetNginxOPENAI")]
        public async Task<string> GetNginxOPENAI(string input)
        {
            //var input = "hello!你好啊";
            var client = new RestClient("https://api.openai-proxy.com/v1/chat/completions");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", OPENAPI_TOKEN);
            var messages = new[] { new { role = "user", content = input } };
            var body = JsonConvert.SerializeObject(new { model = "gpt-3.5-turbo", messages = messages });
            request.AddJsonBody(body);

            var response = client.Execute(request);
            //Console.WriteLine(response.Content);

            return response.Content;
        }
        [HttpPost, Route("GetOPENAI")]
        public async Task<string>  GetOPENAI(string input)
        {
            OpenAIService service = new OpenAIService(new OpenAiOptions() { ApiKey = OPENAPI_TOKEN });
            CompletionCreateRequest createRequest = new CompletionCreateRequest()
            {

                Prompt = input,
                Temperature = 0.3f,
                MaxTokens = 1000
            };

            var res = await service.Completions.CreateCompletion(createRequest, Models.TextDavinciV3);

            if (res.Successful)
            {
                string ss = res.Choices.FirstOrDefault().Text;
                return ss;
            }
            else
            {
                return "没有返回数据";
            }
            
        }
        [HttpGet, Route("GetRedis")]
        public async Task<string> GetRedis(string input)
        {
            ///测试redis链接
            string test = "Name";
            string value = "Yeyh";
            string time = "1000";
            RedisHelper redis = new RedisHelper();
            redis.SetStringKey(test, value, int.Parse(time));

            var acd = redis.GetStringValue(test);



            return acd;
        }
    }
}