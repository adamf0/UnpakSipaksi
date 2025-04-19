using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian
{
    public sealed partial class UrgensiPenelitian
    {
        public sealed class UrgensiPenelitianBuilder
        {
            private readonly UrgensiPenelitian _akurasiPenelitian;
            private Result? _result;

            public UrgensiPenelitianBuilder(UrgensiPenelitian akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<UrgensiPenelitian> Build()
            {
                return HasError ? Result.Failure<UrgensiPenelitian>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public UrgensiPenelitianBuilder ChangeRelevansiProdukKepentinganNasional(int RelevansiProdukKepentinganNasionalId)
            {
                if (HasError) return this;

                _akurasiPenelitian.RelevansiProdukKepentinganNasionalId = RelevansiProdukKepentinganNasionalId;
                return this;
            }
            public UrgensiPenelitianBuilder ChangeKetajamanPerumusanMasalah(int KetajamanPerumusanMasalahId)
            {
                if (HasError) return this;

                _akurasiPenelitian.KetajamanPerumusanMasalahId = KetajamanPerumusanMasalahId;
                return this;
            }
            public UrgensiPenelitianBuilder ChangeInovasiPemecahanMasalah(int InovasiPemecahanMasalahId)
            {
                if (HasError) return this;

                _akurasiPenelitian.InovasiPemecahanMasalahId = InovasiPemecahanMasalahId;
                return this;
            }
            public UrgensiPenelitianBuilder ChangeSotaKebaharuan(int SotaKebaharuanId)
            {
                if (HasError) return this;

                _akurasiPenelitian.SotaKebaharuanId = SotaKebaharuanId;
                return this;
            }
            public UrgensiPenelitianBuilder ChangeRoadmapPenelitian(int RoadmapPenelitianId)
            {
                if (HasError) return this;

                _akurasiPenelitian.RoadmapPenelitianId = RoadmapPenelitianId;
                return this;
            }
        }
    }
}
