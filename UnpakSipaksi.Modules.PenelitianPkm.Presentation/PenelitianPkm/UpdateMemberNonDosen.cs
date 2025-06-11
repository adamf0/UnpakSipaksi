using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateMemberNonDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/MemberNonDosen", async (UpdateMemberNonDosenPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMemberNonDosenCommand(
                    request.Uuid,
                    request.UuidPenelitianPkm,
                    request.NomorIdentitas,
                    request.Nama,
                    request.Afiliasi
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateMemberNonDosenPkmRequest
        {
            public string Uuid { get; set; }
            public string UuidPenelitianPkm { get; set; }
            public string? NomorIdentitas { get; set; }
            public string? Nama { get; set; }
            public string? Afiliasi { get; set; }
        }
    }
}
