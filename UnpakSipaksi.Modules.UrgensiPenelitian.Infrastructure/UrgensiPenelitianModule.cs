using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure.UrgensiPenelitian;
using UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure.Database;
using UnpakSipaksi.Modules.UrgensiPenelitian.Presentation.UrgensiPenelitian;
using UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure
{
    public static class UrgensiPenelitianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            UrgensiPenelitianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddUrgensiPenelitianModule(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddInfrastructure(configuration);

            return services;
        }

        private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string databaseConnectionString = configuration.GetConnectionString("Database")!;

            services.AddScoped<IDbConnectionFactory>(_ => new DbConnectionFactory(databaseConnectionString));

            services.AddDbContext<UrgensiPenelitianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IUrgensiPenelitianRepository, UrgensiPenelitianRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UrgensiPenelitianDbContext>());
        }
    }
}
