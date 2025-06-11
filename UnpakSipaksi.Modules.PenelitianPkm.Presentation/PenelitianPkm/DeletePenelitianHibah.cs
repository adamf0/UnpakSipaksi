using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeletePenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal class DeletePenelitianPkm
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianPkm/{UuidPenelitianPkm}/{nidn}", async (string UuidPenelitianPkm, string nidn, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePenelitianPkmCommand(UuidPenelitianPkm, nidn)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
