using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetMemberNonDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/MemberNonDosen/{Uuid}/{UuidPenelitianHibah}", async (string Uuid, string UuidPenelitianHibah, ISender sender) =>
            {
                Result<MemberNonDosenResponse> result = await sender.Send(new GetMemberNonDosenQuery(Uuid,UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
