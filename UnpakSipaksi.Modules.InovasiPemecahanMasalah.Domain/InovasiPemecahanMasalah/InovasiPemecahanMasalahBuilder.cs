using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah
{
    public sealed partial class InovasiPemecahanMasalah
    {
        public sealed class InovasiPemecahanMasalahBuilder
        {
            private readonly InovasiPemecahanMasalah _inovasiPemecahanMasalah;
            private Result? _result;

            public InovasiPemecahanMasalahBuilder(InovasiPemecahanMasalah inovasiPemecahanMasalah)
            {
                _inovasiPemecahanMasalah = inovasiPemecahanMasalah;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<InovasiPemecahanMasalah> Build()
            {
                return HasError ? Result.Failure<InovasiPemecahanMasalah>(_result!.Error) : Result.Success(_inovasiPemecahanMasalah);
            }

            public InovasiPemecahanMasalahBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<InovasiPemecahanMasalah>(InovasiPemecahanMasalahErrors.NamaNotFound);
                    return this;
                }*/

                _inovasiPemecahanMasalah.Nama = nama;
                return this;
            }

            public InovasiPemecahanMasalahBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                if (bobotPDP < 0)
                {
                    _result = Result.Failure<InovasiPemecahanMasalah>(InovasiPemecahanMasalahErrors.InvalidBobotPDP());
                    return this;
                }

                _inovasiPemecahanMasalah.BobotPDP = bobotPDP;
                return this;
            }

            public InovasiPemecahanMasalahBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                if (bobotTerapan < 0)
                {
                    _result = Result.Failure<InovasiPemecahanMasalah>(InovasiPemecahanMasalahErrors.InvalidBobotTerapan());
                    return this;
                }

                _inovasiPemecahanMasalah.BobotTerapan = bobotTerapan;
                return this;
            }

            public InovasiPemecahanMasalahBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                if (bobotKerjasama < 0)
                {
                    _result = Result.Failure<InovasiPemecahanMasalah>(InovasiPemecahanMasalahErrors.InvalidBobotKerjasama());
                }

                _inovasiPemecahanMasalah.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public InovasiPemecahanMasalahBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                if (bobotPenelitianDasar < 0)
                {
                    _result = Result.Failure<InovasiPemecahanMasalah>(InovasiPemecahanMasalahErrors.InvalidBobotPenelitianDasar());
                    return this;
                }

                _inovasiPemecahanMasalah.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public InovasiPemecahanMasalahBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                if (skor < 0)
                {
                    _result = Result.Failure<InovasiPemecahanMasalah>(InovasiPemecahanMasalahErrors.InvalidSkor());
                    return this;
                }

                _inovasiPemecahanMasalah.Skor = skor;
                return this;
            }

        }
    }
}
