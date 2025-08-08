namespace ChatTxtWithGPT.Models
{
    public class ChatRequest
    {
        public string Question { get; set; }
        public IFormFile File { get; set; }
    }
}