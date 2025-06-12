using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPengabdian.Application.UpdateFokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Presentation.FokusPengabdian
{
    internal static class UpdateFokusPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("FokusPengabdian", async (UpdateFokusPengabdianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateFokusPengabdianCommand(
                    request.Uuid,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.FokusPengabdian);
        }

        internal sealed class UpdateFokusPengabdianRequest
        {
            public string Uuid { get; set; }
            public string Nama { get; set; }
        }
    }
}
