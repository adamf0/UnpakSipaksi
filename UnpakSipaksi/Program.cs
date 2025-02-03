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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
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
]);

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

var app = builder.Build();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use((context, next) =>
{
    var userAgent = context.Request.Headers["User-Agent"].ToString();

    if (string.IsNullOrEmpty(userAgent))
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Detail = "Unknown User-Agent"
        };
        context.Response.StatusCode = problemDetails.Status.Value;

        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(problemDetails);
    }

    return next();
});

app.Use((context, next) =>
{
    context.Response.Headers.Remove("X-AspNet-Version");
    context.Response.Headers["X-DNS-Prefetch-Control"] = "off";

    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["Content-Type"] = "application/json; charset=UTF-8";
    context.Response.Headers["Strict-Transport-Security"] = "max-age=60; includeSubDomains; preload";
    context.Response.Headers["Access-Control-Allow-Origin"] = "https://localhost";
    context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";
    context.Response.Headers["Cross-Origin-Embedder-Policy"] = "require-corp";
    context.Response.Headers["Cross-Origin-Resource-Policy"] = "same-site";

    context.Response.Headers.Remove("X-Powered-By");
    return next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
