using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisPublikasi.Application.DeleteJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Presentation.JenisPublikasi
{
    internal class DeleteJenisPublikasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("JenisPublikasi/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteJenisPublikasiCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.JenisPublikasi);
        }
    }
}
