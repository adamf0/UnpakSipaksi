using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Substansi.Application.GetAllSubstansiInternal;
using UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Presentation.Substansi
{
    internal class GetAllSubstansi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("SubstansiInternal/{UuidPenelitianHibah}", async (Guid UuidPenelitianHibah, ISender sender) =>
            {
                Result<List<SubstansiInfoInternalResponse>> result = await sender.Send(new GetAllSubstansiInternalQuery(UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Substansi);
        }
    }
}
