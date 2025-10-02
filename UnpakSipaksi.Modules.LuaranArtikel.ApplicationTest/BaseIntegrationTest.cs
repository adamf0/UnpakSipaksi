using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.LuaranArtikel.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly LuaranArtikelDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<LuaranArtikelDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
