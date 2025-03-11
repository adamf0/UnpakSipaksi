using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Komponen.Application.GetAllKomponen;
using UnpakSipaksi.Modules.Komponen.Application.GetKomponen;

namespace UnpakSipaksi.Modules.Komponen.Presentation.Komponen
{
    internal class GetAllKomponen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Komponen", async (ISender sender) =>
            {
                Result<List<KomponenResponse>> result = await sender.Send(new GetAllKomponenQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Komponen);
        }
    }
}
