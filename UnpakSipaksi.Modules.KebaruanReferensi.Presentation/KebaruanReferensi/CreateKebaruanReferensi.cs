using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.CreateKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Presentation;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Presentation.KebaruanReferensi
{
    internal static class CreateKebaruanReferensi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KebaruanReferensi", async (CreateKebaruanReferensiRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKebaruanReferensiCommand(
                    request.Nama,
                    request.Skor
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KebaruanReferensi);
        }

        internal sealed class CreateKebaruanReferensiRequest
        {
            public string Nama { get; set; }
            public int Skor { get; set; }
        }
    }
}
