using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Insentif.Application.GetAllInsentif;
using UnpakSipaksi.Modules.Insentif.Application.GetInsentif;

namespace UnpakSipaksi.Modules.Insentif.Presentation.Insentif
{
    internal class GetAllInsentif
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Insentif", async (ISender sender) =>
            {
                Result<List<InsentifResponse>> result = await sender.Send(new GetAllInsentifQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Insentif);
        }
    }
}
