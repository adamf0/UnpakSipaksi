using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.GetAllArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation.ArtikelMediaMassa
{
    internal class GetAllArtikelMediaMassa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("ArtikelMediaMassa", async (ISender sender) =>
            {
                Result<List<ArtikelMediaMassaResponse>> result = await sender.Send(new GetAllArtikelMediaMassaQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.ArtikelMediaMassa);
        }
    }
}
