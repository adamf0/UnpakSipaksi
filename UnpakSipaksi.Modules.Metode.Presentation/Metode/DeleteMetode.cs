using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Metode.Presentation;
using UnpakSipaksi.Modules.Metode.Application.DeleteMetode;

namespace UnpakSipaksi.Modules.Metode.Presentation.Metode
{
    internal class DeleteMetode
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Metode/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteMetodeCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Metode);
        }
    }
}
