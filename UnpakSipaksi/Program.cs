using Serilog;
using UnpakSipaksi.Common.Application;
using UnpakSipaksi.Common.Infrastructure;
using UnpakSipaksi.Api.Middleware;
using UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure;
using UnpakSipaksi.Modules.AuthorSinta.Infrastructure;
using UnpakSipaksi.Modules.FokusPenelitian.Infrastructure;
using UnpakSipaksi.Modules.TemaPenelitian.Infrastructure;
using UnpakSipaksi.Modules.TopikPenelitian.Infrastructure;
using UnpakSipaksi.Modules.FokusPengabdian.Infrastructure;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure;
using UnpakSipaksi.Modules.JenisPublikasi.Infrastructure;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure;
using UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure;
using UnpakSipaksi.Modules.KategoriTkt.Infrastructure;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure;
using UnpakSipaksi.Modules.KelompokRab.Infrastructure;
using UnpakSipaksi.Modules.KelompokMitra.Infrastructure;
using UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure;
using UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure;
using UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure;
using UnpakSipaksi.Modules.Komponen.Infrastructure;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure;
using UnpakSipaksi.Modules.KualitasIpteks.Infrastructure;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure;
using UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Microsoft.OpenApi.Models;
using UnpakSipaksi.Middleware;
using UnpakSipaksi.Security;
using UnpakSipaksi.Extensions;
using UnpakSipaksi.Common.Presentation.Security;

var builder = WebApplication.CreateBuilder(args);
RuntimeFeature.IsDynamicCodeSupported.Equals(false);
RuntimeFeature.IsDynamicCodeCompiled.Equals(false);
AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", false);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));
builder.Services.AddSingleton<TokenValidator>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute()); // Abaikan antiforgery untuk semua request
});
builder.Services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);


//builder.Configuration.AddModuleConfiguration(["Asset"]);
builder.Services.AddApplication([
    UnpakSipaksi.Modules.AkurasiPenelitian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.ArtikelMediaMassa.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.AuthorSinta.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.FokusPenelitian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.TemaPenelitian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.TopikPenelitian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.FokusPengabdian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.JenisPublikasi.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KategoriSumberDana.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KategoriTkt.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KebaruanReferensi.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KelompokRab.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KelompokMitra.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KesesuaianJadwal.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KesesuaianPenugasan.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KesesuaianTkt.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KetajamanAnalisis.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Komponen.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KualitasIpteks.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KuantitasStatusKi.Application.AssemblyReference.Assembly,
]);

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN"; // Token harus dikirim melalui header ini
    options.Cookie.Name = "XSRF-TOKEN"; // Nama cookie antiforgery
});

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);
builder.Services.AddAkurasiPenelitianModule(builder.Configuration);
builder.Services.AddArtikelMediaMassaModule(builder.Configuration);
builder.Services.AddAuthorSintaModule(builder.Configuration);
builder.Services.AddFokusPenelitianModule(builder.Configuration);
builder.Services.AddTemaPenelitianModule(builder.Configuration);
builder.Services.AddTopikPenelitianModule(builder.Configuration);
builder.Services.AddFokusPengabdianModule(builder.Configuration);
builder.Services.AddInovasiPemecahanMasalahModule(builder.Configuration);
builder.Services.AddJenisPublikasiModule(builder.Configuration);
builder.Services.AddJumlahKolaboratorPublikasBereputasiModule(builder.Configuration);
builder.Services.AddKategoriMitraPenelitianModule(builder.Configuration);
builder.Services.AddKategoriSumberDanaModule(builder.Configuration);
builder.Services.AddKategoriTktModule(builder.Configuration);
builder.Services.AddKebaruanReferensiModule(builder.Configuration);
builder.Services.AddKejelasanPembagianTugasTimModule(builder.Configuration);
builder.Services.AddKelompokRabModule(builder.Configuration);
builder.Services.AddKelompokMitraModule(builder.Configuration);
builder.Services.AddKesesuaianJadwalModule(builder.Configuration);
builder.Services.AddKesesuaianPenugasanModule(builder.Configuration);
builder.Services.AddKesesuaianSolusiMasalahMitraModule(builder.Configuration);
builder.Services.AddKesesuaianTktModule(builder.Configuration);
builder.Services.AddKesesuaianWaktuRabLuaranFasilitasModule(builder.Configuration);
builder.Services.AddKetajamanAnalisisModule(builder.Configuration);
builder.Services.AddKetajamanPerumusanMasalahModule(builder.Configuration);
builder.Services.AddKewajaranTahapanTargetModule(builder.Configuration);
builder.Services.AddKomponenModule(builder.Configuration);
builder.Services.AddKredibilitasMitraDukunganModule(builder.Configuration);
builder.Services.AddKualitasIpteksModule(builder.Configuration);
builder.Services.AddKualitasKuantitasPublikasiJurnalIlmiahModule(builder.Configuration);
builder.Services.AddKualitasKuantitasPublikasiProsidingModule(builder.Configuration);
builder.Services.AddKuantitasStatusKiModule(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UnpakSipaksi API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Masukkan token JWT dengan format 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.OperationFilter<AuthorizeCheckOperationFilter>();
    c.DocumentFilter<SwaggerAddApiPrefixDocumentFilter>();
    c.OperationFilter<SwaggerFileOperationFilter>();
});

builder.Services.AddAuthorization();
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

SecurityConfig.PreventDynamicCodeExecution();

var app = builder.Build();
app.UseUserAgentMiddleware();
app.UseSecurityHeadersMiddleware();

AkurasiPenelitianModule.MapEndpoints(app);
ArtikelMediaMassaModule.MapEndpoints(app);
AuthorSintaModule.MapEndpoints(app);
FokusPenelitianModule.MapEndpoints(app);
TemaPenelitianModule.MapEndpoints(app);
TopikPenelitianModule.MapEndpoints(app);
FokusPengabdianModule.MapEndpoints(app);
InovasiPemecahanMasalahModule.MapEndpoints(app);
JenisPublikasiModule.MapEndpoints(app);
JumlahKolaboratorPublikasBereputasiModule.MapEndpoints(app);
KategoriMitraPenelitianModule.MapEndpoints(app);
KategoriSumberDanaModule.MapEndpoints(app);
KategoriTktModule.MapEndpoints(app);
KebaruanReferensiModule.MapEndpoints(app);
KejelasanPembagianTugasTimModule.MapEndpoints(app);
KelompokRabModule.MapEndpoints(app);
KelompokMitraModule.MapEndpoints(app);
KesesuaianJadwalModule.MapEndpoints(app);
KesesuaianPenugasanModule.MapEndpoints(app);
KesesuaianSolusiMasalahMitraModule.MapEndpoints(app);
KesesuaianTktModule.MapEndpoints(app);
KesesuaianWaktuRabLuaranFasilitasModule.MapEndpoints(app);
KetajamanAnalisisModule.MapEndpoints(app);
KetajamanPerumusanMasalahModule.MapEndpoints(app);
KewajaranTahapanTargetModule.MapEndpoints(app);
KomponenModule.MapEndpoints(app);
KredibilitasMitraDukunganModule.MapEndpoints(app);
KualitasIpteksModule.MapEndpoints(app);
KualitasKuantitasPublikasiJurnalIlmiahModule.MapEndpoints(app);
KualitasKuantitasPublikasiProsidingModule.MapEndpoints(app);
KuantitasStatusKiModule.MapEndpoints(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseAuthorization();
app.MapControllers();
app.Run();
