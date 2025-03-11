using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetAllKuantitasStatusKi;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Presentation.KuantitasStatusKi
{
    internal class GetAllKuantitasStatusKi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KuantitasStatusKi", async (ISender sender) =>
            {
                Result<List<KuantitasStatusKiResponse>> result = await sender.Send(new GetAllKuantitasStatusKiQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KuantitasStatusKi);
        }
    }
}
