using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Kategori.Application.GetAllKategori;
using UnpakSipaksi.Modules.Kategori.Application.GetKategori;

namespace UnpakSipaksi.Modules.Kategori.Presentation.Kategori
{
    internal class GetAllKategori
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Kategori", async (ISender sender) =>
            {
                Result<List<KategoriResponse>> result = await sender.Send(new GetAllKategoriQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Kategori);
        }
    }
}
