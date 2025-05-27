using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPenelitian.Application.CreateFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Presentation.FokusPenelitian
{
    internal static class CreateFokusPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("FokusPenelitian", async (CreateFokusPenelitianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateFokusPenelitianCommand(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.FokusPenelitian);
        }

        internal sealed class CreateFokusPenelitianRequest
        {
            public string Nama { get; set; }
        }
    }
}
