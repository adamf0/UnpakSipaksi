using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TemaPenelitian.Application.UpdateTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Presentation;

namespace UnpakSipaksi.Modules.TemaPenelitian.Presentation.TemaPenelitian
{
    internal static class UpdateTemaPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("TemaPenelitian", async (UpdateTemaPenelitianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateTemaPenelitianCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    request.FokusPenelitianId
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.TemaPenelitian);
        }

        internal sealed class UpdateTemaPenelitianRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public Guid FokusPenelitianId { get; set; }
        }
    }
}
