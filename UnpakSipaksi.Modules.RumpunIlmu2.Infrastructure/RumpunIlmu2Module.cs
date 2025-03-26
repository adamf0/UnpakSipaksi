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
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu2.Presentation.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure.Database;
using UnpakSipaksi.Modules.RumpunIlmu2.PublicApi;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure
{
    public static class RumpunIlmu2Module
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RumpunIlmu2Endpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRumpunIlmu2Module(
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

            services.AddDbContext<RumpunIlmu2DbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRumpunIlmu2Repository, RumpunIlmu2Repository>();
            services.AddScoped<IRumpunIlmu2Api, IRumpunIlmu2Api>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RumpunIlmu2DbContext>());
        }
    }
}
