using Application.Common.Interfaces;
using Domain.Core;
using InstagramApiSharp;

namespace Infrastructure.Services;

public class InstagramService : IInstagramService<Dictionary<string, List<string>>, Post, Participant>
{
    private readonly IInstagramConnectionFactory _instConnectionFactory;

    public InstagramService(IInstagramConnectionFactory instConnectionFactory)
        => _instConnectionFactory = instConnectionFactory;
    async public Task<Dictionary<string, List<string>>> CheckPostAsync(Post post, List<Participant> participants)
    {
        var _instaApi = await _instConnectionFactory.GetOpenConnection();

        var mediaId = await _instaApi.MediaProcessor.GetMediaIdFromUrlAsync(new Uri(post.Url));
        var resp = await _instaApi.MediaProcessor.GetMediaByIdAsync(mediaId.Value);
        var comments = await _instaApi.CommentProcessor.GetMediaCommentsAsync(mediaId.Value, PaginationParameters.MaxPagesToLoad(50));
        var likes = await _instaApi.MediaProcessor.GetMediaLikersAsync(mediaId.Value);

        var trueParticipants = participants
            .Where(p => likes.Value.Select(l => l.UserName).Contains(p.UserName))
            .Intersect(participants.Where(p => comments.Value.Comments.Select(c => c.User.UserName).Contains(p.UserName)))
            .ToList();

        return new Dictionary<string, List<string>>
        {
            {post.Url, trueParticipants.Select(p => $"{p.UserName} points: {post.Points}").ToList() },
        };
    }
}
