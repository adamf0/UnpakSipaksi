using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TopikPenelitian.Application.UpdateTopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Presentation.TopikPenelitian
{
    internal static class UpdateTopikPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("TopikPenelitian", async (UpdateTopikPenelitianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateTopikPenelitianCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.TemaPenelitianId
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.TopikPenelitian);
        }

        internal sealed class UpdateTopikPenelitianRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public Guid TemaPenelitianId { get; set; }
        }
    }
}
