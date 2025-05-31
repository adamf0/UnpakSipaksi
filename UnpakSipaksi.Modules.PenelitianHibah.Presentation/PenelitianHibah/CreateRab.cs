using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateRAB;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class CreateRAB
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianHibah/RAB", async (CreateRABRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRABCommand(
                  request.UuidPenelitianHibah,
                  request.UuidKelompokRab,
                  request.UuidKomponen,
                  request.Item,
                  request.UuidSatuan,
                  request.HargaSatuan,
                  request.Total
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class CreateRABRequest
        {
            public string UuidPenelitianHibah { get; set; }
            public string? UuidKelompokRab { get; set; }
            public string? UuidKomponen { get; set; }
            public int? Item { get; set; }
            public string? UuidSatuan { get; set; }
            public int? HargaSatuan { get; set; }
            public int? Total { get; set; }
        }
    }
}
