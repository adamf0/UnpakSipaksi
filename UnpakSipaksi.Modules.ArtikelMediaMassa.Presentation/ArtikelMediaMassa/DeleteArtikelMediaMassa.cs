using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.DeleteArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation.ArtikelMediaMassa
{
    internal class DeleteArtikelMediaMassa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("ArtikelMediaMassa/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteArtikelMediaMassaCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.ArtikelMediaMassa);
        }
    }
}
