using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KategoriTkt.Application.UpdateKategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Presentation;

namespace UnpakSipaksi.Modules.KategoriTkt.Presentation.KategoriTkt
{
    internal static class UpdateKategoriTkt
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KategoriTkt", async (UpdateKategoriTktRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKategoriTktCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama)
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KategoriTkt);
        }

        internal sealed class UpdateKategoriTktRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
        }
    }
}
