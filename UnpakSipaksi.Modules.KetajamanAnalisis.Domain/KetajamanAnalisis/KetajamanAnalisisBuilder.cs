using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis
{
    public sealed partial class KetajamanAnalisis
    {
        public sealed class KetajamanAnalisisBuilder
        {
            private readonly KetajamanAnalisis _akurasiPenelitian;
            private Result? _result;

            public KetajamanAnalisisBuilder(KetajamanAnalisis akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KetajamanAnalisis> Build()
            {
                return HasError ? Result.Failure<KetajamanAnalisis>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KetajamanAnalisisBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanAnalisis>(KetajamanAnalisisErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KetajamanAnalisisBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanAnalisis>(KetajamanAnalisisErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }

        }
    }
}
