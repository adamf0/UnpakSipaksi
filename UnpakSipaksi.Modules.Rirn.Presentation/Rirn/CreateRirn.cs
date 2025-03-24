using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Rirn.Presentation;
using UnpakSipaksi.Modules.Rirn.Application.CreateRirn;

namespace UnpakSipaksi.Modules.Rirn.Presentation.Rirn
{
    internal static class CreateRirn
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Rirn", async (CreateRirnRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRirnCommand(
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Rirn);
        }

        internal sealed class CreateRirnRequest
        {
            public string Nama { get; set; }
            public Guid TemaPenelitianId { get; set; }
        }
    }
}
