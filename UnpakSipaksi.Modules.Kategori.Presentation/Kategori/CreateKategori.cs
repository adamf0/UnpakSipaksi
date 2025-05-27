using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Kategori.Application.CreateKategori;
using UnpakSipaksi.Modules.Kategori.Presentation;

namespace UnpakSipaksi.Modules.Kategori.Presentation.Kategori
{
    internal static class CreateKategori
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Kategori", async (CreateKategoriRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKategoriCommand(
                    request.Nama
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Kategori);
        }

        internal sealed class CreateKategoriRequest
        {
            public string Nama { get; set; }
        }
    }
}
