using Application.Common.Interfaces;
using Domain.Core;
using InstagramApiSharp;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Net;

namespace Infrastructure.Services;

public class InstagramService : IInstagramService<Dictionary<string, List<string>>, Post, Participant>
{
    private readonly IConfiguration _configuration;

    public InstagramService(IConfiguration configuration)
        => _configuration = configuration;
    async public Task<Dictionary<string, List<string>>> CheckPostAsync(Post post, List<Participant> participants)
    {
        var delay = RequestDelay.FromSeconds(0, 20);

        var userSession = new UserSessionData
        {
            UserName = _configuration.GetSection("Credentials").GetSection("InstagramLogin").Value,
            Password = _configuration.GetSection("Credentials").GetSection("InstagramPassword").Value
        };

        WebProxy wp = new WebProxy()
        {
            Address = new Uri($"http://83.168.113.242:5175"),
            Credentials = new NetworkCredential("user126047", "yixrhf"),

        };
        var httpClientHandler = new HttpClientHandler()
        {
            Proxy = wp,
            UseProxy = true

        };
        var _instaApi = InstaApiBuilder.CreateBuilder()
            .UseHttpClientHandler(httpClientHandler)
               .SetUser(userSession)
               .UseLogger(new DebugLogger(InstagramApiSharp.Logger.LogLevel.Exceptions)) // use logger for requests and debug messages
               .SetRequestDelay(delay)
               .Build();

        if (!_instaApi.IsUserAuthenticated)
        {
            // login
            Console.WriteLine($"Logging in as {userSession.UserName}");
            delay.Disable();
            var logInResult = await _instaApi.LoginAsync();
            delay.Enable();
            if (!logInResult.Succeeded)
            {
                Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
            }
        }



        //Можно удалить сдесь лайки со своей страницы var result = _instaApi.FeedProcessor.GetLikedFeedAsync(PaginationParameters.MaxPagesToLoad(5)).Result.Value[0].PreviewComments;
        var mediaId = await _instaApi.MediaProcessor.GetMediaIdFromUrlAsync(new Uri(post.Url));
        var resp = await _instaApi.MediaProcessor.GetMediaByIdAsync(mediaId.Value);
        var comments = await _instaApi.CommentProcessor.GetMediaCommentsAsync(mediaId.Value, PaginationParameters.MaxPagesToLoad(10));
        var likes = await _instaApi.MediaProcessor.GetMediaLikersAsync(mediaId.Value);


        var trueParticipants = participants
            .Where(p => likes.Value.Select(l => l.UserName).Contains(p.UserName))
            .Intersect(participants.Where(p => comments.Value.Comments.Select(c => c.User.UserName).Contains(p.UserName)))
            .ToList();

        //var reposts = _instaApi.UserProcessor.GetUserMediaAsync(likes[0].UserName, PaginationParameters.MaxPagesToLoad(5));
        //var result = await _instaApi.CommentProcessor.GetMediaCommentsAsync(result.Value[0].PreviewCommentsPaginationParameters.MaxPagesToLoad(5));
        //var result =_instaApi.GetUserAsync("philomelka");
        //var followers = await _instaApi.GetUserFollowersAsync("anderdogen",
        //    PaginationParameters.MaxPagesToLoad(5));
        return new Dictionary<string, List<string>>
        {
            {post.Url, trueParticipants.Select(p => $"{p.UserName} points: {post.Points}").ToList() },
        };
    }
}
