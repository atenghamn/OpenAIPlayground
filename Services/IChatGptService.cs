namespace ChatGptApi.Services;

public interface IChatGptService
{
    Task<string?> PostQuestion(string question);
}