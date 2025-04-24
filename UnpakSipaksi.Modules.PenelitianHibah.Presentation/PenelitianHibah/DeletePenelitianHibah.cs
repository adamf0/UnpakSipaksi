using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeletePenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Presentation.PenelitianHibah
{
    internal class DeletePenelitianHibah
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PenelitianHibah/{id}/{nidn}", async (Guid id, string nidn, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePenelitianHibahCommand(id,nidn)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PenelitianHibah);
        }
    }
}
