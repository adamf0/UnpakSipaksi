using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.PublicApi;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.PublicApi;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi;
using UnpakSipaksi.Modules.RoadmapPenelitian.PublicApi;
using UnpakSipaksi.Modules.SotaKebaharuan.PublicApi;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.CreateUrgensiPenelitian
{
    internal sealed class CreateUrgensiPenelitianCommandHandler(
    IUrgensiPenelitianRepository UrgensiPenelitianRepository,
    IRelevansiProdukKepentinganNasionalApi RelevansiProdukKepentinganNasionalApi,
    IKetajamanPerumusanMasalahApi KetajamanPerumusanMasalahApi,
    IInovasiPemecahanMasalahApi InovasiPemecahanMasalahApi,
    ISotaKebaharuanApi SotaKebaharuanApi,
    IRoadmapPenelitianApi RoadmapPenelitianApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUrgensiPenelitianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateUrgensiPenelitianCommand request, CancellationToken cancellationToken)
        {
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

            Result<Domain.UrgensiPenelitian.UrgensiPenelitian> result = Domain.UrgensiPenelitian.UrgensiPenelitian.Create(
                int.Parse(RelevansiProdukKepentinganNasional?.Id ?? "0"),
                int.Parse(KetajamanPerumusanMasalah?.Id ?? "0"),
                int.Parse(InovasiPemecahanMasalah?.Id ?? "0"),
                int.Parse(SotaKebaharuan?.Id ?? "0"),
                int.Parse(RoadmapPenelitian?.Id ?? "0")
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            UrgensiPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
