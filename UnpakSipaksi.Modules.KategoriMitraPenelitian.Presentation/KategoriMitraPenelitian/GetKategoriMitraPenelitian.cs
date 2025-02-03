using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian
{
    internal static class GetKategoriMitraPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriMitraPenelitian/{id}", async (Guid id, ISender sender) =>
            {
                Result<KategoriMitraPenelitianResponse> result = await sender.Send(new GetKategoriMitraPenelitianQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriMitraPenelitian);
        }
    }
}
