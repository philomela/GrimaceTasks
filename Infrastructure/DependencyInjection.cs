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
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.Hangfire;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration["DbConnection"];

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


        services.AddSingleton<IInstagramConnectionFactory, InstagramConnectionFactory>();
        
        services.AddSingleton<IInstagramService<Dictionary<string, List<string>>, Post, Participant>, InstagramService>();
        
        services.AddScoped<IScheduledTasks, ScheduledCheckPostsJob>();

        services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true
       }));
        services.AddHangfireServer();

        return services;
    }
}
