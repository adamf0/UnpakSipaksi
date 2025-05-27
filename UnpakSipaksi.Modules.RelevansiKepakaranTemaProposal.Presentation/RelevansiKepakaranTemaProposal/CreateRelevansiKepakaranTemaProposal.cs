using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.CreateRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation.RelevansiKepakaranTemaProposal
{
    internal static class CreateRelevansiKepakaranTemaProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("RelevansiKepakaranTemaProposal", async (CreateRelevansiKepakaranTemaProposalRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateRelevansiKepakaranTemaProposalCommand(
                    request.Nama,
                    request.BobotSkor
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.RelevansiKepakaranTemaProposal);
        }

        internal sealed class CreateRelevansiKepakaranTemaProposalRequest
        {
            public string Nama { get; set; }
            public int BobotSkor { get; set; }
        }
    }
}
