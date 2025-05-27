using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.DeleteKuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Presentation.KuantitasStatusKi
{
    internal class DeleteKuantitasStatusKi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KuantitasStatusKi/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKuantitasStatusKiCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KuantitasStatusKi);
        }
    }
}
