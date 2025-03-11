using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan
{
    public sealed partial class KredibilitasMitraDukungan
    {
        public sealed class KredibilitasMitraDukunganBuilder
        {
            private readonly KredibilitasMitraDukungan _akurasiPenelitian;
            private Result? _result;

            public KredibilitasMitraDukunganBuilder(KredibilitasMitraDukungan akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<KredibilitasMitraDukungan> Build()
            {
                return HasError ? Result.Failure<KredibilitasMitraDukungan>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public KredibilitasMitraDukunganBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KredibilitasMitraDukungan>(KredibilitasMitraDukunganErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public KredibilitasMitraDukunganBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KredibilitasMitraDukungan>(KredibilitasMitraDukunganErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public KredibilitasMitraDukunganBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KredibilitasMitraDukungan>(KredibilitasMitraDukunganErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public KredibilitasMitraDukunganBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KredibilitasMitraDukungan>(KredibilitasMitraDukunganErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public KredibilitasMitraDukunganBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KredibilitasMitraDukungan>(KredibilitasMitraDukunganErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public KredibilitasMitraDukunganBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<KredibilitasMitraDukungan>(KredibilitasMitraDukunganErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
