using Application;
using Infrastructure;
using Application.Participants.Queries;
using Application.Posts.Commands;
using Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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
}


app.MapGet("/getresult", async ([FromServices] IMediator mediator) =>
{
    var posts = await mediator.Send(new GetActivePostsQuery());
    var participants = await mediator.Send(new GetParticipantsBySocialNetworkQuery());

    var checkResults = await mediator.Send(new CheckPostsCommand() { ActivePosts = posts, Participants = participants});

    return checkResults;
});

app.Run();
