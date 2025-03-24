using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan
{
    public sealed partial class MetodeRencanaKegiatan
    {
        public sealed class MetodeRencanaKegiatanBuilder
        {
            private readonly MetodeRencanaKegiatan _akurasiPenelitian;
            private Result? _result;

            public MetodeRencanaKegiatanBuilder(MetodeRencanaKegiatan akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<MetodeRencanaKegiatan> Build()
            {
                return HasError ? Result.Failure<MetodeRencanaKegiatan>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public MetodeRencanaKegiatanBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<MetodeRencanaKegiatan>(MetodeRencanaKegiatanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public MetodeRencanaKegiatanBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<MetodeRencanaKegiatan>(MetodeRencanaKegiatanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }
        }
    }
}
