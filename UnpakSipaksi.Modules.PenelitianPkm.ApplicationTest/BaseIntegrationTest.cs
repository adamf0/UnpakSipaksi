using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly Infrastructure.Database.MemberDosenDbContext DBContextDosen;
        protected readonly Infrastructure.Database.MemberMahasiswaDbContext DBContextMahasiswa;
        protected readonly Infrastructure.Database.MemberNonDosenDbContext DBContextNonDosen;
        protected readonly Infrastructure.Database.LuaranDbContext DBContextLuaran;
        protected readonly Infrastructure.Database.DokumenMitraDbContext DBContextDokumenMitra;
        protected readonly Infrastructure.Database.DokumenLainnyaDbContext DBContextDokumenLainnya;
        protected readonly Infrastructure.Database.SubstansiDbContext DBContextSubstansiUsulan;
        protected readonly Infrastructure.Database.RABDbContext DBContextRab;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContextDosen = _scope.ServiceProvider.GetRequiredService<MemberDosenDbContext>();
            DBContextMahasiswa = _scope.ServiceProvider.GetRequiredService<MemberMahasiswaDbContext>();
            DBContextNonDosen = _scope.ServiceProvider.GetRequiredService<MemberNonDosenDbContext>();
            DBContextLuaran = _scope.ServiceProvider.GetRequiredService<LuaranDbContext>();
            DBContextDokumenMitra = _scope.ServiceProvider.GetRequiredService<DokumenMitraDbContext>();
            DBContextDokumenLainnya = _scope.ServiceProvider.GetRequiredService<DokumenLainnyaDbContext>();
            DBContextSubstansiUsulan = _scope.ServiceProvider.GetRequiredService<SubstansiDbContext>();
            DBContextRab = _scope.ServiceProvider.GetRequiredService<RABDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
