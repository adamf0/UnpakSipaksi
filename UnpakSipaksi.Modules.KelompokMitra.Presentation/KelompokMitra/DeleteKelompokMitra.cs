using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokMitra.Presentation;
using UnpakSipaksi.Modules.KelompokMitra.Application.DeleteKelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Presentation.KelompokMitra
{
    internal class DeleteKelompokMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KelompokMitra/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKelompokMitraCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KelompokMitra);
        }
    }
}
