using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.DeleteRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation.RelevansiKepakaranTemaProposal
{
    internal class DeleteRelevansiKepakaranTemaProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("RelevansiKepakaranTemaProposal/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeleteRelevansiKepakaranTemaProposalCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RelevansiKepakaranTemaProposal);
        }
    }
}
