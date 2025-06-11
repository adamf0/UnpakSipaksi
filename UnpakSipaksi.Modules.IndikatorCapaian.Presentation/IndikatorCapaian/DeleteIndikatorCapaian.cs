using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.DeleteIndikatorCapaian;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Presentation.IndikatorCapaian
{
    internal class DeleteIndikatorCapaian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("IndikatorCapaian/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteIndikatorCapaianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.IndikatorCapaian);
        }
    }
}
