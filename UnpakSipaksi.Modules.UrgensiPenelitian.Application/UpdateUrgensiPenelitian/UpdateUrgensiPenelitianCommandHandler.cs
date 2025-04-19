using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.PublicApi;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.PublicApi;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi;
using UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi;
using UnpakSipaksi.Modules.SotaKebaharuan.PublicApi;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.UpdateUrgensiPenelitian
{
    internal sealed class UpdateUrgensiPenelitianCommandHandler(
    IUrgensiPenelitianRepository UrgensiPenelitianRepository,
    IRelevansiProdukKepentinganNasionalApi RelevansiProdukKepentinganNasionalApi,
    IKetajamanPerumusanMasalahApi KetajamanPerumusanMasalahApi,
    IInovasiPemecahanMasalahApi InovasiPemecahanMasalahApi,
    ISotaKebaharuanApi SotaKebaharuanApi,
    IRoadmapPenelitianApi RoadmapPenelitianApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUrgensiPenelitianCommand>
    {
        public async Task<Result> Handle(UpdateUrgensiPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.UrgensiPenelitian.UrgensiPenelitian? existingUrgensiPenelitian = await UrgensiPenelitianRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingUrgensiPenelitian is null)
            {
                Result.Failure(UrgensiPenelitianErrors.NotFound(request.Uuid));
            }

            var RelevansiProdukKepentinganNasionalTask = RelevansiProdukKepentinganNasionalApi.GetAsync(Guid.Parse(request.UuidRelevansiProdukKepentinganNasional));
            var KetajamanPerumusanMasalahTask = KetajamanPerumusanMasalahApi.GetAsync(Guid.Parse(request.UuidKetajamanPerumusanMasalah));
            var InovasiPemecahanMasalahTask = InovasiPemecahanMasalahApi.GetAsync(Guid.Parse(request.UuidInovasiPemecahanMasalah));
            var SotaKebaharuanTask = SotaKebaharuanApi.GetAsync(Guid.Parse(request.UuidSotaKebaharuan));
            var RoadmapPenelitianTask = RoadmapPenelitianApi.GetAsync(Guid.Parse(request.UuidRoadmapPenelitian));

            await Task.WhenAll(
                RelevansiProdukKepentinganNasionalTask,
                KetajamanPerumusanMasalahTask,
                InovasiPemecahanMasalahTask,
                SotaKebaharuanTask,
                RoadmapPenelitianTask
            );

            RelevansiProdukKepentinganNasionalResponse? RelevansiProdukKepentinganNasional = await RelevansiProdukKepentinganNasionalTask;
            KetajamanPerumusanMasalahResponse? KetajamanPerumusanMasalah = await KetajamanPerumusanMasalahTask;
            InovasiPemecahanMasalahResponse? InovasiPemecahanMasalah = await InovasiPemecahanMasalahTask;
            SotaKebaharuanResponse? SotaKebaharuan = await SotaKebaharuanTask;
            RoadmapPenelitianResponse? RoadmapPenelitian = await RoadmapPenelitianTask;

            Result<Domain.UrgensiPenelitian.UrgensiPenelitian> asset = Domain.UrgensiPenelitian.UrgensiPenelitian.Update(existingUrgensiPenelitian!)
                         .ChangeRelevansiProdukKepentinganNasional(int.Parse(RelevansiProdukKepentinganNasional?.Id ?? "0"))
                         .ChangeKetajamanPerumusanMasalah(int.Parse(KetajamanPerumusanMasalah?.Id ?? "0"))
                         .ChangeInovasiPemecahanMasalah(int.Parse(InovasiPemecahanMasalah?.Id ?? "0"))
                         .ChangeSotaKebaharuan(int.Parse(SotaKebaharuan?.Id ?? "0"))
                         .ChangeRoadmapPenelitian(int.Parse(RoadmapPenelitian?.Id ?? "0"))
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
