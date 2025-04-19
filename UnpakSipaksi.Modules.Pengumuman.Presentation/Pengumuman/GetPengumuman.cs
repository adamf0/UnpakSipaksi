using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman;
using UnpakSipaksi.Modules.Pengumuman.Presentation;

namespace UnpakSipaksi.Modules.Pengumuman.Presentation.Pengumuman
{
    internal static class GetPengumuman
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Pengumuman/{id}", async (Guid id, ISender sender) =>
            {
                Result<PengumumanResponse> result = await sender.Send(new GetPengumumanQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Pengumuman);
        }
    }
}
