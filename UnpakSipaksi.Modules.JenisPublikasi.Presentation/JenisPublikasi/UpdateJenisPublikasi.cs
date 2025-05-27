using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisPublikasi.Application.UpdateJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Presentation.JenisPublikasi
{
    internal static class UpdateJenisPublikasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("JenisPublikasi", async (UpdateJenisPublikasiRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateJenisPublikasiCommand(
                    request.Id,
                    request.Nama,
                    request.Sbu
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.JenisPublikasi);
        }

        internal sealed class UpdateJenisPublikasiRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Sbu { get; set; }
        }
    }
}
