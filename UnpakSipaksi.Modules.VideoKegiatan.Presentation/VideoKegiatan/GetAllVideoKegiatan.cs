using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetAllVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Presentation;

namespace UnpakSipaksi.Modules.VideoKegiatan.Presentation.VideoKegiatan
{
    internal class GetAllVideoKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("VideoKegiatan", async (ISender sender) =>
            {
                Result<List<VideoKegiatanResponse>> result = await sender.Send(new GetAllVideoKegiatanQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.VideoKegiatan);
        }
    }
}
