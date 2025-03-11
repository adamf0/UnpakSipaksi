using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget
{
    public sealed partial class KewajaranTahapanTarget
    {
        public sealed class KewajaranTahapanTargetBuilder
        {
            private readonly KewajaranTahapanTarget _akurasiPenelitian;
            private Result? _result;

            public KewajaranTahapanTargetBuilder(KewajaranTahapanTarget akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KewajaranTahapanTarget> Build()
            {
                return HasError ? Result.Failure<KewajaranTahapanTarget>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KewajaranTahapanTargetBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KewajaranTahapanTarget>(KewajaranTahapanTargetErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KewajaranTahapanTargetBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KewajaranTahapanTarget>(KewajaranTahapanTargetErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }

        }
    }
}
