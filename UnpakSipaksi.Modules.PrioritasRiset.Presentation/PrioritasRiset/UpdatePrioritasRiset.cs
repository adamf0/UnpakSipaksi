using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PrioritasRiset.Presentation;
using UnpakSipaksi.Modules.PrioritasRiset.Application.UpdatePrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Presentation.PrioritasRiset
{
    internal static class UpdatePrioritasRiset
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PrioritasRiset", async (UpdatePrioritasRisetRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdatePrioritasRisetCommand(
                    request.Id,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PrioritasRiset);
        }

        internal sealed class UpdatePrioritasRisetRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
