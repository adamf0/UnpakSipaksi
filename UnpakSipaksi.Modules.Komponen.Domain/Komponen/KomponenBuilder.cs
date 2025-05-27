using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Komponen.Domain.Komponen
{
    public sealed partial class Komponen
    {
        public sealed class KomponenBuilder
        {
            private readonly Komponen _akurasiPenelitian;
            private Result? _result;

            public KomponenBuilder(Komponen akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Komponen> Build()
            {
                return HasError ? Result.Failure<Komponen>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KomponenBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<Komponen>(KomponenErrors.NamaEmpty());
                    return this;
                }

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KomponenBuilder ChangeMaxBiaya(int? maxBiaya)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<Komponen>(KomponenErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.MaxBiaya = maxBiaya;
                return this;
            }

        }
    }
}
