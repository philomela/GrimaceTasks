using Application.Common.Interfaces;
using Domain.Core;
using Infrastructure.Services;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        //var connectionString = config["DbConnection"];

        //services.AddDbContext<AdminDbContext>(options =>
        //{
        //    options.UseSqlServer(connectionString);
        //});

        //services.AddScoped<IAdminDbContext>(provider => provider.GetService<AdminDbContext>());
        //services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(provider
        //    => new SqlConnectionFactory(connectionString));



        services.AddSingleton<IInstaApi>(provider =>
        {
            var userSession = new UserSessionData
            {
                UserName = configuration.GetSection("Credentials").GetSection("InstagramLogin").Value,
                Password = configuration.GetSection("Credentials").GetSection("InstagramPassword").Value
            };

            var wp = new WebProxy()
            {
                Address = new Uri($"{configuration
            .GetSection("Proxy")
            .GetSection("Address").Value}:{configuration
            .GetSection("Proxy")
            .GetSection("Port").Value}"),
                Credentials = new NetworkCredential(configuration
            .GetSection("Proxy")
            .GetSection("Username").Value, configuration
            .GetSection("Proxy")
            .GetSection("Password").Value),

            };

            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = wp,
                UseProxy = true

            };

            return InstaApiBuilder.CreateBuilder()
               .UseHttpClientHandler(httpClientHandler)
                  .SetUser(userSession)
                  .UseLogger(new DebugLogger(InstagramApiSharp.Logger.LogLevel.Exceptions)) // use logger for requests and debug messages
                  .SetRequestDelay(RequestDelay.FromSeconds(0, 5)) //Delay requests
                  .Build();
        });


        services.AddScoped<IInstagramConnectionFactory, InstagramConnectionFactory>();
        services.AddTransient<IInstagramService<Dictionary<string, List<string>>, Post, Participant>, InstagramService>();

        return services;
    }
}
