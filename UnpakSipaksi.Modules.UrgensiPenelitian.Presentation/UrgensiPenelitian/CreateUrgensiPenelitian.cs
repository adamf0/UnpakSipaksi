using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.UrgensiPenelitian.Presentation;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.CreateUrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Presentation.UrgensiPenelitian
{
    internal static class CreateUrgensiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("UrgensiPenelitian", async (CreateUrgensiPenelitianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateUrgensiPenelitianCommand(
                    request.UuidRelevansiProdukKepentinganNasional,
                    request.UuidKetajamanPerumusanMasalah,
                    request.UuidInovasiPemecahanMasalah,
                    request.UuidSotaKebaharuan,
                    request.UuidRoadmapPenelitian,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.UrgensiPenelitian);
        }

        internal sealed class CreateUrgensiPenelitianRequest
        {
            public string UuidRelevansiProdukKepentinganNasional { get; set; }
            public string UuidKetajamanPerumusanMasalah { get; set; }
            public string UuidInovasiPemecahanMasalah { get; set; }
            public string UuidSotaKebaharuan { get; set; }
            public string UuidRoadmapPenelitian { get; set; }
            public int Nilai { get; set; }
        }
    }
}
