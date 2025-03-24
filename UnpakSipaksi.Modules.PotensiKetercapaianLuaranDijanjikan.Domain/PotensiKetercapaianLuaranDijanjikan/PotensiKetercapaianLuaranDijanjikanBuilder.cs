using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan
{
    public sealed partial class PotensiKetercapaianLuaranDijanjikan
    {
        public sealed class PotensiKetercapaianLuaranDijanjikanBuilder
        {
            private readonly PotensiKetercapaianLuaranDijanjikan _akurasiPenelitian;
            private Result? _result;

            public PotensiKetercapaianLuaranDijanjikanBuilder(PotensiKetercapaianLuaranDijanjikan akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<PotensiKetercapaianLuaranDijanjikan> Build()
            {
                return HasError ? Result.Failure<PotensiKetercapaianLuaranDijanjikan>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public PotensiKetercapaianLuaranDijanjikanBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PotensiKetercapaianLuaranDijanjikan>(PotensiKetercapaianLuaranDijanjikanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public PotensiKetercapaianLuaranDijanjikanBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PotensiKetercapaianLuaranDijanjikan>(PotensiKetercapaianLuaranDijanjikanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public PotensiKetercapaianLuaranDijanjikanBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PotensiKetercapaianLuaranDijanjikan>(PotensiKetercapaianLuaranDijanjikanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public PotensiKetercapaianLuaranDijanjikanBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PotensiKetercapaianLuaranDijanjikan>(PotensiKetercapaianLuaranDijanjikanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public PotensiKetercapaianLuaranDijanjikanBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PotensiKetercapaianLuaranDijanjikan>(PotensiKetercapaianLuaranDijanjikanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public PotensiKetercapaianLuaranDijanjikanBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<PotensiKetercapaianLuaranDijanjikan>(PotensiKetercapaianLuaranDijanjikanErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
