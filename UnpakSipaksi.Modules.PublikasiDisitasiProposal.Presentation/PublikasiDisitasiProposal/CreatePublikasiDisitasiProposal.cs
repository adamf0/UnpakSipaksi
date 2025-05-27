using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.CreatePublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation.PublikasiDisitasiProposal
{
    internal static class CreatePublikasiDisitasiProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("PublikasiDisitasiProposal", async (CreatePublikasiDisitasiProposalRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreatePublikasiDisitasiProposalCommand(
                    request.Nama,
                    request.BobotSkor
                    )
                );

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PublikasiDisitasiProposal);
        }

        internal sealed class CreatePublikasiDisitasiProposalRequest
        {
            public string Nama { get; set; }
            public int BobotSkor { get; set; }
        }
    }
}
