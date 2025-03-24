using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.UpdateMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation.MetodeRencanaKegiatan
{
    internal static class UpdateMetodeRencanaKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("MetodeRencanaKegiatan", async (UpdateMetodeRencanaKegiatanRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMetodeRencanaKegiatanCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.MetodeRencanaKegiatan);
        }

        internal sealed class UpdateMetodeRencanaKegiatanRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
