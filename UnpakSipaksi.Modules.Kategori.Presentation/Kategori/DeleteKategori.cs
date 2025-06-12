using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Kategori.Application.DeleteKategori;
using UnpakSipaksi.Modules.Kategori.Presentation;

namespace UnpakSipaksi.Modules.Kategori.Presentation.Kategori
{
    internal class DeleteKategori
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("Kategori/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKategoriCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Kategori);
        }
    }
}
