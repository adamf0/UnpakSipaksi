using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding
{
    public sealed partial class KualitasKuantitasPublikasiProsiding
    {
        public sealed class KualitasKuantitasPublikasiProsidingBuilder
        {
            private readonly KualitasKuantitasPublikasiProsiding _akurasiPenelitian;
            private Result? _result;

            public KualitasKuantitasPublikasiProsidingBuilder(KualitasKuantitasPublikasiProsiding akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KualitasKuantitasPublikasiProsiding> Build()
            {
                return HasError ? Result.Failure<KualitasKuantitasPublikasiProsiding>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KualitasKuantitasPublikasiProsidingBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KualitasKuantitasPublikasiProsiding>(KualitasKuantitasPublikasiProsidingErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KualitasKuantitasPublikasiProsidingBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KualitasKuantitasPublikasiProsiding>(KualitasKuantitasPublikasiProsidingErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
