namespace EFSoft.Shared.Cqrs.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class CqrsInstaller
{
    /// <summary>
    /// Registers Cqrs Executors (Scoped)
    /// (i.e. IQueryExecutor, ICommandExecutor, and ICommandExecutorWithResult)
    /// </summary>
    /// <param name="services">The service collection that the CQRS infrastructure should be added to</param>
    /// <param name="configurator">For extra configurations such as adding logging or registering Handlers</param>
    public static IServiceCollection AddCqrs(
        this IServiceCollection services,
        Action<CqrsConfigurator>? configurator = null)
    {
        var serviceCollectionConfigurator = new CqrsConfigurator(services);

        configurator?.Invoke(serviceCollectionConfigurator);

        return services;
    }
}