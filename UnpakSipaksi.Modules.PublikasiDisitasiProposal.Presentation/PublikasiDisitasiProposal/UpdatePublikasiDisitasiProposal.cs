using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.UpdatePublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation.PublikasiDisitasiProposal
{
    internal static class UpdatePublikasiDisitasiProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("PublikasiDisitasiProposal", async (UpdatePublikasiDisitasiProposalRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdatePublikasiDisitasiProposalCommand(
                    request.Id,
                    request.Nama,
                    request.BobotSkor
                    )
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PublikasiDisitasiProposal);
        }

        internal sealed class UpdatePublikasiDisitasiProposalRequest
        {
            public string Id { get; set; }
            public string Nama { get; set; }
            public int BobotSkor { get; set; }
        }
    }
}
