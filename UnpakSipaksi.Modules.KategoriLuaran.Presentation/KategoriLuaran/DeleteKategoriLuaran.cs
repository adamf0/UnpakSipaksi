using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriLuaran.Application.DeleteKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Presentation;

namespace UnpakSipaksi.Modules.KategoriLuaran.Presentation.KategoriLuaran
{
    internal class DeleteKategoriLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KategoriLuaran/{uuid}", async (string uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKategoriLuaranCommand(uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriLuaran);
        }
    }
}
