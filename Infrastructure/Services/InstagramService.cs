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
    private readonly IInstagramConnectionFactory _instConnectionFactory;

    public InstagramService(IInstagramConnectionFactory instConnectionFactory)
        => _instConnectionFactory = instConnectionFactory;
    async public Task<Dictionary<string, List<string>>> CheckPostAsync(Post post, List<Participant> participants)
    {

        var _instaApi = await _instConnectionFactory.GetOpenConnection();

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
