using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.UpdateKualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Presentation.KualitasKuantitasPublikasiJurnalIlmiah
{
    internal static class UpdateKualitasKuantitasPublikasiJurnalIlmiah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KualitasKuantitasPublikasiJurnalIlmiah", async (UpdateKualitasKuantitasPublikasiJurnalIlmiahRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKualitasKuantitasPublikasiJurnalIlmiahCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiJurnalIlmiah);
        }

        internal sealed class UpdateKualitasKuantitasPublikasiJurnalIlmiahRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
