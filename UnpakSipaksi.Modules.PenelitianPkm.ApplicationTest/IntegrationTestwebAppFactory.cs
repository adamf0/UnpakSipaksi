using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UnpakSipaksi.Modules.PenelitianPkm.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
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
                var descriptorDosen = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MemberDosenDbContext>));
                if (descriptorDosen != null)
                {
                    services.Remove(descriptorDosen);
                }

                var descriptorMahasiswa = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MemberMahasiswaDbContext>));
                if (descriptorMahasiswa != null)
                {
                    services.Remove(descriptorMahasiswa);
                }

                var descriptorNonDosen = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MemberNonDosenDbContext>));
                if (descriptorNonDosen != null)
                {
                    services.Remove(descriptorNonDosen);
                }

                var descriptorLuaran = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<LuaranDbContext>));
                if (descriptorLuaran != null)
                {
                    services.Remove(descriptorLuaran);
                }

                var descriptorDokumenMitra= services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DokumenMitraDbContext>));
                if (descriptorDokumenMitra != null)
                {
                    services.Remove(descriptorDokumenMitra);
                }

                var descriptorDokumenLainnya = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DokumenLainnyaDbContext>));
                if (descriptorDokumenLainnya != null)
                {
                    services.Remove(descriptorDokumenLainnya);
                }

                var descriptorSubstansiUsulan = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<SubstansiDbContext>));
                if (descriptorSubstansiUsulan != null)
                {
                    services.Remove(descriptorSubstansiUsulan);
                }

                var descriptorRab = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<SubstansiDbContext>));
                if (descriptorRab != null)
                {
                    services.Remove(descriptorRab);
                }

                // Tambahkan DbContext dengan koneksi ke MySQL container
                services.AddDbContext<MemberDosenDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<MemberMahasiswaDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<MemberNonDosenDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<LuaranDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<DokumenMitraDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<DokumenLainnyaDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<SubstansiDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<RABDbContext>(options =>
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
                DROP TABLE IF EXISTS `pkm_mitra`;
                CREATE TABLE `pkm_mitra` (
                  `id` int(11) NOT NULL AUTO_INCREMENT,
                  `uuid` varchar(36) DEFAULT NULL,
                  `id_pkm` int(11) NOT NULL,
                  `mitra` varchar(200) NOT NULL,
                  `provinsi` char(2) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci NOT NULL,
                  `kota` char(4) CHARACTER SET utf8mb3 COLLATE utf8mb3_unicode_ci NOT NULL,
                  `kelompokMitra` int(11) NOT NULL,
                  `pemimpinMitra` varchar(200) NOT NULL,
                  `kontakMitra` varchar(255) DEFAULT NULL,
                  `suratPernyataan` text DEFAULT NULL,
                  `link` text DEFAULT NULL,
                  `created_at` timestamp NULL DEFAULT NULL,
                  `updated_at` timestamp NULL DEFAULT NULL,
                  PRIMARY KEY (`id`),
                  KEY `id_pkm` (`id_pkm`),
                  KEY `kelompokMitra` (`kelompokMitra`),
                  KEY `provinsi` (`provinsi`),
                  KEY `kota` (`kota`) 
                );

                DROP TABLE IF EXISTS `pkm_dokumen_kontrak`;
                CREATE TABLE `pkm_dokumen_kontrak` (
                   `id` int(11) NOT NULL AUTO_INCREMENT,
                   `uuid` varchar(36) DEFAULT NULL,
                   `id_pkm` int(11) NOT NULL,
                   `file_kontrak` text DEFAULT NULL,
                   `link_kontrak` text DEFAULT NULL,
                   `keterangan` text DEFAULT NULL,
                   `created_at` datetime DEFAULT NULL,
                   `updated_at` datetime DEFAULT NULL,
                   PRIMARY KEY (`id`),
                   KEY `id_pkm` (`id_pkm`) 
                 );

                DROP TABLE IF EXISTS `pkm_anggota_dosen`;
                CREATE TABLE `pkm_anggota_dosen` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pkm` int(11) NOT NULL,
                `NIDN` varchar(50) NOT NULL,
                `status` tinyint(1) DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pkm` (`id_pkm`) 
                );

                DROP TABLE IF EXISTS `pkm_anggota_non_dosen`;
                CREATE TABLE `pkm_anggota_non_dosen` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pkm` int(11) NOT NULL,
                `nim` varchar(50) DEFAULT NULL,
                `bukti_mbkm` text DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pkm` (`id_pkm`) 
                );

                DROP TABLE IF EXISTS `pkm_anggota_non_dosen2`;
                CREATE TABLE `pkm_anggota_non_dosen2` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pkm` int(11) NOT NULL,
                `nomorIdentitas` varchar(255) DEFAULT NULL,
                `nama` varchar(255) DEFAULT NULL,
                `afiliasi` varchar(255) DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `pkm_anggota_non_dosen_ibfk_1_copy` (`id_pkm`) 
                );

                DROP TABLE IF EXISTS `pkm_luaran`;
                CREATE TABLE `pkm_luaran` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pkm` int(11) DEFAULT NULL,
                `id_jenis_luaran` int(11) DEFAULT NULL,
                `id_indikator_capaian` int(11) DEFAULT NULL,
                `keterangan` text DEFAULT NULL,
                `link` text DEFAULT NULL,
                `jenis` enum('','wajib','tambahan') DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pkm` (`id_pkm`) 
                );

                DROP TABLE IF EXISTS `pkm_substansi_usulan`;
                CREATE TABLE `pkm_substansi_usulan` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pkm` int(11) NOT NULL,
                `file` text DEFAULT NULL,
                `link` text DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pkm` (`id_pkm`) 
                );

            DROP TABLE IF EXISTS `pkm_rab`;
            CREATE TABLE `pkm_rab` (
              `id` int(11) NOT NULL AUTO_INCREMENT,
              `uuid` varchar(36) DEFAULT NULL,
              `id_pkm` int(11) NOT NULL,
              `kelompok_rab` int(11) DEFAULT NULL,
              `komponen` int(11) DEFAULT NULL,
              `item` int(11) DEFAULT NULL,
              `satuan` int(11) DEFAULT NULL,
              `harga_satuan` bigint(255) DEFAULT NULL,
              `total` bigint(255) DEFAULT NULL,
              `created_at` timestamp NULL DEFAULT NULL,
              `updated_at` timestamp NULL DEFAULT NULL,
              PRIMARY KEY (`id`),
              KEY `id_pkm` (`id_pkm`),
              KEY `kelompok_rab` (`kelompok_rab`),
              KEY `komponen` (`komponen`),
              KEY `satuan` (`satuan`) 
            );
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
