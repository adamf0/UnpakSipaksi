using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetAllKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Presentation;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetkesesuaianJadwalRepository;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Presentation.KesesuaianJadwal
{
    internal class GetAllKesesuaianJadwal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KesesuaianJadwal", async (ISender sender) =>
            {
                Result<List<KesesuaianJadwalResponse>> result = await sender.Send(new GetAllKesesuaianJadwalQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianJadwal);
        }
    }
}
