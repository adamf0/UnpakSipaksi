using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.CreateRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation.RelevansiProdukKepentinganNasional
{
    internal static class CreateRelevansiProdukKepentinganNasional
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RelevansiProdukKepentinganNasional", async (CreateRelevansiProdukKepentinganNasionalRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRelevansiProdukKepentinganNasionalCommand(
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RelevansiProdukKepentinganNasional);
        }

        internal sealed class CreateRelevansiProdukKepentinganNasionalRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
