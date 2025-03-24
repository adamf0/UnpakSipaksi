using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra
{
    public sealed partial class PeningkatanKeberdayaanMitra
    {
        public sealed class PeningkatanKeberdayaanMitraBuilder
        {
            private readonly PeningkatanKeberdayaanMitra _akurasiPenelitian;
            private Result? _result;

            public PeningkatanKeberdayaanMitraBuilder(PeningkatanKeberdayaanMitra akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<PeningkatanKeberdayaanMitra> Build()
            {
                return HasError ? Result.Failure<PeningkatanKeberdayaanMitra>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public PeningkatanKeberdayaanMitraBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PeningkatanKeberdayaanMitra>(PeningkatanKeberdayaanMitraErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public PeningkatanKeberdayaanMitraBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PeningkatanKeberdayaanMitra>(PeningkatanKeberdayaanMitraErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }

        }
    }
}
