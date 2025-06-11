using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class UpdateMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianPkm/MemberMahasiswa", async (UpdateMemberMahasiswaPkmRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMemberMahasiswaCommand(
                    request.Uuid,
                    request.UuidPenelitianPkm,
                    request.NPM
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class UpdateMemberMahasiswaPkmRequest
        {
            public string Uuid { get; set; }
            public string NPM { get; set; }
            public string UuidPenelitianPkm { get; set; }
        }
    }
}
