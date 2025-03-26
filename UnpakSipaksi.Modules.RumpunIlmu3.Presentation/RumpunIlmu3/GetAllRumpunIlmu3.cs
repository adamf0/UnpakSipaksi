using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.GetAllRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.GetRumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Presentation.RumpunIlmu3
{
    internal class GetAllRumpunIlmu3
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RumpunIlmu3", async (ISender sender) =>
            {
                Result<List<RumpunIlmu3Response>> result = await sender.Send(new GetAllRumpunIlmu3Query());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu3);
        }
    }
}
