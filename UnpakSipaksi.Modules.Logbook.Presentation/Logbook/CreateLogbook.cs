using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Logbook.Application.CreateLogbook;

namespace UnpakSipaksi.Modules.Logbook.Presentation.Logbook
{
    internal static class CreateLogbook
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Logbook", async (CreateLogbookRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateLogbookCommand(
                    request.UuidPenelitianHibah,
                    request.UuidPenelitianPkm,
                    request.TanggalKegiatan,
                    request.Lampiran,
                    request.Deskripsi,
                    request.Persentase
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Logbook);
        }

        internal sealed class CreateLogbookRequest
        {
            public string? UuidPenelitianHibah { get; set; }
            public string? UuidPenelitianPkm { get; set; }
            public string TanggalKegiatan { get; set; }
            public string Lampiran { get; set; }
            public string Deskripsi { get; set; }
            public double Persentase { get; set; }
        }
    }
}
