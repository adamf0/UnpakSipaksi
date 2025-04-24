using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class UpdateMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PenelitianHibah/MemberMahasiswa", async (UpdateMemberMahasiswaRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateMemberMahasiswaCommand(
                    request.Uuid,
                    request.UuidPenelitianHibah,
                    request.NPM
                ));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class UpdateMemberMahasiswaRequest
        {
            public string Uuid { get; set; }
            public string NPM { get; set; }
            public string UuidPenelitianHibah { get; set; }
        }
    }
}
