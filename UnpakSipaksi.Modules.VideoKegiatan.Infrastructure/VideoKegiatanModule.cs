using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.VideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.Database;
using UnpakSipaksi.Modules.VideoKegiatan.Presentation.VideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.PublicApi;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.VideoKegiatan.Infrastructure
{
    public static class VideoKegiatanModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            VideoKegiatanEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddVideoKegiatanModule(
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

            services.AddDbContext<VideoKegiatanDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IVideoKegiatanRepository, VideoKegiatanRepository>();

            services.AddScoped<IVideoKegiatanApi, VideoKegiatanApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<VideoKegiatanDbContext>());
        }
    }
}
