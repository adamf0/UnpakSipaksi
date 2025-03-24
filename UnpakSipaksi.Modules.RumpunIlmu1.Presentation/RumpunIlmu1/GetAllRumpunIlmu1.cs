using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetAllRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Presentation;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Presentation.RumpunIlmu1
{
    internal class GetAllRumpunIlmu1
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RumpunIlmu1", async (ISender sender) =>
            {
                Result<List<RumpunIlmu1Response>> result = await sender.Send(new GetAllRumpunIlmu1Query());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RumpunIlmu1);
        }
    }
}
