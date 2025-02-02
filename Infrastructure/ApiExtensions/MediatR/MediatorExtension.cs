using Domain.Features.Leagues.Add;
using Domain.Features.Leagues.Get;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ApiExtensions;

public static class MediatorExtension
{
    public static void AddMediator(this IServiceCollection services)
    {
        AssemblyScanner
            .FindValidatorsInAssembly(typeof(AddLeagueHandler).Assembly)
            .ForEach(result => services.AddTransient(result.InterfaceType, result.ValidatorType));
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}