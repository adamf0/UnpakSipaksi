using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.FokusPengabdian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Presentation.FokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.Database;
using UnpakSipaksi.Modules.FokusPengabdian.Infrastructure.FokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Infrastructure
{
    public static class FokusPengabdianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            FokusPengabdianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddFokusPengabdianModule(
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

            services.AddDbContext<FokusPengabdianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IFokusPengabdianRepository, FokusPengabdianRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<FokusPengabdianDbContext>());
        }
    }
}
