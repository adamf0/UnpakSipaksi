using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/Mahasiswa/{Uuid}/{UuidPenelitianHibah}", async (string Uuid, string UuidPenelitianHibah, ISender sender) =>
            {
                Result<MemberMahasiswaResponse> result = await sender.Send(new GetMemberMahasiswaQuery(Uuid,UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
