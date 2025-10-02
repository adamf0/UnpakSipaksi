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

                // Tambahkan DbContext dengan koneksi ke MySQL container
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

            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            // Ambil koneksi MySQL dari container
            using var connection = new MySql.Data.MySqlClient.MySqlConnection(_dbContainer.GetConnectionString());
            await connection.OpenAsync();

            // Baca file SQL

            var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "Seed", "init.sql");
            var script = await File.ReadAllTextAsync(sqlFilePath);

            using var cmd = new MySql.Data.MySqlClient.MySqlCommand(script, connection);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
