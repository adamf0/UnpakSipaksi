using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Presentation.RumpunIlmu1
{
    internal static class GetRumpunIlmu1
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RumpunIlmu1/{id}", async (Guid id, ISender sender) =>
            {
                Result<RumpunIlmu1Response> result = await sender.Send(new GetRumpunIlmu1Query(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu1);
        }
    }
}
