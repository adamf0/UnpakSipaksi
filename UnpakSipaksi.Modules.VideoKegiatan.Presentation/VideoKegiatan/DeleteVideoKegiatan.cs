using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.VideoKegiatan.Application.DeleteVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Presentation;

namespace UnpakSipaksi.Modules.VideoKegiatan.Presentation.VideoKegiatan
{
    internal class DeleteVideoKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("VideoKegiatan/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteVideoKegiatanCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.VideoKegiatan);
        }
    }
}
