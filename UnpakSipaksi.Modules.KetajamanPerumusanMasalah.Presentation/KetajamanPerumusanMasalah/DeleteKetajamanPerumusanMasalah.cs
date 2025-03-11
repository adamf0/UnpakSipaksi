using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.DeleteKetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Presentation.KetajamanPerumusanMasalah
{
    internal class DeleteKetajamanPerumusanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KetajamanPerumusanMasalah/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKetajamanPerumusanMasalahCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KetajamanPerumusanMasalah);
        }
    }
}
