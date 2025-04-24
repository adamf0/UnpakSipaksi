using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllPenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal class GetAllPenelitianHibah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah", async (ISender sender) =>
            {
                Result<List<PenelitianHibahResponse>> result = await sender.Send(new GetAllPenelitianHibahQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
