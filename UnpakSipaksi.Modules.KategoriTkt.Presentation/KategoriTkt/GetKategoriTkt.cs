using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Presentation;

namespace UnpakSipaksi.Modules.KategoriTkt.Presentation.KategoriTkt
{
    internal static class GetKategoriTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriTkt/{id}", async (Guid id, ISender sender) =>
            {
                Result<KategoriTktResponse> result = await sender.Send(new GetKategoriTktQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriTkt);
        }
    }
}
