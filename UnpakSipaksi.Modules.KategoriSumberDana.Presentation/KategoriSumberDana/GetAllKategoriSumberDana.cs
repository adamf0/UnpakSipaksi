using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.GetKategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.GetAllKategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Presentation.KategoriSumberDana
{
    internal class GetAllKategoriSumberDana
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriSumberDana", async (ISender sender) =>
            {
                Result<List<KategoriSumberDanaResponse>> result = await sender.Send(new GetAllKategoriSumberDanaQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriSumberDana);
        }
    }
}
