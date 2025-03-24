using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation.RelevansiKepakaranTemaProposal
{
    public static class RelevansiKepakaranTemaProposalEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateRelevansiKepakaranTemaProposal.MapEndpoint(app);
            UpdateRelevansiKepakaranTemaProposal.MapEndpoint(app);
            DeleteRelevansiKepakaranTemaProposal.MapEndpoint(app);
            GetRelevansiKepakaranTemaProposal.MapEndpoint(app);
            GetAllRelevansiKepakaranTemaProposal.MapEndpoint(app);
        }
    }
}
