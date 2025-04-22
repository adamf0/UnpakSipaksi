using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Insentif.Application.GetInsentif;

namespace UnpakSipaksi.Modules.Insentif.Presentation.Insentif
{
    internal static class GetInsentif
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Insentif/{id}", async (Guid id, ISender sender) =>
            {
                Result<InsentifResponse> result = await sender.Send(new GetInsentifQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Insentif);
        }
    }
}
