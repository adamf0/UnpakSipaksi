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
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Infrastructure
{
    public static class PenelitianHibahModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PenelitianHibahEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPenelitianHibahModule(
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

            services.AddDbContext<PenelitianHibahDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<MemberDosenDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            
            services.AddDbContext<MemberNonDosenDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<MemberMahasiswaDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            


            services.AddScoped<IPenelitianHibahRepository, PenelitianHibahRepository>();

            services.AddScoped<IMemberDosenRepository, MemberDosenRepository>();

            services.AddScoped<IMemberNonDosenRepository, MemberNonDosenRepository>();

            services.AddScoped<IMemberMahasiswaRepository, MemberMahasiswaRepository>();

            
            
            services.AddScoped<IPenelitianHibahApi, PenelitianHibahApi>();

            
            
            services.AddScoped<IUnitOfWorkHibah>(sp => sp.GetRequiredService<PenelitianHibahDbContext>());

            services.AddScoped<IUnitOfWorkMember>(sp => sp.GetRequiredService<MemberDosenDbContext>());

            services.AddScoped<IUnitOfWorkNonMember>(sp => sp.GetRequiredService<MemberNonDosenDbContext>());

            services.AddScoped<IUnitOfWorkMemberMahasiswa>(sp => sp.GetRequiredService<MemberMahasiswaDbContext>());
        }
    }
}
