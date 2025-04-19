using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.Database;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.PublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation.PublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.PublicApi;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure
{
    public static class PublikasiDisitasiProposalModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PublikasiDisitasiProposalEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPublikasiDisitasiProposalModule(
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

            services.AddDbContext<PublikasiDisitasiProposalDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IPublikasiDisitasiProposalRepository, PublikasiDisitasiProposalRepository>();

            services.AddScoped<IPublikasiDisitasiProposalApi, PublikasiDisitasiProposalApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PublikasiDisitasiProposalDbContext>());
        }
    }
}
