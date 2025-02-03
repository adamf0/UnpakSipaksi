using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPengabdian.Application.CreateFokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Presentation.FokusPengabdian
{
    internal static class CreateFokusPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("FokusPengabdian", async (CreateFokusPengabdianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateFokusPengabdianCommand(
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.FokusPengabdian);
        }

        internal sealed class CreateFokusPengabdianRequest
        {
            public string Nama { get; set; }

        }
    }
}
