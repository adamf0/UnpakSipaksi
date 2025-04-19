using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Presentation.Pengumuman
{
    internal static class CreatePengumuman
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            //[PR] upload file
            app.MapPost("Pengumuman", async (CreatePengumumanRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreatePengumumanCommand(
                    request.Pesan,
                    request.File,
                    request.Url,
                    request.Type,
                    request.Target,
                    request.Nidn,
                    request.KodeFaKultas,
                    request.TypeExpired,
                    request.TanggalAwal,
                    request.TanggalAkhir
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.Pengumuman);
        }

        internal sealed class CreatePengumumanRequest
        {
            public string Pesan { get; private set; } = null!;
            public string? File { get; private set; }
            public string? Url { get; private set; }
            public string Type { get; private set; } = null!;
            public string? Target { get; private set; }
            public string? Nidn { get; private set; }
            public string? KodeFaKultas { get; private set; }
            public string TypeExpired { get; private set; }
            public string? TanggalAwal { get; private set; }
            public string? TanggalAkhir { get; private set; }
        }
    }
}
