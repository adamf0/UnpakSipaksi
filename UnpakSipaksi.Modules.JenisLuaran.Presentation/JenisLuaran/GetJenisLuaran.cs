using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Presentation;

namespace UnpakSipaksi.Modules.JenisLuaran.Presentation.JenisLuaran
{
    internal static class GetJenisLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("JenisLuaran/{id}", async (Guid id, ISender sender) =>
            {
                Result<JenisLuaranResponse> result = await sender.Send(new GetJenisLuaranQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JenisLuaran);
        }
    }
}
