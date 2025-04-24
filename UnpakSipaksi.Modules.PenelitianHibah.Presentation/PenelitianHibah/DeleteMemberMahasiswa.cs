using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.MemberMahasiswa
{
    internal class DeleteMemberMahasiswa
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianHibah/MemberMahasiswa/{id}/{npm}", async (Guid id, string npm, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteMemberMahasiswaCommand(id, npm)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
