using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokRab.Application.DeleteKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Presentation;

namespace UnpakSipaksi.Modules.KelompokRab.Presentation.KelompokRab
{
    internal class DeleteKelompokRab
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KelompokRab/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKelompokRabCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KelompokRab);
        }
    }
}
