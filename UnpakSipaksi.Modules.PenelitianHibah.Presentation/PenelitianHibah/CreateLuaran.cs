using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateLuaran;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class CreateLuaran
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianHibah/Luaran", async (CreateLuaranRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateLuaranCommand(
                  request.UuidPenelitianHibah,
                  request.UuidKategori,
                  request.UuidKategoriLuaran,
                  request.Keterangan,
                  request.Link,
                  request.Jenis
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class CreateLuaranRequest
        {
            public string UuidPenelitianHibah { get; set; }
            public string UuidKategori { get; set; }
            public string UuidKategoriLuaran { get; set; }
            public string Keterangan { get; set; }
            public string Link { get; set; }
            public string Jenis { get; set; }
        }
    }
}
