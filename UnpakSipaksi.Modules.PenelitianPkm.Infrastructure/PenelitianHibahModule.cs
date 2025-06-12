using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Common.Presentation.FileManager;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.RAB;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Luaran;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.RAB;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Substansi;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.DokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.Infrastructure
{
    public static class PenelitianPkmModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PenelitianPkmEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPenelitianPkmModule(
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

            services.TryAddSingleton<IFileProvider, FileProvider>();


            services.AddDbContext<PenelitianPkmDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<MemberDosenDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<MemberNonDosenDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<MemberMahasiswaDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<SubstansiDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<LuaranDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<RABDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<DokumenLainnyaDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddDbContext<DokumenMitraDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));



            services.AddScoped<IPenelitianPkmRepository, PenelitianPkmRepository>();

            services.AddScoped<IMemberDosenRepository, MemberDosenRepository>();

            services.AddScoped<IMemberNonDosenRepository, MemberNonDosenRepository>();

            services.AddScoped<IMemberMahasiswaRepository, MemberMahasiswaRepository>();

            services.AddScoped<ISubstansiRepository, SubstansiRepository>();

            services.AddScoped<ILuaranRepository, LuaranRepository>();

            services.AddScoped<IRABRepository, RABRepository>();

            services.AddScoped<IDokumenLainnyaRepository, DokumenLainnyaRepository>();

            services.AddScoped<IDokumenMitraRepository, DokumenMitraRepository>();


            services.AddScoped<IPenelitianPkmApi, PenelitianPkmApi>();



            services.AddScoped<IUnitOfWorkHibah>(sp => sp.GetRequiredService<PenelitianPkmDbContext>());

            services.AddScoped<IUnitOfWorkMember>(sp => sp.GetRequiredService<MemberDosenDbContext>());

            services.AddScoped<IUnitOfWorkNonMember>(sp => sp.GetRequiredService<MemberNonDosenDbContext>());

            services.AddScoped<IUnitOfWorkMemberMahasiswa>(sp => sp.GetRequiredService<MemberMahasiswaDbContext>());

            services.AddScoped<IUnitOfWorkSubstansi>(sp => sp.GetRequiredService<SubstansiDbContext>());

            services.AddScoped<IUnitOfWorkLuaran>(sp => sp.GetRequiredService<LuaranDbContext>());

            services.AddScoped<IUnitOfWorkRAB>(sp => sp.GetRequiredService<RABDbContext>());

            services.AddScoped<IUnitOfWorkDokumenLainnya>(sp => sp.GetRequiredService<DokumenLainnyaDbContext>());

            services.AddScoped<IUnitOfWorkDokumenMitra>(sp => sp.GetRequiredService<DokumenMitraDbContext>());

        }
    }
}
