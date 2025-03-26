using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Presentation;

namespace UnpakSipaksi.Modules.KategoriSkema.Presentation.KategoriSkema
{
    internal static class GetKategoriSkema
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriSkema/{id}", async (Guid id, ISender sender) =>
            {
                Result<KategoriSkemaResponse> result = await sender.Send(new GetKategoriSkemaQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriSkema);
        }
    }
}
