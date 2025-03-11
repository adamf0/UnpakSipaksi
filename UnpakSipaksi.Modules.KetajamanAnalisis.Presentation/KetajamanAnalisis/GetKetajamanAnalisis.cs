using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Presentation.KetajamanAnalisis
{
    internal static class GetKetajamanAnalisis
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KetajamanAnalisis/{id}", async (Guid id, ISender sender) =>
            {
                Result<KetajamanAnalisisResponse> result = await sender.Send(new GetKetajamanAnalisisQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KetajamanAnalisis);
        }
    }
}
