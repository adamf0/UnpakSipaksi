using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Presentation.KesesuaianTkt
{
    internal static class GetKesesuaianTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KesesuaianTkt/{id}", async (Guid id, ISender sender) =>
            {
                Result<KesesuaianTktResponse> result = await sender.Send(new GetKesesuaianTktQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianTkt);
        }
    }
}
