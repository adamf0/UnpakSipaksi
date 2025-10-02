using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.JenisLuaran.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.JenisLuaran.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly JenisLuaranDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<JenisLuaranDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
