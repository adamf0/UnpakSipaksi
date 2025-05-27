using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.UpdateKuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Presentation.KuantitasStatusKi
{
    internal static class UpdateKuantitasStatusKi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KuantitasStatusKi", async (UpdateKuantitasStatusKiRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKuantitasStatusKiCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KuantitasStatusKi);
        }

        internal sealed class UpdateKuantitasStatusKiRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
