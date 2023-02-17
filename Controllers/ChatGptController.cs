using ChatGptApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatGptApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatGptController : ControllerBase
{
    private readonly ILogger<ChatGptController> _logger;
    private readonly IChatGptService _chatGptService;

    public ChatGptController(ILogger<ChatGptController> logger, IChatGptService chatGptService)
    {
        _logger = logger;
        _chatGptService = chatGptService;
    }

    [HttpPost(Name = "PostChat")]
    public Task<string?> Post(string question)
    {
        return _chatGptService.PostQuestion(question);
    }
}