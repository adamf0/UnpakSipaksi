using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.CreateMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation.MetodeRencanaKegiatan
{
    internal static class CreateMetodeRencanaKegiatan
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("MetodeRencanaKegiatan", async (CreateMetodeRencanaKegiatanRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMetodeRencanaKegiatanCommand(
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.MetodeRencanaKegiatan);
        }

        internal sealed class CreateMetodeRencanaKegiatanRequest
        {
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
