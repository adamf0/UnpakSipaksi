using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetAllKelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Presentation;

namespace UnpakSipaksi.Modules.KelompokMitra.Presentation.KelompokMitra
{
    internal class GetAllKelompokMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KelompokMitra", async (ISender sender) =>
            {
                Result<List<KelompokMitraResponse>> result = await sender.Send(new GetAllKelompokMitraQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KelompokMitra);
        }
    }
}
