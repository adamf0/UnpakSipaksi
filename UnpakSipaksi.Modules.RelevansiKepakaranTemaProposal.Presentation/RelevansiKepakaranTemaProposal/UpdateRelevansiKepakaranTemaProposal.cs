using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.UpdateRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation.RelevansiKepakaranTemaProposal
{
    internal static class UpdateRelevansiKepakaranTemaProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("RelevansiKepakaranTemaProposal", async (UpdateRelevansiKepakaranTemaProposalRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateRelevansiKepakaranTemaProposalCommand(
                    request.Id,
                    request.Nama,
                    request.BobotSkor
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RelevansiKepakaranTemaProposal);
        }

        internal sealed class UpdateRelevansiKepakaranTemaProposalRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int BobotSkor { get; set; }
        }
    }
}
