using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetRab;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class GetRab
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianPkm/Rab/{Uuid}/{UuidPenelitianPkm}", async (string Uuid, string UuidPenelitianPkm, ISender sender) =>
            {
                Result<RabResponse> result = await sender.Send(new GetRabQuery(Uuid, UuidPenelitianPkm));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
