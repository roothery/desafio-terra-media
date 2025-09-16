using Desafio.TerraMedia.Application;
using Desafio.TerraMedia.Common.HealthChecks;
using Desafio.TerraMedia.Common.Logging;
using Desafio.TerraMedia.Common.Security;
using Desafio.TerraMedia.Common.Validation;
using Desafio.TerraMedia.IoC;
using Desafio.TerraMedia.ORM;
using Desafio.TerraMedia.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Serilog;

namespace Desafio.TerraMedia.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Desafio.TerraMedia.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //var inMemoryNetwork = new InMemNetwork();
            //builder.Services.AddRebus(configure => configure
            //.Transport(t => t.UseInMemoryTransport(inMemoryNetwork, "sales-queue"))
            //.Logging(l => l.Console())
            //.Options(o => o.LogPipeline()));

            builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
