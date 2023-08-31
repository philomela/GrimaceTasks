using Application.Common.Interfaces;
using InstagramApiSharp.API;

namespace Infrastructure.Services;

internal class InstagramConnectionFactory : IInstagramConnectionFactory
{
    private readonly IInstaApi _instaApi;

    public InstagramConnectionFactory(IInstaApi instaApi)
        => _instaApi = instaApi;

    public async Task<IInstaApi> GetOpenConnection()
    {
        if (_instaApi != null && !_instaApi.IsUserAuthenticated)
        {
            // login

            var logInResult = await _instaApi.LoginAsync();

            if (!logInResult.Succeeded)
            {
                Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
            }
        }

        return _instaApi;
    }
}
