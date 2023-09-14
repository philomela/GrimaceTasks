using Application.Common.Behaviors;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            cfg.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(PostsRestApiPreProcessor<>));
            cfg.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(ParticipantsRestApiPreProcessor<>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

        });
        //services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RestApiPreProcessor<>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
        return services;
    }
}