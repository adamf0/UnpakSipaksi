using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Presentation;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetAllKategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Presentation.KategoriSkema
{
    internal class GetAllKategoriSkema
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriSkema", async (ISender sender) =>
            {
                Result<List<KategoriSkemaResponse>> result = await sender.Send(new GetAllKategoriSkemaQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriSkema);
        }
    }
}
