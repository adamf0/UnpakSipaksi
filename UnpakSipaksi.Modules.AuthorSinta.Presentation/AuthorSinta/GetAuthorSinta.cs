using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AuthorSinta.Application.GetAuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Presentation.AuthorSinta
{
    internal static class GetAuthorSinta
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AuthorSinta/{id}", async (Guid id, ISender sender) =>
            {
                Result<AuthorSintaResponse> result = await sender.Send(new GetAuthorSintaQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.AuthorSinta);
        }
    }
}
