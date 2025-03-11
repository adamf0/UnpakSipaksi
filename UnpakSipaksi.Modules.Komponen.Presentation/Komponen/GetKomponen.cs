using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Komponen.Application.GetKomponen;

namespace UnpakSipaksi.Modules.Komponen.Presentation.Komponen
{
    internal static class GetKomponen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Komponen/{id}", async (Guid id, ISender sender) =>
            {
                Result<KomponenResponse> result = await sender.Send(new GetKomponenQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Komponen);
        }
    }
}
