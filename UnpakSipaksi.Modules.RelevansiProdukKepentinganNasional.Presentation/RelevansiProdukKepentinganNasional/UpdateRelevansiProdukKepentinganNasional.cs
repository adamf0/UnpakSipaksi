using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.UpdateRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation.RelevansiProdukKepentinganNasional
{
    internal static class UpdateRelevansiProdukKepentinganNasional
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RelevansiProdukKepentinganNasional", async (UpdateRelevansiProdukKepentinganNasionalRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRelevansiProdukKepentinganNasionalCommand(
                    request.Id,
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RelevansiProdukKepentinganNasional);
        }

        internal sealed class UpdateRelevansiProdukKepentinganNasionalRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
