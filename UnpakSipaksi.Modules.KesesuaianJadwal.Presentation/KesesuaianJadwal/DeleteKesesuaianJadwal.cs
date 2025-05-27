using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.DeleteKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Presentation;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Presentation.KesesuaianJadwal
{
    internal class DeleteKesesuaianJadwal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("KesesuaianJadwal/{id}", async (string id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteKesesuaianJadwalCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.KesesuaianJadwal);
        }
    }
}
