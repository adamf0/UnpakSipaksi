using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.ApplicationTest
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
                    d => d.ServiceType == typeof(DbContextOptions<KesesuaianWaktuRabLuaranFasilitasDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Tambahkan DbContext dengan koneksi ke MySQL container
                services.AddDbContext<KesesuaianWaktuRabLuaranFasilitasDbContext>(options =>
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
                DROP TABLE IF EXISTS `kesesuaian_waktu_rab_luaran_fasilitas`;
                CREATE TABLE `kesesuaian_waktu_rab_luaran_fasilitas` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `name` text NOT NULL,
                `bobot_pdp` int(11) NOT NULL DEFAULT 0,
                `bobot_terapan` int(11) NOT NULL DEFAULT 0,
                `bobot_kerjasama` int(11) NOT NULL DEFAULT 0,
                `bobot_penelitian_dasar` int(11) NOT NULL DEFAULT 0,
                `skor` int(11) NOT NULL DEFAULT 0,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`)
                ) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
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
