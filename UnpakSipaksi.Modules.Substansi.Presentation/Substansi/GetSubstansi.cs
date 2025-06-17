using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Presentation.Substansi
{
    internal static class GetSubstansi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("SubstansiInternal/{Uuid}/{UuidPenelitianHibah}", async (Guid Uuid, Guid UuidPenelitianHibah, ISender sender) =>
            {
                Result<SubstansiInternalResponse> result = await sender.Send(new GetSubstansiInternalQuery(Uuid, UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Substansi);
        }
    }
}
