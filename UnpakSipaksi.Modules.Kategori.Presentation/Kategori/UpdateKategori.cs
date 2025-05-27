using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Kategori.Application.UpdateKategori;

namespace UnpakSipaksi.Modules.Kategori.Presentation.Kategori
{
    internal static class UpdateKategori
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Kategori", async (UpdateKategoriRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriCommand(
                    request.Uuid,
                    request.Nama
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Kategori);
        }

        internal sealed class UpdateKategoriRequest
        {
            public string Uuid { get; set; }
            public string Nama { get; set; }
        }
    }
}
