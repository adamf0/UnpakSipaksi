using Microsoft.AspNetCore.Routing;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Presentation.PublikasiDisitasiProposal
{
    public static class PublikasiDisitasiProposalEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreatePublikasiDisitasiProposal.MapEndpoint(app);
            UpdatePublikasiDisitasiProposal.MapEndpoint(app);
            DeletePublikasiDisitasiProposal.MapEndpoint(app);
            GetPublikasiDisitasiProposal.MapEndpoint(app);
            GetAllPublikasiDisitasiProposal.MapEndpoint(app);
        }
    }
}
