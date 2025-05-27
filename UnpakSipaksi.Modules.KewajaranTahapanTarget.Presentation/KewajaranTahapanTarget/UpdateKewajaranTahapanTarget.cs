using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.UpdateKewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Presentation.KewajaranTahapanTarget
{
    internal static class UpdateKewajaranTahapanTarget
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KewajaranTahapanTarget", async (UpdateKewajaranTahapanTargetRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKewajaranTahapanTargetCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KewajaranTahapanTarget);
        }

        internal sealed class UpdateKewajaranTahapanTargetRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
