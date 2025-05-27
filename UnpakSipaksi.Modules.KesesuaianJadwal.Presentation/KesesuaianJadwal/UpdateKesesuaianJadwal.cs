using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.UpdateKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Presentation.KesesuaianJadwal
{
    internal static class UpdateKesesuaianJadwal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("KesesuaianJadwal", async (UpdateKesesuaianJadwalRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateKesesuaianJadwalCommand(
                    request.Id,
                    request.Nama,
                    request.Nilai
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianJadwal);
        }

        internal sealed class UpdateKesesuaianJadwalRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int Nilai { get; set; }
        }
    }
}
