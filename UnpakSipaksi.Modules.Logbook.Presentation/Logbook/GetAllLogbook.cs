using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Logbook.Application.GetLogbook;
using UnpakSipaksi.Modules.Logbook.Application.GetAllLogbook;

namespace UnpakSipaksi.Modules.Logbook.Presentation.Logbook
{
    internal class GetAllLogbook
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Logbook/List", async (GetAllLogbookRequest request, ISender sender) =>
            {
                Result<List<LogbookResponse>> result = await sender.Send(new GetAllLogbookQuery(
                    request.NIDN,
                    request.UuidPenelitianHibah,
                    request.UuidPenelitianPkm
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Logbook);
        }
        internal sealed class GetAllLogbookRequest
        {
            public string? UuidPenelitianHibah { get; set; }
            public string? UuidPenelitianPkm { get; set; }
            public string NIDN { get; set; }
        }
    }
}
