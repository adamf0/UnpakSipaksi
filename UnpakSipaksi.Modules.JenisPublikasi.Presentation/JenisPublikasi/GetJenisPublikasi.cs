using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisPublikasi.Application.GetJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Presentation.JenisPublikasi
{
    internal static class GetJenisPublikasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("JenisPublikasi/{id}", async (Guid id, ISender sender) =>
            {
                Result<JenisPublikasiResponse> result = await sender.Send(new GetJenisPublikasiQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JenisPublikasi);
        }
    }
}
