using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Referensi.Application.GetAllReferensi;
using UnpakSipaksi.Modules.Referensi.Application.GetReferensi;
using UnpakSipaksi.Modules.Referensi.Presentation;

namespace UnpakSipaksi.Modules.Referensi.Presentation.Referensi
{
    internal class GetAllReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Referensi", async (ISender sender) =>
            {
                Result<List<ReferensiResponse>> result = await sender.Send(new GetAllReferensiQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Referensi);
        }
    }
}
