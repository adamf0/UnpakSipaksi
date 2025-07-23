using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal
{
    public static class PublikasiDisitasiProposalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PublikasiDisitasiProposal.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("PublikasiDisitasiProposal.NotSameValue", "not the same value in data 'skor'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("PublikasiDisitasiProposal.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueSkor() =>
            Error.NotFound("PublikasiDisitasiProposal.InvalidValueSkor", "Invalid value 'skor'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("PublikasiDisitasiProposal.NotFound", $"Publikasi disitasi proposal with the identifier {Id} was not found");

    }
}
