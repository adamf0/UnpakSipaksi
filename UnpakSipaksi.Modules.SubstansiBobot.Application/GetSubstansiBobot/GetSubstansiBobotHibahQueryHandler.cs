using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.PublicApi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.PublicApi;
using UnpakSipaksi.Modules.KebaruanReferensi.PublicApi;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi;
using UnpakSipaksi.Modules.KesesuaianTkt.PublicApi;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.PublicApi;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.PublicApi;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.PublicApi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi;
using UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi;
using UnpakSipaksi.Modules.SotaKebaharuan.PublicApi;

namespace UnpakSipaksi.Modules.SubstansiBobot.Application.GetSubstansiBobot
{
    internal sealed class GetSubstansiBobotHibahQueryHandler(
                IPublikasiDisitasiProposalApi PublikasiDisitasiProposalApi,
                IRelevansiKepakaranTemaProposalApi RelevansiKepakaranTemaProposalApi,
                IJumlahKolaboratorPublikasBereputasiApi JumlahKolaboratorPublikasiBereputasiApi,
                IRelevansiProdukKepentinganNasionalApi RelevansiProdukKepentinganNasionalApi,
                IKetajamanPerumusanMasalahApi KetajamanPerumusanMasalahApi,
                IInovasiPemecahanMasalahApi InovasiPemecahanMasalahApi,
                ISotaKebaharuanApi SotaKebaharuanApi,
                IRoadmapPenelitianApi RoadmapPenelitianApi,
                IAkurasiPenelitianApi AkurasiPenelitianApi,
                IKejelasanPembagianTugasTimApi KejelasanPembagianTugasTimApi,
                IKesesuaianWaktuRabLuaranFasilitasApi KesesuaianWaktuRabLuaranFasilitasApi,
                IPotensiKetercapaianLuaranDijanjikanApi PotensiKetercapaianLuaranDijanjikanApi,
                IModelFeasibilityStudysApi ModelFeasibilityStudyApi,
                IKesesuaianTktApi KesesuaianTktApi,
                IKredibilitasMitraDukunganApi KredibilitasMitraDukunganApi,
                IKebaruanReferensiApi KebaruanReferensiApi,
                IRelevansiKualitasReferensiApi RelevansiKualitasReferensiApi
        ) : IQueryHandler<GetSubstansiBobotHibahQuery, SubstansiBobotHibahResponse>
    {
        public async Task<Result<SubstansiBobotHibahResponse>> Handle(GetSubstansiBobotHibahQuery request, CancellationToken cancellationToken)
        {
            var PublikasiDisitasiProposalTask = PublikasiDisitasiProposalApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var RelevansiKepakaranTemaProposalTask = RelevansiKepakaranTemaProposalApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var JumlahKolaboratorPublikasiBereputasiTask = JumlahKolaboratorPublikasiBereputasiApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var RelevansiProdukKepentinganNasionalTask = RelevansiProdukKepentinganNasionalApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var KetajamanPerumusanMasalahTask = KetajamanPerumusanMasalahApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var InovasiPemecahanMasalahTask = InovasiPemecahanMasalahApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var SotaKebaharuanTask = SotaKebaharuanApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var RoadmapPenelitianTask = RoadmapPenelitianApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var AkurasiPenelitianTask = AkurasiPenelitianApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var KejelasanPembagianTugasTimTask = KejelasanPembagianTugasTimApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var KesesuaianWaktuRabLuaranFasilitasTask = KesesuaianWaktuRabLuaranFasilitasApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var PotensiKetercapaianLuaranDijanjikanTask = PotensiKetercapaianLuaranDijanjikanApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var ModelFeasibilityStudyTask = ModelFeasibilityStudyApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var KesesuaianTktTask = KesesuaianTktApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var KredibilitasMitraDukunganTask = KredibilitasMitraDukunganApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var KebaruanReferensiTask = KebaruanReferensiApi.GetBobotWithoutTargetAsync(request.KategoriSkema);
            var RelevansiKualitasReferensiTask = RelevansiKualitasReferensiApi.GetBobotWithoutTargetAsync(request.KategoriSkema);

            await Task.WhenAll(
                PublikasiDisitasiProposalTask,
                RelevansiKepakaranTemaProposalTask,
                JumlahKolaboratorPublikasiBereputasiTask,
                RelevansiProdukKepentinganNasionalTask,
                KetajamanPerumusanMasalahTask,
                InovasiPemecahanMasalahTask,
                SotaKebaharuanTask,
                RoadmapPenelitianTask,
                AkurasiPenelitianTask,
                KejelasanPembagianTugasTimTask,
                KesesuaianWaktuRabLuaranFasilitasTask,
                PotensiKetercapaianLuaranDijanjikanTask,
                ModelFeasibilityStudyTask,
                KesesuaianTktTask,
                KredibilitasMitraDukunganTask,
                KebaruanReferensiTask,
                RelevansiKualitasReferensiTask
            );

            int publikasiDisitasiProposal = PublikasiDisitasiProposalTask.Result ?? 0;
            int relevansiKepakaranTemaProposal = RelevansiKepakaranTemaProposalTask.Result ?? 0;
            int jumlahKolaboratorPublikasiBereputasi = JumlahKolaboratorPublikasiBereputasiTask.Result ?? 0;
            int relevansiProdukKepentinganNasional = RelevansiProdukKepentinganNasionalTask.Result ?? 0;
            int ketajamanPerumusanMasalah = KetajamanPerumusanMasalahTask.Result ?? 0;
            int inovasiPemecahanMasalah = InovasiPemecahanMasalahTask.Result ?? 0;
            int sotaKebaharuan = SotaKebaharuanTask.Result ?? 0;
            int roadmapPenelitian = RoadmapPenelitianTask.Result ?? 0;
            int akurasiPenelitian = AkurasiPenelitianTask.Result ?? 0;
            int kejelasanPembagianTugasTim = KejelasanPembagianTugasTimTask.Result ?? 0;
            int kesesuaianWaktuRabLuaranFasilitas = KesesuaianWaktuRabLuaranFasilitasTask.Result ?? 0;
            int potensiKetercapaianLuaranDijanjikan = PotensiKetercapaianLuaranDijanjikanTask.Result ?? 0;
            int modelFeasibilityStudy = ModelFeasibilityStudyTask.Result ?? 0;
            int kesesuaianTkt = KesesuaianTktTask.Result ?? 0;
            int kredibilitasMitraDukungan = KredibilitasMitraDukunganTask.Result ?? 0;
            int kebaruanReferensi = KebaruanReferensiTask.Result ?? 0;
            int relevansiKualitasReferensi = RelevansiKualitasReferensiTask.Result ?? 0;

            return Result.Success(new SubstansiBobotHibahResponse
            {
                PublikasiDisitasiProposal = publikasiDisitasiProposal,
                RelevansiKepakaranTemaProposal = relevansiKepakaranTemaProposal,
                JumlahKolaboratorPublikasiBereputasi = jumlahKolaboratorPublikasiBereputasi,
                RelevansiProdukKepentinganNasional = relevansiProdukKepentinganNasional,
                KetajamanPerumusanMasalah = ketajamanPerumusanMasalah,
                InovasiPemecahanMasalah = inovasiPemecahanMasalah,
                SotaKebaharuan = sotaKebaharuan,
                RoadmapPenelitian = roadmapPenelitian,
                AkurasiPenelitian = akurasiPenelitian,
                KejelasanPembagianTugasTim = kejelasanPembagianTugasTim,
                KesesuaianWaktuRabLuaranFasilitas = kesesuaianWaktuRabLuaranFasilitas,
                PotensiKetercapaianLuaranDijanjikan = potensiKetercapaianLuaranDijanjikan,
                ModelFeasibilityStudy = modelFeasibilityStudy,
                KesesuaianTkt = kesesuaianTkt,
                KredibilitasMitraDukungan = kredibilitasMitraDukungan,
                KebaruanReferensi = kebaruanReferensi,
                RelevansiKualitasReferensi = relevansiKualitasReferensi
            });
        }
    }
}
