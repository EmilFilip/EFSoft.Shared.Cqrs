

namespace EFSoft.Shared.Cqrs.DependencyInjection;

[ExcludeFromCodeCoverage]
public class CqrsConfigurator
{
    private readonly IServiceCollection _services;

    public CqrsConfigurator(IServiceCollection services)
    {
        _services = services ??
            throw new ArgumentNullException(nameof(services));

        AddDefaultDependencies();
    }

    /// <summary>
    /// Registers all Handlers found across all dependencies of the application (Scoped)
    /// (i.e. IQueryHandler, ICommandHandler, and ICommandHandlerWithResult)
    /// </summary>
    public CqrsConfigurator AddHandlers()
    {
        return AddHandlers(null);
    }

    /// <summary>
    /// Registers all Handlers found across all dependencies of the application (Scoped)
    /// (i.e. IQueryHandler, ICommandHandler, and ICommandHandlerWithResult)
    /// </summary>
    public CqrsConfigurator AddHandlers(params Assembly[]? assemblies)
    {
        _services.Scan(scan =>
          {
              var sources = assemblies == null || assemblies.Length == 0 ?
                scan.FromAssemblyDependencies(Assembly.GetEntryAssembly()) :
                scan.FromAssemblies(assemblies);

              sources
                  .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                      .AsSelfWithInterfaces()
                      .WithScopedLifetime()
                  .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                      .AsSelfWithInterfaces()
                      .WithScopedLifetime()
                  .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandlerWithResult<,>)))
                      .AsSelfWithInterfaces()
                      .WithScopedLifetime();
          });

        return this;
    }

    private void AddDefaultDependencies()
    {
        _services
            .AddScoped<IQueryExecutor, QueryExecutor>()
            .AddScoped<ICommandExecutor, CommandExecutor>()
            .AddScoped<ICommandExecutorWithResult, CommandExecutorWithResult>();
    }
}
