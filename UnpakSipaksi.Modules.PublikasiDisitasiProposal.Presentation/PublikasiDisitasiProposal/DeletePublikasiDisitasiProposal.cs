using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Common.Presentation.ApiResults;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.DeletePublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation.PublikasiDisitasiProposal
{
    internal class DeletePublikasiDisitasiProposal
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("PublikasiDisitasiProposal/{Uuid}", async (string Uuid, ISender sender) =>
            {
                Result result = await sender.Send(
                    new DeletePublikasiDisitasiProposalCommand(Uuid)
                );

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            }).WithTags(Tags.PublikasiDisitasiProposal);
        }
    }
}
