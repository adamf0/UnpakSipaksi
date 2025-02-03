using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Presentation;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Presentation.JumlahKolaboratorPublikasBereputasi
{
    internal static class GetJumlahKolaboratorPublikasBereputasi
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("JumlahKolaboratorPublikasBereputasi/{id}", async (Guid id, ISender sender) =>
            {
                Result<JumlahKolaboratorPublikasBereputasiResponse> result = await sender.Send(new GetJumlahKolaboratorPublikasBereputasiQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.JumlahKolaboratorPublikasBereputasi);
        }
    }
}
