using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman;
using UnpakSipaksi.Modules.Pengumuman.Presentation;
using UnpakSipaksi.Modules.Pengumuman.Application.GetAllPengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Presentation.Pengumuman
{
    internal class GetAllPengumuman
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Pengumuman", async (ISender sender) =>
            {
                Result<List<PengumumanResponse>> result = await sender.Send(new GetAllPengumumanQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Pengumuman);
        }
    }
}
