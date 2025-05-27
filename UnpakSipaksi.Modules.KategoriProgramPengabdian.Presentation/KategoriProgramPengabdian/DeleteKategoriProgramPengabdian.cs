using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.DeleteKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation.KategoriProgramPengabdian
{
    internal class DeleteKategoriProgramPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KategoriProgramPengabdian/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKategoriProgramPengabdianCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriProgramPengabdian);
        }
    }
}
