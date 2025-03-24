using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetAllPublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation.PublikasiDisitasiProposal
{
    internal class GetAllPublikasiDisitasiProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PublikasiDisitasiProposal", async (ISender sender) =>
            {
                Result<List<PublikasiDisitasiProposalResponse>> result = await sender.Send(new GetAllPublikasiDisitasiProposalQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PublikasiDisitasiProposal);
        }
    }
}
