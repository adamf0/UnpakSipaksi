using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Metode.Presentation;
using UnpakSipaksi.Modules.Metode.Application.UpdateMetode;

namespace UnpakSipaksi.Modules.Metode.Presentation.Metode
{
    internal static class UpdateMetode
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Metode", async (UpdateMetodeRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMetodeCommand(
                    request.Id,
                    request.UuidAkurasiPenelitian,
                    request.UuidKejelasanPembagianTugasTim,
                    request.UuidKesesuaianWaktuRabLuaranFasilitas,
                    request.UuidPotensiKetercapaianLuaranDijanjikan,
                    request.UuidModelFeasibilityStudy,
                    request.UuidKesesuaianTkt,
                    request.UuidKredibilitasMitraDukungan
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Metode);
        }

        internal sealed class UpdateMetodeRequest
        {
            public string Id { get; set; }
            public string UuidAkurasiPenelitian { get; private set; }
            public string UuidKejelasanPembagianTugasTim { get; private set; }
            public string UuidKesesuaianWaktuRabLuaranFasilitas { get; private set; }
            public string UuidPotensiKetercapaianLuaranDijanjikan { get; private set; }
            public string UuidModelFeasibilityStudy { get; private set; }
            public string UuidKesesuaianTkt { get; private set; }
            public string UuidKredibilitasMitraDukungan { get; private set; }
        }
    }
}
