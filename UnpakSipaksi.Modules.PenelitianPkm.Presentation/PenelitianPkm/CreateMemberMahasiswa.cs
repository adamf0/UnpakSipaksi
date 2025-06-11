using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class CreateMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianPkm/MemberMahasiswa", async (CreateMemberMahasiswaPkmRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMemberMahasiswaCommand(
                    request.UuidPenelitianPkm,
                    request.NPM
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class CreateMemberMahasiswaPkmRequest
        {
            public string NPM { get; set; }
            public string UuidPenelitianPkm { get; set; }
        }
    }
}
