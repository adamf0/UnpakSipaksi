using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Presentation.VideoKegiatan
{
    internal static class GetVideoKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("VideoKegiatan/{id}", async (Guid id, ISender sender) =>
            {
                Result<VideoKegiatanResponse> result = await sender.Send(new GetVideoKegiatanQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.VideoKegiatan);
        }
    }
}
