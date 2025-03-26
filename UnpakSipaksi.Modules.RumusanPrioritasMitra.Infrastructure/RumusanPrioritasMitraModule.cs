using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.RumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.Database;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Presentation.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure
{
    public static class RumusanPrioritasMitraModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RumusanPrioritasMitraEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRumusanPrioritasMitraModule(
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

            services.AddDbContext<RumusanPrioritasMitraDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRumusanPrioritasMitraRepository, RumusanPrioritasMitraRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RumusanPrioritasMitraDbContext>());
        }
    }
}
