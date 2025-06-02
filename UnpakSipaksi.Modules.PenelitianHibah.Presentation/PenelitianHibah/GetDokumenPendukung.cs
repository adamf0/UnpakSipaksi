using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenPendukung;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetDokumenPendukung
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/DokumenPendukung/{Uuid}/{UuidPenelitianHibah}", async (string Uuid, string UuidPenelitianHibah, ISender sender) =>
            {
                Result<DokumenPendukungResponse> result = await sender.Send(new GetDokumenPendukungQuery(Uuid,UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
