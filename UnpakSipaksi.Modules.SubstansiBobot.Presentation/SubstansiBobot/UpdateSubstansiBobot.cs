using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
//using UnpakSipaksi.Modules.SubstansiBobot.Application.UpdateSubstansiBobot;

namespace UnpakSipaksi.Modules.SubstansiBobot.Presentation.SubstansiBobot
{
    internal static class UpdateSubstansiBobot
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("SubstansiBobot", async (UpdateSubstansiBobotRequest request, ISender sender) =>
            {
                /*Result result = await sender.Send(new UpdateSubstansiBobotCommand(
                    request.KategoriSkema,
                    request.KualitasKuantitasPublikasiJurnalIlmiah,
                    request.KualitasKuantitasPublikasiProsiding,
                    request.KuantitasStatusKI,
                    request.KetajamanAnalisis,
                    request.RumusanPrioritasMitra,
                    request.KesesuaianSolusiMasalahMitra,
                    request.MetodeRencanaKegiatan,
                    request.KesesuaianPenugasan,
                    request.KualitasIpteks,
                    request.LuaranArtikel,
                    request.ArtikelMediaMassa,
                    request.VideoKegiatan,
                    request.PeningkatanKeberdayaanMitra,
                    request.KewajaranTahapanTarget,
                    request.KesesuaianJadwal
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
                */
                return Results.Ok();
            }).WithTags(Tags.SubstansiBobot);
        }

        internal sealed class UpdateSubstansiBobotRequest
        {
            public string KategoriSkema { get; set; }
            public int KualitasKuantitasPublikasiJurnalIlmiah { get; set; }
            public int KualitasKuantitasPublikasiProsiding { get; set; }
            public int KuantitasStatusKI { get; set; }
            public int KetajamanAnalisis { get; set; }
            public int RumusanPrioritasMitra { get; set; }
            public int KesesuaianSolusiMasalahMitra { get; set; }
            public int MetodeRencanaKegiatan { get; set; }
            public int KesesuaianPenugasan { get; set; }
            public int KualitasIpteks { get; set; }
            public int LuaranArtikel { get; set; }
            public int ArtikelMediaMassa { get; set; }
            public int VideoKegiatan { get; set; }
            public int PeningkatanKeberdayaanMitra { get; set; }
            public int KewajaranTahapanTarget { get; set; }
            public int KesesuaianJadwal { get; set; }
        }
    }
}
