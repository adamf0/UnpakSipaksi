using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisLuaran.Application.UpdateJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Presentation;

namespace UnpakSipaksi.Modules.JenisLuaran.Presentation.JenisLuaran
{
    internal static class UpdateJenisLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("JenisLuaran", async (UpdateJenisLuaranRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateJenisLuaranCommand(
                    request.Uuid,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.JenisLuaran);
        }

        internal sealed class UpdateJenisLuaranRequest
        {
            public string Uuid { get; set; }
            public string Nama { get; set; }
        }
    }
}
