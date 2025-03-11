using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi
{
    public sealed partial class KuantitasStatusKi
    {
        public sealed class KuantitasStatusKiBuilder
        {
            private readonly KuantitasStatusKi _akurasiPenelitian;
            private Result? _result;

            public KuantitasStatusKiBuilder(KuantitasStatusKi akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KuantitasStatusKi> Build()
            {
                return HasError ? Result.Failure<KuantitasStatusKi>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KuantitasStatusKiBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KuantitasStatusKi>(KuantitasStatusKiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KuantitasStatusKiBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KuantitasStatusKi>(KuantitasStatusKiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
