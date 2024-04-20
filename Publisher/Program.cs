using Hangfire;
using Microsoft.AspNetCore.Builder;
using Publisher;

//using (var server = new BackgroundJobServer())
{
    var builder = WebApplication.CreateBuilder(args);
    const string queueName = "async-pubSub";

    builder.Services.AddHangfireServer();
    Console.WriteLine("Hangfire Server started. Press any key to exit...");



    builder.Services.AddHangfire();

    Job.Run();

    Console.ReadKey();
}