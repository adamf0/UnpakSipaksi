using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.KategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.Database;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation.KategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.PublicApi;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure
{
    public static class KategoriProgramPengabdianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KategoriProgramPengabdianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKategoriProgramPengabdianModule(
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

            services.AddDbContext<KategoriProgramPengabdianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKategoriProgramPengabdianRepository, KategoriProgramPengabdianRepository>();

            services.AddScoped<IKategoriProgramPengabdianApi, KategoriProgramPengabdianApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KategoriProgramPengabdianDbContext>());
        }
    }
}
