using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Referensi.Application.CreateReferensi;

namespace UnpakSipaksi.Modules.Referensi.Presentation.Referensi
{
    internal static class CreateReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Referensi", async (CreateReferensiRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateReferensiCommand(
                    request.Nama,
                    request.UuidKebaruanReferensi,
                    request.UuidRelevansiKualitasReferensi,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Referensi);
        }

        internal sealed class CreateReferensiRequest
        {
            public string Nama { get; set; }
            public string UuidKebaruanReferensi { get; set; }
            public string UuidRelevansiKualitasReferensi { get; set; }
            public int Nilai { get; set; }
        }
    }
}
