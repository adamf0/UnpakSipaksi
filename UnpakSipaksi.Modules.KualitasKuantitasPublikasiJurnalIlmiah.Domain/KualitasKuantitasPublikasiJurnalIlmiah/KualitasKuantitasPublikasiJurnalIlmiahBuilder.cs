using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed partial class KualitasKuantitasPublikasiJurnalIlmiah
    {
        public sealed class KualitasKuantitasPublikasiJurnalIlmiahBuilder
        {
            private readonly KualitasKuantitasPublikasiJurnalIlmiah _akurasiPenelitian;
            private Result? _result;

            public KualitasKuantitasPublikasiJurnalIlmiahBuilder(KualitasKuantitasPublikasiJurnalIlmiah akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KualitasKuantitasPublikasiJurnalIlmiah> Build()
            {
                return HasError ? Result.Failure<KualitasKuantitasPublikasiJurnalIlmiah>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KualitasKuantitasPublikasiJurnalIlmiahBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KualitasKuantitasPublikasiJurnalIlmiah>(KualitasKuantitasPublikasiJurnalIlmiahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KualitasKuantitasPublikasiJurnalIlmiahBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KualitasKuantitasPublikasiJurnalIlmiah>(KualitasKuantitasPublikasiJurnalIlmiahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
