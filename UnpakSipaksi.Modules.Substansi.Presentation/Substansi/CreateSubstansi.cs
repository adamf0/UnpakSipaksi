using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Substansi.Application.CreateSubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Presentation.Substansi
{
    internal static class CreateSubstansi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("SubstansiInternal", async (CreateSubstansiRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateSubstansiInternalCommand(
                        request.UuidPenelitianHibah,
                        request.UuidPublikasiDisitasiProposal,
                        request.UuidRelevansiKepakaranTemaProposal,
                        request.UuidJumlahKolaboratorPublikasiBereputasi,
                        request.UuidRelevansiProdukKepentinganNasional,
                        request.UuidKetajamanPerumusanMasalah,
                        request.UuidInovasiPemecahanMasalah,
                        request.UuidSotaKebaharuan,
                        request.UuidRoadmapPenelitian,
                        request.UuidAkurasiPenelitian,
                        request.UuidKejelasanPembagianTugasTim,
                        request.UuidKesesuaianWaktuRabLuaranFasilitas,
                        request.UuidPotensiKetercapaianLuaranDijanjikan,
                        request.UuidModelFeasibilityStudy,
                        request.UuidKesesuaianTkt,
                        request.UuidKredibilitasMitraDukungan,
                        request.UuidKebaruanReferensi,
                        request.UuidRelevansiKualitasReferensi,
                        request.Komentar,
                        request.ReviewerInternal,
                        request.ReviewerExternal,
                        request.TanggalMulai,
                        request.TanggalBerakhir
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Substansi);
        }

        internal sealed class CreateSubstansiRequest
        {
            public string UuidPenelitianHibah { get; set; } = null!;
            public string? UuidPublikasiDisitasiProposal { get; set; } = null!;
            public string? UuidRelevansiKepakaranTemaProposal { get; set; } = null!;
            public string? UuidJumlahKolaboratorPublikasiBereputasi { get; set; } = null!;
            public string? UuidRelevansiProdukKepentinganNasional { get; set; } = null!;
            public string? UuidKetajamanPerumusanMasalah { get; set; } = null!;
            public string? UuidInovasiPemecahanMasalah { get; set; } = null!;
            public string? UuidSotaKebaharuan { get; set; } = null!;
            public string? UuidRoadmapPenelitian { get; set; } = null!;
            public string? UuidAkurasiPenelitian { get; set; } = null!;
            public string? UuidKejelasanPembagianTugasTim { get; set; } = null!;
            public string? UuidKesesuaianWaktuRabLuaranFasilitas { get; set; } = null!;
            public string? UuidPotensiKetercapaianLuaranDijanjikan { get; set; } = null!;
            public string? UuidModelFeasibilityStudy { get; set; } = null!;
            public string? UuidKesesuaianTkt { get; set; } = null!;
            public string? UuidKredibilitasMitraDukungan { get; set; } = null!;
            public string? UuidKebaruanReferensi { get; set; } = null!;
            public string? UuidRelevansiKualitasReferensi { get; set; } = null!;
            public string? Komentar { get; set; } = null!;
            public string? ReviewerInternal { get; set; } = null!;
            public string? ReviewerExternal { get; set; } = null!;
            public string TanggalMulai { get; set; }
            public string TanggalBerakhir { get; set; }
        }
    }
}
