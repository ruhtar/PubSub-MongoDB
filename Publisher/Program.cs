using Hangfire;
using Microsoft.AspNetCore.Builder;
using Publisher;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire();

builder.Build();

var job = new Job();

job.Start();

using (var server = new BackgroundJobServer())
{
    Console.ReadKey();
}