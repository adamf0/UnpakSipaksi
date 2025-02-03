using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation.KejelasanPembagianTugasTim
{
    internal static class GetKejelasanPembagianTugasTim
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KejelasanPembagianTugasTim/{id}", async (Guid id, ISender sender) =>
            {
                Result<KejelasanPembagianTugasTimResponse> result = await sender.Send(new GetKejelasanPembagianTugasTimQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KejelasanPembagianTugasTim);
        }
    }
}
