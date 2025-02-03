using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisPublikasi.Application.GetJenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Application.GetAllJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Presentation.JenisPublikasi
{
    internal class GetAllJenisPublikasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("JenisPublikasi", async (ISender sender) =>
            {
                Result<List<JenisPublikasiResponse>> result = await sender.Send(new GetAllJenisPublikasiQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JenisPublikasi);
        }
    }
}
