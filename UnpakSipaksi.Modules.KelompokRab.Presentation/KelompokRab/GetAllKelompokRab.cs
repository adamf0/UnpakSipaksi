using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KelompokRab.Application.GetKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.GetAllKelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Presentation;

namespace UnpakSipaksi.Modules.KelompokRab.Presentation.KelompokRab
{
    internal class GetAllKelompokRab
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KelompokRab", async (ISender sender) =>
            {
                Result<List<KelompokRabResponse>> result = await sender.Send(new GetAllKelompokRabQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KelompokRab);
        }
    }
}
