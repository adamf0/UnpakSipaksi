using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.CreateArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation.ArtikelMediaMassa
{
    internal static class CreateArtikelMediaMassa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("ArtikelMediaMassa", async (CreateArtikelMediaMassaRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateArtikelMediaMassaCommand(
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.ArtikelMediaMassa);
        }

        internal sealed class CreateArtikelMediaMassaRequest
        {
            public string Nama { get; set; }

            public int Nilai { get; set; }
        }
    }
}
