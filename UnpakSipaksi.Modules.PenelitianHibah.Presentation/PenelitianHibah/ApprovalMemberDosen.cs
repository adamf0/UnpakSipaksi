using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.ApprovalMemberDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class ApprovalMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/ApprovalMemberDosen", async (ApprovalMemberDosenRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new ApprovalMemberDosenCommand(
                    request.Uuid,
                    request.UuidPenelitianHibah,
                    request.NIDN,
                    request.Status
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class ApprovalMemberDosenRequest
        {
            public string Uuid { get; set; }
            public string NIDN { get; set; }
            public string UuidPenelitianHibah { get; set; }
            public string Status { get; set; }
        }
    }
}
