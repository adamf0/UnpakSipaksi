using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetAllKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Presentation.KategoriProgramPengabdian
{
    internal class GetAllKategoriProgramPengabdian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KategoriProgramPengabdian", async (ISender sender) =>
            {
                Result<List<KategoriProgramPengabdianResponse>> result = await sender.Send(new GetAllKategoriProgramPengabdianQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriProgramPengabdian);
        }
    }
}
