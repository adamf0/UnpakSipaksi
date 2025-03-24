using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Presentation.RumpunIlmu2
{
    internal static class GetRumpunIlmu2
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RumpunIlmu2/{id}", async (Guid id, ISender sender) =>
            {
                Result<RumpunIlmu2Response> result = await sender.Send(new GetRumpunIlmu2Query(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu2);
        }
    }
}
