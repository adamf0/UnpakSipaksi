using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.Database;
using UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.RoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Presentation.RoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure
{
    public static class RoadmapPenelitianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RoadmapPenelitianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRoadmapPenelitianModule(
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

            services.AddDbContext<RoadmapPenelitianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRoadmapPenelitianRepository, RoadmapPenelitianRepository>();

            services.AddScoped<IRoadmapPenelitianApi, RoadmapPenelitianApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RoadmapPenelitianDbContext>());
        }
    }
}
