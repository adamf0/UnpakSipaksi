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
            app.MapDelete("AuthorSinta/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteAuthorSintaCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.AuthorSinta);
        }
    }
}
