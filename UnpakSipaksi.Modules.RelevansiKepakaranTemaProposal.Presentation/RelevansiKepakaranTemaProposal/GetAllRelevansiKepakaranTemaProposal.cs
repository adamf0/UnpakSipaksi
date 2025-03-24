using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetAllRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation.RelevansiKepakaranTemaProposal
{
    internal class GetAllRelevansiKepakaranTemaProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("RelevansiKepakaranTemaProposal", async (ISender sender) =>
            {
                Result<List<RelevansiKepakaranTemaProposalResponse>> result = await sender.Send(new GetAllRelevansiKepakaranTemaProposalQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RelevansiKepakaranTemaProposal);
        }
    }
}
