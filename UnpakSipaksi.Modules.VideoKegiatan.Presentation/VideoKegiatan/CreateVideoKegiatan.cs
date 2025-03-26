using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.VideoKegiatan.Application.CreateVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Presentation;

namespace UnpakSipaksi.Modules.VideoKegiatan.Presentation.VideoKegiatan
{
    internal static class CreateVideoKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("VideoKegiatan", async (CreateVideoKegiatanRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateVideoKegiatanCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.VideoKegiatan);
        }

        internal sealed class CreateVideoKegiatanRequest
        {
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
