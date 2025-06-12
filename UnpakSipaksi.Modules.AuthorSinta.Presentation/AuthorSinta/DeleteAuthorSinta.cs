using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AuthorSinta.Application.DeleteAuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Presentation.AuthorSinta
{
    internal class DeleteAuthorSinta
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("AuthorSinta/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteAuthorSintaCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.AuthorSinta);
        }
    }
}
