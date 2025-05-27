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
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure;
using UnpakSipaksi.Modules.PrioritasRiset.Infrastructure;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure;
using UnpakSipaksi.Modules.Rirn.Infrastructure;
using UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure;
using UnpakSipaksi.Modules.RumpunIlmu1.Infrastructure;
using UnpakSipaksi.Modules.RumpunIlmu2.Infrastructure;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure;
using UnpakSipaksi.Modules.Satuan.Infrastructure;
using UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure;
using UnpakSipaksi.Modules.KategoriSkema.Infrastructure;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure;
using UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure;
using UnpakSipaksi.Modules.Pengumuman.Infrastructure;
using UnpakSipaksi.Modules.Referensi.Infrastructure;
using UnpakSipaksi.Modules.Roadmap.Infrastructure;
using UnpakSipaksi.Modules.Metode.Infrastructure;
using UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure;
using UnpakSipaksi.Modules.SubstansiBobot.Infrastructure;
using UnpakSipaksi.Modules.PenelitianHibah.Infrastructure;
using UnpakSipaksi.Modules.Kategori.Infrastructure;
using UnpakSipaksi.Modules.KategoriLuaran.Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using Microsoft.OpenApi.Models;
using UnpakSipaksi.Middleware;
using UnpakSipaksi.Security;
using UnpakSipaksi.Extensions;
using UnpakSipaksi.Common.Presentation.Security;
using UnpakSipaksi.Modules.Insentif.Infrastructure;

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
    UnpakSipaksi.Modules.LuaranArtikel.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.PrioritasRiset.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Rirn.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RoadmapPenelitian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RumpunIlmu1.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RumpunIlmu2.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RumpunIlmu3.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Satuan.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.SotaKebaharuan.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.VideoKegiatan.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KategoriSkema.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.PenugasanReviewer.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Pengumuman.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Referensi.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Roadmap.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Metode.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.UrgensiPenelitian.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.SubstansiBobot.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Insentif.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.PenelitianHibah.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.Kategori.Application.AssemblyReference.Assembly,
    UnpakSipaksi.Modules.KategoriLuaran.Application.AssemblyReference.Assembly,
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
builder.Services.AddLuaranArtikelModule(builder.Configuration);
builder.Services.AddMetodeRencanaKegiatanModule(builder.Configuration);
builder.Services.AddModelFeasibilityStudysModule(builder.Configuration);
builder.Services.AddPeningkatanKeberdayaanMitraModule(builder.Configuration);
builder.Services.AddPotensiKetercapaianLuaranDijanjikanModule(builder.Configuration);
builder.Services.AddPrioritasRisetModule(builder.Configuration);
builder.Services.AddPublikasiDisitasiProposalModule(builder.Configuration);
builder.Services.AddRelevansiKepakaranTemaProposalModule(builder.Configuration);
builder.Services.AddRelevansiKualitasReferensiModule(builder.Configuration);
builder.Services.AddRelevansiProdukKepentinganNasionalModule(builder.Configuration);
builder.Services.AddRirnModule(builder.Configuration);
builder.Services.AddRoadmapPenelitianModule(builder.Configuration);
builder.Services.AddRumpunIlmu1Module(builder.Configuration);
builder.Services.AddRumpunIlmu2Module(builder.Configuration);
builder.Services.AddRumpunIlmu3Module(builder.Configuration);
builder.Services.AddRumusanPrioritasMitraModule(builder.Configuration);
builder.Services.AddSatuanModule(builder.Configuration);
builder.Services.AddSotaKebaharuanModule(builder.Configuration);
builder.Services.AddVideoKegiatanModule(builder.Configuration);
builder.Services.AddKategoriSkemaModule(builder.Configuration);
builder.Services.AddKategoriProgramPengabdianModule(builder.Configuration);
builder.Services.AddPenugasanReviewerModule(builder.Configuration);
builder.Services.AddPengumumanModule(builder.Configuration);
builder.Services.AddReferensiModule(builder.Configuration);
builder.Services.AddRoadmapModule(builder.Configuration);
builder.Services.AddMetodeModule(builder.Configuration);
builder.Services.AddUrgensiPenelitianModule(builder.Configuration);
builder.Services.AddSubstansiBobotModule(builder.Configuration);
builder.Services.AddInsentifModule(builder.Configuration);
builder.Services.AddPenelitianHibahModule(builder.Configuration);
builder.Services.AddKategoriModule(builder.Configuration);
builder.Services.AddKategoriLuaranModule(builder.Configuration);

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
LuaranArtikelModule.MapEndpoints(app);
MetodeRencanaKegiatanModule.MapEndpoints(app);
ModelFeasibilityStudysModule.MapEndpoints(app);
PeningkatanKeberdayaanMitraModule.MapEndpoints(app);
PotensiKetercapaianLuaranDijanjikanModule.MapEndpoints(app);
PrioritasRisetModule.MapEndpoints(app);
PublikasiDisitasiProposalModule.MapEndpoints(app);
RelevansiKepakaranTemaProposalModule.MapEndpoints(app);
RelevansiKualitasReferensiModule.MapEndpoints(app);
RelevansiProdukKepentinganNasionalModule.MapEndpoints(app);
RirnModule.MapEndpoints(app);
RoadmapPenelitianModule.MapEndpoints(app);
RumpunIlmu1Module.MapEndpoints(app);
RumpunIlmu2Module.MapEndpoints(app);
RumpunIlmu3Module.MapEndpoints(app);
RumusanPrioritasMitraModule.MapEndpoints(app);
SatuanModule.MapEndpoints(app);
SotaKebaharuanModule.MapEndpoints(app);
VideoKegiatanModule.MapEndpoints(app);
KategoriSkemaModule.MapEndpoints(app);
KategoriProgramPengabdianModule.MapEndpoints(app);
PenugasanReviewerModule.MapEndpoints(app);
PengumumanModule.MapEndpoints(app);
ReferensiModule.MapEndpoints(app);
RoadmapModule.MapEndpoints(app);
MetodeModule.MapEndpoints(app);
UrgensiPenelitianModule.MapEndpoints(app);
SubstansiBobotModule.MapEndpoints(app);
InsentifModule.MapEndpoints(app);
PenelitianHibahModule.MapEndpoints(app);
KategoriModule.MapEndpoints(app);
KategoriLuaranModule.MapEndpoints(app);

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
