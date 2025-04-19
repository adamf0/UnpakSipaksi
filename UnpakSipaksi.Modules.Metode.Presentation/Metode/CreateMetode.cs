using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Metode.Application.CreateMetode;

namespace UnpakSipaksi.Modules.Metode.Presentation.Metode
{
    internal static class CreateMetode
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Metode", async (CreateMetodeRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMetodeCommand(
                    request.UuidAkurasiPenelitian,
                    request.UuidKejelasanPembagianTugasTim,
                    request.UuidKesesuaianWaktuRabLuaranFasilitas,
                    request.UuidPotensiKetercapaianLuaranDijanjikan,
                    request.UuidModelFeasibilityStudy,
                    request.UuidKesesuaianTkt,
                    request.UuidKredibilitasMitraDukungan
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Metode);
        }

        internal sealed class CreateMetodeRequest
        {
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
