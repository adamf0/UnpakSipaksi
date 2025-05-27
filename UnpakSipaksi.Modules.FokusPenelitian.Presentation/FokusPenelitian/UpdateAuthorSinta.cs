using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPenelitian.Application.UpdateFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Presentation.FokusPenelitian
{
    internal static class UpdateFokusPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("FokusPenelitian", async (UpdateFokusPenelitianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateFokusPenelitianCommand(
                    request.Id,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.FokusPenelitian);
        }

        internal sealed class UpdateFokusPenelitianRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
