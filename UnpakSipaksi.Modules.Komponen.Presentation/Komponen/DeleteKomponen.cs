using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Komponen.Application.DeleteKomponen;

namespace UnpakSipaksi.Modules.Komponen.Presentation.Komponen
{
    internal class DeleteKomponen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Komponen/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKomponenCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Komponen);
        }
    }
}
