using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.UpdateKategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Presentation.KategoriSumberDana
{
    internal static class UpdateKategoriSumberDana
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KategoriSumberDana", async (UpdateKategoriSumberDanaRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriSumberDanaCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriSumberDana);
        }

        internal sealed class UpdateKategoriSumberDanaRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
