
using Microsoft.EntityFrameworkCore;
using Store.Repositories;

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
       
        Initializations.ServicesInitialization(builder.Services);
        Initializations.RepositoryInitialization(builder.Services);
        
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

