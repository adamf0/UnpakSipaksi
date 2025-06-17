using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.Substansi.Infrastructure.Database;
using UnpakSipaksi.Modules.Substansi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Substansi.Presentation.Substansi;
using UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal;
using UnpakSipaksi.Modules.Substansi.Infrastructure.SubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Infrastructure
{
    public static class SubstansiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            SubstansiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddSubstansiModule(
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

            services.AddDbContext<SubstansiInternalDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<ISubstansiInternalRepository, SubstansiInternalRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<SubstansiInternalDbContext>());
        }
    }
}
