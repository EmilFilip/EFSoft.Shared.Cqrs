namespace EFSoft.Customers.Api.Configuration;

[ExcludeFromCodeCoverage]
public static class Services
{
    public static IServiceCollection Register(
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
