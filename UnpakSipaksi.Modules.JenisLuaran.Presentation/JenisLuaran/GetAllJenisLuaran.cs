using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisLuaran.Application.GetAllJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Presentation;

namespace UnpakSipaksi.Modules.JenisLuaran.Presentation.JenisLuaran
{
    internal class GetAllJenisLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("JenisLuaran", async (ISender sender) =>
            {
                Result<List<JenisLuaranResponse>> result = await sender.Send(new GetAllJenisLuaranQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JenisLuaran);
        }
    }
}
