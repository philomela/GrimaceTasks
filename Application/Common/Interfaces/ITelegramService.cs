namespace Application.Common.Interfaces;

public interface ITelegramService<T, K>
{
    Task<T> CheckPostAsync(List<K> participant);
}
