using InstagramApiSharp.API;
using System.Data;

namespace Application.Common.Interfaces;

public interface IInstagramConnectionFactory
{
    Task<IInstaApi> GetOpenConnection();
}
