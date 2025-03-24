using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal
{
    public static class RelevansiKepakaranTemaProposalErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RelevansiKepakaranTemaProposal.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("RelevansiKepakaranTemaProposal.NotFound", $"The event with the identifier {Id} was not found");

    }
}
