using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisLuaran.Application.DeleteJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Presentation;

namespace UnpakSipaksi.Modules.JenisLuaran.Presentation.JenisLuaran
{
    internal class DeleteJenisLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("JenisLuaran/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteJenisLuaranCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.JenisLuaran);
        }
    }
}
