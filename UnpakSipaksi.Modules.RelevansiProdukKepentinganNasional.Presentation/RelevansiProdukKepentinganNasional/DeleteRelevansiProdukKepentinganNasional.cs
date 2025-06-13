using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.DeleteRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation.RelevansiProdukKepentinganNasional
{
    internal class DeleteRelevansiProdukKepentinganNasional
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RelevansiProdukKepentinganNasional/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRelevansiProdukKepentinganNasionalCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RelevansiProdukKepentinganNasional);
        }
    }
}
