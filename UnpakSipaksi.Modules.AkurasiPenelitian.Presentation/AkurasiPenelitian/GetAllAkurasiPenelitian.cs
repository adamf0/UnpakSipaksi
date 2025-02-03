using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAkurasiPenelitian;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.GetAllAkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Presentation.AkurasiPenelitian
{
    internal class GetAllAkurasiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("AkurasiPenelitian", async (ISender sender) =>
            {
                Result<List<AkurasiPenelitianResponse>> result = await sender.Send(new GetAllAkurasiPenelitianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.AkurasiPenelitian);
        }
    }
}
