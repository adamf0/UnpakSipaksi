using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.Kategori.Infrastructure.Database;
using Xunit;

namespace Application.Integration.Tests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly KategoriDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<KategoriDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
