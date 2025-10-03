using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.RumpunIlmu1.ApplicationTest
{
    public class IntegrationTestWebAppFactory
        : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MySqlContainer _dbContainer =
            new MySqlBuilder()
                .WithImage("mysql:latest")
                .WithDatabase("mydb")
                .WithUsername("root")
                .WithPassword("pass")
                .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Hapus DbContext lama
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<RumpunIlmu1DbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Tambahkan DbContext dengan koneksi ke MySQL container
                services.AddDbContext<RumpunIlmu1DbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            // Ambil koneksi MySQL dari container
            using var connection = new MySql.Data.MySqlClient.MySqlConnection(_dbContainer.GetConnectionString());
            await connection.OpenAsync();

            // Baca file SQL

            // var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "Seed", "init.sql");
            // var script = await File.ReadAllTextAsync(sqlFilePath);
            var script = """
                DROP TABLE IF EXISTS `rumpun_ilmu1`;
                CREATE TABLE `rumpun_ilmu1` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `nama` varchar(200) NOT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`)
                ) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
            """;

            using var cmd = new MySql.Data.MySqlClient.MySqlCommand(script, connection);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
