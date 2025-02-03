using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TemaPenelitian.Application.GetTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Application.GetAllTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Presentation;

namespace UnpakSipaksi.Modules.TemaPenelitian.Presentation.TemaPenelitian
{
    internal class GetAllTemaPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("TemaPenelitian", async (ISender sender) =>
            {
                Result<List<TemaPenelitianResponse>> result = await sender.Send(new GetAllTemaPenelitianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.TemaPenelitian);
        }
    }
}
