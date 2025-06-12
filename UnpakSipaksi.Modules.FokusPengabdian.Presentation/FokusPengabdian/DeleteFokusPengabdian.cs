using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.FokusPengabdian.Application.DeleteFokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Presentation.FokusPengabdian
{
    internal class DeleteFokusPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("FokusPengabdian/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteFokusPengabdianCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.FokusPengabdian);
        }
    }
}
