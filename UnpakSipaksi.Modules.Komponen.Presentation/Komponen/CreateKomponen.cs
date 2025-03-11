using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Komponen.Application.CreateKomponen;

namespace UnpakSipaksi.Modules.Komponen.Presentation.Komponen
{
    internal static class CreateKomponen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("Komponen", async (CreateKomponenRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKomponenCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.MaxNilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Komponen);
        }

        internal sealed class CreateKomponenRequest
        {
            public string Nama { get; set; }

            public string MaxNilai { get; set; }
        }
    }
}
