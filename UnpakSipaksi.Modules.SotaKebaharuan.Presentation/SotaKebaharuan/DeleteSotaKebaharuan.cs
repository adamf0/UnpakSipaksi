using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.DeleteSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Presentation;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Presentation.SotaKebaharuan
{
    internal class DeleteSotaKebaharuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("SotaKebaharuan/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteSotaKebaharuanCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.SotaKebaharuan);
        }
    }
}
