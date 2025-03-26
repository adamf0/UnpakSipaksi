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
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu3.Presentation.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.Database;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure
{
    public static class RumpunIlmu3Module
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RumpunIlmu3Endpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRumpunIlmu3Module(
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

            services.AddDbContext<RumpunIlmu3DbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRumpunIlmu3Repository, RumpunIlmu3Repository>();
            services.AddScoped<IRumpunIlmu3Api, RumpunIlmu3Api>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RumpunIlmu3DbContext>());
        }
    }
}
