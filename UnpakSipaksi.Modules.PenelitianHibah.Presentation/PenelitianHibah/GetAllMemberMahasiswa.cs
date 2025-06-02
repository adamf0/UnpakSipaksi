using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class GetAllMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianHibah/Mahasiswa/{UuidPenelitianHibah}", async (string UuidPenelitianHibah, ISender sender) =>
            {
                Result<List<MemberMahasiswaResponse>> result = await sender.Send(new GetAllMemberMahasiswaQuery(UuidPenelitianHibah));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
