using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.UpdateKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation.KejelasanPembagianTugasTim
{
    internal static class UpdateKejelasanPembagianTugasTim
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KejelasanPembagianTugasTim", async (UpdateKejelasanPembagianTugasTimRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKejelasanPembagianTugasTimCommand(
                    request.Id,
                    request.Nama,
                    request.Skor
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KejelasanPembagianTugasTim);
        }

        internal sealed class UpdateKejelasanPembagianTugasTimRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Skor { get; set; }
        }
    }
}
