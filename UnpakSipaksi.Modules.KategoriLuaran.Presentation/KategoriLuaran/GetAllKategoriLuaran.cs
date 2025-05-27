using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetAllKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Application.GetKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Presentation;

namespace UnpakSipaksi.Modules.KategoriLuaran.Presentation.KategoriLuaran
{
    internal class GetAllKategoriLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriLuaran", async (ISender sender) =>
            {
                Result<List<KategoriLuaranResponse>> result = await sender.Send(new GetAllKategoriLuaranQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriLuaran);
        }
    }
}
