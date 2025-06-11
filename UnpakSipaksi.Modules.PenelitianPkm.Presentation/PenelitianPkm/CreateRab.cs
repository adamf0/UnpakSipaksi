using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateRAB;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class CreateRAB
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianPkm/RAB", async (CreateRABPkmRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRABCommand(
                  request.UuidPenelitianPkm,
                  request.UuidKelompokRab,
                  request.UuidKomponen,
                  request.Item,
                  request.UuidSatuan,
                  request.HargaSatuan,
                  request.Total
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class CreateRABPkmRequest
        {
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
