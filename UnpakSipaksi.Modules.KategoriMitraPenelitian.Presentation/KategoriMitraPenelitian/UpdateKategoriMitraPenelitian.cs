using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.UpdateKategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian
{
    internal static class UpdateKategoriMitraPenelitian
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KategoriMitraPenelitian", async (UpdateKategoriMitraPenelitianRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriMitraPenelitianCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriMitraPenelitian);
        }

        internal sealed class UpdateKategoriMitraPenelitianRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
