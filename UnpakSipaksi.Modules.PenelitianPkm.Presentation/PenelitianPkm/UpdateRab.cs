using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateRAB;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateRAB
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/RAB", async (UpdateRABPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRABCommand(
                  request.Uuid,
                  request.UuidPenelitianPkm,
                  request.UuidKelompokRab,
                  request.UuidKomponen,
                  request.Item,
                  request.UuidSatuan,
                  request.HargaSatuan,
                  request.Total
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateRABPkmRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string? UuidKelompokRab { get; set; }
            public string? UuidKomponen { get; set; }
            public int? Item { get; set; }
            public string? UuidSatuan { get; set; }
            public int? HargaSatuan { get; set; }
            public int? Total { get; set; }
        }
    }
}
