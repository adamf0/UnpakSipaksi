using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisPublikasi.Application.CreateJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Presentation.JenisPublikasi
{
    internal static class CreateJenisPublikasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("JenisPublikasi", async (CreateJenisPublikasiRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateJenisPublikasiCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Sbu))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JenisPublikasi);
        }

        internal sealed class CreateJenisPublikasiRequest
        {
            public string Nama { get; set; }

            public string Sbu { get; set; }
        }
    }
}
