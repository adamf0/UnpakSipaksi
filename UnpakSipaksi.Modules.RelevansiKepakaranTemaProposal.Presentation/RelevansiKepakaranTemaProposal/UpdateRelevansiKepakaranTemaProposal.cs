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
                    HtmlEncoder.Default.Encode(request.Nama),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPDP)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotTerapan)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotKerjasama)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotPenelitianDasar)),
                    int.Parse(HtmlEncoder.Default.Encode(request.BobotSkor))
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.RelevansiKepakaranTemaProposal);
        }

        internal sealed class UpdateRelevansiKepakaranTemaProposalRequest
        {
            public Guid Id { get; set; }
            public string Nama { get; set; }

            public string BobotPDP { get; set; }
            public string BobotTerapan { get; set; }

            public string BobotKerjasama { get; set; }
            public string BobotPenelitianDasar { get; set; }
            public string BobotSkor { get; set; }
        }
    }
}
