using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.DeleteKetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Presentation.KetajamanAnalisis
{
    internal class DeleteKetajamanAnalisis
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KetajamanAnalisis/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKetajamanAnalisisCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KetajamanAnalisis);
        }
    }
}
