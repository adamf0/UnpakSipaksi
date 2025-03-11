using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetAllKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Presentation;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Presentation.KewajaranTahapanTarget
{
    internal class GetAllKewajaranTahapanTarget
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KewajaranTahapanTarget", async (ISender sender) =>
            {
                Result<List<KewajaranTahapanTargetResponse>> result = await sender.Send(new GetAllKewajaranTahapanTargetQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KewajaranTahapanTarget);
        }
    }
}
