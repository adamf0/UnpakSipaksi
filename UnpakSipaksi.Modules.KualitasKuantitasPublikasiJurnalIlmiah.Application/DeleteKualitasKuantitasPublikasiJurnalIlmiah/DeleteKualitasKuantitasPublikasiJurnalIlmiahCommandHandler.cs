using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.DeleteKualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class DeleteKualitasKuantitasPublikasiJurnalIlmiahCommandHandler(
    IKualitasKuantitasPublikasiJurnalIlmiahRepository KualitasKuantitasPublikasiJurnalIlmiahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKualitasKuantitasPublikasiJurnalIlmiahCommand>
    {
        public async Task<Result> Handle(DeleteKualitasKuantitasPublikasiJurnalIlmiahCommand request, CancellationToken cancellationToken)
        {
            Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah? existingKualitasKuantitasPublikasiJurnalIlmiah = await KualitasKuantitasPublikasiJurnalIlmiahRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKualitasKuantitasPublikasiJurnalIlmiah is null)
            {
                return Result.Failure(KualitasKuantitasPublikasiJurnalIlmiahErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await KualitasKuantitasPublikasiJurnalIlmiahRepository.DeleteAsync(existingKualitasKuantitasPublikasiJurnalIlmiah!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
