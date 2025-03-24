using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PrioritasRiset.Application.CreatePrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Presentation;

namespace UnpakSipaksi.Modules.PrioritasRiset.Presentation.PrioritasRiset
{
    internal static class CreatePrioritasRiset
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PrioritasRiset", async (CreatePrioritasRisetRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreatePrioritasRisetCommand(
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PrioritasRiset);
        }

        internal sealed class CreatePrioritasRisetRequest
        {
            public string Nama { get; set; }
            public Guid TemaPenelitianId { get; set; }
        }
    }
}
