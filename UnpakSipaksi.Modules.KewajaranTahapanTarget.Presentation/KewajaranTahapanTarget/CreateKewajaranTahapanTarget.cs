using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.CreateKewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Presentation.KewajaranTahapanTarget
{
    internal static class CreateKewajaranTahapanTarget
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KewajaranTahapanTarget", async (CreateKewajaranTahapanTargetRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKewajaranTahapanTargetCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KewajaranTahapanTarget);
        }

        internal sealed class CreateKewajaranTahapanTargetRequest
        {
            public string Nama { get; set; }

            public string Nilai { get; set; }
        }
    }
}
