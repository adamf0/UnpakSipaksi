using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation.KategoriProgramPengabdian
{
    internal static class GetKategoriProgramPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriProgramPengabdian/{id}", async (Guid id, ISender sender) =>
            {
                Result<KategoriProgramPengabdianResponse> result = await sender.Send(new GetKategoriProgramPengabdianQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriProgramPengabdian);
        }
    }
}
