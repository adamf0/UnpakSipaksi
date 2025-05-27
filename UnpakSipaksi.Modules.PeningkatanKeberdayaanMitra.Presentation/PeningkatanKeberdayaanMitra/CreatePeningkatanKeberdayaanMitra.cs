using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.CreatePeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation.PeningkatanKeberdayaanMitra
{
    internal static class CreatePeningkatanKeberdayaanMitra
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PeningkatanKeberdayaanMitra", async (CreatePeningkatanKeberdayaanMitraRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreatePeningkatanKeberdayaanMitraCommand(
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PeningkatanKeberdayaanMitra);
        }

        internal sealed class CreatePeningkatanKeberdayaanMitraRequest
        {
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
