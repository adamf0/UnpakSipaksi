using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Presentation;

namespace UnpakSipaksi.Modules.KategoriLuaran.Presentation.KategoriLuaran
{
    internal static class GetKategoriLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriLuaran/{uuid}", async (string uuid, ISender sender) =>
            {
                Result<KategoriLuaranResponse> result = await sender.Send(new GetKategoriLuaranQuery(uuid));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriLuaran);
        }
    }
}
