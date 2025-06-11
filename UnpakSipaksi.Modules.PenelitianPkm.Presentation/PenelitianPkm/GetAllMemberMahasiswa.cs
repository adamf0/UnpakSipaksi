using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class GetAllMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianPkm/Mahasiswa/{UuidPenelitianPkm}", async (string UuidPenelitianPkm, ISender sender) =>
            {
                Result<List<MemberMahasiswaResponse>> result = await sender.Send(new GetAllMemberMahasiswaQuery(UuidPenelitianPkm));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
