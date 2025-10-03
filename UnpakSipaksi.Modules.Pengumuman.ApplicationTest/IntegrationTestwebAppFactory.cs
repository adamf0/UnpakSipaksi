using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UnpakSipaksi.Modules.Pengumuman.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.Pengumuman.ApplicationTest
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
                    d => d.ServiceType == typeof(DbContextOptions<PengumumanDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Tambahkan DbContext dengan koneksi ke MySQL container
                services.AddDbContext<PengumumanDbContext>(options =>
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
                DROP TABLE IF EXISTS `pengumuman`;
                CREATE TABLE `pengumuman` (
                `id` INT(11) NOT NULL AUTO_INCREMENT,
                `uuid` VARCHAR(36) DEFAULT NULL,
                `isi` TEXT NOT NULL,
                `file` VARCHAR(500) DEFAULT NULL,
                `url` VARCHAR(1000) DEFAULT NULL,
                `type` VARCHAR(20) NOT NULL DEFAULT 'pengumuman', 
                `type_target` VARCHAR(20) NOT NULL DEFAULT 'all', 
                `nidn` VARCHAR(50) DEFAULT NULL,
                `kode_fakultas` CHAR(9) DEFAULT NULL,
                `created_at` DATETIME DEFAULT NULL,
                `updated_at` DATETIME DEFAULT NULL,
                `type_expire` VARCHAR(20) DEFAULT 'no expire',
                `tanggal_awal` DATETIME DEFAULT NULL,
                `tanggal_akhir` DATETIME DEFAULT NULL,
                PRIMARY KEY (`id`)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
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
