using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys
{
    public sealed partial class ModelFeasibilityStudys
    {
        public sealed class ModelFeasibilityStudysBuilder
        {
            private readonly ModelFeasibilityStudys _akurasiPenelitian;
            private Result? _result;

            public ModelFeasibilityStudysBuilder(ModelFeasibilityStudys akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<ModelFeasibilityStudys> Build()
            {
                return HasError ? Result.Failure<ModelFeasibilityStudys>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public ModelFeasibilityStudysBuilder ChangeNama(string nama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<ModelFeasibilityStudys>(ModelFeasibilityStudysErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.Nama = nama;
                return this;
            }

            public ModelFeasibilityStudysBuilder ChangeBobotPDP(int bobotPDP)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<ModelFeasibilityStudys>(ModelFeasibilityStudysErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPDP = bobotPDP;
                return this;
            }

            public ModelFeasibilityStudysBuilder ChangeBobotTerapan(int bobotTerapan)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<ModelFeasibilityStudys>(ModelFeasibilityStudysErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotTerapan = bobotTerapan;
                return this;
            }

            public ModelFeasibilityStudysBuilder ChangeBobotKerjasama(int bobotKerjasama)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<ModelFeasibilityStudys>(ModelFeasibilityStudysErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotKerjasama = bobotKerjasama;
                return this;
            }

            public ModelFeasibilityStudysBuilder ChangeBobotPenelitianDasar(int bobotPenelitianDasar)
            {
                if (HasError) return this;

                /*if (string.IsNullOrWhiteSpace(nama))
                {
                    _result = Result.Failure<ModelFeasibilityStudys>(ModelFeasibilityStudysErrors.NamaNotFound);
                    return this;
                }*/

                _akurasiPenelitian.BobotPenelitianDasar = bobotPenelitianDasar;
                return this;
            }

            public ModelFeasibilityStudysBuilder ChangeSkor(int skor)
            {
                if (HasError) return this;

                if (skor < 0)
                {
                    _result = Result.Failure<ModelFeasibilityStudys>(ModelFeasibilityStudysErrors.InvalidValueSkor());
                    return this;
                }

                _akurasiPenelitian.Skor = skor;
                return this;
            }

        }
    }
}
