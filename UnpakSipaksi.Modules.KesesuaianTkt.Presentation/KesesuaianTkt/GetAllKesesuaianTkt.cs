using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.GetKesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.GetAllKesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Presentation.KesesuaianTkt
{
    internal class GetAllKesesuaianTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KesesuaianTkt", async (ISender sender) =>
            {
                Result<List<KesesuaianTktResponse>> result = await sender.Send(new GetAllKesesuaianTktQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianTkt);
        }
    }
}
