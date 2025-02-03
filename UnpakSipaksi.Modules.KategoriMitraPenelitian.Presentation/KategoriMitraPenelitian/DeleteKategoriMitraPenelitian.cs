using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.DeleteKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian
{
    internal class DeleteKategoriMitraPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KategoriMitraPenelitian/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKategoriMitraPenelitianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriMitraPenelitian);
        }
    }
}
