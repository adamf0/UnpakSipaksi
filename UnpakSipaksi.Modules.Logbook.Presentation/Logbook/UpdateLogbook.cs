using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Logbook.Application.UpdateLogbook;

namespace UnpakSipaksi.Modules.Logbook.Presentation.Logbook
{
    internal static class UpdateLogbook
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Logbook", async (UpdateLogbookRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateLogbookCommand(
                    request.Uuid,
                    request.UuidPenelitianHibah,
                    request.UuidPenelitianPkm,
                    request.TanggalKegiatan,
                    request.Lampiran,
                    request.Deskripsi,
                    request.Persentase
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Logbook);
        }

        internal sealed class UpdateLogbookRequest
        {
            public string Uuid { get; set; }
            public string? UuidPenelitianHibah { get; set; }
            public string? UuidPenelitianPkm { get; set; }
            public string TanggalKegiatan { get; set; }
            public string Lampiran { get; set; }
            public string Deskripsi { get; set; }
            public double Persentase { get; set; }
        }
    }
}
