using eCommerce.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace eCommerce.SharedLibrary.DependecyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string fileName) where TContext : DbContext
        {
            // Add Generic Database context
            services.AddDbContext<TContext>(option => option.UseSqlServer(
                config
                .GetConnectionString("eCommerceConnection"), sqlserverOption =>
                sqlserverOption.EnableRetryOnFailure())
            );

            // Set up serilog loggin
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(path: $"{fileName}-.text",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {message:lj}{newLine}{Exception}",
                rollingInterval: RollingInterval.Day)
                .CreateLogger();

            //Add Authentication Scheme
            JWTAuthenticacionScheme.AddAuthenticacionScheme(services, config);

            return services;
        }

        public static IApplicationBuilder useSharedPolicies(this IApplicationBuilder app)
        {
            //Use global Exception
            app.UseMiddleware<GlobalException>();
            //Register middleware to bloc all outsiders API calls
            app.UseMiddleware<ListenToOnlyApiGateway>();

            return app;
        }
    }
}
