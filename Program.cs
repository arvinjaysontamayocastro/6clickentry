using ChatTxtWithGPT.Services;

var builder = WebApplication.CreateBuilder(args);
// var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

builder.Services.AddControllers();
builder.Services.AddSingleton<OpenAIService>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();