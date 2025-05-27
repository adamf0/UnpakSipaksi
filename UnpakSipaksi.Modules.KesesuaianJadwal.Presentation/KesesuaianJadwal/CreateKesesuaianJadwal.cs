using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.CreateKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Presentation.KesesuaianJadwal
{
    internal static class CreateKesesuaianJadwal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("KesesuaianJadwal", async (CreateKesesuaianJadwalRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateKesesuaianJadwalCommand(
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianJadwal);
        }

        internal sealed class CreateKesesuaianJadwalRequest
        {
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
