using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal
{
    public sealed partial class PublikasiDisitasiProposal
    {
        public sealed class PublikasiDisitasiProposalBuilder
        {
            private readonly PublikasiDisitasiProposal _akurasiPenelitian;
            private Result? _result;

            public PublikasiDisitasiProposalBuilder(PublikasiDisitasiProposal akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<PublikasiDisitasiProposal> Build()
            {
                return HasError ? Result.Failure<PublikasiDisitasiProposal>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public PublikasiDisitasiProposalBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PublikasiDisitasiProposal>(PublikasiDisitasiProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public PublikasiDisitasiProposalBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PublikasiDisitasiProposal>(PublikasiDisitasiProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public PublikasiDisitasiProposalBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PublikasiDisitasiProposal>(PublikasiDisitasiProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public PublikasiDisitasiProposalBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PublikasiDisitasiProposal>(PublikasiDisitasiProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public PublikasiDisitasiProposalBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PublikasiDisitasiProposal>(PublikasiDisitasiProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public PublikasiDisitasiProposalBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PublikasiDisitasiProposal>(PublikasiDisitasiProposalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
