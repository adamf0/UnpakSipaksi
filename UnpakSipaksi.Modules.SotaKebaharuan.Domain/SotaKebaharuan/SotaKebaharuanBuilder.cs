using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan
{
    public sealed partial class SotaKebaharuan
    {
        public sealed class SotaKebaharuanBuilder
        {
            private readonly SotaKebaharuan _akurasiPenelitian;
            private Result? _result;

            public SotaKebaharuanBuilder(SotaKebaharuan akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<SotaKebaharuan> Build()
            {
                return HasError ? Result.Failure<SotaKebaharuan>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public SotaKebaharuanBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<SotaKebaharuan>(SotaKebaharuanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public SotaKebaharuanBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<SotaKebaharuan>(SotaKebaharuanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public SotaKebaharuanBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<SotaKebaharuan>(SotaKebaharuanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public SotaKebaharuanBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<SotaKebaharuan>(SotaKebaharuanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public SotaKebaharuanBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<SotaKebaharuan>(SotaKebaharuanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public SotaKebaharuanBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                if (skor < 0)
                {
                    _result = Result.Failure<SotaKebaharuan>(SotaKebaharuanErrors.InvalidValueSkor());
                    return this;
                }

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
