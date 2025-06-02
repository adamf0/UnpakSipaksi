using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllDokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenPendukung;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetAllDokumenPendukung
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/DokumenPendukung/{UuidPenelitianHibah}", async (string UuidPenelitianHibah, ISender sender) =>
            {
                Result<List<DokumenPendukungResponse>> result = await sender.Send(new GetAllDokumenPendukungQuery(UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
