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
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;
using UnpakSipaksi.Modules.Rirn.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Rirn.Presentation.Rirn;
using UnpakSipaksi.Modules.Rirn.Infrastructure.Rirn;
using UnpakSipaksi.Modules.Rirn.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Rirn.Infrastructure
{
    public static class RirnModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RirnEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRirnModule(
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

            services.AddDbContext<RirnDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRirnRepository, RirnRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RirnDbContext>());
        }
    }
}
