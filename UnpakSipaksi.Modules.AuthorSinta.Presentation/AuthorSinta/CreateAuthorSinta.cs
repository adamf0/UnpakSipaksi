using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AuthorSinta.Application.CreateAuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Presentation.AuthorSinta
{
    internal static class CreateAuthorSinta
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("AuthorSinta", async (CreateAuthorSintaRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateAuthorSintaCommand(
                    HtmlEncoder.Default.Encode(request.NIDN),
                    HtmlEncoder.Default.Encode(request.AuthorId),
                    int.Parse(HtmlEncoder.Default.Encode(request.Score))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.AuthorSinta);
        }

        internal sealed class CreateAuthorSintaRequest
        {
            public string NIDN { get; set; }
            public string AuthorId { get; set; }

            public string Score { get; set; }
        }
    }
}
