using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.IndikatorCapaian.Presentation;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Presentation.IndikatorCapaian
{
    internal static class CreateIndikatorCapaian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("IndikatorCapaian", async (CreateIndikatorCapaianRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateIndikatorCapaianCommand(
                    request.UuidJenisLuaran,
                    request.Nama,
                    request.Status
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.IndikatorCapaian);
        }

        internal sealed class CreateIndikatorCapaianRequest
        {
            public string UuidJenisLuaran { get; set; }
            public string Nama { get; set; }
            public string? Status { get; set; }
        }
    }
}
