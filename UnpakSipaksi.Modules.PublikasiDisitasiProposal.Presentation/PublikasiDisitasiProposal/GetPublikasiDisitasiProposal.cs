using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation.PublikasiDisitasiProposal
{
    internal static class GetPublikasiDisitasiProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("PublikasiDisitasiProposal/{id}", async (Guid id, ISender sender) =>
            {
                Result<PublikasiDisitasiProposalResponse> result = await sender.Send(new GetPublikasiDisitasiProposalQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            }).WithTags(Tags.PublikasiDisitasiProposal);
        }
    }
}
