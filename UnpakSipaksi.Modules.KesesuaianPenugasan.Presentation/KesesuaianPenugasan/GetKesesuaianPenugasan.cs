using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetKesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation.KesesuaianPenugasan
{
    internal static class GetKesesuaianPenugasan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KesesuaianPenugasan/{id}", async (Guid id, ISender sender) =>
            {
                Result<KesesuaianPenugasanResponse> result = await sender.Send(new GetKesesuaianPenugasanQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianPenugasan);
        }
    }
}
