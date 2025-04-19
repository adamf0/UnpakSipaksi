using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Metode.Domain.Metode
{
    public sealed partial class Metode
    {
        public sealed class MetodeBuilder
        {
            private readonly Metode _akurasiPenelitian;
            private Result? _result;

            public MetodeBuilder(Metode akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Metode> Build()
            {
                return HasError ? Result.Failure<Metode>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public MetodeBuilder ChangeAkurasiPenelitian(int AkurasiPenelitianId)
            {
                if (HasError) return this;

                _akurasiPenelitian.AkurasiPenelitianId = AkurasiPenelitianId;
                return this;
            }

            public MetodeBuilder ChangeKejelasanPembagianTugasTim(int KejelasanPembagianTugasTimId)
            {
                if (HasError) return this;

                _akurasiPenelitian.KejelasanPembagianTugasTimId = KejelasanPembagianTugasTimId;
                return this;
            }

            public MetodeBuilder ChangeKesesuaianWaktuRabLuaranFasilitas(int KesesuaianWaktuRabLuaranFasilitasId)
            {
                if (HasError) return this;

                _akurasiPenelitian.KesesuaianWaktuRabLuaranFasilitasId = KesesuaianWaktuRabLuaranFasilitasId;
                return this;
            }
            public MetodeBuilder ChangePotensiKetercapaianLuaranDijanjikan(int PotensiKetercapaianLuaranDijanjikanId)
            {
                if (HasError) return this;

                _akurasiPenelitian.PotensiKetercapaianLuaranDijanjikanId = PotensiKetercapaianLuaranDijanjikanId;
                return this;
            }

            public MetodeBuilder ChangeModelFeasibilityStudy(int ModelFeasibilityStudyId)
            {
                if (HasError) return this;

                _akurasiPenelitian.ModelFeasibilityStudyId = ModelFeasibilityStudyId;
                return this;
            }
            public MetodeBuilder ChangeKesesuaianTkt(int KesesuaianTktId)
            {
                if (HasError) return this;

                _akurasiPenelitian.KesesuaianTktId = KesesuaianTktId;
                return this;
            }
            public MetodeBuilder ChangeKredibilitasMitraDukungan(int KredibilitasMitraDukunganId)
            {
                if (HasError) return this;

                _akurasiPenelitian.KredibilitasMitraDukunganId = KredibilitasMitraDukunganId;
                return this;
            }

        }
    }
}
