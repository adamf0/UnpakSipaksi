using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetPenelitianHibah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/{id}", async (Guid id, ISender sender) =>
            {
                Result<PenelitianHibahResponse> result = await sender.Send(new GetPenelitianHibahQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
