using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class CreateMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianHibah/MemberMahasiswa", async (CreateMemberMahasiswaRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMemberMahasiswaCommand(
                    request.UuidPenelitianHibah,
                    request.NPM
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class CreateMemberMahasiswaRequest
        {
            public string NPM { get; set; }
            public string UuidPenelitianHibah { get; set; }
        }
    }
}
