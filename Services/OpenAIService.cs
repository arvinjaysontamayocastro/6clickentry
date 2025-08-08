using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;


namespace ChatTxtWithGPT.Services;

public class OpenAIService
{
    private string model;
    private string apiKey;

    public OpenAIService(IConfiguration configuration)
    {
        apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("OpenAI:ApiKey");
        model = configuration["OpenAI:Model"] ?? throw new ArgumentNullException("OpenAI:Model");
    }

    public async Task<string> AskQuestionAsync(string fileContent)
    {
        IChatClient chatClient =
            new OpenAIClient(apiKey).GetChatClient(model).AsIChatClient();

        List<ChatMessage> chatHistory =
              [
                  new ChatMessage(ChatRole.System, """
            You are a super computer
            list top 50 most occuring words comma delimited
        """)
              ];
        string question = "list top 50 most occuring words comma delimited";

        chatHistory.Add(new ChatMessage(ChatRole.User, $"File content:\n{fileContent}\n\nQuestion: {question}"));

        string response = "";
        await foreach (ChatResponseUpdate item in
                chatClient.GetStreamingResponseAsync(chatHistory))
        {
            Console.Write(item.Text);
            response += item.Text;
        }
        chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));

        return response ?? string.Empty;
    }
}

