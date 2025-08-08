using Microsoft.AspNetCore.Mvc;
using ChatTxtWithGPT.Services;

namespace ChatTxtWithGPT.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly OpenAIService _openAIService;

    public ChatController(OpenAIService openAIService)
    {
        _openAIService = openAIService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskQuestion([FromForm] IFormFile file)
    {
        // , [FromForm] string question
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var reader = new StreamReader(file.OpenReadStream());
        var content = await reader.ReadToEndAsync();

        var answer = await _openAIService.AskQuestionAsync(content);
        // , question
        return Ok(new { answer });
    }
}