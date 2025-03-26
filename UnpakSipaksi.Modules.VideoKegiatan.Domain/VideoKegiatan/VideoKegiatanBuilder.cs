using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan
{
    public sealed partial class VideoKegiatan
    {
        public sealed class VideoKegiatanBuilder
        {
            private readonly VideoKegiatan _akurasiPenelitian;
            private Result? _result;

            public VideoKegiatanBuilder(VideoKegiatan akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<VideoKegiatan> Build()
            {
                return HasError ? Result.Failure<VideoKegiatan>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public VideoKegiatanBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<VideoKegiatan>(VideoKegiatanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public VideoKegiatanBuilder ChangeNilai(int nilai)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<VideoKegiatan>(VideoKegiatanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nilai = nilai;
                return this;
            }

        }
    }
}
