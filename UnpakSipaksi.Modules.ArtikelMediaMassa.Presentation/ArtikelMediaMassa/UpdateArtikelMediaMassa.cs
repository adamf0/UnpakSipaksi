using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.UpdateArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation.ArtikelMediaMassa
{
    internal static class UpdateArtikelMediaMassa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("ArtikelMediaMassa", async (UpdateArtikelMediaMassaRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateArtikelMediaMassaCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.ArtikelMediaMassa);
        }

        internal sealed class UpdateArtikelMediaMassaRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }

            public string Nilai { get; set; }
        }
    }
}
