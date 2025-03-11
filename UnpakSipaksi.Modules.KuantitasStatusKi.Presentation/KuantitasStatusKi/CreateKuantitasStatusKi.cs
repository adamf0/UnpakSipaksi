using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.CreateKuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Presentation.KuantitasStatusKi
{
    internal static class CreateKuantitasStatusKi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KuantitasStatusKi", async (CreateKuantitasStatusKiRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKuantitasStatusKiCommand(
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KuantitasStatusKi);
        }

        internal sealed class CreateKuantitasStatusKiRequest
        {
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
