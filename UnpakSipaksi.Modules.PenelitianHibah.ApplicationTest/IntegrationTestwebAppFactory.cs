using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
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

                var descriptorDokumenPendukung = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DokumenPendukungDbContext>));
                if (descriptorDokumenPendukung != null)
                {
                    services.Remove(descriptorDokumenPendukung);
                }

                var descriptorDokumenKontrak = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DokumenKontrakDbContext>));
                if (descriptorDokumenKontrak != null)
                {
                    services.Remove(descriptorDokumenKontrak);
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

                services.AddDbContext<DokumenPendukungDbContext>(options =>
                {
                    options.UseMySQL(_dbContainer.GetConnectionString());
                });

                services.AddDbContext<DokumenKontrakDbContext>(options =>
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
                DROP TABLE IF EXISTS `penelitian_internal_anggota_dosen`;
                CREATE TABLE `penelitian_internal_anggota_dosen` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pdp` int(11) NOT NULL,
                `NIDN` varchar(50) NOT NULL,
                `status` tinyint(1) DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pdp` (`id_pdp`) 
                ) ENGINE=InnoDB AUTO_INCREMENT=4035 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

                DROP TABLE IF EXISTS `penelitian_internal_anggota_non_dosen`;
                CREATE TABLE `penelitian_internal_anggota_non_dosen` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pdp` int(11) NOT NULL,
                `nim` varchar(50) DEFAULT NULL,
                `bukti_mbkm` text DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pdp` (`id_pdp`) 
                ) ENGINE=InnoDB AUTO_INCREMENT=2266 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

                DROP TABLE IF EXISTS `penelitian_internal_anggota_non_dosen2`;
                CREATE TABLE `penelitian_internal_anggota_non_dosen2` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pdp` int(11) NOT NULL,
                `nomorIdentitas` varchar(255) DEFAULT NULL,
                `nama` varchar(255) DEFAULT NULL,
                `afiliasi` varchar(255) DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `penelitian_internal_anggota_non_dosen_ibfk_1_copy` (`id_pdp`) 
                ) ENGINE=InnoDB AUTO_INCREMENT=763 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

                DROP TABLE IF EXISTS `penelitian_internal_luaran`;
                CREATE TABLE `penelitian_internal_luaran` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pdp` int(11) DEFAULT NULL,
                `id_pdp_kategori` int(11) DEFAULT NULL,
                `id_pdp_kategori_luaran` int(11) DEFAULT NULL,
                `keterangan` text DEFAULT NULL,
                `link` text DEFAULT NULL,
                `jenis` enum('','wajib','tambahan') DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pdp` (`id_pdp`),
                KEY `id_pdp_kategori` (`id_pdp_kategori`),
                KEY `id_pdp_kategori_luaranpdp_kategori_luaran` (`id_pdp_kategori_luaran`) 
                ) ENGINE=InnoDB AUTO_INCREMENT=5150 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

                DROP TABLE IF EXISTS `penelitian_internal_dokumen_pendukung`;
                CREATE TABLE `penelitian_internal_dokumen_pendukung` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pdp` int(11) NOT NULL,
                `file_mitra` text DEFAULT NULL,
                `link` text DEFAULT NULL,
                `kategori` text NOT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pdp` (`id_pdp`) 
                ) ENGINE=InnoDB AUTO_INCREMENT=1275 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

                DROP TABLE IF EXISTS `penelitian_internal_dokumen_kontrak`;
                CREATE TABLE `penelitian_internal_dokumen_kontrak` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pdp` int(11) DEFAULT NULL,
                `file_kontrak` text DEFAULT NULL,
                `link_kontrak` text DEFAULT NULL,
                `created_at` datetime DEFAULT NULL,
                `updated_at` datetime DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pdp` (`id_pdp`) 
                ) ENGINE=InnoDB AUTO_INCREMENT=341 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

                DROP TABLE IF EXISTS `penelitian_internal_substansi_usulan`;
                CREATE TABLE `penelitian_internal_substansi_usulan` (
                `id` int(11) NOT NULL AUTO_INCREMENT,
                `uuid` varchar(36) DEFAULT NULL,
                `id_pdp` int(11) NOT NULL,
                `file` text DEFAULT NULL,
                `link` text DEFAULT NULL,
                `created_at` timestamp NULL DEFAULT NULL,
                `updated_at` timestamp NULL DEFAULT NULL,
                PRIMARY KEY (`id`),
                KEY `id_pdp` (`id_pdp`) 
                ) ENGINE=InnoDB AUTO_INCREMENT=1903 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

            DROP TABLE IF EXISTS `penelitian_internal_rab`;
            CREATE TABLE `penelitian_internal_rab` (
              `id` int(11) NOT NULL AUTO_INCREMENT,
              `uuid` varchar(36) DEFAULT NULL,
              `id_pdp` int(11) NOT NULL,
              `kelompok_rab` int(11) DEFAULT NULL,
              `komponen` int(11) DEFAULT NULL,
              `item` int(11) DEFAULT NULL,
              `satuan` int(11) DEFAULT NULL,
              `harga_satuan` bigint(255) DEFAULT NULL,
              `total` bigint(255) DEFAULT NULL,
              `created_at` timestamp NULL DEFAULT NULL,
              `updated_at` timestamp NULL DEFAULT NULL,
              PRIMARY KEY (`id`),
              KEY `id_pdp` (`id_pdp`),
              KEY `kelompok_rab` (`kelompok_rab`),
              KEY `komponen` (`komponen`),
              KEY `satuan` (`satuan`) 
            ) ENGINE=InnoDB AUTO_INCREMENT=11197 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
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
