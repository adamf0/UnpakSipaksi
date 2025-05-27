using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal
{
    public sealed partial class RelevansiKepakaranTemaProposal
    {
        public sealed class RelevansiKepakaranTemaProposalBuilder
        {
            private readonly RelevansiKepakaranTemaProposal _akurasiPenelitian;
            private Result? _result;

            public RelevansiKepakaranTemaProposalBuilder(RelevansiKepakaranTemaProposal akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RelevansiKepakaranTemaProposal> Build()
            {
                return HasError ? Result.Failure<RelevansiKepakaranTemaProposal>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public RelevansiKepakaranTemaProposalBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKepakaranTemaProposal>(RelevansiKepakaranTemaProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public RelevansiKepakaranTemaProposalBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKepakaranTemaProposal>(RelevansiKepakaranTemaProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
