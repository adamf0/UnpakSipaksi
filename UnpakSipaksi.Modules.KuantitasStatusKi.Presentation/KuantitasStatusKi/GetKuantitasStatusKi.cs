using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Presentation.KuantitasStatusKi
{
    internal static class GetKuantitasStatusKi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KuantitasStatusKi/{id}", async (Guid id, ISender sender) =>
            {
                Result<KuantitasStatusKiResponse> result = await sender.Send(new GetKuantitasStatusKiQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KuantitasStatusKi);
        }
    }
}
