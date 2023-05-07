using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3;

const string OPENAPI_TOKEN = "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";//输入自己的api-key

    OpenAIService service = new OpenAIService(new OpenAiOptions() { ApiKey = OPENAPI_TOKEN });
    CompletionCreateRequest createRequest = new CompletionCreateRequest()
    {

        Prompt = "用C#写一个冒泡排序",
        Temperature = 0.3f,
        MaxTokens = 1000
    };

    var res = await service.Completions.CreateCompletion(createRequest, Models.TextDavinciV3);

    if (res.Successful)
    {
        var ss = res.Choices.FirstOrDefault().Text;
        Console.WriteLine(ss);
    }
