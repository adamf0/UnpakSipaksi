using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Referensi.Application.DeleteReferensi;
using UnpakSipaksi.Modules.Referensi.Presentation;

namespace UnpakSipaksi.Modules.Referensi.Presentation.Referensi
{
    internal class DeleteReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Referensi/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteReferensiCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Referensi);
        }
    }
}
