using Application.Common.Interfaces;
using Domain.Core;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //var connectionString = config["DbConnection"];

        //services.AddDbContext<AdminDbContext>(options =>
        //{
        //    options.UseSqlServer(connectionString);
        //});

        //services.AddScoped<IAdminDbContext>(provider => provider.GetService<AdminDbContext>());
        //services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(provider
        //    => new SqlConnectionFactory(connectionString));

        services.AddTransient<IInstagramService<Dictionary<string, List<string>>, Post, Participant>, InstagramService>();

        return services;
    }
}
