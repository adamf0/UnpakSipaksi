using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.DeleteRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation.RelevansiKualitasReferensi
{
    internal class DeleteRelevansiKualitasReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RelevansiKualitasReferensi/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRelevansiKualitasReferensiCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RelevansiKualitasReferensi);
        }
    }
}
