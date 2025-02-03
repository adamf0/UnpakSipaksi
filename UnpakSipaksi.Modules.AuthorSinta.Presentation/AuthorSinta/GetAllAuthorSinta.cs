using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AuthorSinta.Application.GetAuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Application.GetAllAuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Presentation.AuthorSinta
{
    internal class GetAllAuthorSinta
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AuthorSinta", async (ISender sender) =>
            {
                Result<List<AuthorSintaResponse>> result = await sender.Send(new GetAllAuthorSintaQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.AuthorSinta);
        }
    }
}
