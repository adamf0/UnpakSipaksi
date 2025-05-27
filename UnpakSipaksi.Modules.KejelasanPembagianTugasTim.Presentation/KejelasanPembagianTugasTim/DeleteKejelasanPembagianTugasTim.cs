using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.DeleteKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation.KejelasanPembagianTugasTim
{
    internal class DeleteKejelasanPembagianTugasTim
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KejelasanPembagianTugasTim/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKejelasanPembagianTugasTimCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KejelasanPembagianTugasTim);
        }
    }
}
