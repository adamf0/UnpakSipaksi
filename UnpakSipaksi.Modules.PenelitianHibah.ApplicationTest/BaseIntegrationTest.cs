using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.Database;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly MemberDosenDbContext DBContextDosen;
        protected readonly MemberMahasiswaDbContext DBContextMahasiswa;
        protected readonly MemberNonDosenDbContext DBContextNonDosen;
        protected readonly LuaranDbContext DBContextLuaran;
        protected readonly DokumenPendukungDbContext DBContextDokumenPendukung;
        protected readonly DokumenKontrakDbContext DBContextDokumenKontrak;
        protected readonly SubstansiDbContext DBContextSubstansiUsulan;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContextDosen = _scope.ServiceProvider.GetRequiredService<MemberDosenDbContext>();
            DBContextMahasiswa = _scope.ServiceProvider.GetRequiredService<MemberMahasiswaDbContext>();
            DBContextNonDosen = _scope.ServiceProvider.GetRequiredService<MemberNonDosenDbContext>();
            DBContextLuaran = _scope.ServiceProvider.GetRequiredService<LuaranDbContext>();
            DBContextDokumenKontrak = _scope.ServiceProvider.GetRequiredService<DokumenKontrakDbContext>();
            DBContextSubstansiUsulan = _scope.ServiceProvider.GetRequiredService<SubstansiDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
