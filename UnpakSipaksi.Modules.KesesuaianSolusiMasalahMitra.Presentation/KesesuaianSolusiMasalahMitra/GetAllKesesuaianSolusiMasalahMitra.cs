using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetAllKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation.KesesuaianSolusiMasalahMitra
{
    internal class GetAllKesesuaianSolusiMasalahMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KesesuaianSolusiMasalahMitra", async (ISender sender) =>
            {
                Result<List<KesesuaianSolusiMasalahMitraResponse>> result = await sender.Send(new GetAllKesesuaianSolusiMasalahMitraQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianSolusiMasalahMitra);
        }
    }
}
