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
using UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.PublicApi;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.PublicApi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi;
using UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi;
using UnpakSipaksi.Modules.SotaKebaharuan.PublicApi;
using UnpakSipaksi.Modules.Substansi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Application.UpdateSubstansiInternal
{
    internal sealed class UpdateSubstansiInternalCommandHandler(
        IPenelitianHibahApi PenelitianHibahApi,
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
        IRelevansiKualitasReferensiApi RelevansiKualitasReferensiApi,
        ISubstansiInternalRepository SubstansiRepository,
        IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSubstansiInternalCommand>
    {
        public async Task<Result> Handle(UpdateSubstansiInternalCommand request, CancellationToken cancellationToken)
        {
            SubstansiInternal? existingSubstansi = await SubstansiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingSubstansi is null)
            {
                return Result.Failure(SubstansiInternalErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            var PenelitianHibahTask = PenelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenelitianHibah));
            var PublikasiDisitasiProposalTask = PublikasiDisitasiProposalApi.GetAsync(Guid.Parse(request.UuidPublikasiDisitasiProposal));
            var RelevansiKepakaranTemaProposalTask = RelevansiKepakaranTemaProposalApi.GetAsync(Guid.Parse(request.UuidRelevansiKepakaranTemaProposal));
            var JumlahKolaboratorPublikasiBereputasiTask = JumlahKolaboratorPublikasiBereputasiApi.GetAsync(Guid.Parse(request.UuidJumlahKolaboratorPublikasiBereputasi));
            var RelevansiProdukKepentinganNasionalTask = RelevansiProdukKepentinganNasionalApi.GetAsync(Guid.Parse(request.UuidRelevansiProdukKepentinganNasional));
            var KetajamanPerumusanMasalahTask = KetajamanPerumusanMasalahApi.GetAsync(Guid.Parse(request.UuidKetajamanPerumusanMasalah));
            var InovasiPemecahanMasalahTask = InovasiPemecahanMasalahApi.GetAsync(Guid.Parse(request.UuidInovasiPemecahanMasalah));
            var SotaKebaharuanTask = SotaKebaharuanApi.GetAsync(Guid.Parse(request.UuidSotaKebaharuan));
            var RoadmapPenelitianTask = RoadmapPenelitianApi.GetAsync(Guid.Parse(request.UuidRoadmapPenelitian));
            var AkurasiPenelitianTask = AkurasiPenelitianApi.GetAsync(Guid.Parse(request.UuidAkurasiPenelitian));
            var KejelasanPembagianTugasTimTask = KejelasanPembagianTugasTimApi.GetAsync(Guid.Parse(request.UuidKejelasanPembagianTugasTim));
            var KesesuaianWaktuRabLuaranFasilitasTask = KesesuaianWaktuRabLuaranFasilitasApi.GetAsync(Guid.Parse(request.UuidKesesuaianWaktuRabLuaranFasilitas));
            var PotensiKetercapaianLuaranDijanjikanTask = PotensiKetercapaianLuaranDijanjikanApi.GetAsync(Guid.Parse(request.UuidPotensiKetercapaianLuaranDijanjikan));
            var ModelFeasibilityStudyTask = ModelFeasibilityStudyApi.GetAsync(Guid.Parse(request.UuidModelFeasibilityStudy));
            var KesesuaianTktTask = KesesuaianTktApi.GetAsync(Guid.Parse(request.UuidKesesuaianTkt));
            var KredibilitasMitraDukunganTask = KredibilitasMitraDukunganApi.GetAsync(Guid.Parse(request.UuidKredibilitasMitraDukungan));
            var KebaruanReferensiTask = KebaruanReferensiApi.GetAsync(Guid.Parse(request.UuidKebaruanReferensi));
            var RelevansiKualitasReferensiTask = RelevansiKualitasReferensiApi.GetAsync(Guid.Parse(request.UuidRelevansiKualitasReferensi));

            await Task.WhenAll(
                PenelitianHibahTask,
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

            var PenelitianHibah = await PenelitianHibahTask;
            var PublikasiDisitasiProposal = await PublikasiDisitasiProposalTask;
            var RelevansiKepakaranTemaProposal = await RelevansiKepakaranTemaProposalTask;
            var JumlahKolaboratorPublikasiBereputasi = await JumlahKolaboratorPublikasiBereputasiTask;
            var RelevansiProdukKepentinganNasional = await RelevansiProdukKepentinganNasionalTask;
            var KetajamanPerumusanMasalah = await KetajamanPerumusanMasalahTask;
            var InovasiPemecahanMasalah = await InovasiPemecahanMasalahTask;
            var SotaKebaharuan = await SotaKebaharuanTask;
            var RoadmapPenelitian = await RoadmapPenelitianTask;
            var AkurasiPenelitian = await AkurasiPenelitianTask;
            var KejelasanPembagianTugasTim = await KejelasanPembagianTugasTimTask;
            var KesesuaianWaktuRabLuaranFasilitas = await KesesuaianWaktuRabLuaranFasilitasTask;
            var PotensiKetercapaianLuaranDijanjikan = await PotensiKetercapaianLuaranDijanjikanTask;
            var ModelFeasibilityStudy = await ModelFeasibilityStudyTask;
            var KesesuaianTkt = await KesesuaianTktTask;
            var KredibilitasMitraDukungan = await KredibilitasMitraDukunganTask;
            var KebaruanReferensi = await KebaruanReferensiTask;
            var RelevansiKualitasReferensi = await RelevansiKualitasReferensiTask;

            Result<SubstansiInternal> result = SubstansiInternal.Update(
                Guid.Parse(request.Uuid),
                existingSubstansi,
                int.Parse(PenelitianHibah?.Id ?? "0"),
                int.Parse(PublikasiDisitasiProposal?.Id ?? "0"),
                int.Parse(RelevansiKepakaranTemaProposal?.Id ?? "0"),
                int.Parse(JumlahKolaboratorPublikasiBereputasi?.Id ?? "0"),
                int.Parse(RelevansiProdukKepentinganNasional?.Id ?? "0"),
                int.Parse(KetajamanPerumusanMasalah?.Id ?? "0"),
                int.Parse(InovasiPemecahanMasalah?.Id ?? "0"),
                int.Parse(SotaKebaharuan?.Id ?? "0"),
                int.Parse(RoadmapPenelitian?.Id ?? "0"),
                int.Parse(AkurasiPenelitian?.Id ?? "0"),
                int.Parse(KejelasanPembagianTugasTim?.Id ?? "0"),
                int.Parse(KesesuaianWaktuRabLuaranFasilitas?.Id ?? "0"),
                int.Parse(PotensiKetercapaianLuaranDijanjikan?.Id ?? "0"),
                int.Parse(ModelFeasibilityStudy?.Id ?? "0"),
                int.Parse(KesesuaianTkt?.Id ?? "0"),
                int.Parse(KredibilitasMitraDukungan?.Id ?? "0"),
                int.Parse(KebaruanReferensi?.Id ?? "0"),
                int.Parse(RelevansiKualitasReferensi?.Id ?? "0"),
                request.Komentar,
                request.UuidReviewerInternal,
                0, //[PR] request.UuidReviewerExternal
                request.TanggalMulai,
                request.TanggalBerakhir
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
