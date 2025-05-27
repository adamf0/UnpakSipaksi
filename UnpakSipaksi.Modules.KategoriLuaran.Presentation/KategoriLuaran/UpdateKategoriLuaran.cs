using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriLuaran.Application.UpdateKategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Presentation;

namespace UnpakSipaksi.Modules.KategoriLuaran.Presentation.KategoriLuaran
{
    internal static class UpdateKategoriLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KategoriLuaran", async (UpdateKategoriLuaranRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriLuaranCommand(
                    request.Uuid,
                    request.UuidKategori,
                    request.Nama,
                    request.Status
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriLuaran);
        }

        internal sealed class UpdateKategoriLuaranRequest
        {
            public string Uuid { get; set; }
            public string UuidKategori { get; set; }
            public string Nama { get; set; }
            public string Status { get; set; }
        }
    }
}
