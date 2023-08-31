namespace Application.Common.Interfaces;

public interface IInstagramService<T, K, P>
{
    Task<T> CheckPostAsync(K post, List<P> participant);
}
