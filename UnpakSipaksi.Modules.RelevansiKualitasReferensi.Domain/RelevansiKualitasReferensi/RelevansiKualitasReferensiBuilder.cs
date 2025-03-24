using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi
{
    public sealed partial class RelevansiKualitasReferensi
    {
        public sealed class RelevansiKualitasReferensiBuilder
        {
            private readonly RelevansiKualitasReferensi _akurasiPenelitian;
            private Result? _result;

            public RelevansiKualitasReferensiBuilder(RelevansiKualitasReferensi akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RelevansiKualitasReferensi> Build()
            {
                return HasError ? Result.Failure<RelevansiKualitasReferensi>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public RelevansiKualitasReferensiBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKualitasReferensi>(RelevansiKualitasReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public RelevansiKualitasReferensiBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKualitasReferensi>(RelevansiKualitasReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public RelevansiKualitasReferensiBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKualitasReferensi>(RelevansiKualitasReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public RelevansiKualitasReferensiBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKualitasReferensi>(RelevansiKualitasReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public RelevansiKualitasReferensiBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKualitasReferensi>(RelevansiKualitasReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public RelevansiKualitasReferensiBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiKualitasReferensi>(RelevansiKualitasReferensiErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
