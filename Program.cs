using System.Text.Json.Serialization;
using demo_api_old;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
});

var host = builder.Build();
host.Run();
//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
//{
//    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
//}));
//builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(x =>
//                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//builder.Services.AddEndpointsApiExplorer();
//var app = builder.Build();

//app.UseDefaultFiles();
//app.UseStaticFiles();


//app.MapGet("/", () => "Hello world!");

//app.UseCors("corsapp");
//app.MapControllers();
//app.Run();

