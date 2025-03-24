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
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.RumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.Database;
using UnpakSipaksi.Modules.RumpunIlmu1.Presentation.RumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure
{
    public static class RumpunIlmu1Module
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RumpunIlmu1Endpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRumpunIlmu1Module(
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

            services.AddDbContext<RumpunIlmu1DbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRumpunIlmu1Repository, RumpunIlmu1Repository>();
            services.AddScoped<IRumpunIlmu1Api, RumpunIlmu1Api>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RumpunIlmu1DbContext>());
        }
    }
}
