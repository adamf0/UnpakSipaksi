using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Satuan.Application.DeleteSatuan;
using UnpakSipaksi.Modules.Satuan.Presentation;

namespace UnpakSipaksi.Modules.Satuan.Presentation.Satuan
{
    internal class DeleteSatuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Satuan/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteSatuanCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Satuan);
        }
    }
}
