using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetAllPeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation.PeningkatanKeberdayaanMitra
{
    internal class GetAllPeningkatanKeberdayaanMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PeningkatanKeberdayaanMitra", async (ISender sender) =>
            {
                Result<List<PeningkatanKeberdayaanMitraResponse>> result = await sender.Send(new GetAllPeningkatanKeberdayaanMitraQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PeningkatanKeberdayaanMitra);
        }
    }
}
