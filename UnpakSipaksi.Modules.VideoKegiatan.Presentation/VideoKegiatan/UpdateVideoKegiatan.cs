using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.VideoKegiatan.Application.UpdateVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Presentation;

namespace UnpakSipaksi.Modules.VideoKegiatan.Presentation.VideoKegiatan
{
    internal static class UpdateVideoKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("VideoKegiatan", async (UpdateVideoKegiatanRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateVideoKegiatanCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.VideoKegiatan);
        }

        internal sealed class UpdateVideoKegiatanRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
