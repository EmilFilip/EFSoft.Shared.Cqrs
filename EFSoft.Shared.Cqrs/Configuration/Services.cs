namespace EFSoft.Shared.Cqrs.Configuration;

[ExcludeFromCodeCoverage]
public static class Services
{
    public static IServiceCollection RegisterCqrs(
                    this IServiceCollection services,
                    Assembly assembly)
    {
        return services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
             .AddMediatR(configure =>
             {
                 configure.RegisterServicesFromAssembly(assembly);
             })
             .AddValidatorsFromAssembly(assembly);
    }
}
