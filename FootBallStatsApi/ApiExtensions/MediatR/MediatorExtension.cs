using Domain.Features.Players.Add;
using FluentValidation;
using MediatR;

namespace FootBallStatsApi.ApiExtensions.MediatR;

public static class MediatorExtension
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<AddPlayerHandler>();
        });

        
        AssemblyScanner
            .FindValidatorsInAssembly(typeof(AddPlayerHandler).Assembly)
            .ForEach(result => services.AddTransient(result.InterfaceType, result.ValidatorType));
        
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}