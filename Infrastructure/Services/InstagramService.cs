using Application.Common.Interfaces;
using Domain.Core;
using InstagramApiSharp;

namespace Infrastructure.Services;

public class InstagramService : IInstagramService<Dictionary<long, List<Participant>>, Post, Participant>
{
    private readonly IInstagramConnectionFactory _instConnectionFactory;

    public InstagramService(IInstagramConnectionFactory instConnectionFactory)
        => _instConnectionFactory = instConnectionFactory;
    public async Task<Dictionary<long, List<Participant>>> CheckPostsAsync(List<Post> posts, List<Participant> participants)
    {
        var _instaApi = await _instConnectionFactory.GetOpenConnection();

        var checkResults = new Dictionary<long, List<Participant>>();

        foreach (var post in posts)
        {
            var mediaId = await _instaApi.MediaProcessor.GetMediaIdFromUrlAsync(new Uri(post.Url));
            var mediaShortCode = _instaApi.MediaProcessor.GetMediaShortCodeFromUrlAsync(new Uri(post.Url));

            var resp = await _instaApi.MediaProcessor.GetMediaByIdAsync(mediaId.Value);

            var comments = await _instaApi.CommentProcessor.GetMediaCommentsAsync(mediaId.Value, PaginationParameters.MaxPagesToLoad(50));

            await Task.Delay(TimeSpan.FromMinutes(1));

            var likes = await _instaApi.MediaProcessor.GetMediaLikersAsync(mediaShortCode.Value, PaginationParameters.MaxPagesToLoad(10));

            var trueParticipants = participants
                .Where(p => likes.Value.Select(l => l.UserName).Contains(p.UserName))
                .Intersect(participants.Where(p => comments.Value.Comments.Select(c => c.User.UserName).Contains(p.UserName)))
                .ToList();

            checkResults.Add(post.Id, trueParticipants);

            await Task.Delay(TimeSpan.FromMinutes(3));
        }

        return checkResults;
    }
}