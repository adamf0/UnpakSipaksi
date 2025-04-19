using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.CreateRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation.RelevansiKualitasReferensi
{
    internal static class CreateRelevansiKualitasReferensil
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RelevansiKualitasReferensi", async (CreateRelevansiKualitasReferensiRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRelevansiKualitasReferensiCommand(
                    request.Nama,
                    int.Parse(request.BobotSkor)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RelevansiKualitasReferensi);
        }

        internal sealed class CreateRelevansiKualitasReferensiRequest
        {
            public string Nama { get; set; }

            public string BobotSkor { get; set; }
        }
    }
}
