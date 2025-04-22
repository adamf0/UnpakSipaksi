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
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiLppm;
using UnpakSipaksi.Modules.Insentif.Infrastructure.Database;
using UnpakSipaksi.Modules.Insentif.Infrastructure.Insentif;
using UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiFakultas;
using UnpakSipaksi.Modules.Insentif.Infrastructure.VerifikasiLppm;
using UnpakSipaksi.Modules.Insentif.Presentation.Insentif;

namespace UnpakSipaksi.Modules.Insentif.Infrastructure
{
    public static class InsentifModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            InsentifEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddInsentifModule(
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

            services.AddDbContext<InsentifDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            services.AddDbContext<VerifikasiFakultasDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            services.AddDbContext<VerifikasiLppmDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IInsentifRepository, InsentifRepository>();
            services.AddScoped<IVerifikasiFakultasRepository, VerifikasiFakultasRepository>();
            services.AddScoped<IVerifikasiLppmRepository, VerifikasiLppmRepository>();

            services.AddScoped<IUnitOfWorkInsentif>(sp => sp.GetRequiredService<InsentifDbContext>());
            services.AddScoped<IUnitOfWorkVerifikasiFakultas>(sp => sp.GetRequiredService<VerifikasiFakultasDbContext>());
            services.AddScoped<IUnitOfWorkVerifikasiLppm>(sp => sp.GetRequiredService<VerifikasiLppmDbContext>());
        }
    }
}
