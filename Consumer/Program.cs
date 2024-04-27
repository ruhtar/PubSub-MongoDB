using Consumer.Extensions;
using Microsoft.AspNetCore.Builder;

#region Mass transit

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitConfiguration();
builder.Services.AddRepository();
builder.Services.AddIOptionsImplementation(builder.Configuration);

var app = builder.Build();

app.Run();

#endregion