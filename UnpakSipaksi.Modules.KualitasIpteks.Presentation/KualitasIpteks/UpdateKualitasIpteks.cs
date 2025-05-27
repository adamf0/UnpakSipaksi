using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasIpteks.Application.UpdateKualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Presentation.KualitasIpteks
{
    internal static class UpdateKualitasIpteks
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KualitasIpteks", async (UpdateKualitasIpteksRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKualitasIpteksCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KualitasIpteks);
        }

        internal sealed class UpdateKualitasIpteksRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
