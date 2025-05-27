using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Referensi.Application.UpdateReferensi;

namespace UnpakSipaksi.Modules.Referensi.Presentation.Referensi
{
    internal static class UpdateReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Referensi", async (UpdateReferensiRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateReferensiCommand(
                    request.Id,
                    request.Nama,
                    request.UuidKebaruanReferensi,
                    request.UuidRelevansiKualitasReferensi,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Referensi);
        }

        internal sealed class UpdateReferensiRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public string UuidKebaruanReferensi { get; set; }
            public string UuidRelevansiKualitasReferensi { get; set; }
            public int Nilai { get; set; }
        }
    }
}
