using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian
{
    public sealed partial class RoadmapPenelitian
    {
        public sealed class RoadmapPenelitianBuilder
        {
            private readonly RoadmapPenelitian _akurasiPenelitian;
            private Result? _result;

            public RoadmapPenelitianBuilder(RoadmapPenelitian akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<RoadmapPenelitian> Build()
            {
                return HasError ? Result.Failure<RoadmapPenelitian>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public RoadmapPenelitianBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RoadmapPenelitian>(RoadmapPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public RoadmapPenelitianBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RoadmapPenelitian>(RoadmapPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public RoadmapPenelitianBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RoadmapPenelitian>(RoadmapPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public RoadmapPenelitianBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RoadmapPenelitian>(RoadmapPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public RoadmapPenelitianBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RoadmapPenelitian>(RoadmapPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public RoadmapPenelitianBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<RoadmapPenelitian>(RoadmapPenelitianErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
