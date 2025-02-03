using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Presentation;

namespace UnpakSipaksi.Modules.KelompokRab.Presentation.KelompokRab
{
    internal static class GetKelompokRab
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KelompokRab/{id}", async (Guid id, ISender sender) =>
            {
                Result<KelompokRabResponse> result = await sender.Send(new GetKelompokRabQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KelompokRab);
        }
    }
}
