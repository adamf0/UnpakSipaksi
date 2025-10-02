using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Pengumuman.Presentation;
using UnpakSipaksi.Modules.Pengumuman.Application.DeletePengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Presentation.Pengumuman
{
    internal class DeletePengumuman
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Pengumuman/{uuid}", async (string uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePengumumanCommand(uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Pengumuman);
        }
    }
}
