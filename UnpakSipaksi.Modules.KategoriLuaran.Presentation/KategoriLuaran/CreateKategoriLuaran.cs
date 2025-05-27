using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriLuaran.Application.CreateKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Presentation;

namespace UnpakSipaksi.Modules.KategoriLuaran.Presentation.KategoriLuaran
{
    internal static class CreateKategoriLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KategoriLuaran", async (CreateKategoriLuaranRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKategoriLuaranCommand(
                    request.UuidKategori,
                    request.Nama,
                    request.Status
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriLuaran);
        }

        internal sealed class CreateKategoriLuaranRequest
        {
            public string UuidKategori { get; set; }
            public string Nama { get; set; }
            public string Status { get; set; }
        }
    }
}
