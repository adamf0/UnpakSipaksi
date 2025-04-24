using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal class DeleteMemberNonDosen
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianHibah/MemberNonDosen/{id}", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteMemberNonDosenCommand(id)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
