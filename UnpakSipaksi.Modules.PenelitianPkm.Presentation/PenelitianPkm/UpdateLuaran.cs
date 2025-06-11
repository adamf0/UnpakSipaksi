using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateLuaran;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/Luaran", async (UpdateLuaranPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateLuaranCommand(
                  request.Uuid,
                  request.UuidPenelitianPkm,
                  request.UuidKategori,
                  request.UuidKategoriLuaran,
                  request.Keterangan,
                  request.Link,
                  request.Jenis
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateLuaranPkmRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string UuidKategori { get; set; }
            public string UuidKategoriLuaran { get; set; }
            public string Keterangan { get; set; }
            public string Link { get; set; }
            public string Jenis { get; set; }
        }
    }
}
