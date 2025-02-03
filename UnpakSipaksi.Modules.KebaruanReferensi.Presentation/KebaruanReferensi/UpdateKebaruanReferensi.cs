using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.UpdateKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Presentation;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Presentation.KebaruanReferensi
{
    internal static class UpdateKebaruanReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KebaruanReferensi", async (UpdateKebaruanReferensiRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKebaruanReferensiCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Skor))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KebaruanReferensi);
        }

        internal sealed class UpdateKebaruanReferensiRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string Skor { get; set; }
        }
    }
}
