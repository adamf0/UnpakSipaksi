using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokMitra.Presentation;
using UnpakSipaksi.Modules.KelompokMitra.Application.GetKelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.Presentation.KelompokMitra
{
    internal static class GetKelompokMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KelompokMitra/{id}", async (Guid id, ISender sender) =>
            {
                Result<KelompokMitraResponse> result = await sender.Send(new GetKelompokMitraQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KelompokMitra);
        }
    }
}
