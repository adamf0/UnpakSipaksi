using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetAllKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation.KesesuaianPenugasan
{
    internal class GetAllKesesuaianPenugasan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KesesuaianPenugasan", async (ISender sender) =>
            {
                Result<List<KesesuaianPenugasanResponse>> result = await sender.Send(new GetAllKesesuaianPenugasanQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianPenugasan);
        }
    }
}
