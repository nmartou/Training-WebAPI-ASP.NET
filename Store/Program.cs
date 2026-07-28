
using Microsoft.EntityFrameworkCore;
using Store.Repositories;
using Store.Services;

namespace Store;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        
        builder.Services.AddDbContext<StoreContext>(options=> options.UseSqlite(builder.Configuration.GetConnectionString("StoreConnection")));
       
        builder.Services.AddScoped<IPriceService, PriceService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IPersonService, PersonService>();
        builder.Services.AddScoped<ICommandService, CommandService>();
        
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseMiddleware<ExceptionLoggingMiddleware>();
            // app.UseExceptionHandler("/Error");
            // app.UseAuthentication();
            // app.UseAuthorization();
            
            app.UseHsts();
            app.UseHttpsRedirection();
        }
        app.MapControllers();

        app.Run();
    }
}

