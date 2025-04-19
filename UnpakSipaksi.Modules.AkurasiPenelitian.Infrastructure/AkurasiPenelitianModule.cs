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
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.AkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.Database;
using UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.AkurasiPenelitian.Presentation.AkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure
{
    public static class AkurasiPenelitianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            AkurasiPenelitianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddAkurasiPenelitianModule(
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

            services.AddDbContext<AkurasiPenelitianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IAkurasiPenelitianRepository, AkurasiPenelitianRepository>();

            services.AddScoped<IAkurasiPenelitianApi, AkurasiPenelitianApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AkurasiPenelitianDbContext>());
        }
    }
}
