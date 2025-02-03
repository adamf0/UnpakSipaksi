using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.CreateKategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Presentation.KategoriSumberDana
{
    internal static class CreateKategoriSumberDana
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KategoriSumberDana", async (CreateKategoriSumberDanaRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKategoriSumberDanaCommand(
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KategoriSumberDana);
        }

        internal sealed class CreateKategoriSumberDanaRequest
        {
            public string Nama { get; set; }
        }
    }
}
