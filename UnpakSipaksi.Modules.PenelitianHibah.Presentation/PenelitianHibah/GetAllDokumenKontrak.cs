using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllDokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenKontrak;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetAllDokumenKontrak
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/DokumenKontrak/{UuidPenelitianHibah}", async (string UuidPenelitianHibah, ISender sender) =>
            {
                Result<List<DokumenKontrakResponse>> result = await sender.Send(new GetAllDokumenKontrakQuery(UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
