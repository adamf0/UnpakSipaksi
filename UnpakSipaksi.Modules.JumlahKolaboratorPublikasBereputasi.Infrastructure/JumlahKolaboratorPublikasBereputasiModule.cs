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
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure.Database;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Presentation.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.PublicApi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure
{
    public static class JumlahKolaboratorPublikasBereputasiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            JumlahKolaboratorPublikasBereputasiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddJumlahKolaboratorPublikasBereputasiModule(
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

            services.AddDbContext<JumlahKolaboratorPublikasBereputasiDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IJumlahKolaboratorPublikasBereputasiRepository, JumlahKolaboratorPublikasBereputasiRepository>();
            services.AddScoped<IJumlahKolaboratorPublikasBereputasiApi, JumlahKolaboratorPublikasBereputasiApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<JumlahKolaboratorPublikasBereputasiDbContext>());
        }
    }
}
