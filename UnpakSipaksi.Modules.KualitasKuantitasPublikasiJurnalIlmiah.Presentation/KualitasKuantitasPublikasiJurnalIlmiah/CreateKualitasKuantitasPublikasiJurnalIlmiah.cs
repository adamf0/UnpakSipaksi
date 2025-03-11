using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.CreateKualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Presentation.KualitasKuantitasPublikasiJurnalIlmiah
{
    internal static class CreateKualitasKuantitasPublikasiJurnalIlmiah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KualitasKuantitasPublikasiJurnalIlmiah", async (CreateKualitasKuantitasPublikasiJurnalIlmiahRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKualitasKuantitasPublikasiJurnalIlmiahCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KualitasKuantitasPublikasiJurnalIlmiah);
        }

        internal sealed class CreateKualitasKuantitasPublikasiJurnalIlmiahRequest
        {
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
