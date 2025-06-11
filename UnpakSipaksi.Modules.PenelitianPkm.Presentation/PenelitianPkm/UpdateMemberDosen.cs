using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/MemberDosen", async (UpdateMemberDosenPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMemberDosenCommand(
                    request.Uuid,
                    request.UuidPenelitianPkm,
                    request.NIDN
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateMemberDosenPkmRequest
        {
            public string Uuid { get; set; }
            public string NIDN { get; set; }
            public string UuidPenelitianPkm { get; set; }
        }
    }
}
