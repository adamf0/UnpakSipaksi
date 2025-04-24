using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.MemberDosen
{
    internal class DeleteMemberDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianHibah/MemberDosen/{id}/{nidn}", async (Guid id, string nidn, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteMemberDosenCommand(id, nidn)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
