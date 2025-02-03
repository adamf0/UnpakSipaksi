using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AuthorSinta.Application.UpdateAuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.Presentation.AuthorSinta
{
    internal static class UpdateAuthorSinta
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("AuthorSinta", async (UpdateAuthorSintaRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateAuthorSintaCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.NIDN),
                    HtmlEncoder.Default.Encode(request.AuthorId),
                    int.Parse(HtmlEncoder.Default.Encode(request.Score))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.AuthorSinta);
        }

        internal sealed class UpdateAuthorSintaRequest
        {
            public Guid Id { get; set; }
            public string NIDN { get; set; }
            public string AuthorId { get; set; }

            public string Score { get; set; }
        }
    }
}
