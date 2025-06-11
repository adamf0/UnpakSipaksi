using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JenisLuaran.Application.CreateJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Presentation;

namespace UnpakSipaksi.Modules.JenisLuaran.Presentation.JenisLuaran
{
    internal static class CreateJenisLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("JenisLuaran", async (CreateJenisLuaranRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateJenisLuaranCommand(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JenisLuaran);
        }

        internal sealed class CreateJenisLuaranRequest
        {
            public string Nama { get; set; }
        }
    }
}
