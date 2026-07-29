using Store.Services;
using Store.Repositories;

namespace Store;

public class Initializations
{
    public static void ServicesInitialization(IServiceCollection service)
    {
        service.AddScoped<IPriceService, PriceService>();
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<IPersonService, PersonService>();
        service.AddScoped<ICommandService, CommandService>();
        service.AddScoped<IProductCommandService, ProductCommandService>();
    }

    public static void RepositoryInitialization(IServiceCollection service)
    {
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}