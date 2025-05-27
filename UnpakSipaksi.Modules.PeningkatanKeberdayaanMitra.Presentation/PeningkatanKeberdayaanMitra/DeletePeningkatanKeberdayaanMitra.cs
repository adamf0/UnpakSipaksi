using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.DeletePeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation.PeningkatanKeberdayaanMitra
{
    internal class DeletePeningkatanKeberdayaanMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PeningkatanKeberdayaanMitra/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePeningkatanKeberdayaanMitraCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PeningkatanKeberdayaanMitra);
        }
    }
}
