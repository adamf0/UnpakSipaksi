using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.UpdateAdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiInternal
{
    internal static class UpdateAdministrasiInternal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("AdministrasiInternal", async (UpdateAdministrasiInternalRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateAdministrasiInternalCommand(
                    request.UuidPenelitianPkm,
                    request.Uuid,
                    request.Level,
                    request.Judul,
                    request.HalamanSampul,
                    request.HalamanPengesahan,
                    request.IdentitasPengusul,
                    request.IdentitasMahasiswa,
                    request.MitraKerjasama,
                    request.LuaranTargetCapaian,
                    request.Rab,
                    request.LatarBelakangRumusanMasalah,
                    request.PendekatanPemecahanMasalah,
                    request.Sota,
                    request.PenjelasanCapaianRisetKepakaran,
                    request.PetaJalan,
                    request.TahapanPenelitian,
                    request.IndikatorCapaianPadaRab,
                    request.Jadwal,
                    request.DaftarPustaka,
                    request.BiodataKetuaAnggota,
                    request.PaktaIntegritas,
                    request.SuratKetersediaanMitra,
                    request.Keputusan,
                    request.Komentar
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }

        internal sealed class UpdateAdministrasiInternalRequest
        {
            public string UuidPenelitianPkm { get; set; }
            public string Uuid { get; set; }
            public string Level { get; private set; }
            public string? Judul { get; private set; }
            public string? HalamanSampul { get; private set; }
            public string? HalamanPengesahan { get; private set; }
            public string? IdentitasPengusul { get; private set; }
            public string? IdentitasMahasiswa { get; private set; }
            public string? MitraKerjasama { get; private set; }
            public string? LuaranTargetCapaian { get; private set; }
            public string? Rab { get; private set; }
            public string? LatarBelakangRumusanMasalah { get; private set; }
            public string? PendekatanPemecahanMasalah { get; private set; }
            public string? Sota { get; private set; }
            public string? PenjelasanCapaianRisetKepakaran { get; private set; }
            public string? PetaJalan { get; private set; }
            public string? TahapanPenelitian { get; private set; }
            public string? IndikatorCapaianPadaRab { get; private set; }
            public string? Jadwal { get; private set; }
            public string? DaftarPustaka { get; private set; }
            public string? BiodataKetuaAnggota { get; private set; }
            public string? PaktaIntegritas { get; private set; }
            public string? SuratKetersediaanMitra { get; private set; }
            public string Keputusan { get; private set; }
            public string? Komentar { get; private set; }
        }
    }
}
