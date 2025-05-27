using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Metode.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Metode.Domain.Metode;
using UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi;
using UnpakSipaksi.Modules.KesesuaianTkt.PublicApi;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi;

namespace UnpakSipaksi.Modules.Metode.Application.UpdateMetode
{
    internal sealed class UpdateMetodeCommandHandler(
        IMetodeRepository metodeRepository,
        IAkurasiPenelitianApi akurasiPenelitianApi,
        IKejelasanPembagianTugasTimApi kejelasanPembagianTugasTimApi,
        IKesesuaianWaktuRabLuaranFasilitasApi kesesuaianWaktuRabLuaranFasilitasApi,
        IPotensiKetercapaianLuaranDijanjikanApi potensiKetercapaianLuaranDijanjikanApi,
        IModelFeasibilityStudysApi modelFeasibilityStudyApi,
        IKesesuaianTktApi kesesuaianTktApi,
        IKredibilitasMitraDukunganApi kredibilitasMitraDukunganApi,
        IUnitOfWork unitOfWork
    ) : ICommandHandler<UpdateMetodeCommand>
    {
        public async Task<Result> Handle(UpdateMetodeCommand request, CancellationToken cancellationToken)
        {
            var existingMetode = await metodeRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingMetode is null)
            {
                return Result.Failure(MetodeErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            var akurasiPenelitianTask = akurasiPenelitianApi.GetAsync(Guid.Parse(request.UuidAkurasiPenelitian));
            var kejelasanPembagianTugasTimTask = kejelasanPembagianTugasTimApi.GetAsync(Guid.Parse(request.UuidKejelasanPembagianTugasTim));
            var kesesuaianWaktuRabLuaranFasilitasTask = kesesuaianWaktuRabLuaranFasilitasApi.GetAsync(Guid.Parse(request.UuidKesesuaianWaktuRabLuaranFasilitas));
            var potensiKetercapaianLuaranDijanjikanTask = potensiKetercapaianLuaranDijanjikanApi.GetAsync(Guid.Parse(request.UuidPotensiKetercapaianLuaranDijanjikan));
            var modelFeasibilityStudyTask = modelFeasibilityStudyApi.GetAsync(Guid.Parse(request.UuidModelFeasibilityStudy));
            var kesesuaianTktTask = kesesuaianTktApi.GetAsync(Guid.Parse(request.UuidKesesuaianTkt));
            var kredibilitasMitraDukunganTask = kredibilitasMitraDukunganApi.GetAsync(Guid.Parse(request.UuidKredibilitasMitraDukungan));

            await Task.WhenAll(
                akurasiPenelitianTask,
                kejelasanPembagianTugasTimTask,
                kesesuaianWaktuRabLuaranFasilitasTask,
                potensiKetercapaianLuaranDijanjikanTask,
                modelFeasibilityStudyTask,
                kesesuaianTktTask,
                kredibilitasMitraDukunganTask
            );

            var akurasiPenelitian = await akurasiPenelitianTask;
            var kejelasanPembagianTugasTim = await kejelasanPembagianTugasTimTask;
            var kesesuaianWaktuRabLuaranFasilitas = await kesesuaianWaktuRabLuaranFasilitasTask;
            var potensiKetercapaianLuaranDijanjikan = await potensiKetercapaianLuaranDijanjikanTask;
            var modelFeasibilityStudy = await modelFeasibilityStudyTask;
            var kesesuaianTkt = await kesesuaianTktTask;
            var kredibilitasMitraDukungan = await kredibilitasMitraDukunganTask;

            if (akurasiPenelitian is null)
                return Result.Failure<Guid>(MetodeErrors.AkurasiPenelitianNotFound());
            if (kejelasanPembagianTugasTim is null)
                return Result.Failure<Guid>(MetodeErrors.KejelasanPembagianTugasTimTaskNotFound());
            if (kesesuaianWaktuRabLuaranFasilitas is null)
                return Result.Failure<Guid>(MetodeErrors.KesesuaianWaktuRabLuaranFasilitasNotFound());
            if (potensiKetercapaianLuaranDijanjikan is null)
                return Result.Failure<Guid>(MetodeErrors.PotensiKetercapaianLuaranDijanjikanNotFound());
            if (modelFeasibilityStudy is null)
                return Result.Failure<Guid>(MetodeErrors.ModelFeasibilityStudyNotFound());
            if (kesesuaianTkt is null)
                return Result.Failure<Guid>(MetodeErrors.KesesuaianTktNotFound());
            if (kredibilitasMitraDukungan is null)
                return Result.Failure<Guid>(MetodeErrors.KredibilitasMitraDukunganNotFound());

            var updatedMetodeResult = Domain.Metode.Metode.Update(existingMetode)
                .ChangeAkurasiPenelitian(int.Parse(akurasiPenelitian?.Id ?? "0"))
                .ChangeKejelasanPembagianTugasTim(int.Parse(kejelasanPembagianTugasTim?.Id ?? "0"))
                .ChangeKesesuaianWaktuRabLuaranFasilitas(int.Parse(kesesuaianWaktuRabLuaranFasilitas?.Id ?? "0"))
                .ChangePotensiKetercapaianLuaranDijanjikan(int.Parse(potensiKetercapaianLuaranDijanjikan?.Id ?? "0"))
                .ChangeModelFeasibilityStudy(int.Parse(modelFeasibilityStudy?.Id ?? "0"))
                .ChangeKesesuaianTkt(int.Parse(kesesuaianTkt?.Id ?? "0"))
                .ChangeKredibilitasMitraDukungan(int.Parse(kredibilitasMitraDukungan?.Id ?? "0"))
                .Build();

            if (updatedMetodeResult.IsFailure)
            {
                return Result.Failure<Guid>(updatedMetodeResult.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
