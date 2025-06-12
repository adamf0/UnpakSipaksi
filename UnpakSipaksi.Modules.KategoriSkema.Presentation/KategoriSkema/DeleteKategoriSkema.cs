using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSkema.Presentation;
using UnpakSipaksi.Modules.KategoriSkema.Application.DeleteKategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Presentation.KategoriSkema
{
    internal class DeleteKategoriSkema
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KategoriSkema/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKategoriSkemaCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriSkema);
        }
    }
}
