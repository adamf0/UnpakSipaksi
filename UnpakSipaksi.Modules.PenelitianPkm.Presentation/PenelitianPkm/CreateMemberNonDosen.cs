using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class CreateMemberNonDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianPkm/MemberNonDosen", async (CreateMemberNonDosenPkmRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMemberNonDosenCommand(
                    request.UuidPenelitianPkm,
                    request.NomorIdentitas,
                    request.Nama,
                    request.Afiliasi
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class CreateMemberNonDosenPkmRequest
        {
            public string UuidPenelitianPkm { get; set; }
            public string? NomorIdentitas { get; set; }
            public string? Nama { get; set; }
            public string? Afiliasi { get; set; }
        }
    }
}
