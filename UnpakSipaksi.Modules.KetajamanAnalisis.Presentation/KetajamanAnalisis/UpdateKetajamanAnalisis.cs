using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.UpdateKetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Presentation.KetajamanAnalisis
{
    internal static class UpdateKetajamanAnalisis
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KetajamanAnalisis", async (UpdateKetajamanAnalisisRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKetajamanAnalisisCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KetajamanAnalisis);
        }

        internal sealed class UpdateKetajamanAnalisisRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
