using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Satuan.Application.UpdateSatuan;
using UnpakSipaksi.Modules.Satuan.Presentation;

namespace UnpakSipaksi.Modules.Satuan.Presentation.Satuan
{
    internal static class UpdateSatuan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Satuan", async (UpdateSatuanRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateSatuanCommand(
                    request.Id,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Satuan);
        }

        internal sealed class UpdateSatuanRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
