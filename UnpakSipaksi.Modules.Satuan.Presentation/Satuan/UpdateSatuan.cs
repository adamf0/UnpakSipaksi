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
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Satuan);
        }

        internal sealed class UpdateSatuanRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public Guid TemaPenelitianId { get; set; }
        }
    }
}
