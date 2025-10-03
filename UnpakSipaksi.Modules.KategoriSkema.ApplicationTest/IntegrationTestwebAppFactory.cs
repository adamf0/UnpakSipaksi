using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UnpakSipaksi.Modules.KategoriSkema.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriSkema.ApplicationTest
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
                    d => d.ServiceType == typeof(DbContextOptions<KategoriSkemaDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Tambahkan DbContext dengan koneksi ke MySQL container
                services.AddDbContext<KategoriSkemaDbContext>(options =>
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
                DROP TABLE IF EXISTS `kategori_skema`;
                CREATE TABLE `kategori_skema` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `nama` text NOT NULL,
                `min` int(11) DEFAULT NULL,
                `max` int(11) DEFAULT NULL,
                `old_rule` VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT '{"operation": "and","rules": {}}',
                `rule` VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT '[]',
                `keyName` text DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`)
                ) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
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
