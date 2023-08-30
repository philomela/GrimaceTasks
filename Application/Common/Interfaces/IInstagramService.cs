namespace Application.Common.Interfaces;

public interface IInstagramService<T, K>
{
    Task<T> CheckPostAsync(List<K> participant);
}
