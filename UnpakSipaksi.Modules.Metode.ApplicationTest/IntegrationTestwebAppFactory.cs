using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UnpakSipaksi.Modules.Metode.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.Metode.ApplicationTest
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
                    d => d.ServiceType == typeof(DbContextOptions<MetodeDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Tambahkan DbContext dengan koneksi ke MySQL container
                services.AddDbContext<MetodeDbContext>(options =>
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
                DROP TABLE IF EXISTS `metode`;
                CREATE TABLE `metode` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_akurasi_penelitian` int(11) NOT NULL,
                `id_kejelasan_pembagian_tugas_tim` int(11) NOT NULL,
                `id_kesesuaian_waktu_rab_luaran_fasilitas` int(11) NOT NULL,
                `id_potensi_ketercapaian_luaran_dijanjikan` int(11) NOT NULL,
                `id_model_feasibility_study` int(11) NOT NULL,
                `id_kesesuaian_tkt` int(11) NOT NULL,
                `id_kredibilitas_mitra_dukungan` int(11) NOT NULL,
                `nilai` int(11) NOT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                UNIQUE KEY `id_akurasi_penelitian` (`id_akurasi_penelitian`,`id_kejelasan_pembagian_tugas_tim`,`id_kesesuaian_waktu_rab_luaran_fasilitas`,`id_potensi_ketercapaian_luaran_dijanjikan`,`id_model_feasibility_study`,`id_kesesuaian_tkt`,`id_kredibilitas_mitra_dukungan`),
                KEY `id_akurasi_penelitian_2` (`id_akurasi_penelitian`,`id_kejelasan_pembagian_tugas_tim`,`id_kesesuaian_waktu_rab_luaran_fasilitas`,`id_potensi_ketercapaian_luaran_dijanjikan`,`id_model_feasibility_study`,`id_kesesuaian_tkt`,`id_kredibilitas_mitra_dukungan`),
                KEY `id_kejelasan_pembagian_tugas_tim` (`id_kejelasan_pembagian_tugas_tim`),
                KEY `id_kesesuaian_tkt` (`id_kesesuaian_tkt`),
                KEY `id_kesesuaian_waktu_rab_luaran_fasilitas` (`id_kesesuaian_waktu_rab_luaran_fasilitas`),
                KEY `id_kredibilitas_mitra_dukungan` (`id_kredibilitas_mitra_dukungan`),
                KEY `id_model_feasibility_study` (`id_model_feasibility_study`),
                KEY `id_potensi_ketercapaian_luaran_dijanjikan` (`id_potensi_ketercapaian_luaran_dijanjikan`) 
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
