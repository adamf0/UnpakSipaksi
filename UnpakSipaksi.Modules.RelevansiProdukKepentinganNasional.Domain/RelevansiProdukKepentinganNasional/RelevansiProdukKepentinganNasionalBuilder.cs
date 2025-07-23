using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional
{
    public sealed partial class RelevansiProdukKepentinganNasional
    {
        public sealed class RelevansiProdukKepentinganNasionalBuilder
        {
            private readonly RelevansiProdukKepentinganNasional _akurasiPenelitian;
            private Result? _result;

            public RelevansiProdukKepentinganNasionalBuilder(RelevansiProdukKepentinganNasional akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RelevansiProdukKepentinganNasional> Build()
            {
                return HasError ? Result.Failure<RelevansiProdukKepentinganNasional>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public RelevansiProdukKepentinganNasionalBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiProdukKepentinganNasional>(RelevansiProdukKepentinganNasionalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public RelevansiProdukKepentinganNasionalBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiProdukKepentinganNasional>(RelevansiProdukKepentinganNasionalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public RelevansiProdukKepentinganNasionalBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiProdukKepentinganNasional>(RelevansiProdukKepentinganNasionalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public RelevansiProdukKepentinganNasionalBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiProdukKepentinganNasional>(RelevansiProdukKepentinganNasionalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public RelevansiProdukKepentinganNasionalBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RelevansiProdukKepentinganNasional>(RelevansiProdukKepentinganNasionalErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public RelevansiProdukKepentinganNasionalBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                if (skor < 0)
                {
                    _result = Result.Failure<RelevansiProdukKepentinganNasional>(RelevansiProdukKepentinganNasionalErrors.InvalidValueSkor());
                    return this;
                }

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
