using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace ChatGptApi.Services;

public class ChatGptService : IChatGptService
{
    private ILogger<ChatGptService> _logger;
    private OpenAIService gpt3;

    public ChatGptService(ILogger<ChatGptService> logger)
    {
        _logger = logger;
        gpt3 = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = ""
        });       
    }

    public async Task<string?> PostQuestion (string question)
    {
        var completionResult = await gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
        {
            Prompt = question,
            Model = Models.TextDavinciV2,
            Temperature = 0.5F,
            MaxTokens = 100
        });

        var response = ""; 
        
        if (completionResult.Successful)
        {
            foreach (var choice in completionResult.Choices)
            {
                response += choice.Text + " ";
            }                
        }
        else
        {
            if (completionResult.Error == null)
            {
                throw new Exception("Unknown Error");
            }
            _logger.Log(LogLevel.Critical, "{ErrorCode}: {ErrorMessage}", completionResult.Error.Code, completionResult.Error.Message);
        }
        return response;
    }
}