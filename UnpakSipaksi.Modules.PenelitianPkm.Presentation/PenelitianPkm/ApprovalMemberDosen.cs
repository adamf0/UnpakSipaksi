using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.ApprovalMemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class ApprovalMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/ApprovalMemberDosen", async (ApprovalMemberDosenPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new ApprovalMemberDosenCommand(
                    request.Uuid,
                    request.UuidPenelitianPkm,
                    request.NIDN,
                    request.Status
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class ApprovalMemberDosenPkmRequest
        {
            public string Uuid { get; set; }
            public string NIDN { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string Status { get; set; }
        }
    }
}
