using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetkesesuaianJadwalRepository;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Presentation.KesesuaianJadwal
{
    internal static class GetKesesuaianJadwal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("KesesuaianJadwal/{id}", async (Guid id, ISender sender) =>
            {
                Result<KesesuaianJadwalResponse> result = await sender.Send(new GetKesesuaianJadwalQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.KesesuaianJadwal);
        }
    }
}
