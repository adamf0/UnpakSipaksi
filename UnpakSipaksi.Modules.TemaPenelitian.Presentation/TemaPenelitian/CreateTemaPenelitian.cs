using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.TemaPenelitian.Application.CreateTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Presentation;

namespace UnpakSipaksi.Modules.TemaPenelitian.Presentation.TemaPenelitian
{
    internal static class CreateTemaPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("TemaPenelitian", async (CreateTemaPenelitianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateTemaPenelitianCommand(
                    request.Nama,
                    request.FokusPenelitianId
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.TemaPenelitian);
        }

        internal sealed class CreateTemaPenelitianRequest
        {
            public string Nama { get; set; }
            public string FokusPenelitianId { get; set; }
        }
    }
}
