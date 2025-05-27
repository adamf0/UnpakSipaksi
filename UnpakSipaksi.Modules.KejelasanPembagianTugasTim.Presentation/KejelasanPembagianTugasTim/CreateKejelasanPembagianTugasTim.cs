using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.CreateKejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation.KejelasanPembagianTugasTim
{
    internal static class CreateKejelasanPembagianTugasTim
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KejelasanPembagianTugasTim", async (CreateKejelasanPembagianTugasTimRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKejelasanPembagianTugasTimCommand(
                    request.Nama,
                    request.Skor
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KejelasanPembagianTugasTim);
        }

        internal sealed class CreateKejelasanPembagianTugasTimRequest
        {
            public string Nama { get; set; }
            public int Skor { get; set; }
        }
    }
}
