using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateLuaran;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class CreateLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianPkm/Luaran", async (CreateLuaranPkmRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateLuaranCommand(
                  request.UuidPenelitianPkm,
                  request.UuidKategori,
                  request.UuidKategoriLuaran,
                  request.Keterangan,
                  request.Link,
                  request.Jenis
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class CreateLuaranPkmRequest
        {
            public string UuidPenelitianPkm { get; set; }
            public string UuidKategori { get; set; }
            public string UuidKategoriLuaran { get; set; }
            public string Keterangan { get; set; }
            public string Link { get; set; }
            public string Jenis { get; set; }
        }
    }
}
