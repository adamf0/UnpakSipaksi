using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.Abstractions.Data;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure.Database;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure.ModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Presentation.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure
{
    public static class ModelFeasibilityStudysModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            ModelFeasibilityStudysEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddModelFeasibilityStudysModule(
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

            services.AddDbContext<ModelFeasibilityStudysDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IModelFeasibilityStudysRepository, ModelFeasibilityStudysRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ModelFeasibilityStudysDbContext>());
        }
    }
}
