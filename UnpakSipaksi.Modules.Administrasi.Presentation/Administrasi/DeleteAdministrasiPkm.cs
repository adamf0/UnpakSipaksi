using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Administrasi.Application.DeleteAdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Presentation.AdministrasiPkm
{
    internal class DeleteAdministrasiPkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("AdministrasiPkm/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteAdministrasiPkmCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Administrasi);
        }
    }
}
