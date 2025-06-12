using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Rirn.Application.GetRirn;
using UnpakSipaksi.Modules.Rirn.Presentation;

namespace UnpakSipaksi.Modules.Rirn.Presentation.Rirn
{
    internal static class GetRirn
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Rirn/{Uuid}", async (Guid Uuid, ISender sender) =>
            {
                Result<RirnResponse> result = await sender.Send(new GetRirnQuery(Uuid));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Rirn);
        }
    }
}
