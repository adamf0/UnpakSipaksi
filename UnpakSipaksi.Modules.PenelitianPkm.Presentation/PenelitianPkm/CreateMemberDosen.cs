using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Presentation;

namespace UnpakSipaksi.Modules.PenelitianPkm.Presentation.PenelitianPkm
{
    internal static class CreateMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianPkm/MemberDosen", async (CreateMemberDosenPkmRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMemberDosenCommand(
                    request.UuidPenelitianPkm,
                    request.NIDN
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianPkm);
        }

        internal sealed class CreateMemberDosenPkmRequest
        {
            public string NIDN { get; set; }
            public string UuidPenelitianPkm { get; set; }
        }
    }
}
