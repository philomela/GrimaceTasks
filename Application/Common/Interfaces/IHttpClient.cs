namespace Application.Common.Interfaces;

public interface IHttpClient<T, K>
{
    Task<T> GetAsync(CancellationToken token);

    Task<T> PostAsync(K data, CancellationToken token);
}
