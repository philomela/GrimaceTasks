using Application;
using Infrastructure;
using Application.Participants.Queries;
using Application.Posts.Commands;
using Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Interfaces;
using Hangfire;
using Application.Common.Mappings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAutoMapper(cfg
    =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grimace API V1");
        c.RoutePrefix = string.Empty;
    }
    );
    app.UseCors("CorsPolicy");
    app.UseHangfireDashboard("/dashboard");
}


app.MapGet("/getresult", async ([FromServices] IMediator mediator) =>
{
    var posts = await mediator.Send(new GetActivePostsQuery());
    var participants = await mediator.Send(new GetParticipantsBySocialNetworkQuery());

    var checkResults = await mediator.Send(new CheckPostsCommand() { Posts = posts, Participants = participants});

    return checkResults;
});

RecurringJob.AddOrUpdate<IScheduledTasks>("CheckTasks", x => x.ExecuteTaskOnSchedule(), "0 * * * *");

app.Run();
