using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetAllKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Presentation;

namespace UnpakSipaksi.Modules.KategoriTkt.Presentation.KategoriTkt
{
    internal class GetAllKategoriTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriTkt", async (ISender sender) =>
            {
                Result<List<KategoriTktResponse>> result = await sender.Send(new GetAllKategoriTktQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriTkt);
        }
    }
}
