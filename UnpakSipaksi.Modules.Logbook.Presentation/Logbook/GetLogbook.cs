using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Logbook.Application.GetLogbook;

namespace UnpakSipaksi.Modules.Logbook.Presentation.Logbook
{
    internal static class GetLogbook
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Logbook/Info", async (GetLogbookRequest request, ISender sender) =>
            {
                Result<LogbookResponse> result = await sender.Send(new GetLogbookQuery(
                    request.Id,
                    request.NIDN,
                    request.UuidPenelitianHibah,
                    request.UuidPenelitianPkm
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Logbook);
        }
        internal sealed class GetLogbookRequest
        {
            public string? UuidPenelitianHibah { get; set; }
            public string? UuidPenelitianPkm { get; set; }
            public string NIDN { get; set; }
            public string Id { get; set; }
        }
    }
}
