namespace Application.Common.Interfaces;

public interface IInstagramService<T, K, P>
{
    Task<T> CheckPostsAsync(List<K> posts, List<P> participants);
}
