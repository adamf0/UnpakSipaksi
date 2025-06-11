using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteSubstansiUsulan;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal class DeleteSubstansiUsulan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianPkm/SubstansiUsulan/{Uuid}/{UuidPenelitianPkm}", async (string Uuid, string UuidPenelitianPkm, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteSubstansiUsulanCommand(Uuid, UuidPenelitianPkm)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
