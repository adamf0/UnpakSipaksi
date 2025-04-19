using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal
{
    public static class RelevansiKepakaranTemaProposalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RelevansiKepakaranTemaProposal.EmptyData", "data is not found");
        public static Error NotSameValue() =>
           Error.NotFound("RelevansiKepakaranTemaProposal.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("RelevansiKepakaranTemaProposal.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RelevansiKepakaranTemaProposal.NotFound", $"Relevansi kepakaran tema proposal with the identifier {Id} was not found");

    }
}
