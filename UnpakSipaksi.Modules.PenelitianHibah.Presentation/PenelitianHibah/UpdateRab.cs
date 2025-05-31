using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRAB;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateRAB
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/RAB", async (UpdateRABRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRABCommand(
                  request.Uuid,
                  request.UuidPenelitianHibah,
                  request.UuidKelompokRab,
                  request.UuidKomponen,
                  request.Item,
                  request.UuidSatuan,
                  request.HargaSatuan,
                  request.Total
                ));

                return result.Match(()=> Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateRABRequest
        {
            public string Uuid { get; set; }
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
