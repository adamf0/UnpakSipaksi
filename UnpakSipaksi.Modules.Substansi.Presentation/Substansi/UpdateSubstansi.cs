using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Substansi.Application.UpdateSubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Presentation.Substansi
{
    internal static class UpdateSubstansi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("SubstansiInternal", async (UpdateSubstansiRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateSubstansiInternalCommand(
                        request.Uuid,
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

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Substansi);
        }

        internal sealed class UpdateSubstansiRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianHibah { get; private set; }
            public string? UuidPublikasiDisitasiProposal { get; private set; } = null!;
            public string? UuidRelevansiKepakaranTemaProposal { get; private set; } = null!;
            public string? UuidJumlahKolaboratorPublikasiBereputasi { get; private set; } = null!;
            public string? UuidRelevansiProdukKepentinganNasional { get; private set; } = null!;
            public string? UuidKetajamanPerumusanMasalah { get; private set; } = null!;
            public string? UuidInovasiPemecahanMasalah { get; private set; } = null!;
            public string? UuidSotaKebaharuan { get; private set; } = null!;
            public string? UuidRoadmapPenelitian { get; private set; } = null!;
            public string? UuidAkurasiPenelitian { get; private set; } = null!;
            public string? UuidKejelasanPembagianTugasTim { get; private set; } = null!;
            public string? UuidKesesuaianWaktuRabLuaranFasilitas { get; private set; } = null!;
            public string? UuidPotensiKetercapaianLuaranDijanjikan { get; private set; } = null!;
            public string? UuidModelFeasibilityStudy { get; private set; } = null!;
            public string? UuidKesesuaianTkt { get; private set; } = null!;
            public string? UuidKredibilitasMitraDukungan { get; private set; } = null!;
            public string? UuidKebaruanReferensi { get; private set; } = null!;
            public string? UuidRelevansiKualitasReferensi { get; private set; } = null!;
            public string? Komentar { get; private set; } = null!;
            public string? ReviewerInternal { get; private set; } = null!;
            public string? ReviewerExternal { get; private set; } = null!;
            public string TanggalMulai { get; private set; }
            public string TanggalBerakhir { get; private set; }
        }
    }
}
