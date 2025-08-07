using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Logbook.Application.DeleteLogbook;

namespace UnpakSipaksi.Modules.Logbook.Presentation.Logbook
{
    internal class DeleteLogbook
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Logbook/Delete", async (DeleteLogbookRequest request, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteLogbookCommand(
                        request.Uuid,
                        request.UuidPenelitianHibah,
                        request.UuidPenelitianPkm
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Logbook);
        }
        internal sealed class DeleteLogbookRequest
        {
            public string? UuidPenelitianHibah { get; set; }
            public string? UuidPenelitianPkm { get; set; }
            public string Uuid { get; set; }
        }
    }
}
