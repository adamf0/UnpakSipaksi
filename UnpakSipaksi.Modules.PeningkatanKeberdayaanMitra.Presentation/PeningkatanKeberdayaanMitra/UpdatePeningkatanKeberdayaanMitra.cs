using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.UpdatePeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation.PeningkatanKeberdayaanMitra
{
    internal static class UpdatePeningkatanKeberdayaanMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PeningkatanKeberdayaanMitra", async (UpdatePeningkatanKeberdayaanMitraRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdatePeningkatanKeberdayaanMitraCommand(
                    request.Id,
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.Nilai))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PeningkatanKeberdayaanMitra);
        }

        internal sealed class UpdatePeningkatanKeberdayaanMitraRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }
            public string Nilai { get; set; }
        }
    }
}
