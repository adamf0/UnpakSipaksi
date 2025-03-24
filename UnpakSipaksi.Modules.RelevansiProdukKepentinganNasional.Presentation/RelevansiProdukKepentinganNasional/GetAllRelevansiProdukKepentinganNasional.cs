using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetAllRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation.RelevansiProdukKepentinganNasional
{
    internal class GetAllRelevansiProdukKepentinganNasional
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RelevansiProdukKepentinganNasional", async (ISender sender) =>
            {
                Result<List<RelevansiProdukKepentinganNasionalResponse>> result = await sender.Send(new GetAllRelevansiProdukKepentinganNasionalQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RelevansiProdukKepentinganNasional);
        }
    }
}
