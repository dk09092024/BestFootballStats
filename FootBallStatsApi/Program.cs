using System.Reflection;
using FootBallStatsApi.ApiExtensions;
using FootBallStatsApi.ApiExtensions.DataSource;
using FootBallStatsApi.ApiExtensions.MediatR;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationDatabase();
builder.Services.AddRepositories();
builder.Services.AddMediator();

builder.Services.AddHangfire();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();