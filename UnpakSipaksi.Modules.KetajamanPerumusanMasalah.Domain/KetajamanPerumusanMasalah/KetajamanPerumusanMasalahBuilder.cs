using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah
{
    public sealed partial class KetajamanPerumusanMasalah
    {
        public sealed class KetajamanPerumusanMasalahBuilder
        {
            private readonly KetajamanPerumusanMasalah _akurasiPenelitian;
            private Result? _result;

            public KetajamanPerumusanMasalahBuilder(KetajamanPerumusanMasalah akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KetajamanPerumusanMasalah> Build()
            {
                return HasError ? Result.Failure<KetajamanPerumusanMasalah>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KetajamanPerumusanMasalahBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanPerumusanMasalah>(KetajamanPerumusanMasalahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KetajamanPerumusanMasalahBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanPerumusanMasalah>(KetajamanPerumusanMasalahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public KetajamanPerumusanMasalahBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanPerumusanMasalah>(KetajamanPerumusanMasalahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public KetajamanPerumusanMasalahBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanPerumusanMasalah>(KetajamanPerumusanMasalahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public KetajamanPerumusanMasalahBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanPerumusanMasalah>(KetajamanPerumusanMasalahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public KetajamanPerumusanMasalahBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KetajamanPerumusanMasalah>(KetajamanPerumusanMasalahErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
