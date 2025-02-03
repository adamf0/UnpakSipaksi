using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.GetKategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Presentation.KategoriSumberDana
{
    internal static class GetKategoriSumberDana
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriSumberDana/{id}", async (Guid id, ISender sender) =>
            {
                Result<KategoriSumberDanaResponse> result = await sender.Send(new GetKategoriSumberDanaQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriSumberDana);
        }
    }
}
