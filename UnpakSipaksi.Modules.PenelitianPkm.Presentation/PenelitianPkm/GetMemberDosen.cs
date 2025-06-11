using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class GetMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianPkm/MemberDosen/{Uuid}/{UuidPenelitianPkm}", async (string Uuid, string UuidPenelitianPkm, ISender sender) =>
            {
                Result<MemberDosenResponse> result = await sender.Send(new GetMemberDosenQuery(Uuid, UuidPenelitianPkm));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
