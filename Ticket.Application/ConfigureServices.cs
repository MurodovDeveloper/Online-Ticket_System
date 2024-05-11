using MarketManager.Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ticket.Application.Common.Behaviours;
using Ticket.Application.Common.JWT.Interfaces;
using Ticket.Application.Common.JWT.Services;

namespace MarketManager.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            option.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            option.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        });
        services.AddScoped<IUserRefreshToken, RefreshToken>();
        services.AddScoped<IJwtToken, JwtToken>();
        services.AddScoped<GenericExcelReport>();

        return services;
    }
}
