using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TopikPenelitian.Application.CreateTopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Presentation.TopikPenelitian
{
    internal static class CreateTopikPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("TopikPenelitian", async (CreateTopikPenelitianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateTopikPenelitianCommand(
                    request.Nama,
                    request.TemaPenelitianId
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.TopikPenelitian);
        }

        internal sealed class CreateTopikPenelitianRequest
        {
            public string Nama { get; set; }
            public string TemaPenelitianId { get; set; }
        }
    }
}
