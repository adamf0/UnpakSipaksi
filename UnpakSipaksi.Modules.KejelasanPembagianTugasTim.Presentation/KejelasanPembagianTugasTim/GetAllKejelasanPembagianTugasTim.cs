using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetAllKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation.KejelasanPembagianTugasTim
{
    internal class GetAllKejelasanPembagianTugasTim
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KejelasanPembagianTugasTim", async (ISender sender) =>
            {
                Result<List<KejelasanPembagianTugasTimResponse>> result = await sender.Send(new GetAllKejelasanPembagianTugasTimQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KejelasanPembagianTugasTim);
        }
    }
}
