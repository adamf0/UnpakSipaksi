using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLuaran;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/Luaran", async (UpdateLuaranRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateLuaranCommand(
                  request.Uuid,
                  request.UuidPenelitianHibah,
                  request.UuidKategori,
                  request.UuidKategoriLuaran,
                  request.Keterangan,
                  request.Link,
                  request.Jenis
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateLuaranRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianHibah { get; set; }
            public string UuidKategori { get; set; }
            public string UuidKategoriLuaran { get; set; }
            public string Keterangan { get; set; }
            public string Link { get; set; }
            public string Jenis { get; set; }
        }
    }
}
