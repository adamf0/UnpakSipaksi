using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.CreateKualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class CreateKualitasKuantitasPublikasiJurnalIlmiahHandler(
    IKualitasKuantitasPublikasiJurnalIlmiahRepository KualitasKuantitasPublikasiJurnalIlmiahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKualitasKuantitasPublikasiJurnalIlmiahCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKualitasKuantitasPublikasiJurnalIlmiahCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah> result = Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KualitasKuantitasPublikasiJurnalIlmiahRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
