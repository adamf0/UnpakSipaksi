using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks
{
    public sealed partial class KualitasIpteks
    {
        public sealed class KualitasIpteksBuilder
        {
            private readonly KualitasIpteks _akurasiPenelitian;
            private Result? _result;

            public KualitasIpteksBuilder(KualitasIpteks akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KualitasIpteks> Build()
            {
                return HasError ? Result.Failure<KualitasIpteks>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KualitasIpteksBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KualitasIpteks>(KualitasIpteksErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KualitasIpteksBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                if (nilai < 0)
                {
                    _result = Result.Failure<KualitasIpteks>(KualitasIpteksErrors.InvalidValueNilai());
                    return this;
                }

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
