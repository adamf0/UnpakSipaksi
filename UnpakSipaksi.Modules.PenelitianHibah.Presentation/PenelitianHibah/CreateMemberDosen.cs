using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal static class CreateMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PenelitianHibah/MemberDosen", async (CreateMemberDosenRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateMemberDosenCommand(
                    request.UuidPenelitianHibah,
                    request.NIDN
                ));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }

        internal sealed class CreateMemberDosenRequest
        {
            public string NIDN { get; set; }
            public string UuidPenelitianHibah { get; set; }
        }
    }
}
