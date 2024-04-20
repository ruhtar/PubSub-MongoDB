using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Publisher;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfireServer();
Console.WriteLine("Hangfire Server started. Press any key to exit...");

builder.Services.AddHangfire();
builder.Services.AddSingleton<IJob, Job>();


var app = builder.Build();
var _job = app.Services.GetRequiredService<IJob>();

_job.Run();

using (var server = new BackgroundJobServer())
{
    Console.ReadKey();
}