using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi
{
    public sealed partial class JumlahKolaboratorPublikasBereputasi
    {
        public sealed class JumlahKolaboratorPublikasBereputasiBuilder
        {
            private readonly JumlahKolaboratorPublikasBereputasi _akurasiPenelitian;
            private Result? _result;

            public JumlahKolaboratorPublikasBereputasiBuilder(JumlahKolaboratorPublikasBereputasi akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<JumlahKolaboratorPublikasBereputasi> Build()
            {
                return HasError ? Result.Failure<JumlahKolaboratorPublikasBereputasi>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public JumlahKolaboratorPublikasBereputasiBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public JumlahKolaboratorPublikasBereputasiBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                if (bobotPDP < 0)
                {
                    _result = Result.Failure<JumlahKolaboratorPublikasBereputasi>(JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotPDP());
                    return this;
                }

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public JumlahKolaboratorPublikasBereputasiBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                if (bobotTerapan < 0)
                {
                    _result = Result.Failure<JumlahKolaboratorPublikasBereputasi>(JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotTerapan());
                    return this;
                }

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public JumlahKolaboratorPublikasBereputasiBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                if (bobotKerjasama < 0)
                {
                    _result = Result.Failure<JumlahKolaboratorPublikasBereputasi>(JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotKerjasama());
                    return this;
                }

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public JumlahKolaboratorPublikasBereputasiBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                if (bobotPenelitianDasar < 0)
                {
                    _result = Result.Failure<JumlahKolaboratorPublikasBereputasi>(JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotPenelitianDasar());
                    return this;
                }

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public JumlahKolaboratorPublikasBereputasiBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                if (skor < 0)
                {
                    _result = Result.Failure<JumlahKolaboratorPublikasBereputasi>(JumlahKolaboratorPublikasBereputasiErrors.InvalidSkor());
                    return this;
                }

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
