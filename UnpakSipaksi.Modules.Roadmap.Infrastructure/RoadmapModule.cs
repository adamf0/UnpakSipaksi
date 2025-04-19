using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.Roadmap.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Roadmap.Infrastructure.Database;
using UnpakSipaksi.Modules.Roadmap.Infrastructure.Roadmap;
using UnpakSipaksi.Modules.Roadmap.Presentation.Roadmap;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Infrastructure
{
    public static class RoadmapModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RoadmapEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRoadmapModule(
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

            services.AddDbContext<RoadmapDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRoadmapRepository, RoadmapRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RoadmapDbContext>());
        }
    }
}
