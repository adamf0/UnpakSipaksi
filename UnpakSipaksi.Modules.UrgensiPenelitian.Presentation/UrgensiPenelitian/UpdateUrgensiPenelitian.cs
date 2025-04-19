using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.UrgensiPenelitian.Presentation;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.UpdateUrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Presentation.UrgensiPenelitian
{
    internal static class UpdateUrgensiPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("UrgensiPenelitian", async (UpdateUrgensiPenelitianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateUrgensiPenelitianCommand(
                    request.Id,
                    request.UuidRelevansiProdukKepentinganNasional,
                    request.UuidKetajamanPerumusanMasalah,
                    request.UuidInovasiPemecahanMasalah,
                    request.UuidSotaKebaharuan,
                    request.UuidRoadmapPenelitian,
                    request.Nilai
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.UrgensiPenelitian);
        }

        internal sealed class UpdateUrgensiPenelitianRequest
        {
            public Guid Id { get; set; }
            public string UuidRelevansiProdukKepentinganNasional { get; set; }
            public string UuidKetajamanPerumusanMasalah { get; set; }
            public string UuidInovasiPemecahanMasalah { get; set; }
            public string UuidSotaKebaharuan { get; set; }
            public string UuidRoadmapPenelitian { get; set; }
            public int Nilai { get; set; }
        }
    }
}
