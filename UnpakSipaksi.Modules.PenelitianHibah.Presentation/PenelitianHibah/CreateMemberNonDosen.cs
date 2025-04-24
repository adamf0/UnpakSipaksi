using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class CreateMemberNonDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianHibah/MemberNonDosen", async (CreateMemberNonDosenRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMemberNonDosenCommand(
                    request.UuidPenelitianHibah,
                    request.NomorIdentitas,
                    request.Nama,
                    request.Afiliasi
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class CreateMemberNonDosenRequest
        {
            public string UuidPenelitianHibah { get; set; }
            public string? NomorIdentitas { get; set; }
            public string? Nama { get; set; }
            public string? Afiliasi { get; set; }
        }
    }
}
