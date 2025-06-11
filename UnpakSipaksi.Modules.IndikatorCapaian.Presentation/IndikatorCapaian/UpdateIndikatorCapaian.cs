using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.IndikatorCapaian.Presentation;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.UpdateIndikatorCapaian;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Presentation.IndikatorCapaian
{
    internal static class UpdateIndikatorCapaian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("IndikatorCapaian", async (UpdateIndikatorCapaianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateIndikatorCapaianCommand(
                    request.Uuid,
                    request.UuidJenisLuaran,
                    request.Nama,
                    request.Status
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.IndikatorCapaian);
        }

        internal sealed class UpdateIndikatorCapaianRequest
        {
            public string Uuid { get; set; }
            public string UuidJenisLuaran { get; set; }
            public string Nama { get; set; }
            public string? Status { get; set; }
        }
    }
}
