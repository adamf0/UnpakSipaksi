using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Kategori.Application.GetKategori;

namespace UnpakSipaksi.Modules.Kategori.Presentation.Kategori
{
    internal static class GetKategori
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Kategori/{id}", async (string Uuid, ISender sender) =>
            {
                Result<KategoriResponse> result = await sender.Send(new GetKategoriQuery(Uuid));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Kategori);
        }
    }
}
