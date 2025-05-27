using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Komponen.Application.UpdateKomponen;

namespace UnpakSipaksi.Modules.Komponen.Presentation.Komponen
{
    internal static class UpdateKomponen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("Komponen", async (UpdateKomponenRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKomponenCommand(
                    request.Id,
                    request.Nama,
                    request?.MaxNilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.Komponen);
        }

        internal sealed class UpdateKomponenRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int? MaxNilai { get; set; }
        }
    }
}
