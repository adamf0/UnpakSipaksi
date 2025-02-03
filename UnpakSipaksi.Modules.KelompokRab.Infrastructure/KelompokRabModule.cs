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
using UnpakSipaksi.Modules.KelompokRab.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Presentation.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Infrastructure.Database;
using UnpakSipaksi.Modules.KelompokRab.Infrastructure.KelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.Infrastructure
{
    public static class KelompokRabModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KelompokRabEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKelompokRabModule(
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

            services.AddDbContext<KelompokRabDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKelompokRabRepository, KelompokRabRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KelompokRabDbContext>());
        }
    }
}
