using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasIpteks.Application.CreateKualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Presentation.KualitasIpteks
{
    internal static class CreateKualitasIpteks
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KualitasIpteks", async (CreateKualitasIpteksRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKualitasIpteksCommand(
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasIpteks);
        }

        internal sealed class CreateKualitasIpteksRequest
        {
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
