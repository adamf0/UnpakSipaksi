using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class GetMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PenelitianPkm/Mahasiswa/{Uuid}/{UuidPenelitianPkm}", async (string Uuid, string UuidPenelitianPkm, ISender sender) =>
            {
                Result<MemberMahasiswaResponse> result = await sender.Send(new GetMemberMahasiswaQuery(Uuid, UuidPenelitianPkm));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }
    }
}
