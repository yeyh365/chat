//using OpenAI.GPT3.Managers;
//using OpenAI.GPT3.ObjectModels.RequestModels;
//using OpenAI.GPT3.ObjectModels;
//using OpenAI.GPT3;

//const string OPENAPI_TOKEN = "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";//输入自己的api-key

//    OpenAIService service = new OpenAIService(new OpenAiOptions() { ApiKey = OPENAPI_TOKEN });
//    CompletionCreateRequest createRequest = new CompletionCreateRequest()
//    {

//        Prompt = "用C#写一个冒泡排序",
//        Temperature = 0.3f,
//        MaxTokens = 1000
//    };

//    var res = await service.Completions.CreateCompletion(createRequest, Models.TextDavinciV3);

//    if (res.Successful)
//    {
//        var ss = res.Choices.FirstOrDefault().Text;
//        Console.WriteLine(ss);
//    }
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text.Json;
//using System.Threading.Tasks;

//var query = "hello";
//    var client = new HttpClient();

//    // Set the API endpoint URL
//    client.BaseAddress = new Uri("https://api.openai.com/v1/");

//    // Set the API request headers
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
//    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//    // Set the API request content
//    var content = new StringContent(JsonSerializer.Serialize(new { prompt = query }), System.Text.Encoding.UTF8, "application/json");

//    // Send the API request
//    var response = await client.PostAsync("gpt-3.5-turbo", content);

//    // Read the API response content
//    var responseContent = await response.Content.ReadAsStringAsync();

//    // Return the API response


//Console.WriteLine(responseContent);

//using System.Net.Http;
//using System.Text;
//using System.Text.Json;

//var httpClient = new HttpClient();
//var requestContent = new StringContent(
//    JsonSerializer.Serialize(new { model = "John", Age = 30 }),
//    Encoding.UTF8,
//    "application/json");

//var response = await httpClient.PostAsync("https://api.openai-proxy.com/v1/chat/completions", requestContent);
//var responseContent = await response.Content.ReadAsStringAsync();

//using Newtonsoft.Json;
//using RestSharp;

//var client = new RestClient("https://api.openai-proxy.com/v1/chat/completions");
//var request = new RestRequest("", Method.Post);
//request.AddHeader("Content-Type", "application/json");
//request.AddHeader("Authorization", "Bearer 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");
//var messages = new[] { new { role = "user", content = "诗人杜甫一共写了多少首诗词" } };
//var body = JsonConvert.SerializeObject(new { model = "gpt-3.5-turbo", messages = messages });
//request.AddJsonBody(body);

//var response = client.Execute(request);
//Console.WriteLine(response.Content);

using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text;

var json = new
{
    model = "gpt-3.5-turbo",
    max_tokens = 1000,
    temperature = 0,
    stream = true,
    messages = new object[]
            {
                new
                {
                    role ="user",
                    content = "c#的数据结构"
                }
            }
};


var response = await HttpRequestRaw("https://open666.cn/api/v1/Chats/SendMessage", json);

var line = "";
var reader = new StreamReader(await response.Content.ReadAsStreamAsync());
while ((line = reader.ReadLine()) != null)
{
    Console.WriteLine(line);
}

static async Task<HttpResponseMessage> HttpRequestRaw(string url, object postData = null)
{
    HttpRequestMessage req = new(HttpMethod.Post, url);

    if (postData != null)
    {
        if (postData is HttpContent data)
        {
            req.Content = data;
        }
        else
        {
            string jsonContent = JsonSerializer.Serialize(postData, new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            req.Content = stringContent;
        }
    }

    var http = new HttpClient();

    req.Headers.Remove("Authorization");
    req.Headers.Add("Authorization", "Bearer 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");
    var response = await http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);

    if (response.IsSuccessStatusCode)
    {
        return response;
    }

    throw new HttpRequestException();
}

