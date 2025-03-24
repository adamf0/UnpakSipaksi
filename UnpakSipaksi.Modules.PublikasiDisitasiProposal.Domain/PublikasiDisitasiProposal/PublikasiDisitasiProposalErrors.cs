using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal
{
    public static class PublikasiDisitasiProposalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PublikasiDisitasiProposal.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("PublikasiDisitasiProposal.NotFound", $"The event with the identifier {Id} was not found");

    }
}
