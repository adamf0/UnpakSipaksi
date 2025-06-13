using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.UpdateAdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiPkm
{
    internal static class UpdateAdministrasiPkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("AdministrasiPkm", async (UpdateAdministrasiPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateAdministrasiPkmCommand(
                    request.UuidPenelitianPkm,
                    request.Uuid,
                    request.Level,
                    request.Judul,
                    request.HalamanSampul,
                    request.HalamanPengesahan,
                    request.IdentitasPengusul,
                    request.MitraPkm,
                    request.LuaranTargetCapaian,
                    request.Rab,
                    request.RingkasanKataKunci,
                    request.AnalisisSituasi,
                    request.PermasalahanMitra,
                    request.SolusiPermasalahan,
                    request.MetodePelaksanaan,
                    request.Jadwal,
                    request.DaftarPustaka,
                    request.BiodataKetuaAnggota,
                    request.GambaranIptek,
                    request.PetaLokasiMitra,
                    request.SuratPernyataanKetuaPelaksana,
                    request.SuratKetersediaanMitra,
                    request.MelibatkanMahasiswa,
                    request.MendukungIKU,
                    request.Keputusan,
                    request.Komentar
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }

        internal sealed class UpdateAdministrasiPkmRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string Level { get; private set; }
            public string Judul { get; private set; }
            public string? HalamanSampul { get; private set; }
            public string? HalamanPengesahan { get; private set; }
            public string? IdentitasPengusul { get; private set; }
            public string? MitraPkm { get; private set; }
            public string? LuaranTargetCapaian { get; private set; }
            public string? Rab { get; private set; }
            public string? RingkasanKataKunci { get; private set; }
            public string? AnalisisSituasi { get; private set; }
            public string? PermasalahanMitra { get; private set; }
            public string? SolusiPermasalahan { get; private set; }
            public string? MetodePelaksanaan { get; private set; }
            public string? Jadwal { get; private set; }
            public string? DaftarPustaka { get; private set; }
            public string? BiodataKetuaAnggota { get; private set; }
            public string? GambaranIptek { get; private set; }
            public string? PetaLokasiMitra { get; private set; }
            public string? SuratPernyataanKetuaPelaksana { get; private set; }
            public string? SuratKetersediaanMitra { get; private set; }
            public string? MelibatkanMahasiswa { get; private set; }
            public string? MendukungIKU { get; private set; }
            public string Keputusan { get; private set; }
            public string? Komentar { get; private set; }
        }
    }
}
