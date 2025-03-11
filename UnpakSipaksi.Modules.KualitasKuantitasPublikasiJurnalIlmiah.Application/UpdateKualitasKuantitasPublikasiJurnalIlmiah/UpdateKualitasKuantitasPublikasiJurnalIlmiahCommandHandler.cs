using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.UpdateKualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class UpdateKualitasKuantitasPublikasiJurnalIlmiahCommandHandler(
    IKualitasKuantitasPublikasiJurnalIlmiahRepository KualitasKuantitasPublikasiJurnalIlmiahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKualitasKuantitasPublikasiJurnalIlmiahCommand>
    {
        public async Task<Result> Handle(UpdateKualitasKuantitasPublikasiJurnalIlmiahCommand request, CancellationToken cancellationToken)
        {
            Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah? existingKualitasKuantitasPublikasiJurnalIlmiah = await KualitasKuantitasPublikasiJurnalIlmiahRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKualitasKuantitasPublikasiJurnalIlmiah is null)
            {
                Result.Failure(KualitasKuantitasPublikasiJurnalIlmiahErrors.NotFound(request.Uuid));
            }

            Result<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah> asset = Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah.Update(existingKualitasKuantitasPublikasiJurnalIlmiah!)
                         .ChangeNama(request.Nama)
                         .ChangeNilai(request.Nilai)
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
