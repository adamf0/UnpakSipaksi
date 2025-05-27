using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.DeleteKategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Presentation.KategoriSumberDana
{
    internal class DeleteKategoriSumberDana
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KategoriSumberDana/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKategoriSumberDanaCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriSumberDana);
        }
    }
}
