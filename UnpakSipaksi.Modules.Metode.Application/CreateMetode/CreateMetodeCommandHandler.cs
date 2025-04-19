using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi;
using UnpakSipaksi.Modules.KesesuaianTkt.PublicApi;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi;
using UnpakSipaksi.Modules.Metode.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Metode.Domain.Metode;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi;

namespace UnpakSipaksi.Modules.Metode.Application.CreateMetode;

internal sealed class CreateMetodeCommandHandler(
    IMetodeRepository metodeRepository,
    IAkurasiPenelitianApi akurasiPenelitianApi,
    IKejelasanPembagianTugasTimApi kejelasanPembagianTugasTimApi,
    IKesesuaianWaktuRabLuaranFasilitasApi kesesuaianWaktuRabLuaranFasilitasApi,
    IPotensiKetercapaianLuaranDijanjikanApi potensiKetercapaianLuaranDijanjikanApi,
    IModelFeasibilityStudysApi modelFeasibilityStudyApi,
    IKesesuaianTktApi kesesuaianTktApi,
    IKredibilitasMitraDukunganApi kredibilitasMitraDukunganApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMetodeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMetodeCommand request, CancellationToken cancellationToken)
    {
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

        var akurasi = await akurasiPenelitianTask;
        var tugasTim = await kejelasanPembagianTugasTimTask;
        var kesesuaianWaktu = await kesesuaianWaktuRabLuaranFasilitasTask;
        var potensi = await potensiKetercapaianLuaranDijanjikanTask;
        var model = await modelFeasibilityStudyTask;
        var tkt = await kesesuaianTktTask;
        var mitra = await kredibilitasMitraDukunganTask;

        if (akurasi is null) 
            return Result.Failure<Guid>(MetodeErrors.AkurasiPenelitianNotFound());
        if (tugasTim is null)
            return Result.Failure<Guid>(MetodeErrors.KejelasanPembagianTugasTimTaskNotFound());
        if (kesesuaianWaktu is null)
            return Result.Failure<Guid>(MetodeErrors.KesesuaianWaktuRabLuaranFasilitasNotFound());
        if (potensi is null)
            return Result.Failure<Guid>(MetodeErrors.PotensiKetercapaianLuaranDijanjikanNotFound());
        if (model is null)
            return Result.Failure<Guid>(MetodeErrors.ModelFeasibilityStudyNotFound());
        if (tkt is null)
            return Result.Failure<Guid>(MetodeErrors.KesesuaianTktNotFound());
        if (mitra is null)
            return Result.Failure<Guid>(MetodeErrors.KredibilitasMitraDukunganNotFound());

        var result = Domain.Metode.Metode.Create(
            int.Parse(akurasi?.Id ?? "0"),
            int.Parse(tugasTim?.Id ?? "0"),
            int.Parse(kesesuaianWaktu?.Id ?? "0"),
            int.Parse(potensi?.Id ?? "0"),
            int.Parse(model?.Id ?? "0"),
            int.Parse(tkt?.Id ?? "0"),
            int.Parse(mitra?.Id ?? "0")
        );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        metodeRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value.Uuid);
    }
}
