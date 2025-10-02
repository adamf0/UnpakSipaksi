using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.DeleteLuaranHibah
{
    internal sealed class DeleteLuaranHibahCommandHandler(
    ILuaranHibahRepository repository,
    IUnitOfWorkLuaranHibah unitOfWork)
    : ICommandHandler<DeleteLuaranHibahCommand>
    {
        public async Task<Result> Handle(DeleteLuaranHibahCommand request, CancellationToken cancellationToken)
        {
            Domain.Luaran.Luaran? existing = await repository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existing is null)
            {
                return Result.Failure(LuaranErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await repository.DeleteAsync(existing!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
