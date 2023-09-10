namespace Application.Common.Interfaces;

public interface IHttpClient<T, K, P>
{
    Task<T> GetAsync(string url, CancellationToken token);

    Task<P> PostAsync(string url, K data, CancellationToken token);
}
