using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Rirn.Application.GetAllRirn;
using UnpakSipaksi.Modules.Rirn.Application.GetRirn;
using UnpakSipaksi.Modules.Rirn.Presentation;

namespace UnpakSipaksi.Modules.Rirn.Presentation.Rirn
{
    internal class GetAllRirn
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Rirn", async (ISender sender) =>
            {
                Result<List<RirnResponse>> result = await sender.Send(new GetAllRirnQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Rirn);
        }
    }
}
