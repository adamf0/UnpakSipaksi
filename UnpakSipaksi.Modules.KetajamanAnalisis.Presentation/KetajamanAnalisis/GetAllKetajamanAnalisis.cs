using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetAllKetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Presentation.KetajamanAnalisis
{
    internal class GetAllKetajamanAnalisis
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KetajamanAnalisis", async (ISender sender) =>
            {
                Result<List<KetajamanAnalisisResponse>> result = await sender.Send(new GetAllKetajamanAnalisisQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KetajamanAnalisis);
        }
    }
}
