using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.CreateInovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Presentation.InovasiPemecahanMasalah
{
    internal static class CreateInovasiPemecahanMasalah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("InovasiPemecahanMasalah", async (CreateInovasiPemecahanMasalahRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateInovasiPemecahanMasalahCommand(
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.InovasiPemecahanMasalah);
        }

        internal sealed class CreateInovasiPemecahanMasalahRequest
        {
            public string Nama { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
